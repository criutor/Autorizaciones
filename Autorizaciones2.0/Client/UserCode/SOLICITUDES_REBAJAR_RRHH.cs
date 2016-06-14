using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;
using System.Text;
namespace LightSwitchApplication
{
    public partial class SOLICITUDES_REBAJAR_RRHH
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

        partial void RebajarSolicitud_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result;

            if (this.SOLICITUDES.SelectedItem.HorasExtras == true)
            {

                if (DateTime.Today > this.SOLICITUDES.SelectedItem.Inicio.Value)
                {
                    
                }
                else
                {
                    this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de realización", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                }

                #region dentro del primer if
                this.OpenModalWindow("Group");
                #endregion
            }
            else
            {
                if (DateTime.Today > this.SOLICITUDES.SelectedItem.Termino.Value)
                {
                    
                }
                else
                {
                    this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                }

                #region dentro del segundo if
                string Mensaje = "";
                

                if (this.SOLICITUDES.SelectedItem.Administrativo == true)
                    {
                        Mensaje = "Los días administrativos solicitados por el empleado serán descontados de su saldo. ¿Desea continuar?";
                    }
                    else
                        if (this.SOLICITUDES.SelectedItem.Vacaciones == true)
                        {
                            //Mensaje = "Las vacaciones solicitadas por el empleado serán descontadas de su saldo. ¿Desea continuar?";
                            Mensaje = "Esta acción indica que RR.HH ha recibido esta solicitud y gestionará su respectivo descuento. ¿Desea continuar?";
                        }
                        else
                            if (this.SOLICITUDES.SelectedItem.OtroPermiso == true)
                            {
                                result = this.ShowMessageBox("Esta acción indica que RR.HH ha recibido esta solicitud y gestionará su recuperación, descuento o compensación. ¿Desea continuar?");
                            }

                    result = this.ShowMessageBox(Mensaje, "ADVERTENCIA", MessageBoxOption.YesNo);


                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        if (this.SOLICITUDES.SelectedItem.Administrativo == true)
                        {
                            this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoDiasAdmins = this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoDiasAdmins - this.SOLICITUDES.SelectedItem.DiasSolicitados;
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

        partial void CerrarModalWindow_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("Group");
        }

        partial void GuardarHorasTrabajadas_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result;

            result = this.ShowMessageBox("Esta acción indica que RR.HH ha recibido esta solicitud y gestionará el respectivo pago de las horas extras. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes  )
            {
                if (this.SOLICITUDES.SelectedItem.HorasTrabajadas != null && this.SOLICITUDES.SelectedItem.HorasTrabajadas != 0)
                {
                    this.CloseModalWindow("Group");

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
                else {

                    this.ShowMessageBox("Las horas trabajadas no pueden ser igual a '0'", "HORAS TRABAJADAS", MessageBoxOption.Ok);

                }
            }
        }

        partial void SOLICITUDES_REBAJAR_RRHH_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            ADMINISTRATIVO = true;
            VACACIONES = true;
            OTROPERMISO = true;
            HORASEXTRAS = true;
        }

        partial void RebajarTodas_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.SOLICITUDES.Count() == 0) { result = false; }
        }

        partial void RebajarTodas_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("Esta acción indica que RR.HH ha recibido estas solicitud y gestionará su respectivo descuento. ¿Desea rebajar todas las solicitudes (Menos Horas extras) ?", "REBAJAR TODAS LAS SOLICITUDES", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                /*
                int contador = this.SOLICITUDES.Count();

                while (contador != 0)
                {
                    while (this.SOLICITUDES.ElementAt(contador - 1).HorasExtras == true)
                    {
                        contador = contador - 1;
                    }

                    if (this.SOLICITUDES.ElementAt(contador - 1).Administrativo == true)
                    {
                        this.SOLICITUDES.ElementAt(contador - 1).PersonaItem1.SaldoDiasAdmins = this.SOLICITUDES.ElementAt(contador - 1).PersonaItem1.SaldoDiasAdmins - this.SOLICITUDES.ElementAt(contador - 1).DiasSolicitados;
                    }

                    this.SOLICITUDES.ElementAt(contador - 1).Rebajada = true;
                    this.SOLICITUDES.ElementAt(contador - 1).Completada = false;
                    this.SOLICITUDES.ElementAt(contador - 1).Estado = "Rebajada";

                    this.NUEVOESTADO = new ESTADOSItem();
                    this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.ElementAt(contador - 1);
                    this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                    this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                    this.NUEVOESTADO.CreadoAt = DateTime.Now;

                    contador = contador - 1;
                }
                */

                foreach (SOLICITUDESItem solicitud in this.SOLICITUDES)
                {
                    if (solicitud.HorasExtras != true)
                    {

                        if (solicitud.Administrativo == true)
                        {
                            solicitud.PersonaItem1.SaldoDiasAdmins = solicitud.PersonaItem1.SaldoDiasAdmins - solicitud.DiasSolicitados;
                        }

                        solicitud.Rebajada = true;
                        solicitud.Completada = false;
                        solicitud.Estado = "Rebajada";

                        this.NUEVOESTADO = new ESTADOSItem();
                        this.NUEVOESTADO.SOLICITUDESItem = solicitud;
                        this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                        this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                        this.NUEVOESTADO.CreadoAt = DateTime.Now;
                    }
                }

                this.Save();
                this.Refresh();
            }
        }

 


    }
}
