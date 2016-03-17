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
    public partial class PERSONAL_BUSCAR_FIN700
    {
        partial void CrearTrabajador_Execute()
        {
            
                       
            IDPERSONA = this.PersonasContratadas.SelectedItem.Rut_Persona;
            
            if (this.Persona.Count() == 0)//Si la persona no existe en la bd de la aplicacion....
            {
                
                PersonaItem personaNueva = new PersonaItem();

                personaNueva.Rut_Persona = this.PersonasContratadas.SelectedItem.Rut_Persona;
                personaNueva.Nombres = this.PersonasContratadas.SelectedItem.Nombres;
                personaNueva.AP_Paterno = this.PersonasContratadas.SelectedItem.AP_Paterno;
                personaNueva.AP_Materno = this.PersonasContratadas.SelectedItem.AP_Materno;
                
                //personaNueva.Division_AreaItem = this.Division_AreaItem;
                personaNueva.Division_AreaItem = null;

                personaNueva.SaldoDiasAdmins = 3.0;
                personaNueva.Es_Gerente = false;
                personaNueva.Es_JefeDirecto = false;
                personaNueva.Es_SubGerente = false;
                personaNueva.EsRolPrivado = false;

                try //Traer el cargo de fin700
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    personaNueva.Cargo = CtoT_CargoItem.Descripcion_Cargo;

                    personaNueva.FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }

                this.Save();
                this.Close(true);
            }
            else {//Si la persona existe en la bd de la aplicacion....

                this.ShowMessageBox("Este empleado ya existe en la bd de datos de la aplicación", "ERROR", MessageBoxOption.Ok);

                /*
                try//Actualizar cargo...
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    this.Persona.First().Cargo = CtoT_CargoItem.Descripcion_Cargo;

                    Persona.First().FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }

                if (this.CodigoPantalla == 1)//Actualizar area...
                {

                    this.Persona.First().Division_AreaItem = this.Division_AreaItem;
                }
                */
                /*
                if (this.CodigoPantalla == 2)
                {
                    if (this.Persona.First().Es_JefeDirecto == true)// exite en la bd y es jda
                    {
                        this.Persona.First().Superior_JefeDirecto.First().Division_AreaItem = this.Division_AreaItem;
                    }
                    else {// existe en la bd y no es jda aun

                        Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                        JefeDirecto.PersonaItem1 = this.Persona.First();
                        JefeDirecto.Division_AreaItem = this.Division_AreaItem;
                        this.Division_AreaItem.JefeDeArea = this.Persona.First().NombreAD;
                        this.Persona.First().Es_JefeDirecto = true;
                    }

                    this.Persona.First().Division_AreaItem = this.Division_AreaItem;
                }
                */
            }



            
        }

    }
}
