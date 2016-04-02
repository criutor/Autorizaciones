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
    public partial class Application
    {

        partial void ADMINISTRAR_DIVISIONES_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_EMPLEADOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_FERIADOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_CONVENIOS_COLECTIVOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_CARGOS_ROL_PRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_ADMINISTRATIVOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_HORASEXTRAS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_PERMISOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_VACACIONES_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_HISTORICO_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }
    }
}
