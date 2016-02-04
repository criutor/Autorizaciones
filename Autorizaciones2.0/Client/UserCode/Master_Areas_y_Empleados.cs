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
    public partial class Master_Areas_y_Empleados
    {
        partial void PersonaAddAndEditNew_CanExecute(ref bool result)
        {
            // Escriba el código aquí.

        }

        partial void PersonaAddAndEditNew_Execute()
        {
            // Escriba el código aquí.
            this.Application.ShowBuscar_empleado(this.Division_Area.SelectedItem.Id_Area);
        }

        partial void PersonaAddAndEditNew1_CanExecute(ref bool result)
        {
            // Escriba el código aquí.

        }

        partial void PersonaAddAndEditNew1_Execute()
        {
            // Escriba el código aquí.
            this.Application.ShowBuscar_empleado(this.Division_Area.SelectedItem.Id_Area);
        }
    }
}
