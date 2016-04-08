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
    public partial class PROCESOS_PERIODICOS_RRHH
    {
        partial void ResetearDíasAdministrativos_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("El saldo de días administrativos de todos los empleados que NO son rol privado será igual a '3'. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                foreach (PersonaItem empleado in this.Persona)
                {
                    if (empleado.EsRolPrivado != true)
                    {
                        empleado.SaldoDiasAdmins = 3;
                        this.Save();
                    }
                }

                this.ShowMessageBox("Saldos actualizados con éxito");
            }

        }

    }
}
