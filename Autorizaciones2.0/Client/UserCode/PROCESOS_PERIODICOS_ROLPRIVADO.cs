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
        //Calcula los días laborales entre dos fechas descontando los feriados.
        public static int DíasLaborales(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;

            //if (firstDay > lastDay)
            //    throw new ArgumentException("Día de Término debe ser mayor al día de Inicio " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                //int firstDayOfWeek = (int)firstDay.DayOfWeek;
                //int lastDayOfWeek = (int)lastDay.DayOfWeek;

                int firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)firstDay.DayOfWeek;
                int lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)lastDay.DayOfWeek;

                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay && !(bh.DayOfWeek == DayOfWeek.Sunday || bh.DayOfWeek == DayOfWeek.Saturday))
                    --businessDays;
            }
            return businessDays;
        }

        partial void GenerarVacacionesProporcionales_Execute()
        {
            System.Windows.MessageBoxResult result = this.ShowMessageBox("El saldo de vacaciones todos los empleados que son rol privado aumentará en '1,25'. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                DateTime[] FERIADOS = new DateTime[100];//DateTime[] FERIADOS = new DateTime[] { };
                int dia; int mes; int año; int i = 0;

                foreach (FeriadosItem feriado in this.Feriados)
                {
                    dia = feriado.Feriado.Day;
                    mes = feriado.Feriado.Month;
                    año = feriado.Feriado.Year;
                    DateTime DiaAux = new DateTime(año, mes, dia);
                    FERIADOS[i] = DiaAux;
                    i++;
                }

                foreach (PersonaItem empleado in this.Persona)
                {
                    if (empleado.EsRolPrivado == true)
                    {
                        if (empleado.VacacionesPrimerMesDevengado == true)
                        {
                            empleado.SaldoVacaciones2 = empleado.SaldoVacaciones2 + 1.25;
                            //empleado.SaldoVacaciones2 = Math.Round(empleado.SaldoVacaciones2.Value, 2);
                        }
                        else if (empleado.VacacionesPrimerMesDevengado != true)
                        {
                            empleado.SaldoVacaciones2 = empleado.SaldoVacaciones2 + ((1.25 / 30) * DíasLaborales(empleado.FechaVigencia.Value, DateTime.Today, FERIADOS));
                            //empleado.SaldoVacaciones2 = Math.Round(empleado.SaldoVacaciones2.Value, 2);
                            empleado.VacacionesPrimerMesDevengado = true;
                        }
                    }
                }

                HistorialPPRolPrivadoVacacionesProporcionalesItem historial = new HistorialPPRolPrivadoVacacionesProporcionalesItem();

                historial.EjecutadoPor = this.Application.User.FullName;
                historial.FechaEjecución = DateTime.Now;

                this.Save();

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
                        
                    }
                }

                HistorialPPRolPrivadoResetearSaldoDiasAdminsItem historial = new HistorialPPRolPrivadoResetearSaldoDiasAdminsItem ();

                historial.EjecutadoPor = this.Application.User.FullName;
                historial.FechaEjecución = DateTime.Now;

                this.Save();

                this.ShowMessageBox("Saldos actualizados con éxito");
            }
        }

        partial void PROCESOS_PERIODICOS_ROLPRIVADO_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            try
            {
                this.FechaDeEjecución = this.HistorialPPRolPrivadoResetearSaldoDiasAdmins.Last().FechaEjecución;
                this.EjecutadoPor = this.HistorialPPRolPrivadoResetearSaldoDiasAdmins.Last().EjecutadoPor;

                this.FechaDeEjecución2 = this.HistorialPPRolPrivadoVacacionesProporcionales.Last().FechaEjecución;
                this.EjecutadoPor2 = this.HistorialPPRolPrivadoVacacionesProporcionales.Last().EjecutadoPor;
            }
            catch { }
        }
    }
}
