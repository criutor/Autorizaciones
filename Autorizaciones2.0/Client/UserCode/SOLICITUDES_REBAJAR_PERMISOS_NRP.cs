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
    public partial class SOLICITUDES_REBAJAR_PERMISOS_NRP
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

             System.Windows.MessageBoxResult result = this.ShowMessageBox("Esta acción indica que RR.HH ha recibido esta solicitud. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

             if (result == System.Windows.MessageBoxResult.Yes)
             {

                 this.PermisosAprobados.SelectedItem.Rebajada = true;
                 this.PermisosAprobados.SelectedItem.Completada = false;


                 //this.PermisosAprobados.SelectedItem.Estado = "Rebajada por RR.HH";
                 this.PermisosAprobados.SelectedItem.Estado = "Rebajada";

                 this.NUEVOESTADO = new ESTADOSItem();
                 this.NUEVOESTADO.SOLICITUDESItem = this.PermisosAprobados.SelectedItem;
                 this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                 this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                 this.NUEVOESTADO.CreadoAt = DateTime.Now;

                 this.Save();
                 this.Refresh();
             }

        }

        partial void AnularSolicitud_Execute()
        {
            System.Windows.MessageBoxResult result = this.ShowMessageBox("La solicitud será anulada. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {

                this.PermisosAprobados.SelectedItem.Caducada = true;
                this.PermisosAprobados.SelectedItem.Completada = false;

                //this.PermisosAprobados.SelectedItem.Estado = "Anulada por RR.HH";
                this.PermisosAprobados.SelectedItem.Estado = "Anulada";

                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.PermisosAprobados.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA CADUCADO (RR.HH):";
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
                if (this.PermisosAprobados.SelectedItem == null)
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
                if (this.PermisosAprobados.SelectedItem == null)
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
