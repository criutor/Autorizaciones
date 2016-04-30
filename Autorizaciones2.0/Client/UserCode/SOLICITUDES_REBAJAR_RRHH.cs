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
                    this.OpenModalWindow("Group");
                }
                else
                {
                    this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de realización", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                }
            }
            else
            {
                if (DateTime.Today > this.SOLICITUDES.SelectedItem.Termino.Value)
                {
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
                    else
                    if (this.SOLICITUDES.SelectedItem.OtroPermiso == true)
                    {
                        result = this.ShowMessageBox("Esta acción indica que RR.HH ha recibido esta solicitud y gestionará su recuperación, descuento o compensación. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);
                    }

                    result = this.ShowMessageBox( Mensaje , "ADVERTENCIA", MessageBoxOption.YesNo);


                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        if (this.SOLICITUDES.SelectedItem.Administrativo == true)
                        {
                            this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoDiasAdmins = this.SOLICITUDES.SelectedItem.PersonaItem1.SaldoDiasAdmins - this.SOLICITUDES.SelectedItem.NumeroDiasTomados;
                        }

                        this.SOLICITUDES.SelectedItem.Rebajada = true;
                        this.SOLICITUDES.SelectedItem.Completada = false;

                        //this.DiasAdministrativosAprobados.SelectedItem.Estado = "Rebajada por RR.HH";
                        this.SOLICITUDES.SelectedItem.Estado = "Rebajada";

                        this.NUEVOESTADO = new ESTADOSItem();
                        this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.SelectedItem;
                        this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                        this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                        this.NUEVOESTADO.CreadoAt = DateTime.Now;

                        this.Save();
                        this.Refresh();
                    }
                }
                else
                {
                    this.ShowMessageBox("Para rebajar una solicitud debes esperar hasta después de la fecha de término", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                }
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

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                this.SOLICITUDES.SelectedItem.Rebajada = true;
                this.SOLICITUDES.SelectedItem.Completada = false;

                //this.DiasAdministrativosAprobados.SelectedItem.Estado = "Rebajada por RR.HH";
                this.SOLICITUDES.SelectedItem.Estado = "Rebajada";

                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO REBAJADA POR:";
                this.NUEVOESTADO.MensajeBy = this.Application.User.FullName.ToUpper();
                this.NUEVOESTADO.CreadoAt = DateTime.Now;

                this.Save();
                this.CloseModalWindow("Group");
                this.Refresh();
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
    }
}
