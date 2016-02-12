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
    public partial class Solicitudes_Ver_Aprobar_Rechazar
    {
        partial void Solicitudes_Ver_Aprobar_Rechazar_Activated()
        {
            // Escriba el código aquí.

            if (PANTALLA == 1)
            {
                this.FindControl("CancelarSolicitudUsuario").IsVisible = true;
            }
            if (PANTALLA == 2)
            {
                this.FindControl("AprobarSolicitud").IsVisible = true;
                this.FindControl("RechazarSolicitud").IsVisible = true;
            }

            if (TIPOSOLICITUD == 1)
            {

                this.FindControl("Solicitud_Estados_Administrativo_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_Administrativo").IsVisible = true;
                this.FindControl("Administrativo").IsVisible = true;
                               
            }else

            if (TIPOSOLICITUD == 3)
            {

                this.FindControl("Solicitud_Estados_HorasExtras_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_HorasExtras").IsVisible = true;
                this.FindControl("HorasExtras").IsVisible = true;
            
            }else

            if (TIPOSOLICITUD == 2)
            {

                this.FindControl("Solicitud_Estados_Vacaciones_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_Vacaciones").IsVisible = true;
                this.FindControl("Vacaciones").IsVisible = true;
                
            }else

            if (TIPOSOLICITUD == 4)
            {

                this.FindControl("Solicitud_Estados_OtroPermiso_SelectedItem").IsVisible = true;
                this.FindControl("Solicitud_Estados_OtroPermiso").IsVisible = true;
                this.FindControl("OtroPermiso").IsVisible = true;
                
            }

        }

        partial void CancelarSolicitudUsuario_Execute()
        {
            // Escriba el código aquí.

        }

        partial void AprobarSolicitud_Execute()
        {
            // Escriba el código aquí.
          
        }

        partial void RechazarSolicitud_Execute()
        {
            // Escriba el código aquí.

        }
    }
}
