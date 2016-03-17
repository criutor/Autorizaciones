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
    public partial class SOLICITUDES_REBAJAR_ADMINISTRATIVOS
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

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.DiasAdministrativosAprobados.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();


        }

        partial void ResetearSaldoATodos_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("El saldo de todos los empleados será igual a '3'. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            { 
                foreach( PersonaItem empleado in this.Persona )
                {
                    empleado.SaldoDiasAdmins = 3;
                    this.Save();
                }
            }
        }

        partial void CancelarSolicitud_Execute()
        {
            if (this.DiasAdministrativosAprobados.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para cancelar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            // Escriba el código aquí.
            this.DiasAdministrativosAprobados.SelectedItem.Caducada = true;

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.DiasAdministrativosAprobados.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();
        }


    }
}
