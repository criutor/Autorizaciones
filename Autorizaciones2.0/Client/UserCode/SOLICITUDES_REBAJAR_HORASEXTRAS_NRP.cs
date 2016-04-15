using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
namespace LightSwitchApplication
{
    public partial class SOLICITUDES_REBAJAR_HORASEXTRAS_NRP
    {
        partial void RebajarSolicitud_Execute()
        {
            // Escriba el código aquí.
            if (this.HorasExtrasAprobadas.SelectedItem.Inicio.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            //enviar horas extras a fin700 ******!!!

            this.AbrirVentanaModalHorasTrabajadas_Execute();
    
        }

        partial void AnularSolicitud_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("La solicitud será anulada. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {

                this.HorasExtrasAprobadas.SelectedItem.Caducada = true;
                this.HorasExtrasAprobadas.SelectedItem.Completada = false;


                //this.HorasExtrasAprobadas.SelectedItem.Estado = "Anulada por RR.HH";
                this.HorasExtrasAprobadas.SelectedItem.Estado = "Anulada";

                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.HorasExtrasAprobadas.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
                this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                this.NUEVOESTADO.CreadoAt = DateTime.Now;

                this.Save();
                this.Refresh();
            }
            
        }

        partial void CerrarVentanaModalHorasTrabajadas_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("VentanaModalHorasTrabajadas");
            //this.HORASTRABAJADAS = null;
        }

        partial void GuardarHorasTrabajadas_Execute()
        {
            // Escriba el código aquí.

            if (this.HORASTRABAJADAS > 0)
            {
                this.HorasExtrasAprobadas.SelectedItem.HorasTrabajadas = this.HORASTRABAJADAS;

                System.Windows.MessageBoxResult result = this.ShowMessageBox("Las horas extras serán enviadas a Fin700. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

                if (result == System.Windows.MessageBoxResult.Yes)
                {

                    this.HorasExtrasAprobadas.SelectedItem.Rebajada = true;
                    this.HorasExtrasAprobadas.SelectedItem.Completada = false;


                    //this.HorasExtrasAprobadas.SelectedItem.Estado = "Rebajada por RR.HH";
                    this.HorasExtrasAprobadas.SelectedItem.Estado = "Rebajada";

                    this.NUEVOESTADO = new ESTADOSItem();
                    this.NUEVOESTADO.SOLICITUDESItem = this.HorasExtrasAprobadas.SelectedItem;
                    this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                    this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                    this.NUEVOESTADO.CreadoAt = DateTime.Now;

                    this.CloseModalWindow("VentanaModalHorasTrabajadas");
                    this.Save();
                    this.Refresh();
                }
            
            }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes ingresar las horas trabajadas", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            } 

        }

        partial void HORASTRABAJADAS_Validate(ScreenValidationResultsBuilder results)
        {
            
            // results.AddPropertyError("<Mensaje de error>");
            if (this.HORASTRABAJADAS <= 0 || this.HORASTRABAJADAS == null)
            {

                results.AddPropertyError("Horas trabajadas debe ser mayor a 0");
            }
            
        }

        partial void AbrirVentanaModalHorasTrabajadas_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("VentanaModalHorasTrabajadas");
        }

        partial void RebajarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            try
            {
                if (this.HorasExtrasAprobadas.SelectedItem == null)
                {
                    result = false;
                }
                else { result = true; }
            }
            catch { }
        }

        partial void AnularSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            try
            {
                if (this.HorasExtrasAprobadas.SelectedItem == null)
                {
                    result = false;
                }
                else { result = true; }
            }
            catch { }
        }

        partial void SeleccionarEmpleado_Execute()
        {
            // Escriba el código aquí.

            try
            {
                this.EmpleadoFiltroSolicitudes = this.Persona.SelectedItem.Rut_Persona;
                this.NombreEmpleadoSeleccionado = this.Persona.SelectedItem.NombreAD;
            }
            catch { }

            this.CloseModalWindow("Empleados");
        }

        partial void TodosLosEmpleados_Execute()
        {
            // Escriba el código aquí.
            this.EmpleadoFiltroSolicitudes = null;
            this.NombreEmpleadoSeleccionado = null;
        }
    }
}
