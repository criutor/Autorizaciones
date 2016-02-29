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
    public partial class Persona_Buscar_empleado
    {
        partial void SeleccionarTrabajador_Execute()
        {
                       
            IDPERSONA = this.PersonasContratadas.SelectedItem.Rut_Persona;
            
            if (this.Persona.Count() == 0)
            {
                
                PersonaItem personaNueva = new PersonaItem();

                personaNueva.Rut_Persona = this.PersonasContratadas.SelectedItem.Rut_Persona;
                personaNueva.Nombres = this.PersonasContratadas.SelectedItem.Nombres;
                personaNueva.AP_Paterno = this.PersonasContratadas.SelectedItem.AP_Paterno;
                personaNueva.AP_Materno = this.PersonasContratadas.SelectedItem.AP_Materno;
                personaNueva.Division_AreaItem = this.Division_AreaItem;
                personaNueva.SaldoDiasAdmins = 3.0;
                personaNueva.Es_Gerente = false;
                personaNueva.Es_JefeDirecto = false;
                personaNueva.Es_SubGerente = false;

                try
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    personaNueva.Cargo = CtoT_CargoItem.Descripcion_Cargo;

                    personaNueva.FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }
                
                /*
                string[] porPalabrasNombre = this.PersonasContratadas.SelectedItem.Nombres.Split(new Char[] { ' ' });
                string[] porPalabrasAPP = this.PersonasContratadas.SelectedItem.AP_Paterno.Split(new Char[] { ' ' });
                string[] porPalabrasAPM = this.PersonasContratadas.SelectedItem.AP_Materno.Split(new Char[] { ' ' });
                persona.NombreAD = porPalabrasAPP[0] + " " + porPalabrasAPM[0] + ", " + porPalabrasNombre[0];
                persona.NombreAD.ToUpper();
                */
            }
            else {

                try
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    this.Persona.First().Cargo = CtoT_CargoItem.Descripcion_Cargo;

                    Persona.First().FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }

                this.Persona.First().Division_AreaItem = this.Division_AreaItem;
            }


            this.Save();
            this.Close(true);
            
        }
    }
}
