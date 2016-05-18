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
    public partial class SOLICITUDES_REBAJAR_VACACIONES_RP
    {

        partial void AnularSolicitud_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("La solicitud será anulada. Esta acción se utiliza cuando el empleado no ha tomado los días solicitados. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {

                this.Vacaciones_Aprobadas.SelectedItem.Caducada = true;
                this.Vacaciones_Aprobadas.SelectedItem.Completada = false;


                //this.Vacaciones_Aprobadas.SelectedItem.Estado = "Anulada por RR.HH";
                this.Vacaciones_Aprobadas.SelectedItem.Estado = "Anulada";


                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.Vacaciones_Aprobadas.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
                this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                this.NUEVOESTADO.CreadoAt = DateTime.Now;

                this.Save();
                this.Refresh();
            }

        }

        partial void RebajarSolicitud_Execute()
        {

            if (this.Vacaciones_Aprobadas.SelectedItem.Termino.Value > DateTime.Today)
            { }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            System.Windows.MessageBoxResult result = this.ShowMessageBox("Los días solicitados por el empleado serán descontados de su saldo de vacaciones. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {

                this.Vacaciones_Aprobadas.SelectedItem.Rebajada = true;
                this.Vacaciones_Aprobadas.SelectedItem.Completada = false;

                //this.Vacaciones_Aprobadas.SelectedItem.Estado = "Rebajada por RR.HH";
                this.Vacaciones_Aprobadas.SelectedItem.Estado = "Rebajada";

                if (this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.EsRolPrivado == true)
                {
                    //***this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.SaldoVacaciones = this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.SaldoVacaciones - this.Vacaciones_Aprobadas.SelectedItem.NumeroDiasTomados.Value;
                    this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.SaldoVacaciones2 = this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.SaldoVacaciones2 - this.Vacaciones_Aprobadas.SelectedItem.DiasSolicitados.Value;
                    //this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.VacacionesSaldo = this.Vacaciones_Aprobadas.SelectedItem.PersonaItem1.VacacionesSaldo - this.Vacaciones_Aprobadas.SelectedItem.NumeroDiasTomados.Value;

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

        partial void RebajarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            try
            {
                if (this.Vacaciones_Aprobadas.SelectedItem == null)
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
                if (this.Vacaciones_Aprobadas.SelectedItem == null)
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
