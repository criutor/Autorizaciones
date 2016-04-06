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
    public partial class DIVISIONES_AREA_DETALLES
    {
        partial void Division_AreaItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Division_AreaItem);
        }

        partial void Division_AreaItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Division_AreaItem);
        }

        partial void DIVISIONES_AREA_DETALLES_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Division_AreaItem);
        }

        partial void Agregar_nuevo_empleado_Execute()
        {
            // Escriba el código aquí.
            this.Application.ShowPERSONAL_LISTAR_NOROLPRIVADO(1,this.Division_AreaItem.Id_Area);

        }

        partial void DIVISIONES_AREA_DETALLES_Closing(ref bool cancel)
        {
            // Escriba el código aquí.


        }
    }
}