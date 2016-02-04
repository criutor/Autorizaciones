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
    public partial class Master_SolicitudesPendientes
    {

        partial void Master_SolicitudesPendientes_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.

        }

        partial void Master_SolicitudesPendientes_Activated()
        {
            // Escriba el código aquí.

            NOMBREAD = this.Application.User.FullName; //Guarda el nombre del usuario de la aplicación(Active directory)
            

            if (Persona.First().Es_Gerente == true)
            {
                IDGERENCIA = Persona.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
             
                
            }

            if (Persona.First().Es_SubGerente == true)
            {
                IDSUBGERENCIA = Persona.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;

            }

            if (Persona.First().Es_JefeDirecto == true)
            {
                IDAREA = Persona.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;

            }
         
        }
          

    }
}
