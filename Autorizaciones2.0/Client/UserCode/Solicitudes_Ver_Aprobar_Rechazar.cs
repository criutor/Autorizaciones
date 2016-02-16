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
    public partial class Solicitudes_Ver_Aprobar_Rechazar
    {
        partial void Solicitudes_Ver_Aprobar_Rechazar_Activated()
        {
            if (PANTALLA == 1)//Habilita el botón cancelar solicitudes para el usuario en la vista
            {
                this.FindControl("CancelarSolicitudUsuario").IsVisible = true;

            }else
            if (PANTALLA == 2)//Habilita los botones Aprobar y Rechazar solicitud para los cargos de jefatura en la vista
            {
                this.FindControl("AprobarSolicitud").IsVisible = true;
                this.FindControl("RechazarSolicitud").IsVisible = true;
            }

            if (TIPOSOLICITUD == 1)//Habitila los elementos de solicitudes de días administrativos en la vista
            {
                this.FindControl("Solicitud_Estados_Administrativo_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_Administrativo").IsVisible = true;
                this.FindControl("Administrativo").IsVisible = true;
                               
            }else

                if (TIPOSOLICITUD == 2)//Habitila los elementos de solicitudes de vacaciones en la vista
            {
                this.FindControl("Solicitud_Estados_Vacaciones_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_Vacaciones").IsVisible = true;
                this.FindControl("Vacaciones").IsVisible = true;
            
            }else

                if (TIPOSOLICITUD == 3)//Habitila los elementos de solicitudes horas extras en la vista
            {
                this.FindControl("Solicitud_Estados_HorasExtras_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_HorasExtras").IsVisible = true;
                this.FindControl("HorasExtras").IsVisible = true;
             
            }else

                if (TIPOSOLICITUD == 4)//Habitila los elementos de solicitudes de otros permisos en la vista
            {
                this.FindControl("Solicitud_Estados_OtroPermiso_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_OtroPermiso").IsVisible = true;
                this.FindControl("OtroPermiso").IsVisible = true;
            }
        }

        partial void CancelarSolicitudUsuario_Execute()
        {
            // Escriba el código aquí.

        }

        partial void AprobarSolicitud_Execute()
        {
            // Escriba el código aquí.
          
        }

        partial void RechazarSolicitud_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("EnviarRespuestaMW");
        }

        partial void EnviarRespuesta_Execute()
        {
            if (TIPOSOLICITUD == 1)
            {

                this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Rechazada = true;

                Solicitud_Estados_AdministrativoItem NuevoEstado1 = new Solicitud_Estados_AdministrativoItem();

                NuevoEstado1.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";

                NuevoEstado1.Observaciones = NuevoComentario;

                NuevoEstado1.MensajeBy = this.Application.User.FullName;

                NuevoEstado1.Solicitud_Detalle_AdministrativoItem = this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem;

            }
            else

                if (TIPOSOLICITUD == 2)
                {

                    this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Rechazada = true;

                    Solicitud_Estados_VacacionesItem NuevoEstado2 = new Solicitud_Estados_VacacionesItem();

                    NuevoEstado2.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";

                    NuevoEstado2.Observaciones = NuevoComentario;

                    NuevoEstado2.MensajeBy = this.Application.User.FullName;

                    NuevoEstado2.Solicitud_Detalle_VacacionesItem = this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem;

                }
                else

                    if (TIPOSOLICITUD == 3)
                    {

                        this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada = true;

                        Solicitud_Estados_HorasExtrasItem NuevoEstado3 = new Solicitud_Estados_HorasExtrasItem();

                        NuevoEstado3.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";

                        NuevoEstado3.Observaciones = NuevoComentario;

                        NuevoEstado3.MensajeBy = this.Application.User.FullName;

                        NuevoEstado3.Solicitud_Detalle_HorasExtrasItem = this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem;

                    }
                    else

                        if (TIPOSOLICITUD == 4)
                        {

                            this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Rechazada = true;

                            Solicitud_Estados_OtroPermisoItem NuevoEstado4 = new Solicitud_Estados_OtroPermisoItem();

                            NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";

                            NuevoEstado4.Observaciones = NuevoComentario;

                            NuevoEstado4.MensajeBy = this.Application.User.FullName;

                            NuevoEstado4.Solicitud_Detalle_OtroPermisoItem = this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem;

                        }

            this.CloseModalWindow("EnviarRespuestaMW");

            this.Save();

        }

        partial void Solicitudes_Ver_Aprobar_Rechazar_Saved()
        {
            // Escriba el código aquí.
            if (PANTALLA == 2) {
                this.Close(true);
            }

        }
    }
}
