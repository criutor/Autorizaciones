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
    public partial class PERSONAL_DETALLES
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

        partial void PERSONAL_DETALLES_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);
            this.Close(true);
        }

        partial void PersonaItem_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            try
            {
                this.PersonaItem.Cargo = this.CargoRP.Nombre;
            }
            catch { }
            */
            try
            {
                if (this.PersonaItem.EsRolPrivado == true)
                {
                    this.FindControl("CargoRP").IsVisible = true;
                }
                else { this.FindControl("Cargo").IsVisible = true; }
            }
            catch { }


        }

        partial void EMAIL_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            //this.PersonaItem.Email = this.EMAIL;
        }

        partial void APELLIDOPATERNO_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItem.AP_Paterno = this.APELLIDOPATERNO;
        }

        partial void APELLIDOMATERNO_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItem.AP_Materno = this.APELLIDOMATERNO;
        }

        partial void NOMBRES_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItem.Nombres = this.NOMBRES;

        }

        partial void PERSONAL_DETALLES_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            this.APELLIDOPATERNO = this.PersonaItem.AP_Paterno;
            this.APELLIDOMATERNO = this.PersonaItem.AP_Materno;
            this.NOMBRES = this.PersonaItem.Nombres;
        }
    }
}