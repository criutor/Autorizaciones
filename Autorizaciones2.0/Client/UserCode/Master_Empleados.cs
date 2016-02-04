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
    public partial class Master_Empleados
    {
        partial void Master_Empleados_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.

          
        }

        partial void PersonaItemListAddAndEditNew_Execute()
        {
            // Escriba el código aquí.

            this.Application.ShowPersona_Crear_Nueva();

        }

        partial void ActualizarDirectorio_Execute()
        {
            // Escriba el código aquí.

         

        }
    }
}

namespace UserCode
{
    public partial class PersonaListDetail
    {
       
    }
}
