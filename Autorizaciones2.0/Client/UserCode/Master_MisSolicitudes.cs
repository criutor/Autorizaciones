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
    public partial class Master_MisSolicitudes
    {
        partial void Master_MisSolicitudes_Activated()
        {
            //Mostrar todas las solicitudes por defecto (Parametros de la query)
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.Administrativo = true;
            this.Vacaciones = true;
            this.OtroPermiso = true;
            this.HorasExtras = true;
            this.RechazadaAprobadaAbierta = null;
            this.FALSAS = false;
            this.VERDADERAS = true;
            this.Solicitud_Header.Load();
            
            //****CAMBIAR POR RUT****
            NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper(); 
            //NOMBREAD = "RUBIO FLORES, GUSTAVO";

            //Verificar que que tipo de usuario esta ingresando a la pantalla.
            if (Persona.Count() == 0)
            {
                this.MENSAJEPersonaNoCreada_Execute(); this.Close(true);

            }
            else if ((Persona.First().Es_Gerente != true) && (Persona.First().Es_JefeDirecto != true) && (Persona.First().Es_SubGerente != true) && Persona.First().Division_AreaItem == null)
            {

                this.MENSAJECuentaNoAsociada_Execute(); this.Close(true);

            }
            else
            {
                RUTPERSONA = this.Persona.First().Rut_Persona; // Parametro de query para ver solo mis solicitudes.

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
            this.OpenModalWindow("SeleccioneTipoDeSolicitud");
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

        }

        partial void SolicitarDiaAdministrativo_Execute()
        {
           
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 1);
        }


        partial void SolicitarVacaciones_Execute()
        {
            
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 2);
        }

        partial void SolicitarHorasExtras_Execute()
        {
            
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 3);
        }

        partial void SolicitarOtroPermiso_Execute()
        {
            
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 4);

        }

        partial void RechazadaAprobadaAbierta_Validate(ScreenValidationResultsBuilder results)
        {
            //Si se escoge alguna de las tres opciones de búsqueda, no aplicar los filtros FALSAS ni VERDADERAS
            if (RechazadaAprobadaAbierta != null) { this.FALSAS = null; this.VERDADERAS = null; } 
            //Al cambiar la opción se cambian los filtros
            if (RechazadaAprobadaAbierta == "Rechazadas") { this.Rechazada = true; this.Completada = false; }else
            if (RechazadaAprobadaAbierta == "Aprobadas") { this.Completada = true; this.Rechazada = false; }else
            if (RechazadaAprobadaAbierta == "Abiertas") { this.Rechazada = false; this.Completada = false; }else
            if (RechazadaAprobadaAbierta == "Todos los estados") {

                    this.FechaSolicitudDesde = null;
                    this.FechaSolicitudHasta = null;
                    this.Administrativo = true;
                    this.Vacaciones = true;
                    this.OtroPermiso = true;
                    this.HorasExtras = true;
                    this.RechazadaAprobadaAbierta = null;
                    this.FALSAS = false;
                    this.VERDADERAS = true;
                    this.Solicitud_Header.Load();
            }
        }

        partial void SolicitarHorasExtras_CanExecute(ref bool result)
        {
            // Solamente los cargos superiores pueden solicitar horas extras.
            if (this.Persona.First().Es_Gerente == true || this.Persona.First().Es_JefeDirecto == true || this.Persona.First().Es_SubGerente == true)
            {
                result = true;
            }
            else { result = false; }

        }

        partial void Solicitud_Header_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            //Cargar los estados de la solicitud.

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

        }


    }
}
