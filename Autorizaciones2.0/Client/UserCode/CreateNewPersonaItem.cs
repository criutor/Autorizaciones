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
    public partial class CreateNewPersonaItem
    {
        partial void CreateNewPersonaItem_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            this.PersonaItemProperty = new PersonaItem();
            this.PersonaItemProperty.Nombres = ""; // Sin esto lanza un null exception ya que nombres es Null

        }

        partial void CreateNewPersonaItem_Saved()
        {
            // Escriba el código aquí.
            this.Close(true);
            //Application.Current.ShowDefaultScreen(this.PersonaItemProperty);
        }
    }
}