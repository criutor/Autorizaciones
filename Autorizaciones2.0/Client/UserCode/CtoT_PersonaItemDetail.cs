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
    public partial class CtoT_PersonaItemDetail
    {
        partial void CtoT_PersonaItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.CtoT_PersonaItem);
        }

        partial void CtoT_PersonaItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.CtoT_PersonaItem);
        }

        partial void CtoT_PersonaItemDetail_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.CtoT_PersonaItem);
        }
    }
}