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
    public partial class SOLICITUDES_DETALLES
    {
        partial void SOLICITUD_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.SOLICITUD);
        }

        partial void SOLICITUD_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.SOLICITUD);
        }

        partial void SOLICITUDES_DETALLES_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.SOLICITUD);
        }

        partial void SOLICITUDES_DETALLES_Activated()
        {
            // Escriba el código aquí.
            if (this.SOLICITUD.Administrativo == true)//administrativo
            {

                this.FindControl("ADMINISTRATIVO").IsVisible = true;

            }
            else

                if (this.SOLICITUD.Vacaciones == true)//vacaciones
                {

                    this.FindControl("VACACIONES").IsVisible = true;


                }
                else

                    if (this.SOLICITUD.HorasExtras == true)//horas extras
                    {

                        this.FindControl("HORASEXTRAS").IsVisible = true;

                    }
                    else

                        if (this.SOLICITUD.OtroPermiso == true)//otro permiso
                        {

                            this.FindControl("PERMISO").IsVisible = true;

                        }

        }




    }
}