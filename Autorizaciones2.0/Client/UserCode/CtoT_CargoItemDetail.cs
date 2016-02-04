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
    public partial class CtoT_CargoItemDetail
    {
        partial void CtoT_CargoItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.CtoT_CargoItem);
        }

        partial void CtoT_CargoItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.CtoT_CargoItem);
        }

        partial void CtoT_CargoItemDetail_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.CtoT_CargoItem);
        }
    }
}