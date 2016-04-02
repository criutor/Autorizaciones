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
    public partial class SOLICITUDES_REBAJAR_VACACIONES
    {
        partial void CancelarSolicitud_Execute()
        {

            // Escriba el código aquí.
            if (this.Vacaciones_Aprobadas.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para cancelar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            // Escriba el código aquí.
            this.Vacaciones_Aprobadas.SelectedItem.Caducada = true;
            this.Vacaciones_Aprobadas.SelectedItem.Estado = "Anulada por RR.HH";


            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.Vacaciones_Aprobadas.SelectedItem;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
            this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
            this.NUEVOESTADO.CreadoAt = DateTime.Now;

            this.Save();
            this.Refresh();
        }

        partial void RebajarSolicitud_Execute()
        {
            // Escriba el código aquí.

            if (this.Vacaciones_Aprobadas.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            //enviar Vacaciones a fin700 *****!!!!


            this.Vacaciones_Aprobadas.SelectedItem.Rebajada = true;
            this.Vacaciones_Aprobadas.SelectedItem.Estado = "Rebajada por RR.HH";

            if(this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.EsRolPrivado == true)
            {
                this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.SaldoVacaciones = this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.SaldoVacaciones - this.Vacaciones_Aprobadas.SelectedItem.NumeroDiasTomados.Value;
            }
            else if (this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.EsRolPrivado == false)
            {
                //stored procedure
            }


                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.Vacaciones_Aprobadas.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                this.NUEVOESTADO.CreadoAt = DateTime.Now;

                this.Save();
                this.Refresh();


        }
    }
}
