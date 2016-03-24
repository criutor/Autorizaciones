using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

using System.ComponentModel;
using Microsoft.LightSwitch.Threading;
using System.ServiceModel.DomainServices.Client;



namespace LightSwitchApplication
{
    public partial class SOLICITUDES_MIS_SOLICITUDES
    {

        partial void SOLICITUDES_MIS_SOLICITUDES_Activated()
        {
            //this.ConsultarInfoUsuarioAD_Execute();

            //this.Application.User.
            //Mostrar todas las solicitudes por defecto (Parametros de la query)
            
            this.FiltroEstados = "Todos los estados";
            
            
            //****CAMBIAR POR RUT****
            //NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper(); 
            NOMBREAD = "RUBIO FLORES, GUSTAVO";

            this.RutUsuarioFiltroSolicitudes = this.PersonaPorNombreAD.First().Rut_Persona;

            //Verificar que que tipo de usuario esta ingresando a la pantalla.
            if (this.PersonaPorNombreAD.Count() == 0)
            {
                this.MENSAJEPersonaNoCreada_Execute(); this.Close(true);

            }
            else if ((this.PersonaPorNombreAD.First().Es_Gerente != true) && (this.PersonaPorNombreAD.First().Es_JefeDirecto != true) && (this.PersonaPorNombreAD.First().Es_SubGerente != true) && this.PersonaPorNombreAD.First().Division_AreaItem == null)
            {

                this.MENSAJECuentaNoAsociada_Execute(); this.Close(true);

            }
        }

        //Quitar acentos del nombre de active directory.
        public static string removerSignosAcentos(String conAcentos)
        {
            int largo = conAcentos.Length;
            char[] NombreAD = new char[largo];
            int i = 0;

            foreach (char ch in conAcentos)
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

            return Nombreaux.ToUpper();
        }

        partial void NuevaSolicitud_Execute()
        {
            // Escriba el código aquí.
            if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true)
            {
                this.OpenModalWindow("SeleccioneTipoDeSolicitud");
            }
            else { this.OpenModalWindow("SeleccioneTipoDeSolicitudSinHorasExtras"); }
        }


        partial void MENSAJECuentaNoAsociada_Execute()
        {
            // Escriba el código aquí.
            this.ShowMessageBox("Lo sentimos, tu nombre aún no ha sido asociado a un área de trabajo o cargo de jefatura. Contacta al administrador", "SIN ÁREA DE TRABAJO O CARGO SUPERIOR!", MessageBoxOption.Ok);

        }

        partial void MENSAJEPersonaNoCreada_Execute()
        {
            // Escriba el código aquí.
            this.ShowMessageBox("Lo sentimos, tu perfil no aparece en nuestra base de datos. Contacta al administrador", "USUARIO NO ENCONTRADO!", MessageBoxOption.Ok);

        }

        partial void MasDetalles_Execute()
        {
            /*
            if (this.Solicitud_Header.SelectedItem.Administrativo == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Administrativo.First().Id_Administrativo, 1, 1);
            }
            if (this.Solicitud_Header.SelectedItem.Vacaciones == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Vacaciones.First().Id_Vacaciones, 2, 1);
            }
            if (this.Solicitud_Header.SelectedItem.HorasExtras == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_HorasExtras.First().Id_HorasExtras, 3, 1);
            }
            if (this.Solicitud_Header.SelectedItem.OtroPermiso == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_OtroPermiso.First().Id_OtroPermiso, 4, 1);
            }
            */

        }

        partial void SolicitarDiaAdministrativo_Execute()
        {
            //Cuenta si hay solicitudes por días administrativos en espera de aprobación, si es así, no creará una nueva solicitud.

            if(this.PersonaPorNombreAD.First().ConvenioColectivoItem == null){

                this.ShowMessageBox("Lo sentimos, para pedir un día administrativo necesitas estar asociado(a) a un convenio colectivo", "NO TIENES ASOCIADO UN CONVENIO COLECTIVO!", MessageBoxOption.Ok);

            }else{

                int ContAdmin = 0;

                    foreach(SOLICITUDESItem solicitudes in this.SOLICITUDES)
                    {
                        //if (solicitudes.Administrativo == true && solicitudes.Estado == "Siendo procesada") { ContAdmin = ContAdmin + 1; }
                        //if (solicitudes.Administrativo == true && solicitudes.Cancelada != true && solicitudes.Rechazada != true && solicitudes.Completada != true) { ContAdmin = ContAdmin + 1; }
                        if (solicitudes.Administrativo == true && solicitudes.Rebajada != true && solicitudes.Caducada != true && solicitudes.Cancelada != true && solicitudes.Rechazada != true) { ContAdmin = ContAdmin + 1; }

                    }

                if (ContAdmin > 0)
                {

                    this.SolicitudAdministrativoEnEspera_Execute(); this.CloseModalWindow("SeleccioneTipoDeSolicitudSinHorasExtras");
                }
                else
                {
                    this.CloseModalWindow("SeleccioneTipoDeSolicitudSinHorasExtras");
                    this.CloseModalWindow("SeleccioneTipoDeSolicitud");
                    //this.Application.ShowSolicitudes_Crear(this.PersonaPorNombreAD.First().Rut_Persona, 1);
                    this.Application.ShowSOLICITUDES_NUEVA(1);
                }
            }
        }


        partial void SolicitarVacaciones_Execute()
        {
            //Cuenta si hay solicitudes por vacaciones en espera de aprobación, si es así, no creará una nueva solicitud.

            int ContAdmin = 0;

            foreach (SOLICITUDESItem solicitudes in this.SOLICITUDES)
            {
                //if (solicitudes.Vacaciones == true && solicitudes.Estado == "Siendo procesada") { ContAdmin = ContAdmin + 1; }
                //if (solicitudes.Vacaciones == true && solicitudes.Cancelada != true && solicitudes.Rechazada != true && solicitudes.Completada != true) { ContAdmin = ContAdmin + 1; }
                if (solicitudes.Vacaciones == true && solicitudes.Rebajada != true && solicitudes.Caducada != true && solicitudes.Cancelada != true && solicitudes.Rechazada != true) { ContAdmin = ContAdmin + 1; }

            }

            if (ContAdmin > 0)
            {

                this.SolicitudVacacionesEnEspera_Execute(); this.CloseModalWindow("SeleccioneTipoDeSolicitudSinHorasExtras");
            }
            else
            {
                this.CloseModalWindow("SeleccioneTipoDeSolicitudSinHorasExtras");
                this.CloseModalWindow("SeleccioneTipoDeSolicitud");
                //this.Application.ShowSolicitudes_Crear(this.PersonaPorNombreAD.First().Rut_Persona, 2);
                this.Application.ShowSOLICITUDES_NUEVA(2);
            }
        }

        partial void SolicitarHorasExtras_Execute()
        {
            
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            //this.Application.ShowSolicitudes_Crear(this.PersonaPorNombreAD.First().Rut_Persona, 3);
            this.Application.ShowSOLICITUDES_NUEVA(3);
        }

        partial void SolicitarOtroPermiso_Execute()
        {

            this.CloseModalWindow("SeleccioneTipoDeSolicitudSinHorasExtras");
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            //this.Application.ShowSolicitudes_Crear(this.PersonaPorNombreAD.First().Rut_Persona, 4);
            this.Application.ShowSOLICITUDES_NUEVA(4);

        }

        partial void FiltroEstados_Validate(ScreenValidationResultsBuilder results)
        {
            //Si se escoge alguna de las tres opciones de búsqueda, no aplicar los filtros FALSAS ni VERDADERAS
            if (FiltroEstados != null) { this.FALSAS = null; this.VERDADERAS = null; } 
            //Al cambiar la opción se cambian los filtros
            if (FiltroEstados == "Rechazadas") { this.Rechazada = true; this.Completada = false; this.Cancelada = false; }
            else
                if (FiltroEstados == "Aprobadas") { this.Completada = true; this.Rechazada = false; this.Cancelada = false; }
                else
                    if (FiltroEstados == "Abiertas") { this.Rechazada = false; this.Completada = false; this.Cancelada = false; }
                    else
                        if (FiltroEstados == "Canceladas") { this.Rechazada = false; this.Completada = false; this.Cancelada = true; }
                        else
                            if (FiltroEstados == "Caducadas") {  this.Caducada = true; }
                            else
                                if (FiltroEstados == "Rebajadas") { this.Rebajada = true; }
                                else
                            if (FiltroEstados == "Todos los estados")
                            {

                                //this.FechaSolicitudDesde = null;
                                //this.FechaSolicitudHasta = null;
                                
                                this.Administrativo = true;
                                this.Vacaciones = true;
                                this.OtroPermiso = true;
                                this.HorasExtras = true;
                                this.FiltroEstados = null;
                                this.FALSAS = false;
                                this.VERDADERAS = true;
                                //this.Solicitud_Header.Load();
                            }
        }

        partial void SolicitarHorasExtras_CanExecute(ref bool result)
        {   
            // Solamente los Jefes de área pueden solicitar horas extras.
            if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true )
            {
                result = true;
            }
            else { result = false; }
        }

        partial void Solicitud_Header_Validate(ScreenValidationResultsBuilder results)
        {
            
            /*VERSION 1.0
             
            if(this.Solicitud_Header.Count > 0)
            {

                if (this.Solicitud_Header.SelectedItem.Administrativo.HasValue == true)
                {
                    if (this.Solicitud_Header.SelectedItem.Administrativo.Value == true)
                    {
                        
                        this.FindControl("EstadosAdministrativo").IsVisible = true;
                        this.FindControl("EstadosVacaciones").IsVisible = false;
                        this.FindControl("EstadosHorasExtras").IsVisible = false;
                        this.FindControl("EstadosOtroPermiso").IsVisible = false;
                    }
                }
                else
                    if (this.Solicitud_Header.SelectedItem.Vacaciones.HasValue == true)
                    {
                        if (this.Solicitud_Header.SelectedItem.Vacaciones.Value == true)
                        {
                            
                            this.FindControl("EstadosAdministrativo").IsVisible = false;
                            this.FindControl("EstadosVacaciones").IsVisible = true;
                            this.FindControl("EstadosHorasExtras").IsVisible = false;
                            this.FindControl("EstadosOtroPermiso").IsVisible = false;
                        }
                    }
                    else
                        if (this.Solicitud_Header.SelectedItem.HorasExtras.HasValue == true)
                        {
                            if (this.Solicitud_Header.SelectedItem.HorasExtras.Value == true)
                            {
                                

                                this.FindControl("EstadosAdministrativo").IsVisible = false;
                                this.FindControl("EstadosVacaciones").IsVisible = false;
                                this.FindControl("EstadosHorasExtras").IsVisible = true;
                                this.FindControl("EstadosOtroPermiso").IsVisible = false;
                            }
                        }
                        else
                            if (this.Solicitud_Header.SelectedItem.OtroPermiso.Value == true)
                            {
                                if (this.Solicitud_Header.SelectedItem.OtroPermiso.Value == true)
                                {
                                    
                                    this.FindControl("EstadosAdministrativo").IsVisible = false;
                                    this.FindControl("EstadosVacaciones").IsVisible = false;
                                    this.FindControl("EstadosHorasExtras").IsVisible = false;
                                    this.FindControl("EstadosOtroPermiso").IsVisible = true;
                                }
                            }
            }
            */

        }

        partial void AceptarSolicitud_Execute()
        {
            this.OpenModalWindow("AceptarSolicitudMW");
        }

        partial void CancelarSolicitud_Execute()
        {
            this.OpenModalWindow("CancelarSolicitudMW");
        }

        partial void CerrarModalWindowAceptarSolicitud_Execute()
        {
            this.CloseModalWindow("AceptarSolicitudMW");
        }

        partial void CerrarModalWindowCancelarSolicitud_Execute()
        {
            this.CloseModalWindow("CancelarSolicitudMW");
        }

        partial void EnviarRespuestaAceptar_Execute()
        {
            // Escriba el código aquí.
            if (this.NuevoComentarioAceptar == null || this.NuevoComentarioAceptar.Length <= 100)
            {
                //Instanciar un nuevo estado
                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO ACEPTADA POR:";
                this.NUEVOESTADO.MensajeBy = this.PersonaPorNombreAD.First().NombreAD;
                this.NUEVOESTADO.CreadoAt = DateTime.Now;
                this.NUEVOESTADO.Observaciones = this.NuevoComentarioAceptar;

                this.SOLICITUDES.SelectedItem.VB_Empleado = true;
                this.SOLICITUDES.SelectedItem.Estado = "Aceptada por el empleado";

                this.CloseModalWindow("AceptarSolicitudMW");

                this.Save();
                this.Refresh();
            }
        }

        partial void EnviarRespuestaCancelar_Execute()
        {
            //Ejecutar solo si el largo del comentario es el permitido, de lo contrario creará estados de mas.
            if (this.NuevoComentarioCancelar == null || this.NuevoComentarioCancelar.Length <= 100)
            {
                this.NUEVOESTADO = new ESTADOSItem();
                this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUDES.SelectedItem;
                this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO CANCELADA POR:";
                this.NUEVOESTADO.MensajeBy = this.PersonaPorNombreAD.First().NombreAD;
                this.NUEVOESTADO.CreadoAt = DateTime.Now;
                this.NUEVOESTADO.Observaciones = this.NuevoComentarioCancelar;

                if (this.SOLICITUDES.SelectedItem.HorasExtras == true)
                {
                    this.SOLICITUDES.SelectedItem.Estado = "Cancelada por el empleado";
                }
                else { this.SOLICITUDES.SelectedItem.Estado = "Cancelada por el solicitante"; }
                
                this.SOLICITUDES.SelectedItem.Cancelada = true;
                this.SOLICITUDES.SelectedItem.Completada = false;

                this.CloseModalWindow("CancelarSolicitudMW");

                this.Save();
                this.Refresh();
            }
        }

        partial void CancelarSolicitud_CanExecute(ref bool result)
        {
            try
            {
                if (this.SOLICITUDES.SelectedItem == null)
                {
                    result = false;
                }

                if (this.SOLICITUDES.SelectedItem.Rechazada == true || this.SOLICITUDES.SelectedItem.Cancelada == true || this.SOLICITUDES.SelectedItem.Rebajada == true)
                {
                    result = false;
                }
                else { result = true; }
            }
            catch { }
        }

        partial void AceptarSolicitud_CanExecute(ref bool result)
        {
            try
            {
                if (this.SOLICITUDES.SelectedItem == null)
                {
                    result = false;
                }

                if (this.SOLICITUDES.SelectedItem.Cancelada == true)
                {
                    result = false;
                }
                if (this.SOLICITUDES.SelectedItem.Rechazada == true)
                {
                    result = false;
                }

                if (this.SOLICITUDES.SelectedItem.HorasExtras == true && this.SOLICITUDES.SelectedItem.VB_Empleado == false)
                {
                    result = true;
                }
                else { result = false; }
            }
            catch { }
        }

        partial void LimpiarFiltros_Execute()
        {
            this.FiltroEstados = null;
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.FALSAS = false;
            this.VERDADERAS = true;
            this.SOLICITUDES.Load();
        }

        partial void SolicitudAdministrativoEnEspera_Execute()
        {
            this.ShowMessageBox("Lo sentimos, ya tienes una solicitud por días administrativos en espera de aprobación", "NO PUEDES TENER MÁS DE UNA SOLICITUD EN ESPERA!", MessageBoxOption.Ok);
        }

        partial void SolicitudVacacionesEnEspera_Execute()
        {
            this.ShowMessageBox("Lo sentimos, ya tienes una solicitud por Vacaciones en espera de aprobación", "NO PUEDES TENER MÁS DE UNA SOLICITUD EN ESPERA!", MessageBoxOption.Ok);
        }
        /*
        partial void ConsultarInfoUsuarioAD_Execute()
        {
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarInfoUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarInfoUsuarioAD.AddNew();

            operation.NombreUsuario = this.Application.User.FullName;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.EmailUsuarioAD = operation.EmailUsuario;

            this.RutUsuarioAD = operation.RutUsuario;

        }
        */

    }
}
