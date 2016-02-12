using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Solicitud_Detalle_VacacionesItem
    {
        partial void Inicio_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");

            if (this.Inicio <= DateTime.Today)
            {
                results.AddPropertyError("La fecha de inicio debe ser después de hoy");
            }
        }

        partial void Termino_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");

            if (this.Termino < this.Inicio)
            {
                results.AddPropertyError("La fecha de término debe ser mayor o igual a la fecha de inicio");
            }
        }

        partial void NumeroDias_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
                TimeSpan diferenciaDias = this.Termino - this.Inicio;
                //long dif = diferenciaDias.Ticks;
                int dias = diferenciaDias.Days;
                if (this.NumeroDias > this.SALDO)
                {
                    results.AddPropertyError("El número de días a solicitar debe ser menor que tu SALDO DE DÍAS");
                }
        }

        partial void SALDO_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            
        }

        partial void Solicitud_Detalle_VacacionesItem_Created()
        {

        }

    }
}
