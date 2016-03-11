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
    public partial class Persona_Crear
    {
        partial void Persona_Crear_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            this.PersonaItemProperty = new PersonaItem();
            this.PersonaItemProperty.SaldoDiasAdmins = 3.0;
            this.PersonaItemProperty.Es_Gerente = false;
            this.PersonaItemProperty.Es_JefeDirecto = false;
            this.PersonaItemProperty.Es_SubGerente = false;
            this.EsRolPrivado = true;
            //this.PersonaItemProperty.Nombres = ""; // Sin esto lanza un null exception ya que nombres es Null

        }

        partial void Persona_Crear_Saved()
        {
            // Escriba el código aquí.
            this.Close(true);
            //Application.Current.ShowDefaultScreen(this.PersonaItemProperty);
        }

        partial void EsRolPrivado_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItemProperty.EsRolPrivado = this.EsRolPrivado;

            if (this.EsRolPrivado == true) {

                try
                {
                    this.FindControl("DatosRolPrivado").IsVisible = true;
                    this.FindControl("DatosNoRolPrivado").IsVisible = false;
                }
                catch { }

                this.PersonaItemProperty.Division_AreaItem = null;

                if (this.PersonaItemProperty.CargoRolPrivadoItem != null)
                {
                    this.PersonaItemProperty.Cargo = this.PersonaItemProperty.CargoRolPrivadoItem.Nombre;
                }


                if (this.PersonaItemProperty.CargoRolPrivadoItem == null)
                {
                    results.AddPropertyError("Debe escoger un cargo para rol privado");
                }

            }
            else
            {
                this.PersonaItemProperty.CargoRolPrivadoItem = null;
                this.FindControl("DatosRolPrivado").IsVisible = false; 
                this.FindControl("DatosNoRolPrivado").IsVisible = true; 
            }


        }
    }
}