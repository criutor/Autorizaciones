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
    }
}
