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
    public partial class Solicitud_Estados_AdministrativoItemDetail
    {
        partial void Solicitud_Estados_AdministrativoItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Solicitud_Estados_AdministrativoItem);
        }

        partial void Solicitud_Estados_AdministrativoItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Solicitud_Estados_AdministrativoItem);
        }

        partial void Solicitud_Estados_AdministrativoItemDetail_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Solicitud_Estados_AdministrativoItem);
        }
    }
}