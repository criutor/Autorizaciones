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
    public partial class ADMINISTRAR_CARGOS_ROL_PRIVADO
    {
        partial void NuevoCargo_Execute()
        {
            // Escriba el código aquí.
            this.Application.ShowCARGOS_ROL_PRIVADO_NUEVO();
        }
    }
}
