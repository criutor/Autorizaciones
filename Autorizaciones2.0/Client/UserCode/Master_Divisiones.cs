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
    public partial class Master_Divisiones
    {
        partial void SeleccionarGerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Division_Gerencia.SelectedItem.Superior_GerenteQuery.FirstOrDefault() == null)
            {
                this.Application.ShowPersona_Buscar_Superior(1, this.Division_Gerencia.SelectedItem.Id_Gerencia);
            }
            else{
                this.Application.ShowPersona_Buscar_Superior(2, this.Division_Gerencia.SelectedItem.Id_Gerencia);
                }
            
        }

        partial void SeleccionarSubGerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Division_SubGerencia.SelectedItem.Superior_SubGerenteQuery.FirstOrDefault() == null)
            {
                this.Application.ShowPersona_Buscar_Superior(3, this.Division_SubGerencia.SelectedItem.Id_SubGerencia);
            }
            else
            {
                this.Application.ShowPersona_Buscar_Superior(4, this.Division_SubGerencia.SelectedItem.Id_SubGerencia);
            }
            
        }

        partial void SeleccionarJefeDirecto_Execute()
        {
            // Escriba el código aquí.
            if (this.Division_Area.SelectedItem.Superior_JefeDirectoQuery.FirstOrDefault() == null)
            {
                this.Application.ShowPersona_Buscar_Superior(5, this.Division_Area.SelectedItem.Id_Area);
            }
            else
            {
                this.Application.ShowPersona_Buscar_Superior(6, this.Division_Area.SelectedItem.Id_Area);
            }
            
        }


       
    }
}
