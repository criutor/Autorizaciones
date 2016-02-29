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

            this.ShowMessageBox("Esta opción es para agregar a quienes no están en FIN 700 (Gerentes, Subgerentes o algún caso especial), de lo contrario deben ser seleccionados desde el botón 'Agregar nuevo empleado' en la ventana 'Área' al hacer click en el nombre del área interesada en la ventana 'Admin. Divisiones' el menú 'ADMINISTRATION' ", "NOTA", MessageBoxOption.Ok);

            this.Application.ShowPersona_Crear();

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
