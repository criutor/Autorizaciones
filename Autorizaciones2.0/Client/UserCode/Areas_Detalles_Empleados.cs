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
    public partial class Areas_Detalles_Empleados
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

        partial void Areas_Detalles_Empleados_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Division_AreaItem);
        }




        partial void Agregar_nuevo_empleado_Execute()
        {
            // Escriba el código aquí.
            this.Application.ShowPersona_Buscar_empleado(this.Division_AreaItemId_Area);
        }
    }
}