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
    public partial class Master_MisSolicitudes
    {

        
        partial void Master_MisSolicitudes_Activated()
        {
                // Escriba el código aquí.

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
            // -------------------------------------------------------------------------------------------------------------
            NOMBREAD = Nombreaux; // ****CAMBIAR POR RUT ****
           

            if (Persona.Count() == 0)
            {
                this.MENSAJEPersonaNoCreada(); this.Close(true);

            }
            else if ((Persona.First().Es_Gerente != true) && (Persona.First().Es_JefeDirecto != true) && (Persona.First().Es_SubGerente != true) && Persona.First().Division_AreaItem == null)
            {

                this.MENSAJECuentaNoAsociada(); this.Close(true);

            }

            RUTPERSONA = this.Persona.First().Rut_Persona; // Para ver solo mis solicitudes

        }

        partial void NuevaSolicitud_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("SeleccioneTipoDeSolicitud");
        }

        partial void SolicitarDiaAdministrativo_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona,1);
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

        partial void MasDetallesAdministrativo_Execute()
        {
            // Escriba el código aquí.

            if (this.Solicitud_Header.SelectedItem.Administrativo == true) {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Administrativo.First().Id_Administrativo, 1, 1);
            }
            if (this.Solicitud_Header.SelectedItem.Vacaciones == true) {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Vacaciones.First().Id_Vacaciones, 2, 1);
            }
            if (this.Solicitud_Header.SelectedItem.HorasExtras == true) {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_HorasExtras.First().Id_HorasExtras, 3, 1);
            }
            if (this.Solicitud_Header.SelectedItem.OtroPermiso == true) {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_OtroPermiso.First().Id_OtroPermiso, 4, 1);
            }
  
        }

        partial void SolicitarVacaciones_Execute()
        {
            // Escriba el código aquí.

            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 2);
           

        }

        partial void SolicitarHorasExtras_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 3);

        }

        partial void SolicitarOtroPermiso_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("SeleccioneTipoDeSolicitud");
            this.Application.ShowSolicitudes_Crear(Persona.First().Rut_Persona, 4);

        }

        partial void LimpiarFiltros_Execute()
        {
            // Escriba el código aquí.
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;

            this.Solicitud_Header.Load();
        }
    }
}
