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
    public partial class Buscar_empleado
    {
        partial void SeleccionarTrabajador_Execute()
        {
                       
            IDPERSONA = this.PersonasContratadas.SelectedItem.Rut_Persona;

            if (this.Persona.First() == null)
            {

                PersonaItem persona = new PersonaItem();
                persona.Rut_Persona = this.PersonasContratadas.SelectedItem.Rut_Persona;
                persona.Nombres = this.PersonasContratadas.SelectedItem.Nombres;
                persona.AP_Paterno = this.PersonasContratadas.SelectedItem.AP_Paterno;
                persona.AP_Materno = this.PersonasContratadas.SelectedItem.AP_Materno;
                persona.Division_AreaItem = this.Division_AreaItem;

                string[] porPalabrasNombre = this.PersonasContratadas.SelectedItem.Nombres.Split(new Char[] { ' ' });
                string[] porPalabrasAPP = this.PersonasContratadas.SelectedItem.AP_Paterno.Split(new Char[] { ' ' });
                string[] porPalabrasAPM = this.PersonasContratadas.SelectedItem.AP_Materno.Split(new Char[] { ' ' });
                persona.NombreAD = porPalabrasAPP[0] + " " + porPalabrasAPM[0] + ", " + porPalabrasNombre[0];
                persona.NombreAD.ToUpper();

            }
            else {

                this.Persona.First().Division_AreaItem = this.Division_AreaItem;
            }


            this.Save();
            this.Close(true);
            
        }
    }
}
