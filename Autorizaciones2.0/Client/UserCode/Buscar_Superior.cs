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
    public partial class Buscar_Superior
    {
        partial void SeleccionarPersona_Execute()
        {

            if (this.codigo == 1) //Escoger un gerente por primera vez
            {
                Superior_GerenteItem gerente = new Superior_GerenteItem();
                gerente.PersonaItem1 = this.Persona.SelectedItem;
                gerente.Division_GerenciaItem = this.Division_GerenciaItem;
                this.Persona.SelectedItem.Es_Gerente = true;
                this.Save();
                this.Close(true);
                
            }

            if (this.codigo == 2) //Escoger un gerente después de la primera vez
            {
                this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Es_Gerente = false;
                this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1 = this.Persona.SelectedItem;
                this.Persona.SelectedItem.Es_Gerente = true;
                this.Save();
                this.Close(true);
            }

            if (this.codigo == 3)//Escoger un Subgerente por primera vez
            {
                Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                Subgerente.PersonaItem1 = this.Persona.SelectedItem;
                Subgerente.Division_SubGerenciaItem = this.Division_SubGerenciaItem;
                this.Persona.SelectedItem.Es_SubGerente = true;
                this.Persona.SelectedItem.IDGerencia_para_subgerentes = this.Division_SubGerenciaItem.Id_SubGerencia;
                this.Save();
                this.Close(true);

            }

            if (this.codigo == 4)//Escoger un Subgerente después de la primera vez
            {
                this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Es_SubGerente = false;
                this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1 = this.Persona.SelectedItem;
                this.Persona.SelectedItem.Es_SubGerente = true;
                this.Persona.SelectedItem.IDGerencia_para_subgerentes = this.Division_SubGerenciaItem.Id_SubGerencia;
                this.Save();
                this.Close(true);
            }

            if (this.codigo == 5)//Escoger un jefe directo por primera vez
            {
                Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                JefeDirecto.PersonaItem1 = this.Persona.SelectedItem;
                JefeDirecto.Division_AreaItem = this.Division_AreaItem;
                this.Persona.SelectedItem.Es_JefeDirecto = true;
                this.Save();
                this.Close(true);

            }

            if (this.codigo == 6)//Escoger un jefe directo después de la primera vez
            {
                this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.Es_JefeDirecto = false;
                this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1 = this.Persona.SelectedItem;
                this.Persona.SelectedItem.Es_JefeDirecto = true;
                this.Save();
                this.Close(true);
            }
        }
                      
    }
}
