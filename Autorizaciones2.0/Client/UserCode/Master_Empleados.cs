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
    public partial class Master_Empleados
    {
        partial void Master_Empleados_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.

          
        }

        partial void PersonaItemListAddAndEditNew_Execute()
        {
            // Escriba el código aquí.

            this.ShowMessageBox("Esta opción es para agregar a quienes no están en FIN 700 (Gerentes, Subgerentes o algún caso especial), de lo contrario deben ser seleccionados desde el botón 'Agregar nuevo empleado' en la ventana 'Área' al hacer click en el nombre del área interesada en la ventana 'Admin. Divisiones' el menú 'ADMINISTRATION' ", "NOTA", MessageBoxOption.Ok);

            this.Application.ShowPersona_Crear();

        }


        partial void PersonaItemListDeleteSelected_Execute()
        {
            // Escriba el código aquí.
            
            System.Windows.MessageBoxResult result = this.ShowMessageBox("Sí elimina este empleado, se eliminarán todas las solicitudes asociadas. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                if(this.Persona.SelectedItem.Es_Gerente == true)
                {
                    System.Windows.MessageBoxResult resultG = this.ShowMessageBox("Este empleado es Gerente en: "+ this.Persona.SelectedItem.Superior_Gerente.First().Division_GerenciaItem.Nombre + ". ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);
                    
                    if (resultG == System.Windows.MessageBoxResult.Yes)
                    {
                        this.Persona.SelectedItem.Superior_Gerente.First().Division_GerenciaItem.Gerente = null;

                        this.Persona.SelectedItem.Delete();

                        this.Save();
                    }
                }
                else
                    if (this.Persona.SelectedItem.Es_SubGerente == true)
                    {
                        System.Windows.MessageBoxResult resultS = this.ShowMessageBox("Este empleado es Subgerente en: " + this.Persona.SelectedItem.Superior_SubGerente.First().Division_SubGerenciaItem.Nombre + ". ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);
                        
                        if (resultS == System.Windows.MessageBoxResult.Yes)
                        {
                            this.Persona.SelectedItem.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = null;

                            this.Persona.SelectedItem.Delete();

                            this.Save();
                        }
                    }
                    else
                        if (this.Persona.SelectedItem.Es_JefeDirecto == true)
                        {
                            System.Windows.MessageBoxResult resultJ = this.ShowMessageBox("Este empleado es Jefe de área en: " + this.Persona.SelectedItem.Superior_JefeDirecto.First().Division_AreaItem.Nombre + ". ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

                            if (resultJ == System.Windows.MessageBoxResult.Yes)
                            {
                                this.Persona.SelectedItem.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;

                                this.Persona.SelectedItem.Delete();

                                this.Save();
                            }

                        }else
                            if (result == System.Windows.MessageBoxResult.Yes)
                            {
                                this.Persona.SelectedItem.Delete();

                                this.Save();
                            }
            }
        }
    }
}

namespace UserCode
{
    public partial class PersonaListDetail
    {
       
    }
}
