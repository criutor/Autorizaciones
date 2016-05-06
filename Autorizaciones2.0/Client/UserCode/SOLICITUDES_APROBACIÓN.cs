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
    public partial class SOLICITUDES_APROBACIÓN
    {
        partial void SOLICITUDES_APROBACIÓN_Activated()
        {
            //Guarda en this.RutUsuarioAD el rut del usuario AD
            this.ConsultarRutUsuarioAD_Execute();

            // Filtros para las solicitudes
            if (this.PersonaPorRutAD.Count == 0) 
            {
                //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
                this.MENSAJEPersonaNoCreada(); this.Close(true); 
            } 
            else
            {
                if (this.PersonaPorRutAD.First().Es_GerenteGeneral == true)
                {
                    // si el parametro de consulta(opcional) es igual a null, entonces no lo tomará en cuenta.

                    Es_Gerente = true;//Filtro para traer las solicitudes de los gerentes y para que en los otros escenarios no se aplique.
                    VB_Gerente = null;
                    VB_SubGerente = null; 
                    VB_JefeDirecto = null;
                    VB_Empleado = true;
                }
                else if (this.PersonaPorRutAD.First().Es_Gerente == true)
                {
                    Es_Gerente = false;
                    Id_Gerencia = this.PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                    VB_Gerente = false;
                    VB_SubGerente = null; 
                    VB_JefeDirecto = null;
                    VB_Empleado = true;
                }
                else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                {
                    Es_Gerente = false;
                    Id_SubGerencia = this.PersonaPorRutAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;
                    VB_Gerente = null;
                    VB_SubGerente = false;
                    VB_JefeDirecto = null;
                    VB_Empleado = true;
                }
                else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                {
                    Es_Gerente = false;
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
                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                
                }
                else if (this.PersonaPorRutAD.First().Es_Gerente == true)
                {
                    this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = true;
                    this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                }
                else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                {
                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "En aprobación";
                    this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = true;

                    if (this.SolicitudesAbiertasACargo.SelectedItem.HorasExtras == true) //Solicitudes de horas extras ya estan aprobadas por el jefe de área.
                    {
                        if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente

                            //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                            this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                        }
                        else
                        {
                            // si no hay gerente
                            this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                            this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                        }
                    }
                    else //Solicitudes que no son horas extras
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
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false;//Si hay gerente ***

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                            }
                        }
                        else if (this.SolicitudesAbiertasACargo.SelectedItem.PersonaItem1.Es_JefeDirecto == true)
                        {
                            if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.Completada = true;
                                this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Aprobada";
                            }
                            else 
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; //Si hay gerente

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                            }
                        }
                    }
                }
                else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                {
                    this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto = true;
                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "En aprobación";

                    if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                    {
                        if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = false; // Si hay subgerente ***

                            //ENVIAR EMAIL AL SUBGERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                            this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Email;
                        }
                        else
                        {
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente ***

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                            }
                        }
                    }
                    else
                    {
                        if (this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                        {
                            this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = false; // Si hay gerente ***

                            //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                            this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
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
                    this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                }else
                        
                    if (this.PersonaPorRutAD.First().Es_Gerente == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_Gerente = true;
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                    }
                    else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_SubGerente = true;
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                    }
                    else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                    {
                        this.SolicitudesAbiertasACargo.SelectedItem.VB_JefeDirecto = true;
                        this.SolicitudesAbiertasACargo.SelectedItem.Estado = "Rechazada";
                    }

                    //ENVIAR EMAIL AL SOLICITANTE-> SU SOLICITUD HA SIDO RECHAZADA
                    this.SolicitudesAbiertasACargo.SelectedItem.EmailProximoDestinatario = this.SolicitudesAbiertasACargo.SelectedItem.PersonaItem1.Email;
                    
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
            ConsultarRutUsuarioADItem operation = dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();
            operation.NombreUsuario = this.Application.User.FullName;
            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();
            //this.RutUsuarioAD = operation.RutUsuario;



            if (this.Application.User.HasPermission(Permissions.Salome) == true)
            {
                this.RutUsuarioAD = "15413075-6";//salome
            }
            else

                if (this.Application.User.HasPermission(Permissions.Moises) == true)
                {
                    this.RutUsuarioAD = "9220822-2";//moises
                }
                else

                    if (this.Application.User.HasPermission(Permissions.Valeria) == true)
                    {
                        this.RutUsuarioAD = "17681681-3";//valeria
                    }
                    else

                        if (this.Application.User.HasPermission(Permissions.Gustavo) == true)
                        {
                            this.RutUsuarioAD = "17511042-9";//gustavo
                        }
                        else

                            if (this.Application.User.HasPermission(Permissions.Cesar) == true)
                            {
                                this.RutUsuarioAD = "17229504-5";//cesar
                            }
                            else

                                if (this.Application.User.HasPermission(Permissions.Jair) == true)
                                {
                                    this.RutUsuarioAD = "19566061-1";//Jair
                                }

                            else
                            {
                                this.RutUsuarioAD = operation.RutUsuario;
                            }
        }
    }
}
