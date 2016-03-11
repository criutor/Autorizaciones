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
    public partial class SOLICITUDES_PENDIENTES
    {
        partial void SOLICITUDES_PENDIENTES_Activated()
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

      
            if (this.PersonaPorNombreAD.Count == 0) { this.MENSAJEPersonaNoCreada(); this.Close(true); } //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
            else
            {
                Rut_Persona = this.PersonaPorNombreAD.First().Rut_Persona; // Filtramos que en las solicitudes no aparezcan las del mismo usuario.


                if (this.PersonaPorNombreAD.First().Es_Gerente == true)
                {
                    Id_Gerencia = this.PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                    
                    VB_Gerente = false;
                    VB_GerenteNulo = false;
                    
                    VB_SubGerente = true;
                    VB_SubGerenteNulo = null; 
                    
                    VB_JefeDirecto = true;
                    VB_JefeDirectoNulo = null;
                    
                    VB_Empleado = true;
                    
                }
                else if (this.PersonaPorNombreAD.First().Es_SubGerente == true)
                {
                    Id_SubGerencia = this.PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;
                    
                    VB_Gerente = false;
                    VB_GerenteNulo = null;
                    
                    VB_SubGerente = false;
                    VB_SubGerenteNulo = false; 
                    
                    VB_JefeDirecto = true;
                    VB_JefeDirectoNulo = null;

                    VB_Empleado = true;

                }
                else if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true)
                {
                    Id_Area = this.PersonaPorNombreAD.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;

                    VB_Gerente = false;
                    VB_GerenteNulo = null;

                    VB_SubGerente = false;
                    VB_SubGerenteNulo = null;

                    VB_JefeDirecto = false; 
                    VB_JefeDirectoNulo = false;
                    
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
                this.NUEVOESTADO.MensajeBy = this.PersonaPorNombreAD.First().NombreAD;
                this.NUEVOESTADO.CreadoAt = DateTime.Now;
                this.NUEVOESTADO.Observaciones = this.NuevoComentarioAprobar;
            
                if (this.PersonaPorNombreAD.First().Es_Gerente == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = true;
                        this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Gerente";


                    }
                    else if (this.PersonaPorNombreAD.First().Es_SubGerente == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Sub Gerente";
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = true;

                        if (this.SolicitudesAbiertasACargo.SelectedItem.HorasExtras == true)
                        {
                            if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente
                            }
                            else
                            {
                                // si no hay gerente
                                this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                            }
                        }
                        else
                        {
                            if (this.SolicitudesAbiertasACargo.SelectedItem.PersonaItem1.Es_JefeDirecto != true)
                            {
                                if (this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto == true)
                                {
                                    this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                                }
                                else if (this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto == null)
                                {
                                    if (this.PersonaPorNombreAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                                    {
                                        this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                                    }
                                    else { this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; }
                                }
                            }
                            else {
                                    if (this.PersonaPorNombreAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                                    {
                                        this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                                    }
                                    else { this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; }
                                    }
                        }


                    }
                    else if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto = true;        
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada por el Jefe de Área";

                        

                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                        {
                            if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = false; // Si hay subgerente
                            }
                            else
                            {
                                if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente
                                }
                                else
                                {
                                    // si no hay ni subgerente ni gerente
                                    this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;       
                                }
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
                    this.NUEVOESTADO.MensajeBy = this.PersonaPorNombreAD.First().NombreAD;
                    this.NUEVOESTADO.CreadoAt = DateTime.Now;
                    this.NUEVOESTADO.Observaciones = this.NuevoComentarioRechazar;
                    this.SolicitudesAbiertasACargo.SelectedItem.Rechazada = true;

                    //Al cambiar VB a true, el filtro de solicitudes a mi cargo mostrará las solicitudes canceladas por el usuario que visita la pantalla.
                        if (this.PersonaPorNombreAD.First().Es_Gerente == true)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = true;
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Gerente";
                        }
                        else if (this.PersonaPorNombreAD.First().Es_SubGerente == true)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = true;
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Sub Gerente";
                        }
                        else if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto = true;
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por el Jefe de Área";
                        }

                    //this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada por los superiores";

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

    }
}
