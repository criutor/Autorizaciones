using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;
using System.Text;
namespace LightSwitchApplication
{
    public partial class SOLICITUDES_HORASEXTRAS_REPORTEFIN700
    {
        partial void CONSULTAR_Execute()
        {
            if (this.FechaDesde == null || this.FechaHasta == null)
            {
                this.ShowMessageBox("Las fechas no pueden ser vacías","Fechas vacías", MessageBoxOption.Ok);
            }else

                if (this.FechaDesde > this.FechaHasta || this.FechaHasta < this.FechaDesde)
                {
                    this.ShowMessageBox("'Fecha desde' no puede ser antes que 'Fecha hasta' ", "Fechas incorrectas", MessageBoxOption.Ok);
                }
                else
                {
                    LimpiarPantalla_Execute();

                    foreach (SOLICITUDESItem solicitud in SOLICITUDES)
                    {
                        RUTTRABAJADOR = solicitud.PersonaItem1.Rut_Persona;

                        if (ReporteHorasExtrasItem == null)
                        {
                            ReporteHorasExtrasItem Nuevo = new ReporteHorasExtrasItem();
                            Nuevo.ruttrabajador = solicitud.PersonaItem1.Rut_Persona;
                            Nuevo.formula = 50;
                            Nuevo.valor = null;
                            Nuevo.valorbase = solicitud.HorasTrabajadas.Value;
                        }
                        else
                        {
                            ReporteHorasExtrasItem.valorbase = ReporteHorasExtrasItem.valorbase + solicitud.HorasTrabajadas.Value;
                        }
                        Save();
                    }
                }
        }

        partial void LimpiarPantalla_Execute()
        {
            // Escriba el código aquí.
            foreach(ReporteHorasExtrasItem reporte in ReporteHorasExtras )
            {
                reporte.Delete();
            }
            Save();
        }

        partial void LimpiarPantalla_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (ReporteHorasExtras.Count() == 0)
            {
                result = false;
            }
            else { result = true; }
        }

        partial void Exportar_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (ReporteHorasExtras.Count() == 0)
            {
                result = false;
            }
            else { result = true; }
        }

        partial void SOLICITUDES_HORASEXTRAS_REPORTEFIN700_Closing(ref bool cancel)
        {
            // Escriba el código aquí.
            if (ReporteHorasExtras.Count() != 0)
            {
                foreach (ReporteHorasExtrasItem reporte in ReporteHorasExtras)
                {
                    reporte.Delete();
                }
                Save();
            }
        }
        
        partial void SOLICITUDES_HORASEXTRAS_REPORTEFIN700_Created()
        {
            this.FindControl("tituloExportar").IsEnabled = false;
            // Escriba el código aquí.
            var CSVButton = this.FindControl("Exportar");
            CSVButton.ControlAvailable += DownloadButton_ControlAvailable;
        }
        
        void DownloadButton_ControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            this.FindControl("Exportar").ControlAvailable -= DownloadButton_ControlAvailable;
            var Button = (Button)e.Control;
            Button.Click += DownloadButton_Click;
        }

        private void DownloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.LightSwitch.Details.Client.IScreenCollectionProperty collectionProperty = this.Details.Properties.ReporteHorasExtras;

            dynamic intPageSize = collectionProperty.PageSize;
            //Get the Current PageSize and store to variable
            collectionProperty.PageSize = 0;

            var dialog = new SaveFileDialog();
            dialog.Filter = "Excel (*.xls)|*.xls";

            if (dialog.ShowDialog() == true)
            {
                using (StreamWriter stream = new StreamWriter(dialog.OpenFile()))
                {
                    string csv = getinfo();
                    stream.Write(csv);
                    stream.Close();
                    this.ShowMessageBox("Archivo Excel creado exitosamente. " + "NOTA: Si al abrir el archivo te aparece una ventana preguntando si deseas continuar con el formato actual, solo presiona 'Sí'.", "Exportar a Excel", MessageBoxOption.Ok);
                }
            }
            collectionProperty.PageSize = intPageSize;
            //Reset the Current PageSize
        }

        private string getinfo()
        {
            StringBuilder pli = new StringBuilder();//ok

            int i = 0;//ok

            foreach (var orderRow_loopVariable in ReporteHorasExtras)
            {//ok
                //var orderRow = orderRow_loopVariable;
                ////HEADER
                if (i == 0)
                {//ok
                    int c = 0;//ok
                    foreach (var prop_loopVariable in orderRow_loopVariable.Details.Properties.All().OfType<Microsoft.LightSwitch.Details.IEntityStorageProperty>())
                    {
                        //var prop = prop_loopVariable;
                        if (c > 0)
                        {//ok
                            pli.Append("\t");
                        }
                        c = c + 1;
                        pli.Append(prop_loopVariable.DisplayName);
                    }
                }
                pli.AppendLine("");

                ////DATA ROWS

                int c1 = 0;
                foreach (var prop_loopVariable in orderRow_loopVariable.Details.Properties.All().OfType<Microsoft.LightSwitch.Details.IEntityStorageProperty>())
                {
                    var prop = prop_loopVariable;
                    if (c1 > 0)
                    {
                        pli.Append("\t");
                    }
                    c1 = c1 + 1;
                    pli.Append(prop.Value);
                }
                i = i + 1;
            }

            if (pli.Length > 0)
            {
                return pli.ToString(0, pli.Length);
            }
            else
            {
                return "";
            }
        }

        partial void Exportar_a_excel_Execute()
        {
            
        }
    }
}
