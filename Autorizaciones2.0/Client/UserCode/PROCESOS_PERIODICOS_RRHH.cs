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
            System.Windows.MessageBoxResult result = this.ShowMessageBox("El saldo de días administrativos de todos los empleados que son rol general será igual a '3'. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                foreach (PersonaItem empleado in this.Persona)
                {
                    if (empleado.EsRolPrivado != true)
                    {
                        empleado.SaldoDiasAdmins = 3;
                        
                    }
                }

                HistorialPPRRHHResetearSaldoDiasAdminsItem historial = new HistorialPPRRHHResetearSaldoDiasAdminsItem();

                historial.EjecutadoPor = this.Application.User.FullName;
                historial.FechaEjecución = DateTime.Now;

                this.Save();
                
                this.ShowMessageBox("Saldos actualizados con éxito");
            }
        }

        partial void PROCESOS_PERIODICOS_RRHH_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            try
            {
                this.FechaDeEjecución = this.HistorialPPRRHHResetearSaldoDiasAdmins.Last().FechaEjecución;
                this.EjecutadoPor = this.HistorialPPRRHHResetearSaldoDiasAdmins.Last().EjecutadoPor;
            }
            catch { }
        }

    }
}
