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
    public partial class PROCESOS_PERIODICOS_ROLPRIVADO
    {
        partial void GenerarVacacionesProporcionales_Execute()
        {
            // Escriba el código aquí.
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("El saldo de vacaciones todos los empleados que son rol privado aumentará en '1,25'. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                foreach (PersonaItem empleado in this.Persona)
                {
                    if (empleado.EsRolPrivado == true)
                    {
                        empleado.SaldoVacaciones2 = empleado.SaldoVacaciones2 + 1.25;
                        this.Save();
                    }
                }

                this.ShowMessageBox("Saldos actualizados con éxito");
            }
        }

        partial void ResetearDíasAdministrativos_Execute()
        {
            // Escriba el código aquí.
            System.Windows.MessageBoxResult result = this.ShowMessageBox("El saldo de días administrativos de todos los empleados que son rol privado será igual a '3'. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                foreach (PersonaItem empleado in this.Persona)
                {
                    if (empleado.EsRolPrivado == true)
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
