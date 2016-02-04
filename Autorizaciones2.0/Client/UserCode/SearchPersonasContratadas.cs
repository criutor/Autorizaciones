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
    public partial class SearchPersonasContratadas
    {
        partial void SeleccionarPersonaContratada_Execute()
        {
            // Escriba el código aquí.

          
            //this.DataWorkspace.Autorizaciones_AdminsData.Persona.Single();
             
           //persona.Rut_Persona = this.PersonasContratadas.SelectedItem.Rut_Persona;       
            //persona.Nombres = this.PersonasContratadas.SelectedItem.Nombres;
            //persona.AP_Paterno = this.PersonasContratadas.SelectedItem.AP_Paterno;
            //persona.AP_Materno = this.PersonasContratadas.SelectedItem.AP_Materno;
            //persona.Division_AreaItem = this.Division_AreaItem;
            
            this.Save();
            this.Close(true);

        }
    }
}
