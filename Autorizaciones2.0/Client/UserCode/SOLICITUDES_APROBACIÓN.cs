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
    public partial class SOLICITUDES_APROBACIÓN
    {
        partial void SOLICITUDES_APROBACIÓN_Activated()
        {
            //--------------------------------Quitar acentos del nombre de active directory------------------------------

            string NAD = this.Application.User.FullName;
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
           // ----------------------------------------------------------------------------
            
            NOMBREAD = Nombreaux.ToUpper(); //****CAMBIAR POR RUT****

            //guarda en this.RutUsuarioAD el rut del usuario AD
            this.ConsultarRutUsuarioAD_Execute();

            // Filtros para las solicitudes
            if (this.PersonaPorRutAD.Count == 0) { this.MENSAJEPersonaNoCreada(); this.Close(true); } //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
            else
            {
                //Rut_Persona = this.PersonaPorRutAD.First().Rut_Persona; // Filtramos que en las solicitudes no aparezcan las del mismo usuario.

                if (this.PersonaPorRutAD.First().Es_GerenteGeneral == true)
                {

                    VB_Gerente = null;

                    VB_SubGerente = null; // si el parametro de consulta(opcional) es igual a null, entonces no lo tomará en cuenta.

                    VB_JefeDirecto = null;

                    VB_Empleado = true;

                }
                else

                if (this.PersonaPorRutAD.First().Es_Gerente == true)
                {
                    
                    Id_Gerencia = this.PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                    
                    VB_Gerente = false;
                                        
                    VB_SubGerente = null; // si el parametro de consulta(opcional) es igual a null, entonces no lo tomará en cuenta.
                                        
                    VB_JefeDirecto = null;
                                        
                    VB_Empleado = true;
                    
                }
                else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                {
                    

                    Id_SubGerencia = this.PersonaPorRutAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;

                    VB_Gerente = null;
                    
                    VB_SubGerente = false;
                    
                    VB_JefeDirecto = null;
                    
                    VB_Empleado = true;

                }
                else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                {
                    

                    Id_Area = this.PersonaPorRutAD.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;

                    VB_Gerente = null;

                    VB_SubGerente = null;

                    VB_JefeDirecto = false;

                    VB_Empleado = true;
                    
                }
                else
                {
                    this.MENSAJE_NoEsUnSuperior(); this.Close(true);
                }

            }

        }

        partial void MENSAJE_NoEsUnSuperior_Execute()
        {
            // Escriba el código aquí.

            this.ShowMessageBox("Lo sentimos, esta sección es para aprobar o rechazar solicitudes y tu nombre no está asociado a un cargo de Jefatura. Contacta al administrador si esto es un error", "NO POSEES UN CARGO DE JEFATURA!", MessageBoxOption.Ok);
        }

        partial void MENSAJEPersonaNoCreada_Execute()
        {
            // Escriba el código aquí.
            this.ShowMessageBox("Lo sentimos, tu perfil no aparece en nuestra base de datos. Contacta al administrador", "USUARIO NO ENCONTRADO!", MessageBoxOption.Ok);

        }
        /*
        partial void DetallesSolicitud_Execute()
        {
            // Escriba el código aquí.

            if (this.Solicitud_Header.SelectedItem.Administrativo == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Administrativo.First().Id_Administrativo, 1, 2);
            }
            if (this.Solicitud_Header.SelectedItem.Vacaciones == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Vacaciones.First().Id_Vacaciones, 2, 2);
            }
            if (this.Solicitud_Header.SelectedItem.HorasExtras == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_HorasExtras.First().Id_HorasExtras, 3, 2);
            }
            if (this.Solicitud_Header.SelectedItem.OtroPermiso == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_OtroPermiso.First().Id_OtroPermiso, 4, 2);
            }

        }
        */
        partial void AprobarSolicitud_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("AprobarSolicitudMW");

        }

        partial void RechazarSolicitud_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("RechazarSolicitudMW");

        }

        partial void EnviarRespuestaAprobar_Execute()
        {
            //Ejecutar solo si el largo del comentario es el permitido, de lo contrario creará estados de mas.
            if (this.NuevoComentarioAprobar == null || this.NuevoComentarioAprobar.Length <= 100)
            {
            
                //Instanciar un nuevo estado
                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.SolicitudesAbiertasACargo.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                this.NUEVOESTADO.MensajeBy = this.PersonaPorRutAD.First().NombreAD;
                this.NUEVOESTADO.CreadoAt = DateTime.Now;
                this.NUEVOESTADO.Observaciones = this.NuevoComentarioAprobar;

                if (this.PersonaPorRutAD.First().Es_GerenteGeneral == true)
                {
                    this.SolicitudesAbiertasACargo.SelectedItem.VB_GerenteGeneral = true;

                    this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;

                    //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Gerente General";
                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";


                }
            
                if (this.PersonaPorRutAD.First().Es_Gerente == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = true;
                        this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                        //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Gerente";
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";

                    }
                    else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                    {
                        //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Sub Gerente";
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "En aprobación";
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = true;

                        if (this.SolicitudesAbiertasACargo.SelectedItem.HorasExtras == true) //Solicitudes de horas extras ya estan aprobadas por el jefe de área.
                        {
                            if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                
                            }
                            else
                            {
                                // si no hay gerente
                                this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                                this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                            }
                        }
                        else
                        {
                            if (this.SolicitudesAbiertasACargo.SelectedItem.PersonaItem1.Es_JefeDirecto != true)//Si quien realiza la solicitud no es jefe de área....
                            {
                                if (this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto == true)
                                {
                                    this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;//Si ya fue aprobada por el jda, entonces está completada la aprobación
                                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                                }
                                else if (this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto == null)//Si no tiene jda...
                                {
                                    if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                                    {
                                        this.SolicitudesAbiertasACargo.SelectedItem.Completada = true; //no llega hasta aqui, por que significa que no tiene 2 superiores al momento de crear la solicitud
                                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                                    }
                                    else { 

                                        this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false;//Si hay gerente ***

                                        //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                        //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                        
                                    }
                                }
                            }
                            else if (this.SolicitudesAbiertasACargo.SelectedItem.PersonaItem1.Es_JefeDirecto == true)
                            {

                                int num = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count();
                                    
                                if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                                    {
                                        this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;//no llega hasta aqui, por que significa que no tiene 2 superiores al momento de crear la solicitud
                                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";    
                                }
                                    else {

                                        this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; //Si hay gerente ***

                                        //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                        //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                        
                                }
                                    
                            }
                        }


                    }
                    else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto = true;        
                        //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Jefe de Área";
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "En aprobación";

                        if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                        {
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = false; // Si hay subgerente ***

                                //ENVIAR EMAIL AL SUBGERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                
                                //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Email;
                                
                            }
                            else
                            {
                                if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente ***

                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    
                                    //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                    
                                }

                            }

                        }
                        else
                        {
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente ***

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN

                                //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                
                            }
                        }
                                      
                    }

                this.CloseModalWindow("AprobarSolicitudMW");
            
                this.Save();
                this.Refresh();
            
            }
        }

        partial void EnviarRespuestaRechazar_Execute()
        {
            //Ejecutar solo si el largo del comentario es el permitido, de lo contrario creará estados de mas.
            
                if (this.NuevoComentarioRechazar == null || this.NuevoComentarioRechazar.Length <= 100)
                {
                    this.NUEVOESTADO = new ESTADOSItem();
                    this.NUEVOESTADO.SOLICITUDESItem = this.SolicitudesAbiertasACargo.SelectedItem;
                    this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO RECHAZADA POR:";
                    this.NUEVOESTADO.MensajeBy = this.PersonaPorRutAD.First().NombreAD;
                    this.NUEVOESTADO.CreadoAt = DateTime.Now;
                    this.NUEVOESTADO.Observaciones = this.NuevoComentarioRechazar;
                    this.SolicitudesAbiertasACargo.SelectedItem.Rechazada = true;

                    //Al cambiar VB a true, el filtro de solicitudes a mi cargo mostrará las solicitudes canceladas por el usuario que visita la pantalla.
                    if (this.PersonaPorRutAD.First().Es_GerenteGeneral == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_GerenteGeneral = true;
                        //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Gerente General";
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                    }else
                        
                        if (this.PersonaPorRutAD.First().Es_Gerente == true)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = true;
                            //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Gerente";
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                        }
                        else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = true;
                            //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Sub Gerente";
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                        }
                        else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto = true;
                            //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Jefe de Área";
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                        }

                        //ENVIAR EMAIL AL SOLICITANTE-> sU SOLICITUD HA SIDO RECHAZADA

                        //this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.SolicitudesAbiertasACargo.SelectedItem.PersonaItem1.Email;
                    
                    this.CloseModalWindow("RechazarSolicitudMW");

                    this.Save();
                    this.Refresh();
                }
            
        }

        partial void NuevoComentarioAprobar_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (this.NuevoComentarioAprobar != null)
            {
                if (this.NuevoComentarioAprobar.Length > 100) { results.AddPropertyError("<El comentario no debe pasar los 100 caracteres>"); }
            }
        }

        partial void CerrarModalWindowAprobarSolicitud_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("AprobarSolicitudMW");
        }

        partial void NuevoComentarioRechazar_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (this.NuevoComentarioRechazar != null)
            {
                if (this.NuevoComentarioRechazar.Length > 100) { results.AddPropertyError("<El comentario no debe pasar los 100 caracteres>"); }
            }
        }

        partial void CerrarModalWindowRechazarSolicitud_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("RechazarSolicitudMW");
        }

        partial void AprobarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.SolicitudesAbiertasACargo.SelectedItem == null)
            {
                result = false;
            }
        }

        partial void RechazarSolicitud_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.SolicitudesAbiertasACargo.SelectedItem == null)
            {
                result = false;
            }
        }

        partial void ConsultarRutUsuarioAD_Execute()
        {
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarRutUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();

            operation.NombreUsuario = this.Application.User.FullName;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.RutUsuarioAD = operation.RutUsuario;
        }

    }
}
