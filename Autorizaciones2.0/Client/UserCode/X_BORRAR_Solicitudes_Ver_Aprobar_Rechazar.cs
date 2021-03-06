﻿using System;
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
    public partial class X_BORRAR_Solicitudes_Ver_Aprobar_Rechazar
    {
        partial void Solicitudes_Ver_Aprobar_Rechazar_Activated()
        {

            //--------------------------------Quitar acentos del nombre de active directory------------------------------

            string NAD = this.Application.User.FullName;
            //string NAD = "RUBIO FLORES, GUSTAVO";
            
            int largo = NAD.Length;

            char[] NombreAD = new char[largo];
            int i = 0;

            foreach (char ch in NAD)
            {
                char val = ch;

                switch (val)
                {
                    case 'á':
                    case 'Á':
                        val = 'A'; break;
                    case 'é':
                    case 'É':
                        val = 'E'; break;
                    case 'í':
                    case 'Í':
                        val = 'I'; break;
                    case 'ó':
                    case 'Ó':
                        val = 'O'; break;
                    case 'ú':
                    case 'Ú':
                        val = 'U'; break;
                }
                NombreAD[i] = val;
                i++;
            }

            string Nombreaux = new string(NombreAD);
            // ---------------------------------------------------------------------------------------------------------

            NOMBREAD = Nombreaux.ToUpper(); //****CAMBIAR POR RUT****

            if (PANTALLA == 1)//Habilita el botón cancelar solicitudes para el usuario en la vista
            {
                this.FindControl("CancelarSolicitudUsuario").IsVisible = true;
                if (TIPOSOLICITUD == 3)
                {
                    this.FindControl("AceptarSolicitudUsuario").IsVisible = true;
                }
            }

            if (PANTALLA == 2)//Habilita los botones Aprobar y Rechazar solicitud para los cargos de jefatura en la vista
            {
                this.FindControl("AprobarSolicitud").IsVisible = true;
                this.FindControl("RechazarSolicitud").IsVisible = true;
            }

            if (PANTALLA == 3)//Deshabilita los botones Aprobar, Rechazar, Cancelar y Aceptar solicitud para la vista desde "solicitudes_Empleados_A_Cargo"
            {
                this.FindControl("AprobarSolicitud").IsVisible = false;
                this.FindControl("RechazarSolicitud").IsVisible = false;
                this.FindControl("AceptarSolicitudUsuario").IsVisible = false;
                this.FindControl("CancelarSolicitudUsuario").IsVisible = false;
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

                if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Colacion == true) {  COLACIONSTRING = "Sí"; } else {  COLACIONSTRING = "No"; } //por defecto las etiquetas boolean son en inglés, por eso se deben traducir
                if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Taxi == true) { TAXISTRING = "Sí"; } else { ; TAXISTRING = "No"; }

                             
            }else

                if (TIPOSOLICITUD == 4)//Habitila los elementos de solicitudes de otros permisos en la vista
            {
                this.FindControl("Solicitud_Estados_OtroPermiso_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_OtroPermiso").IsVisible = true;
                this.FindControl("OtroPermiso").IsVisible = true;

                if (this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.ConDescuento == true) { CONDESCUENTOSTRING = "Sí"; } else { CONDESCUENTOSTRING = "No"; } //por defecto las etiquetas boolean son en inglés, por eso se deben traducir

            }
        }


        partial void AceptarSolicitudUsuario_Execute()
        {
            // Aceptar solicitud de horas extras(el empleado que debe realizar las horas extras)
            if (TIPOSOLICITUD == 3)
            {
                this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.VB_Empleado = true;

                Solicitud_Estados_HorasExtrasItem NuevoEstado4 = new Solicitud_Estados_HorasExtrasItem();

                NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO ACEPTADA POR:";
                NuevoEstado4.CreadoAt = DateTime.Now;
                this.NuevoComentario = " ";
                NuevoEstado4.Observaciones = NuevoComentario;

                NuevoEstado4.MensajeBy = this.Application.User.FullName;

                NuevoEstado4.Solicitud_Detalle_HorasExtrasItem = this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem;

                
                this.Save();
            }
        }

        partial void CancelarSolicitudUsuario_Execute()
        {
            TipoDeAccion = 3;//Cancelar solicitud (el empleado que debe realizar las horas extras)
            this.OpenModalWindow("EnviarRespuestaMW");
        }

        partial void AprobarSolicitud_Execute()
        {
            TipoDeAccion = 2;//Aprobar solicitud
            this.OpenModalWindow("EnviarRespuestaMW");
        }

        partial void RechazarSolicitud_Execute()
        {
            TipoDeAccion = 1;//Rechazar solicitud
            this.OpenModalWindow("EnviarRespuestaMW");
        }

        partial void EnviarRespuesta_Execute()
        {
            if (TIPOSOLICITUD == 1)//DIAS ADMINISTRATIVOS
            {
                if (this.CanSave == true)//Solo creará un nuevo estado si el mensaje pasa la validación, de lo contrario se podrían enviar muchos mensajes vacios.
                {
                    
                    Solicitud_Estados_AdministrativoItem NuevoEstado1 = new Solicitud_Estados_AdministrativoItem();

                    if (TipoDeAccion == 1)//Cuando la accion es rechazar una solicitud
                    {
                        this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Rechazada = true;

                        NuevoEstado1.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";

                    }else

                    if (TipoDeAccion == 2)//Cuando la accion es aprobar una solicitud
                    {
                        if(this.Persona.First().Es_JefeDirecto == true)
                        {

                            this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.VB_JefeDirecto = true;

                            NuevoEstado1.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                        
                        }else

                        if (this.Persona.First().Es_SubGerente == true)
                        {

                            this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.VB_SubGerente = true;

                            NuevoEstado1.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";

                        }else

                        if (this.Persona.First().Es_Gerente == true)
                        {

                            this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.VB_Gerente = true;
                            this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Completada = true;
                            NuevoEstado1.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";

                        }

                    }
                    else
                        if (TipoDeAccion == 3)
                        {
                            this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Rechazada = true;

                            NuevoEstado1.TituloObservacion = "LA SOLICITUD HA SIDO CANCELADA POR:";
                        }

                    NuevoEstado1.CreadoAt = DateTime.Now;

                    NuevoEstado1.Observaciones = NuevoComentario;

                    NuevoEstado1.MensajeBy = this.Application.User.FullName;

                    NuevoEstado1.Solicitud_Detalle_AdministrativoItem = this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem;
                }
            }
            else

                if (TIPOSOLICITUD == 2)// VACACIONES
                {
                    if (this.CanSave == true)//Solo creará un nuevo estado si el mensaje pasa la validación, de lo contrario se podrían enviar muchos mensajes vacios.
                    {
                        Solicitud_Estados_VacacionesItem NuevoEstado2 = new Solicitud_Estados_VacacionesItem();

                        if (TipoDeAccion == 1)//Cuando la accion es rechazar una solicitud
                        {
                            this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Rechazada = true;

                            NuevoEstado2.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";

                        }else

                        if (TipoDeAccion == 2)//Cuando la accion es aprobar una solicitud
                        {
                            if (this.Persona.First().Es_JefeDirecto == true)
                            {
                                this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.VB_JefeDirecto = true;

                                NuevoEstado2.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                            }else

                            if (this.Persona.First().Es_SubGerente == true)
                            {
                                this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.VB_SubGerente = true;
                                this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Completada = true;

                                NuevoEstado2.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                            }else

                            if (this.Persona.First().Es_Gerente == true)
                            {
                                this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.VB_Gerente = true;
                                this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Completada = true;//Solo para las solicitudes de los subgerentes

                                NuevoEstado2.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                            }
                        }else
                            if (TipoDeAccion == 3)
                            {
                                this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Rechazada = true;

                                NuevoEstado2.TituloObservacion = "LA SOLICITUD HA SIDO CANCELADA POR:";
                            }

                        NuevoEstado2.CreadoAt = DateTime.Now;

                        NuevoEstado2.Observaciones = NuevoComentario;

                        NuevoEstado2.MensajeBy = this.Application.User.FullName;

                        NuevoEstado2.Solicitud_Detalle_VacacionesItem = this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem;
                    }
                }
                else

                    if (TIPOSOLICITUD == 3)// HORAS EXTRAS
                    {
                        if (this.CanSave == true)
                        {
                            Solicitud_Estados_HorasExtrasItem NuevoEstado3 = new Solicitud_Estados_HorasExtrasItem();

                            if (TipoDeAccion == 1)//Cuando la accion es rechazar una solicitud
                            {
                                this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada = true;

                                NuevoEstado3.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";
                            }
                            else

                                if (TipoDeAccion == 2)//Cuando la accion es aprobar una solicitud
                                {
                                    if (this.Persona.First().Es_JefeDirecto == true)
                                    {
                                        this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.VB_JefeDirecto = true;

                                        NuevoEstado3.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                                    }
                                    else

                                        if (this.Persona.First().Es_SubGerente == true)
                                        {
                                            this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.VB_SubGerente = true;

                                            NuevoEstado3.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                                        }
                                        else

                                            if (this.Persona.First().Es_Gerente == true)
                                            {
                                                this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.VB_Gerente = true;
                                                this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Completada = true;

                                                NuevoEstado3.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                                            }
                                }
                                else
                                    if (TipoDeAccion == 3)
                                    {
                                        this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada = true;

                                        NuevoEstado3.TituloObservacion = "LA SOLICITUD HA SIDO CANCELADA POR:";
                                    }

                            NuevoEstado3.CreadoAt = DateTime.Now;

                            NuevoEstado3.Observaciones = NuevoComentario;

                            NuevoEstado3.MensajeBy = this.Application.User.FullName;

                            NuevoEstado3.Solicitud_Detalle_HorasExtrasItem = this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem;
                        }
                    }
                    else

                        if (TIPOSOLICITUD == 4)// OTROS PERMISOS
                        {
                            if (this.CanSave == true)
                            {
                                Solicitud_Estados_OtroPermisoItem NuevoEstado4 = new Solicitud_Estados_OtroPermisoItem();

                                if (TipoDeAccion == 1)//Cuando la accion es rechazar una solicitud
                                {
                                    this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Rechazada = true;

                                    NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";
                                }
                                else

                                    if (TipoDeAccion == 2)//Cuando la accion es aprobar una solicitud
                                    {
                                        if (this.Persona.First().Es_JefeDirecto == true)
                                        {
                                            this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.VB_JefeDirecto = true;

                                            NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                                        }
                                        else

                                            if (this.Persona.First().Es_SubGerente == true)
                                            {
                                                this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.VB_JefeDirecto = true;

                                                NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                                            }
                                            else

                                                if (this.Persona.First().Es_Gerente == true)
                                                {
                                                    this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.VB_JefeDirecto = true;

                                                    NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                                                }
                                    }
                                    else
                                        if (TipoDeAccion == 3)
                                        {
                                            this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Rechazada = true;

                                            NuevoEstado4.TituloObservacion = "LA SOLICITUD HA SIDO CANCELADA POR:";
                                        }

                                NuevoEstado4.CreadoAt = DateTime.Now;

                                NuevoEstado4.Observaciones = NuevoComentario;

                                NuevoEstado4.MensajeBy = this.Application.User.FullName;

                                NuevoEstado4.Solicitud_Detalle_OtroPermisoItem = this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem;
                            }
                        }

            this.CloseModalWindow("EnviarRespuestaMW");

            this.Save();

        }

        partial void Solicitudes_Ver_Aprobar_Rechazar_Saved()
        {
            // Escriba el código aquí.
            
                this.Close(true);
            
        }

        partial void CancelarSolicitudUsuario_CanExecute(ref bool result)
        {
            // Deshabilitar botón cancelar solicitud en caso que la solicitude ya ha sido rechazada
            if (TIPOSOLICITUD == 1)
            {
                if (this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Rechazada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 2)
            {
                if (this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Rechazada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 3)
            {
                //if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.VB_Empleado == true)
                //{
                //    result = false;
                //}
                if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 4)
            {
                if (this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Rechazada == true)
                {
                    result = false;
                }
            }

        }


        partial void AceptarSolicitudUsuario_CanExecute(ref bool result)
        {
            // Escriba el código aquí.

            if (TIPOSOLICITUD == 3)
            {
                

                if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada == true)
                {
                    result = false;
                }else
                    if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.VB_Empleado == true)
                    {
                        result = false;
                    }
                    else { result = true; }
                
            }

        }

        partial void AprobarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (TIPOSOLICITUD == 1)
            {
                if (this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 2)
            {
                if (this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 3)
            {
                if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }

            }
            if (TIPOSOLICITUD == 4)
            {
                if (this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
            }

        }

        partial void RechazarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (TIPOSOLICITUD == 1)
            {
                if (this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_Administrativo.First().Solicitud_Detalle_AdministrativoItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 2)
            {
                if (this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_Vacaciones.First().Solicitud_Detalle_VacacionesItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
            }
            if (TIPOSOLICITUD == 3)
            {
                if (this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_HorasExtras.First().Solicitud_Detalle_HorasExtrasItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
                
            }
            if (TIPOSOLICITUD == 4)
            {
                if (this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Rechazada == true || this.Solicitud_Estados_OtroPermiso.First().Solicitud_Detalle_OtroPermisoItem.Solicitud_HeaderItem.Completada == true)
                {
                    result = false;
                }
            }
        }



    }
}
