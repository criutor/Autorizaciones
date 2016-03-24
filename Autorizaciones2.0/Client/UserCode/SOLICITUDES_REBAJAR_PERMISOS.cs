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
    public partial class SOLICITUDES_REBAJAR_PERMISOS
    {
        partial void RebajarSolicitud_Execute()
        {
            // Escriba el código aquí.
            if (this.PermisosAprobados.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            //enviar Vacaciones a fin700 *****!!!!


            this.PermisosAprobados.SelectedItem.Rebajada = true;
            this.PermisosAprobados.SelectedItem.Estado = "Rebajada por RR.HH";

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.PermisosAprobados.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();

        }

        partial void CancelarSolicitud_Execute()
        {
            // Escriba el código aquí.
            if (this.PermisosAprobados.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para cancelar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            // Escriba el código aquí.
            this.PermisosAprobados.SelectedItem.Caducada = true;
            this.PermisosAprobados.SelectedItem.Estado = "Cancelada por RR.HH";

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.PermisosAprobados.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();
        }
    }
}
