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
    public partial class Persona_Detalles
    {
        partial void PersonaItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);
        }

        partial void PersonaItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);
        }

        partial void Persona_Detalles_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);
            this.Close(true);
        }

        partial void PersonaItem_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");



            if (this.PersonaItem.CargoRolPrivadoItem != null)
            {
                this.PersonaItem.Cargo = this.PersonaItem.CargoRolPrivadoItem.Nombre;
            }

            

        }

        partial void Persona_Detalles_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.

            if (this.PersonaItem.EsRolPrivado == true)
            {
                this.FindControl("CargoRolPrivadoItem").IsVisible = true;
            }
            else
            {
                this.FindControl("Cargo").IsVisible = true;
            }

        }
    }
}