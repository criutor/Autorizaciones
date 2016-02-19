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
        public static string removerSignosAcentos(String conAcentos)//Quitar acentos del nombre de active directory
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

        partial void Master_MisSolicitudes_Activated()
        {
            
            //NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper(); //****CAMBIAR POR RUT****
            NOMBREAD = "RUBIO FLORES, GUSTAVO";
            if (Persona.Count() == 0)
            {
                //this.MENSAJEPersonaNoCreada(); this.Close(true);
                this.MENSAJEPersonaNoCreada_Execute(); this.Close(true);
                

            }
            else if ((Persona.First().Es_Gerente != true) && (Persona.First().Es_JefeDirecto != true) && (Persona.First().Es_SubGerente != true) && Persona.First().Division_AreaItem == null)
            {

                //this.MENSAJECuentaNoAsociada(); this.Close(true);
                this.MENSAJECuentaNoAsociada_Execute(); this.Close(true);

            }
            else
            {

                RUTPERSONA = this.Persona.First().Rut_Persona; // Parametro de query para ver solo mis solicitudes
            }

            // traer todas las solicitudes por defecto
            this.TodasLasSolicitudes_Execute(); //Aveces lanza la excepción "The operation has already completed"
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
            // Escriba el código aquí.

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
            // Escriba el código aquí.
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

        partial void TodasLasSolicitudes_Execute()
        {
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.Administrativo = true;
            this.Vacaciones = true;
            this.OtroPermiso = true;
            this.HorasExtras = true;

            //this.Rechazada = true;

            //this.Completada = true;

            this.RechazadaAprobadaAbierta = null;
            this.FALSAS = false;
            this.VERDADERAS = true;

            this.Solicitud_Header.Load();
        }

        partial void RechazadaAprobadaAbierta_Validate(ScreenValidationResultsBuilder results)
        {
            if (RechazadaAprobadaAbierta != null) { this.FALSAS = null; this.VERDADERAS = null; }

            if (RechazadaAprobadaAbierta == "Rechazadas") { this.Rechazada = true; this.Completada = false; }else
            if (RechazadaAprobadaAbierta == "Aprobadas") { this.Completada = true; this.Rechazada = false; }else
            if (RechazadaAprobadaAbierta == "Abiertas") { this.Rechazada = false; this.Completada = false; }
                
        }

    }
}
