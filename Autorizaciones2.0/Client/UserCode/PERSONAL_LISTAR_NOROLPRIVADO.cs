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
    public partial class PERSONAL_LISTAR_NOROLPRIVADO
    {

        partial void SeleccionarTrabajador_Execute()
        {
            // Escriba el código aquí.
            if(this.CODIGOPANTALLA == 2) // crear un jefe de area
            {
                if (this.PersonalNoRolPrivado.SelectedItem.Es_JefeDirecto == true)
                { 
                    this.ShowMessageBox("Este empleado posee un cargo de jefe de área en: " + this.PersonalNoRolPrivado.SelectedItem.Superior_JefeDirecto.First().Division_AreaItem.Nombre); 
                }
                else
                {
                    Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                    JefeDirecto.PersonaItem1 = this.PersonalNoRolPrivado.SelectedItem;
                    JefeDirecto.Division_AreaItem = this.Division_AreaItem;
                    this.PersonalNoRolPrivado.SelectedItem.Division_AreaItem = this.Division_AreaItem;
                    this.Division_AreaItem.JefeDeArea = this.PersonalNoRolPrivado.SelectedItem.NombreAD;

                    this.PersonalNoRolPrivado.SelectedItem.AreaDeTrabajo = this.Division_AreaItem.Nombre;

                    this.PersonalNoRolPrivado.SelectedItem.Es_JefeDirecto = true; 
                }


            }

            if (this.CODIGOPANTALLA == 1)// asignar un area de trabajo 
            {
                if (this.PersonalNoRolPrivado.SelectedItem.Es_JefeDirecto == true)
                { this.ShowMessageBox("Este empleado posee un cargo de jefe de área en: " + this.PersonalNoRolPrivado.SelectedItem.Superior_JefeDirecto.First().Division_AreaItem.Nombre); }
                else 
                {
                    this.PersonalNoRolPrivado.SelectedItem.Division_AreaItem = this.Division_AreaItem;

                    this.PersonalNoRolPrivado.SelectedItem.AreaDeTrabajo = this.Division_AreaItem.Nombre;
                }
                
            }

            this.Save();
            this.Close(true);
            
        }
    }
}
