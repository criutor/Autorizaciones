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
    public partial class SOLICITUDES_REBAJAR_ROLPRIVADO
    {
        partial void AnularSolicitud_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("La solicitud será anulada. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {

                this.SOLICITUDES.SelectedItem.Caducada = true;
                this.SOLICITUDES.SelectedItem.Completada = false;

                //this.DiasAdministrativosAprobados.SelectedItem.Estado = "Anulada por RR.HH";
                this.SOLICITUDES.SelectedItem.Estado = "Anulada";

                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO ANULADA POR (RR.HH):";
                this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                this.NUEVOESTADO.CreadoAt = DateTime.Now;

                this.Save();
                this.Refresh();
            }
        }

        partial void RebajarSolicitud_Execute()
        {
            // Escriba el código aquí.

            System.Windows.MessageBoxResult result;

            if (DateTime.Today > this.SOLICITUDES.SelectedItem.Termino.Value)
            {
                
            }
            else
            {
                this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }

            #region dentro del if
            string Mensaje = "";

                if (this.SOLICITUDES.SelectedItem.Administrativo == true)
                {
                    Mensaje = "Los días administrativos solicitados por el empleado serán descontados de su saldo. ¿Desea continuar?";
                }
                else
                    if (this.SOLICITUDES.SelectedItem.Vacaciones == true)
                    {
                        Mensaje = "Las vacaciones solicitadas por el empleado serán descontadas de su saldo. ¿Desea continuar?";
                    }


                result = this.ShowMessageBox(Mensaje, "ADVERTENCIA", MessageBoxOption.YesNo);


                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    if (this.SOLICITUDES.SelectedItem.Administrativo == true)
                    {
                        this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoDiasAdmins = this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoDiasAdmins - this.SOLICITUDES.SelectedItem.DiasSolicitados;
                    }
                    else
                        if (this.SOLICITUDES.SelectedItem.Vacaciones == true)
                        {
                            this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoVacaciones2 = this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoVacaciones2 - this.SOLICITUDES.SelectedItem.DiasSolicitados;
                        }

                    this.SOLICITUDES.SelectedItem.Rebajada = true;
                    this.SOLICITUDES.SelectedItem.Completada = false;
                    this.SOLICITUDES.SelectedItem.Estado = "Rebajada";

                    this.NUEVOESTADO = new ESTADOSItem();
                    this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.SelectedItem;
                    this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                    this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                    this.NUEVOESTADO.CreadoAt = DateTime.Now;

                    this.Save();
                    this.Refresh();
                }
            #endregion
        }

        partial void RebajarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            try
            {
                if (this.SOLICITUDES.SelectedItem == null)
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
                if (this.SOLICITUDES.SelectedItem == null)
                {
                    result = false;
                }
                else { result = true; }
            }
            catch { }
        }

        partial void SOLICITUDES_REBAJAR_ROLPRIVADO_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            ADMINISTRATIVO = true;
            VACACIONES = true;

        }

        partial void RebajarTodas_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("Esta acción indica que el administrador(a) del rol privado ha recibido estas solicitudes y gestionará su respectivo descuento. Las solicitudes en las cuales la fecha de término es menor a la fecha actual no serán rebajadas. ¿Desea rebajar todas las solicitudes ?", "REBAJAR TODAS LAS SOLICITUDES", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                foreach (SOLICITUDESItem solicitud in this.SOLICITUDES)
                {
                    if (solicitud.Administrativo == true)
                    {
                        solicitud.PersonaItem1.SaldoDiasAdmins = solicitud.PersonaItem1.SaldoDiasAdmins - solicitud.DiasSolicitados;
                    }
                    else if (solicitud.Vacaciones == true)
                    {
                        solicitud.PersonaItem1.SaldoVacaciones2 = this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoVacaciones2 - this.SOLICITUDES.SelectedItem.DiasSolicitados;
                    }

                        solicitud.Rebajada = true;
                        solicitud.Completada = false;
                        solicitud.Estado = "Rebajada";

                        this.NUEVOESTADO = new ESTADOSItem();
                        this.NUEVOESTADO.SOLICITUDESItem = solicitud;
                        this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                        this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                        this.NUEVOESTADO.CreadoAt = DateTime.Now;

                        if (DateTime.Now > solicitud.Termino.Value)
                        {
                            //incluir todo lo que está dentro del "foreach"
                        }
                }

                this.Save();
                this.Refresh();
            }
        }

        partial void RebajarTodas_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.SOLICITUDES.Count() == 0) { result = false; }

        }
    }
}
