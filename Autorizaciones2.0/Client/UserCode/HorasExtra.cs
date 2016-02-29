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
    public partial class HorasExtra
    {
        partial void Solicitud_HeaderItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Solicitud_HeaderItem);
        }

        partial void Solicitud_HeaderItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Solicitud_HeaderItem);
        }

        partial void HorasExtra_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Solicitud_HeaderItem);
        }
    }
}