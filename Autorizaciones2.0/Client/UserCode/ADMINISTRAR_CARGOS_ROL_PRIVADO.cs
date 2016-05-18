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
    public partial class ADMINISTRAR_CARGOS_ROL_PRIVADO
    {
        partial void NuevoCargo_Execute()
        {
            // Escriba el código aquí.
            this.Application.ShowCARGOS_ROL_PRIVADO_NUEVO();
        }

        partial void CargoRolPrivado_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                if (this.CargoRolPrivado.SelectedItem.IDArea != null)
                {
                    this.IDAREA = this.CargoRolPrivado.SelectedItem.IDArea;

                    if (this.Division_AreaItem.Division_SubGerenciaItem != null)
                    {
                        this.IDSUBGERENCIA = this.Division_AreaItem.Division_SubGerenciaItem.Id_SubGerencia;
                    }
                    else
                    {
                        this.IDSUBGERENCIA = null;
                    }

                    if (this.Division_AreaItem.Division_GerenciaItem != null)
                    {
                        this.IDGERENCIA = this.Division_AreaItem.Division_GerenciaItem.Id_Gerencia;
                    }
                    else

                        if (this.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem != null)
                        {
                            this.IDGERENCIA = this.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;
                        }
                }else

                    if (this.CargoRolPrivado.SelectedItem.IDSubgerencia != null)
                    {
                        this.IDSUBGERENCIA = this.CargoRolPrivado.SelectedItem.IDSubgerencia;

                        this.IDGERENCIA = this.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;

                        this.IDAREA = null;

                    }else
                        if (this.CargoRolPrivado.SelectedItem.IDGerencia != null)
                        {
                            this.IDGERENCIA = this.CargoRolPrivado.SelectedItem.IDGerencia;

                            this.IDSUBGERENCIA = null;

                            this.IDAREA = null;
                        }
            }
            catch { }
        }

        partial void ADMINISTRAR_CARGOS_ROL_PRIVADO_Activated()
        {
            // Escriba el código aquí.
            this.FindControl("EsJefeDeArea").IsEnabled = false;
            this.FindControl("EsSubgerente").IsEnabled = false;
            this.FindControl("EsGerente").IsEnabled = false;
        }

        partial void EliminarCargo_Execute()
        {
            System.Windows.MessageBoxResult result = this.ShowMessageBox("Sí elimina este cargo, los empleados asociados quedarán sin cargo asignado. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            this.CargoRolPrivado.SelectedItem.Persona.First().Division_AreaItem = null;
            this.CargoRolPrivado.SelectedItem.Persona.First().AreaDeTrabajo = null;
            this.CargoRolPrivado.SelectedItem.Persona.First().Cargo = null;
            this.CargoRolPrivado.SelectedItem.Persona.First().Es_GerenteGeneral = false;
            this.CargoRolPrivado.SelectedItem.Persona.First().Es_Gerente = false;
            this.CargoRolPrivado.SelectedItem.Persona.First().Es_SubGerente = false;
            this.CargoRolPrivado.SelectedItem.Persona.First().Es_JefeDirecto = false;

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                if (this.CargoRolPrivado.SelectedItem.EsGerente == true)
                {
                    if (this.CargoRolPrivado.SelectedItem.Persona.First().Superior_Gerente.Count() > 0)
                    {
                        this.CargoRolPrivado.SelectedItem.Persona.First().Superior_Gerente.First().Delete();
                    }
                }
                else
                    if (this.CargoRolPrivado.SelectedItem.EsSubgerente == true)
                    {
                        if (this.CargoRolPrivado.SelectedItem.Persona.First().Superior_SubGerente.Count() > 0)
                        {
                            this.CargoRolPrivado.SelectedItem.Persona.First().Superior_SubGerente.First().Delete();
                        }
                    }
                    else
                        if (this.CargoRolPrivado.SelectedItem.EsJefeDeArea == true)
                        {
                            if (this.CargoRolPrivado.SelectedItem.Persona.First().Superior_JefeDirecto.Count() > 0)
                            {
                                this.CargoRolPrivado.SelectedItem.Persona.First().Superior_JefeDirecto.First().Delete();
                            }
                        }
                        else
                            {
                                try
                                {
                                    foreach (PersonaItem persona in this.CargoRolPrivado.SelectedItem.Persona)
                                    {
                                        persona.Division_AreaItem = null;
                                        persona.AreaDeTrabajo = null;
                                        persona.Cargo = null;
                                    }
                                }
                                catch { }
                            }

                this.CargoRolPrivado.SelectedItem.Delete();
                this.Save();
            }
        }
    }
}
