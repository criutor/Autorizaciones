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
            NOMBREAD = this.Application.User.FullName;

            if (Persona.Count() == 0)
            {
                this.MENSAJEPersonaNoCreada(); this.Close(true);

            }
            else if ((Persona.First().Es_Gerente != true) && (Persona.First().Es_JefeDirecto != true) && (Persona.First().Es_SubGerente != true) && Persona.First().Division_AreaItem == null)
            {

                this.MENSAJECuentaNoAsociada(); this.Close(true);

            }

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
            this.Application.ShowDia_Administrativo_Crear_Solicitud(Persona.First().Rut_Persona);
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

            if (this.Solicitud_Header.SelectedItem.Administrativo == true)
            {
                this.Application.ShowSolicitud_Estados_AdministrativoListDetail(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Administrativo.First().Id_Administrativo);
            }

        }
    }
}
