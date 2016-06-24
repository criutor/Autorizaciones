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
    public partial class SOLICITUDES_APROBACION
    {
        partial void SOLICITUDES_APROBACION_Activated()
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

            if (this.Application.User.HasPermission(Permissions.SalomeEscobar) == true)
            {
                this.RutUsuarioAD = "15413075-6";//salome
            }
            else

                if (this.Application.User.HasPermission(Permissions.GustavoRubio) == true)
                {
                    this.RutUsuarioAD = "17511042-9";//gustavo
                }
                else

                    if (this.Application.User.HasPermission(Permissions.CesarRiutor) == true)
                    {
                        this.RutUsuarioAD = "17229504-5";//cesar
                    }
                    else

                        if (this.Application.User.HasPermission(Permissions.MauricioHernandez) == true)
                        {
                            this.RutUsuarioAD = "10686667-8";
                        }
                        else

                            if (this.Application.User.HasPermission(Permissions.JimenaAriza) == true)
                            {
                                this.RutUsuarioAD = "10848223-0";
                            }
                            else

                                if (this.Application.User.HasPermission(Permissions.MarceloMonsalve) == true)
                                {
                                    this.RutUsuarioAD = "12233917-3";
                                }
                                else

                                    if (this.Application.User.HasPermission(Permissions.PaulaCastro) == true)
                                    {
                                        this.RutUsuarioAD = "12833658-3";
                                    }
                                    else

                                        if (this.Application.User.HasPermission(Permissions.JanetGomez) == true)
                                        {
                                            this.RutUsuarioAD = "12855246-4";
                                        }
                                        else

                                            if (this.Application.User.HasPermission(Permissions.RodrigoLeiva) == true)
                                            {
                                                this.RutUsuarioAD = "13995715-6";
                                            }
                                            else

                                                if (this.Application.User.HasPermission(Permissions.JoseJoaquinPrat) == true)
                                                {
                                                    this.RutUsuarioAD = "14120256-1";
                                                }
                                                else

                                                    if (this.Application.User.HasPermission(Permissions.CarolinaBarrientos) == true)
                                                    {
                                                        this.RutUsuarioAD = "14335101-7";
                                                    }
                                                    else

                                                        if (this.Application.User.HasPermission(Permissions.IsraelSepulveda) == true)
                                                        {
                                                            this.RutUsuarioAD = "16114128-3";
                                                        }
                                                        else

                                                            if (this.Application.User.HasPermission(Permissions.RodrigoAstudillo) == true)
                                                            {
                                                                this.RutUsuarioAD = "16121554-6";
                                                            }
                                                            else

                                                                if (this.Application.User.HasPermission(Permissions.DanielaOportus) == true)
                                                                {
                                                                    this.RutUsuarioAD = "16191035-K";
                                                                }
                                                                else

                                                                    if (this.Application.User.HasPermission(Permissions.FlorMoraga) == true)
                                                                    {
                                                                        this.RutUsuarioAD = "16524487-7";
                                                                    }
                                                                    else

                                                                        if (this.Application.User.HasPermission(Permissions.MariaJoseVives) == true)
                                                                        {
                                                                            this.RutUsuarioAD = "16570769-9";
                                                                        }
                                                                        else

                                                                            if (this.Application.User.HasPermission(Permissions.VictoriaGallardo) == true)
                                                                            {
                                                                                this.RutUsuarioAD = "17002656-K";
                                                                            }
                                                                            else

                                                                                if (this.Application.User.HasPermission(Permissions.FrancescaTapia) == true)
                                                                                {
                                                                                    this.RutUsuarioAD = "18830554-7";
                                                                                }
                                                                                else

                                                                                    if (this.Application.User.HasPermission(Permissions.AldoPeirano) == true)
                                                                                    {
                                                                                        this.RutUsuarioAD = "6075713-5";
                                                                                    }
                                                                                    else

                                                                                        if (this.Application.User.HasPermission(Permissions.AmeliaReyes) == true)
                                                                                        {
                                                                                            this.RutUsuarioAD = "6509116-K";
                                                                                        }
                                                                                        else

                                                                                            if (this.Application.User.HasPermission(Permissions.JoseUrrutia) == true)
                                                                                            {
                                                                                                this.RutUsuarioAD = "8031707-7";
                                                                                            }
                                                                                            else

                                                                                                if (this.Application.User.HasPermission(Permissions.MarcelaEspinosa) == true)
                                                                                                {
                                                                                                    this.RutUsuarioAD = "8394703-9";
                                                                                                }
                                                                                                else

                                                                                                    if (this.Application.User.HasPermission(Permissions.MoisesArevalo) == true)
                                                                                                    {
                                                                                                        this.RutUsuarioAD = "9220822-2";//moises
                                                                                                    }
                                                                                                    else

                                                                                                        if (this.Application.User.HasPermission(Permissions.MauricioMontero) == true)
                                                                                                        {
                                                                                                            this.RutUsuarioAD = "9258364-3";
                                                                                                        }
                                                                                                        else

                                                                                                            if (this.Application.User.HasPermission(Permissions.PatriciaJofré) == true)
                                                                                                            {
                                                                                                                this.RutUsuarioAD = "9282059-9";
                                                                                                            }
                                                                                                            else

                                                                                                                if (this.Application.User.HasPermission(Permissions.XimenaEspinoza) == true)
                                                                                                                {
                                                                                                                    this.RutUsuarioAD = "9407437-1";
                                                                                                                }
                                                                                                                else

                                                                                                                    if (this.Application.User.HasPermission(Permissions.ElisaMuñoz) == true)
                                                                                                                    {
                                                                                                                        this.RutUsuarioAD = "9453159-4";
                                                                                                                    }
                                                                                                                    else

                                                                                                                        if (this.Application.User.HasPermission(Permissions.MonicaSepulveda) == true)
                                                                                                                        {
                                                                                                                            this.RutUsuarioAD = "9907954-1";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            this.RutUsuarioAD = operation.RutUsuario;
                                                                                                                        }
        }

        partial void AprobarTodas_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.SolicitudesAbiertasACargo.Count() == 0) { result = false; }
        }

        partial void AprobarTodas_Execute()
        {

            System.Windows.MessageBoxResult result = this.ShowMessageBox("¿Desea aprobar todas las solicitudes?", "APROBAR TODAS LAS SOLICITUDES", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {

                int contador = this.SolicitudesAbiertasACargo.Count();
                this.SWICHCerrarAprobarSolicitudMW = true;


                while (contador != 0)
                {
                    this.NUEVOESTADO = new ESTADOSItem();
                    this.NUEVOESTADO.SOLICITUDESItem = this.SolicitudesAbiertasACargo.ElementAt(contador - 1);
                    this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO APROBADA POR:";
                    this.NUEVOESTADO.MensajeBy = this.PersonaPorRutAD.First().NombreAD;
                    this.NUEVOESTADO.CreadoAt = DateTime.Now;
                    this.NUEVOESTADO.Observaciones = this.NuevoComentarioAprobar;

                    if (this.PersonaPorRutAD.First().Es_GerenteGeneral == true)
                    {
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_GerenteGeneral = true;
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Completada = true;
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "Aprobada";

                    }
                    else if (this.PersonaPorRutAD.First().Es_Gerente == true)
                    {
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_Gerente = true;
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Completada = true;
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "Aprobada";
                    }
                    else if (this.PersonaPorRutAD.First().Es_SubGerente == true)
                    {
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "En aprobación";
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_SubGerente = true;

                        if (this.SolicitudesAbiertasACargo.ElementAt(contador - 1).HorasExtras == true) //Solicitudes de horas extras ya estan aprobadas por el jefe de área.
                        {
                            if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_Gerente = false; // Si hay gerente

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                            }
                            else
                            {
                                // si no hay gerente
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Completada = true;
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "Aprobada";
                            }
                        }
                        else //Solicitudes que no son horas extras
                        {
                            if (this.SolicitudesAbiertasACargo.ElementAt(contador - 1).PersonaItem1.Es_JefeDirecto != true)//Si quien realiza la solicitud no es jefe de área....
                            {
                                if (this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_JefeDirecto == true)
                                {
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Completada = true;//Si ya fue aprobada por el jda, entonces está completada la aprobación
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "Aprobada";
                                }
                                else if (this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_JefeDirecto == null)//Si no tiene jda...
                                {
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_Gerente = false;//Si hay gerente ***

                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                }
                            }
                            else if (this.SolicitudesAbiertasACargo.ElementAt(contador - 1).PersonaItem1.Es_JefeDirecto == true)
                            {
                                if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                                {
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Completada = true;
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "Aprobada";
                                }
                                else
                                {
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_Gerente = false; //Si hay gerente

                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                }
                            }
                        }
                    }
                    else if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)
                    {
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_JefeDirecto = true;
                        this.SolicitudesAbiertasACargo.ElementAt(contador - 1).Estado = "En aprobación";

                        if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                        {
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_SubGerente = false; // Si hay subgerente ***

                                //ENVIAR EMAIL AL SUBGERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Email;
                            }
                            else
                            {
                                if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_Gerente = false; // Si hay gerente ***

                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SolicitudesAbiertasACargo.ElementAt(contador - 1).EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                }
                            }
                        }
                        else
                        {
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).VB_Gerente = false; // Si hay gerente ***

                                //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SolicitudesAbiertasACargo.ElementAt(contador - 1).EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                            }
                        }

                    }


                    contador = contador - 1;
                }

                this.Save();
                this.Refresh();

                this.SWICHCerrarAprobarSolicitudMW = false;
            }
        }
    }
}
