using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Text;
using System.Collections;
using System.Security.Permissions;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using Microsoft.LightSwitch.Application;
using Microsoft.LightSwitch.Threading;
using Microsoft.LightSwitch.Client;


namespace LightSwitchApplication
{
    public partial class SOLICITUDES_HISTORICO_RRHH
    {
        
        partial void LimpiarFechas_Execute()
        {
            // Escriba el código aquí.
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.SOLICITUDES.Load();
        }

        partial void FiltroEstados_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (FiltroEstados == "Rechazada") {

                this.Completada = false;
                this.Rechazada = true;
                this.Cancelada = false;
                this.Caducada = false;
                this.Rebajada = false; 
            }
            else
                if (FiltroEstados == "Aprobada") {

                    this.Completada = true;
                    this.Rechazada = false;
                    this.Cancelada = false;
                    this.Caducada = false;
                    this.Rebajada = false; 
                }
                else
                    if (FiltroEstados == "En aprobacion") {

                        this.Completada = false;
                        this.Rechazada = false;
                        this.Cancelada = false;
                        this.Caducada = false;
                        this.Rebajada = false; 
                    }
                    else
                        if (FiltroEstados == "Cancelada") {

                            this.Completada = false;
                            this.Rechazada = false;
                            this.Cancelada = true;
                            this.Caducada = false;
                            this.Rebajada = false; 
                        }
                        else
                            if (FiltroEstados == "Anulada") {

                                this.Completada = false;
                                this.Rechazada = false;
                                this.Cancelada = false;
                                this.Caducada = true;
                                this.Rebajada = false; 
                            }
                            else
                                if (FiltroEstados == "Rebajada") {

                                    this.Completada = false;
                                    this.Rechazada = false;
                                    this.Cancelada = false;
                                    this.Caducada = false;
                                    this.Rebajada = true; 
                                }
                                else
                            if (FiltroEstados == "Todas")
                            {
                                //this.Cancelada = null;

                                //this.FechaSolicitudDesde = null;
                                //this.FechaSolicitudHasta = null;
                                
                                this.Administrativo = true;
                                this.Vacaciones = true;
                                this.OtroPermiso = true;
                                this.HorasExtras = true;
                                this.FiltroEstados = null;

                                this.Completada = false;
                                this.Rechazada = false;
                                this.Cancelada = false;
                                this.Caducada = null;
                                this.Rebajada = null; 
                                //this.Solicitud_Header.Load();
                            }
        }

        partial void TodosLosEmpleados_Execute()
        {
            // Escriba el código aquí.
            this.EMPLEADO = null;
        }



        partial void SOLICITUDES_HISTORICO_RRHH_Activated()
        {
            // Escriba el código aquí.
            this.TodosLosEmpleados_Execute();
            
            this.FiltroEstados = "Todas";
        }

        partial void Gerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                Id_Gerencia = this.Gerencia.Id_Gerencia;
                Id_SubGerencia = -1;
                Id_Area = -1;
                this.SubGerencia = null;
                this.Área = null;
            }
            catch { }
        }

        partial void SubGerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                Id_SubGerencia = this.SubGerencia.Id_SubGerencia;
                Id_Area = -1;
                Id_Gerencia = -1;
                this.Gerencia = null;
                this.Área = null;
            }
            catch { }
        }

        partial void Área_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                Id_Area = this.Área.Id_Area;
                Id_SubGerencia = -1;
                Id_Gerencia = -1;
                this.SubGerencia = null;
                this.Gerencia = null;
            }
            catch { }
        }

        partial void SOLICITUDES_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.NumDeSolicitudes = this.SOLICITUDES.Count();
            this.HorasExtrasTotal = (from s in this.SOLICITUDES where s.HorasExtras == true select s).Count();
            this.PermisosTotal = (from s in this.SOLICITUDES where s.OtroPermiso == true select s).Count();
            this.VacacionesTotal = (from s in this.SOLICITUDES where s.Vacaciones == true select s).Count();
            this.DíasAdministrativosTotal = (from s in this.SOLICITUDES where s.Administrativo == true select s).Count();

            try
            {
                this.HorasExtrasHoras = (from s in this.SOLICITUDES where s.HorasExtras == true select s.HorasTrabajadas).Sum().Value;
                this.PermisosDías = (from s in this.SOLICITUDES where s.OtroPermiso == true select s.DiasSolicitados).Sum().Value;
                this.VacacionesDias = (from s in this.SOLICITUDES where s.Vacaciones == true select s.DiasSolicitados).Sum().Value;
                this.DíasAdministrativosDías = (from s in this.SOLICITUDES where s.Administrativo == true select s.DiasSolicitados).Sum().Value;
            }
            catch { }

        }

        partial void LimpiarDivisiones_Execute()
        {
            // Escriba el código aquí.
            this.SubGerencia = null;
            this.Gerencia = null;
            this.Área = null;
            this.Id_Area = null;
            this.Id_Gerencia = null;
            this.Id_SubGerencia = null;
            //this.FiltroEstados = "Todas";
            //this.TodosLosEmpleados_Execute();
            //this.SOLICITUDES.Load();
            //this.Refresh();
        }


        partial void SOLICITUDES_HISTORICO_RRHH_Created()
        {
            // Escriba el código aquí.
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
            Microsoft.LightSwitch.Details.Client.IScreenCollectionProperty collectionProperty = this.Details.Properties.SOLICITUDES;

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

            foreach (var orderRow_loopVariable in SOLICITUDES)
            {//ok
                //var orderRow = orderRow_loopVariable;
                ////HEADER
                if (i == 0)
                {//ok
                    int c = 0;//ok
                    foreach (var prop_loopVariable in orderRow_loopVariable.Details.Properties.All().OfType<Microsoft.LightSwitch.Details.IEntityStorageProperty>())
                    {
                        var prop = prop_loopVariable;
                        if (c > 0)
                        {//ok
                            pli.Append("\t");
                        }
                        c = c + 1;

                        if (prop.DisplayName == "Rut Empleado")
                        {
                            pli.Append(prop.DisplayName);
                        }
                        else
                            if (prop.DisplayName == "Nombre Empleado")
                            {
                                pli.Append(prop.DisplayName);
                            }
                            else
                                if (prop.DisplayName == "Area De Trabajo")
                                {
                                    pli.Append(prop.DisplayName);
                                }
                                else
                                    if (prop.DisplayName == "Gerencia")
                                    {
                                        pli.Append(prop.DisplayName);
                                    }
                                    else
                                        if (prop.DisplayName == "Tipo De Solicitud")
                                        {
                                            pli.Append(prop.DisplayName);
                                        }
                                        else
                                            if (prop.DisplayName == "Estado")
                                            {
                                                pli.Append(prop.DisplayName);
                                            }
                                            else
                                                if (prop.DisplayName == "Fecha Solicitud")
                                                {
                                                    pli.Append(prop.DisplayName);
                                                }
                                                else
                                                    if (prop.DisplayName == "Saldo Dias")
                                                    {
                                                        pli.Append(prop.DisplayName);
                                                    }
                                                    else
                                                        if (prop.DisplayName == "Dias Solicitados")
                                                        {
                                                            pli.Append(prop.DisplayName);
                                                        }
                                                        else
                                                            if (prop.DisplayName == "Inicio")
                                                            {
                                                                pli.Append(prop.DisplayName);
                                                            }
                                                            else
                                                                if (prop.DisplayName == "Termino")
                                                                {
                                                                    pli.Append(prop.DisplayName);
                                                                }
                                                                else
                                                                    if (prop.DisplayName == "Taxi")
                                                                    {
                                                                        pli.Append(prop.DisplayName);
                                                                    }
                                                                    else
                                                                        if (prop.DisplayName == "Colacion")
                                                                        {
                                                                            pli.Append(prop.DisplayName);
                                                                        }
                                                                        else
                                                                            if (prop.DisplayName == "Con Descuento")
                                                                            {
                                                                                pli.Append(prop.DisplayName);
                                                                            }
                                                                            else
                                                                                if (prop.DisplayName == "Prestamo")
                                                                                {
                                                                                    pli.Append(prop.DisplayName);
                                                                                }
                                                                                else
                                                                                    if (prop.DisplayName == "Horas Autorizadas")
                                                                                    {
                                                                                        pli.Append(prop.DisplayName);
                                                                                    }
                                                                                    else
                                                                                        if (prop.DisplayName == "Horas Trabajadas")
                                                                                        {
                                                                                            pli.Append(prop.DisplayName);
                                                                                        }
                        //pli.Append(prop_loopVariable.DisplayName);
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

                    if (prop.DisplayName == "Rut Empleado")
                    {
                        pli.Append(prop.Value);
                    }
                    else
                        if (prop.DisplayName == "Nombre Empleado")
                        {
                            pli.Append(prop.Value);
                        }
                        else
                            if (prop.DisplayName == "Area De Trabajo")
                            {
                                pli.Append(prop.Value);
                            }
                            else
                                if (prop.DisplayName == "Gerencia")
                                {
                                    pli.Append(prop.Value);
                                }
                                else
                                    if (prop.DisplayName == "Tipo De Solicitud")
                                    {
                                        pli.Append(prop.Value);
                                    }
                                    else
                                        if (prop.DisplayName == "Estado")
                                        {
                                            pli.Append(prop.Value);
                                        }
                                        else
                                            if (prop.DisplayName == "Fecha Solicitud")
                                            {
                                                pli.Append(prop.Value);
                                            }
                                            else
                                                if (prop.DisplayName == "Saldo Dias")
                                                {
                                                    pli.Append(prop.Value);
                                                }
                                                else
                                                    if (prop.DisplayName == "Dias Solicitados")
                                                    {
                                                        pli.Append(prop.Value);
                                                    }
                                                    else
                                                        if (prop.DisplayName == "Inicio")
                                                        {
                                                            pli.Append(prop.Value);
                                                        }
                                                        else
                                                            if (prop.DisplayName == "Termino")
                                                            {
                                                                pli.Append(prop.Value);
                                                            }
                                                            else
                                                                if (prop.DisplayName == "Taxi")
                                                                {
                                                                    pli.Append(prop.Value);
                                                                }
                                                                else
                                                                    if (prop.DisplayName == "Colacion")
                                                                    {
                                                                        pli.Append(prop.Value);
                                                                    }
                                                                    else
                                                                        if (prop.DisplayName == "Con Descuento")
                                                                        {
                                                                            pli.Append(prop.Value);
                                                                        }
                                                                        else
                                                                            if (prop.DisplayName == "Prestamo")
                                                                            {
                                                                                pli.Append(prop.Value);
                                                                            }
                                                                            else
                                                                                if (prop.DisplayName == "Horas Autorizadas")
                                                                                {
                                                                                    pli.Append(prop.Value);
                                                                                }
                                                                                else
                                                                                    if (prop.DisplayName == "Horas Trabajadas")
                                                                                    {
                                                                                        pli.Append(prop.Value);
                                                                                    }
                    
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

        partial void EMPLEADO_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                if (this.EMPLEADO == null)
                {
                    EmpleadoFiltroSolicitudes = null;
                }
                else
                {
                    EmpleadoFiltroSolicitudes = this.EMPLEADO.Rut_Persona;
                }
            }
            catch { }
        }




   


    }
}
