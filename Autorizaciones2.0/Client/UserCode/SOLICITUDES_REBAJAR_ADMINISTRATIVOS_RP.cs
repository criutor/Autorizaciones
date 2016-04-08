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
    public partial class SOLICITUDES_REBAJAR_ADMINISTRATIVOS_RP
    {
        partial void RebajarSolicitud_Execute()
        {
            // Escriba el código aquí.

            if (this.DiasAdministrativosAprobados.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            this.DiasAdministrativosAprobados.SelectedItem.PersonaItem1.SaldoDiasAdmins = this.DiasAdministrativosAprobados.SelectedItem.PersonaItem1.SaldoDiasAdmins - this.DiasAdministrativosAprobados.SelectedItem.NumeroDiasTomados;

            this.DiasAdministrativosAprobados.SelectedItem.Rebajada = true;
            this.DiasAdministrativosAprobados.SelectedItem.Completada = false;

            //this.DiasAdministrativosAprobados.SelectedItem.Estado = "Rebajada por RR.HH";
            this.DiasAdministrativosAprobados.SelectedItem.Estado = "Rebajada";

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.DiasAdministrativosAprobados.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();
        }

        partial void AnularSolicitud_Execute()
        {
            // Escriba el código aquí.
            if (this.DiasAdministrativosAprobados.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para anular una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            // Escriba el código aquí.

            this.DiasAdministrativosAprobados.SelectedItem.Caducada = true;
            this.DiasAdministrativosAprobados.SelectedItem.Completada = false;

            //this.DiasAdministrativosAprobados.SelectedItem.Estado = "Anulada por RR.HH";
            this.DiasAdministrativosAprobados.SelectedItem.Estado = "Anulada";

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.DiasAdministrativosAprobados.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();
        }

        partial void RebajarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            try
            {
                if (this.DiasAdministrativosAprobados.SelectedItem == null)
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
                if (this.DiasAdministrativosAprobados.SelectedItem == null)
                {
                    result = false;
                }
                else { result = true; }
            }
            catch { }
        }

        partial void SeleccionarEmpleado_Execute()
        {

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
