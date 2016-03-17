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
    public partial class SOLICITUDES_REBAJAR_HORASEXTRAS
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

            if (this.HorasExtrasAprobadas.SelectedItem.HorasTrabajadas > 0)
            {
                this.HorasExtrasAprobadas.SelectedItem.Rebajada = true;

                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.HorasExtrasAprobadas.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                this.NUEVOESTADO.CreadoAt = DateTime.Now;

                this.Save();
                this.Refresh();

            }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes ingresar las horas trabajadas", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            
            
        }

        partial void CancelarSolicitud_Execute()
        {
            // Escriba el código aquí.
            if (this.HorasExtrasAprobadas.SelectedItem.Inicio.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para cancelar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            // Escriba el código aquí.
            this.HorasExtrasAprobadas.SelectedItem.Caducada = true;

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.HorasExtrasAprobadas.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();
            
        }

        partial void CerrarVentanaModalHorasTrabajadas_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("VentanaModalHorasTrabajadas");
            this.HORASTRABAJADAS = null;
        }

        partial void GuardarHorasTrabajadas_Execute()
        {
            // Escriba el código aquí.
            if (this.HORASTRABAJADAS > 0)
            {
                this.HorasExtrasAprobadas.SelectedItem.HorasTrabajadas = this.HORASTRABAJADAS;
                this.CloseModalWindow("VentanaModalHorasTrabajadas");
                this.Save();
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
    }
}
