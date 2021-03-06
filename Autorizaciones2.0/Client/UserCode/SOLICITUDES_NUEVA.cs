﻿using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

using System.ComponentModel;
using Microsoft.LightSwitch.Threading;
using System.ServiceModel.DomainServices.Client;

using System.Globalization;

namespace LightSwitchApplication
{
    public partial class SOLICITUDES_NUEVA
    {
        partial void SOLICITUDES_NUEVA_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Guarda el rut del usuario AD en this.RUTUSUARIO
            this.ConsultarRutUsuarioAD_Execute();

            //Valores por defecto 
            this.AdministrativoDesde = "La mañana (Todo el día)";
            this.AdministrativoHasta = "La tarde (Todo el día)"; 

            //Instanciar el objeto solicitud 
            this.SOLICITUD = new SOLICITUDESItem();
            this.SOLICITUD.FechaSolicitud = DateTime.Now;

            this.SOLICITUD.Rechazada = false;
            this.SOLICITUD.Completada = false;
            this.SOLICITUD.Cancelada = false;
            this.SOLICITUD.Rebajada = false;
            this.SOLICITUD.Caducada = false;

            this.SOLICITUD.Estado = "Ingresada";
            this.SOLICITUD.VB_Empleado = true;
        
            //Si la solicitude es de horas extras, la persona es null hasta que se escoja una de la lista
            if (TIPOSOLICITUD == 3) { this.SOLICITUD.PersonaItem1 = null; }
                else
            {
                this.SOLICITUD.PersonaItem1 = this.PersonaPorRutAD.First();
                this.SOLICITUD.RutEmpleado = this.PersonaPorRutAD.First().Rut_Persona;
                this.SOLICITUD.NombreEmpleado = this.PersonaPorRutAD.First().NombreAD;
            }

            //Configura quien será el primero en aprobar la solicitud
            if (this.PersonaPorRutAD.First().Es_GerenteGeneral == true)
            {
                this.SOLICITUD.AreaDeTrabajo = this.PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                this.SOLICITUD.Gerencia = this.PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                this.SOLICITUD.Completada = true;
            }
            else if (this.PersonaPorRutAD.First().Es_Gerente == true)
            {   
                //Si el solicitante es gerente

                if (this.Superior_GerenteGERENTEGENERAL.Count() == 0)
                {
                    this.ShowMessageBox("El gerente general no ha sido asociado a tu área de trabajo para evaluar tu solicitud, por favor contacta al administrador de la aplicación", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                    this.Close(false);
                }
                else
                {

                    this.SOLICITUD.AreaDeTrabajo = this.PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                    this.SOLICITUD.Gerencia = this.PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                    this.SOLICITUD.VB_Gerente = true;
                    this.SOLICITUD.VB_GerenteGeneral = false;

                    //ENVIAR EMAIL AL GERENTE GENERAL-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                    this.SOLICITUD.EmailProximoDestinatario = this.Superior_GerenteGERENTEGENERAL.First().PersonaItem1.Email;
                    //this.ShowMessageBox("Hemos enviado un email de aviso a: " + this.Superior_GerenteGERENTEGENERAL.First().PersonaItem1.Email , "EMAIL ENVIADO", MessageBoxOption.Ok);
                }
            }
            else 
                if (this.PersonaPorRutAD.First().Es_SubGerente == true)//Si el solicitante es subgerente
                {   
                    this.SOLICITUD.AreaDeTrabajo = this.PersonaPorRutAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre;
                    this.SOLICITUD.Gerencia = this.PersonaPorRutAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Division_GerenciaItem.Nombre;
                    this.SOLICITUD.VB_SubGerente = true;

                    //si no hay un gerente, la solicitud queda automaticamente aprobada
                    if (this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0)
                    {
                        this.ShowMessageBox("Debe haber un gerente asociado a tu área de trabajo para evaluar tu solicitud, por favor contacta al administrador de la aplicación", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                        this.Close(false);
                    }
                    else 
                    {
                        this.SOLICITUD.VB_Gerente = false;
                        //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                        this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                    }
                }
                else 
                    if (this.PersonaPorRutAD.First().Es_JefeDirecto == true)//Si el solicitante es Jefe de área
                    {   
                        this.IDAREA = this.PersonaPorRutAD.First().Superior_JefeDirecto.First().Division_AreaItem.Id_Area;
                        this.SOLICITUD.AreaDeTrabajo = this.PersonaPorRutAD.First().Division_AreaItem.Nombre;
                        this.SOLICITUD.Gerencia = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Nombre;
                        this.SOLICITUD.VB_JefeDirecto = true;

                        //Si es solicitud de horas extras
                        if (TIPOSOLICITUD == 3)
                        {
                            this.SOLICITUD.VB_Empleado = false;
                            //ENVIAR EMAIL AL EMPLEADO-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                            // la configuracion se realiza desde SeleccionarPersonal_Execute() cuando el Jefe de área escoge un empleado.
                        }
                        else//Si no es solicitud de horas extras
                        {
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem != null && this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                            {
                                // Si hay subgerencia y subgerente
                                this.SOLICITUD.VB_SubGerente = false;
                                //ENVIAR EMAIL AL SUBGERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Email;
                            }
                            else
                                if (this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    // Si hay gerente
                                    this.SOLICITUD.VB_Gerente = false;
                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                }
                                else if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    // Si hay gerente
                                    this.SOLICITUD.VB_Gerente = false;
                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                }
                                else
                                {
                                    // debe tener por lo menos 1 superior para poder crear una solicitud
                                    this.ShowMessageBox("Debe haber como mínimo 1 superior asociado a tu área de trabajo para evaluar tu solicitud, por favor contacta al administrador de la aplicación", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                                    this.Close(false);
                                }
                            }
                }
                else// Para los empleados que no tienen ningun cargo de supervisión
                {
                    int contarSuperiores = 0;
                    this.SOLICITUD.AreaDeTrabajo = this.PersonaPorRutAD.First().Division_AreaItem.Nombre;
                    this.SOLICITUD.Gerencia = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Nombre;

                    //Contar superiores
                    if (this.PersonaPorRutAD.First().Division_AreaItem.Superior_JefeDirecto.Count() != 0)
                    {
                        contarSuperiores = contarSuperiores + 1;
                    }

                    if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                    {
                        if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                        {
                            contarSuperiores = contarSuperiores + 1;                               
                        }

                        if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                        {
                            contarSuperiores = contarSuperiores + 1;
                        }
                        else if (this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                        {
                            contarSuperiores = contarSuperiores + 1;
                        }
                    }

                    if (contarSuperiores < 1)
                    {
                        // debe tener por lo menos 2 superiores para poder crear una solicitud
                        this.ShowMessageBox("Debe haber como mínimo 1 superior asociado a tu área de trabajo para evaluar tu solicitud, por favor contacta al administrador de la aplicación", "ACCIÓN DENEGADA", MessageBoxOption.Ok);

                        this.Close(false);
                    }
                    else
                    {
                        //Setiar vb's
                        if (this.PersonaPorRutAD.First().Division_AreaItem.Superior_JefeDirecto.Count() != 0)
                        {
                            this.SOLICITUD.VB_JefeDirecto = false; //si hay jefe de área
                            
                            //ENVIAR EMAIL AL JEFE DE AREA-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                            this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.Email;
                        }
                        else
                            if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem != null && this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                            {
                                    this.SOLICITUD.VB_SubGerente = false; // Si hay subgerente

                                    //ENVIAR EMAIL AL SUBGERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Email;
                            }
                            else
                                if (this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem != null && this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    this.SOLICITUD.VB_Gerente = false; // Si hay gerente

                                    //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                    this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                }else
                                    if (this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem != null && this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                    {
                                        this.SOLICITUD.VB_Gerente = false; // Si hay gerente

                                        //ENVIAR EMAIL AL GERENTE-> TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN
                                        this.SOLICITUD.EmailProximoDestinatario = this.PersonaPorRutAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Email;
                                    }

                    }
                    contarSuperiores = 0;
                }
            //Instanciar el objeto detalle de solicitud dependiendo del caso

            this.SOLICITUD.Inicio = DateTime.Today;
            this.SOLICITUD.Termino = DateTime.Today;

            if (TIPOSOLICITUD == 1)
            {
                this.SOLICITUD.Administrativo = true;
                this.SOLICITUD.TipoDeSolicitud = "Día administrativo";
                this.SOLICITUD.SaldoDias = this.PersonaPorRutAD.First().SaldoDiasAdmins.Value;
            }
            else if (TIPOSOLICITUD == 2)
            {
                this.SOLICITUD.Vacaciones = true;
                this.SOLICITUD.TipoDeSolicitud = "Vacaciones";

                this.SOLICITUD.Inicio = DateTime.Today.AddDays(1);
                this.SOLICITUD.Termino = DateTime.Today.AddDays(1);

                this.ConsultarSaldo_Execute();
            }
            else if (TIPOSOLICITUD == 3)
            {
                this.SOLICITUD.HorasExtras = true;
                this.SOLICITUD.TipoDeSolicitud = "Horas Extras";
                //this.SOLICITUD.VB_Empleado = false;
            }
            else if (TIPOSOLICITUD == 4)
            {
                this.SOLICITUD.OtroPermiso = true;
                this.SOLICITUD.TipoDeSolicitud = "Permiso";   
            }

            if (TIPOSOLICITUD == 3)//Solicitudes de horas extras no tienen fecha de término
            {
                this.SOLICITUD.Termino = null;
            }

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUD;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";
            this.NUEVOESTADO.MensajeBy = this.PersonaPorRutAD.First().NombreAD;
            this.NUEVOESTADO.CreadoAt = DateTime.Now;
        }

        partial void SOLICITUDES_NUEVA_Activated()
        {
            this.ConsultarRutUsuarioAD_Execute();

            try
            {
                //Activar los controles en la vista dependiendo del tipo de solicitud
                if (TIPOSOLICITUD == 1)//administrativo
                {
                    this.FindControl("ADMINISTRATIVO").IsVisible = true;
                }
                else
                    if (TIPOSOLICITUD == 2)//vacaciones
                    {
                        this.FindControl("VACACIONES").IsVisible = true;
                    }
                    else
                        if (TIPOSOLICITUD == 3)//horas extras
                        {
                            this.FindControl("HORASEXTRAS").IsVisible = true;
                        }
                        else
                            if (TIPOSOLICITUD == 4)//otro permiso
                            {
                                this.FindControl("OTROPERMISO").IsVisible = true;
                            }
            }
            catch { }
        }

        partial void SOLICITUDES_NUEVA_Saved()
        {
            if (this.SOLICITUD.HorasExtras == true)
            {
                this.ShowMessageBox("Para hacer seguimiento de esta solicitud diríjase al menú 'Reportes' y seleccione la pantalla 'HISTÓRICO'.", "SOLICITUD INGRESADA CON ÉXITO", MessageBoxOption.Ok);
            }
            
            this.Close(true);
            //Application.Current.ShowDefaultScreen(this.SOLICITUDESItemProperty);
        }

        partial void SOLICITUDES_NUEVA_Created()
        {
            // Detecta si se ha hecho algún cambio en la propiedad Solicitud_Detalle_Vacaciones en la pantalla, si es asi, llama a la funcion CrearNuevaSolicitud_PropertyChanged

            if (TIPOSOLICITUD == 2)// si la solicitude es del tipo vacaciones
            {
                Dispatchers.Main.BeginInvoke(() =>
                {
                    ((INotifyPropertyChanged)this.SOLICITUD).PropertyChanged +=
                        new PropertyChangedEventHandler(CrearNuevaSolicitud_PropertyChanged);

                });
            }
        }

        //Detecta si se ha hecho algún cambio en el campo Inicio
        void CrearNuevaSolicitud_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Inicio"))
            {
                InvocarSaldoVacaciones = true;
            }
        }

        //Se conecta con el stored procedure que consulta el saldo de vacaciones
        partial void ConsultarSaldo_Execute()
        {
            if (this.PersonaPorRutAD.First().EsRolPrivado == true)
            {
                if (this.PersonaPorRutAD.First().VacacionesProgresivas != null)
                {
                    this.SOLICITUD.SaldoDias = this.PersonaPorRutAD.First().SaldoVacaciones2 + this.PersonaPorRutAD.First().VacacionesProgresivas;
                }
                else
                {
                    this.SOLICITUD.SaldoDias = this.PersonaPorRutAD.First().SaldoVacaciones2;
                }
            }
            else
            {
                DataWorkspace dataWorkspace = new DataWorkspace();
                ConsultarSaldoVacacionesItem operation = dataWorkspace.Autorizaciones_AdminsData.ConsultarSaldoVacaciones.AddNew();
                operation.Fecha = this.SOLICITUD.Inicio.Value;
                operation.Rut = this.PersonaPorRutAD.First().Rut_Persona_ConCeros;
                RUTUSUARIOPARACONTRATO = this.PersonaPorRutAD.First().Rut_Persona_ConCeros;
                //operation.Rut = "0017511042-9"; //Gustavo
                operation.Contrato = this.ContratoPorRut.Last().Contrato;
                //operation.Contrato = 2063;//Gustavo
                dataWorkspace.Autorizaciones_AdminsData.SaveChanges();
                this.SOLICITUD.SaldoDias = operation.Saldo;
            }
        }

        //Calcula los días laborales entre dos fechas descontando los feriados.
        public static int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
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

        public static double ConvertHoursToTotalDays(double hours)
        {
            TimeSpan result = TimeSpan.FromHours(hours);

            return result.TotalDays;
        }

        public static double ConvertMinutesToTotalDays(double minutes)
        {
            TimeSpan result = TimeSpan.FromMinutes(minutes);

            return result.TotalDays;
        }

        partial void SOLICITUD_Validate(ScreenValidationResultsBuilder results)
        {
            if (InvocarSaldoVacaciones == true) 
            { 
                this.ConsultarSaldo_Execute();
                InvocarSaldoVacaciones = false;
            }
                
            if (TIPOSOLICITUD == 2)// si la solicitud es del tipo vacaciones
            {
                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                //Valida el campo solo si se le ha ingresado un valor
                if (this.SOLICITUD.Prestamo.HasValue)
                {
                    if (this.SOLICITUD.Prestamo.Value < 0 || this.SOLICITUD.Prestamo.Value > 50)
                    {
                        results.AddPropertyError("El prestamo debe ser entre 0 y 50");
                    }
                }

                
                //GUARDAR LOS REGISTROS DE FERIADOS EN UN ARREGLO

                DateTime[] FERIADOS = new DateTime[100];//DateTime[] FERIADOS = new DateTime[] { };
                int dia; int mes; int año; int i = 0;

                foreach (FeriadosItem feriado in this.Feriados)
                {
                    dia = feriado.Feriado.Day;
                    mes = feriado.Feriado.Month;
                    año = feriado.Feriado.Year;
                    //DateTime DiaAux = new DateTime(DateTime.Today.Year, mes, dia);//Cambia el año del registro al año actual
                    DateTime DiaAux = new DateTime(año, mes, dia);
                    FERIADOS[i] = DiaAux;
                    i++;
                }

                //Validar que las fechas de inicio y terminio no sean feriados ni fin de semanas
                foreach (DateTime feriado in FERIADOS)
                {
                    if (this.SOLICITUD.Inicio.Value == feriado)
                    {
                        results.AddPropertyError("La fecha de inicio no puede ser un día feriado ");
                    }

                    if (this.SOLICITUD.Termino.Value == feriado)
                    {

                        results.AddPropertyError("La fecha de término no puede ser un día feriado ");
                    }
                }

                if (this.SOLICITUD.Inicio.Value.DayOfWeek == DayOfWeek.Saturday || this.SOLICITUD.Inicio.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    results.AddPropertyError("La fecha de inicio no puede ser fin de semana ");
                }

                if (this.SOLICITUD.Termino.Value.DayOfWeek == DayOfWeek.Saturday || this.SOLICITUD.Termino.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    results.AddPropertyError("La fecha de término no puede ser fin de semana ");
                }

                if(this.SOLICITUD.Inicio > this.SOLICITUD.Termino){
                    results.AddPropertyError("Día de Término debe ser después o igual al día de Inicio ");
                }else{ 
                    this.SOLICITUD.DiasSolicitados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS);
                }

                if (this.SOLICITUD.Inicio <= DateTime.Today)
                {
                    results.AddPropertyError("Día de Inicio debe ser después hoy");
                }

                if (this.SOLICITUD.DiasSolicitados > this.SOLICITUD.SaldoDias)
                {
                    results.AddPropertyError("El número de días a solicitar debe ser menor o igual a tu SALDO DE DÍAS");
                }

            }

            if (TIPOSOLICITUD == 3)// si la solicitud es del tipo horas extras
            {
                if (this.SOLICITUD.TaxiBoolean == true)
                {
                    this.SOLICITUD.Taxi = "Sí";
                }
                else
                {
                    this.SOLICITUD.Taxi = "No";
                }

                if (this.SOLICITUD.ColacionBoolean == true)
                {
                    this.SOLICITUD.Colacion = "Sí";
                }
                else
                {
                    this.SOLICITUD.Colacion = "No";
                }

                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;
                this.SOLICITUD.ColacionBoolean = this.COLACION;
                this.SOLICITUD.TaxiBoolean = this.TAXI;

                if (this.SOLICITUD.HorasAutorizadas > 2)
                {
                    results.AddPropertyError("El máximo de horas extras a trabajar es 2");

                }
                else { this.SOLICITUD.HorasAutorizadas = this.SOLICITUD.HorasAutorizadas; }

                if (this.SOLICITUD.HorasAutorizadas <= 0 || this.SOLICITUD.HorasAutorizadas == null)
                {
                    results.AddPropertyError("Las horas autorizadas deben ser mayor a 0");
                }
                else { this.SOLICITUD.HorasAutorizadas = this.SOLICITUD.HorasAutorizadas; }

                if (this.SOLICITUD.Inicio < DateTime.Today)
                {
                    results.AddPropertyError("La fecha de realización no puede ser antes de hoy");
                }

                if (this.SOLICITUD.PersonaItem1 == null)
                {
                    results.AddPropertyError("Debe seleccionar un empleado");
                }

                //if (COLACION == true) { this.SOLICITUDESItemProperty.Colacion = true; } else { this.SOLICITUDESItemProperty.Colacion = false; }
                //if (TAXI == true) { this.SOLICITUDESItemProperty.Taxi = true; } else { this.SOLICITUDESItemProperty.Taxi = false; }

            }

            if (TIPOSOLICITUD == 4)// si la solicitud es del tipo permiso
            {
                if (this.SOLICITUD.ConDescuentoBoolean == true)
                {
                    this.SOLICITUD.ConDescuento = "Sí";
                }
                else
                {
                    this.SOLICITUD.ConDescuento = "No";
                }

                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                //GUARDAR LOS REGISTROS DE FERIADOS EN UN ARREGLO

                DateTime[] FERIADOS = new DateTime[50];//DateTime[] FERIADOS = new DateTime[] { };
                int dia; int mes; int año; int i = 0;

                foreach (FeriadosItem feriado in this.Feriados)
                {
                    dia = feriado.Feriado.Day;
                    mes = feriado.Feriado.Month;
                    año = feriado.Feriado.Year;
                    //DateTime DiaAux = new DateTime(DateTime.Today.Year, mes, dia);//Cambia el año del registro al año actual
                    DateTime DiaAux = new DateTime(año, mes, dia);
                    FERIADOS[i] = DiaAux;

                    i++;

                }

                //Validar que las fechas de inicio y terminio no sean feriados ni fin de semanas
                foreach (DateTime feriado in FERIADOS)
                {
                    if (this.SOLICITUD.Inicio.Value == feriado)
                    {
                        results.AddPropertyError("La fecha de inicio no puede ser un día feriado ");
                    }

                    if (this.SOLICITUD.Termino.Value == feriado)
                    {

                        results.AddPropertyError("La fecha de término no puede ser un día feriado ");
                    }
                }

                if (this.SOLICITUD.Inicio.Value.DayOfWeek == DayOfWeek.Saturday || this.SOLICITUD.Inicio.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    results.AddPropertyError("La fecha de inicio no puede ser fin de semana ");
                }

                if (this.SOLICITUD.Termino.Value.DayOfWeek == DayOfWeek.Saturday || this.SOLICITUD.Termino.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    results.AddPropertyError("La fecha de término no puede ser fin de semana ");
                }

                if (this.SOLICITUD.Inicio < DateTime.Today)
                {

                    results.AddPropertyError("La fecha de inicio no puede ser antes de hoy");
                }


                if (this.SOLICITUD.Termino < this.SOLICITUD.Inicio)
                {

                    results.AddPropertyError("La fecha de término debe ser después de la fecha de inicio");

                }else
                    {
                        //this.SOLICITUD.DiasSolicitados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS);

                        this.SOLICITUD.DiasSolicitados = Math.Round((BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS) - 1) + (ConvertHoursToTotalDays(this.SOLICITUD.Termino.Value.Hour - this.SOLICITUD.Inicio.Value.Hour)) + (ConvertMinutesToTotalDays(this.SOLICITUD.Termino.Value.Minute - this.SOLICITUD.Inicio.Value.Minute)), 2);

                        this.SOLICITUD.ddhhmmOTROPERMISO = "(" + (BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS) - 1).ToString() + " Días, " + (this.SOLICITUD.Termino.Value.Hour - this.SOLICITUD.Inicio.Value.Hour).ToString() + " Horas, " + (this.SOLICITUD.Termino.Value.Minute - this.SOLICITUD.Inicio.Value.Minute).ToString() + " Minutos )";
                    }
            }

            if (TIPOSOLICITUD == 1)// si la solicitud es del tipo dia administrativo
            {
                //GUARDAR LOS REGISTROS DE FERIADOS EN UN ARREGLO

                DateTime[] FERIADOS = new DateTime[50];//DateTime[] FERIADOS = new DateTime[] { };
                int dia; int mes; int año; int i = 0;

                foreach (FeriadosItem feriado in this.Feriados)
                {
                    dia = feriado.Feriado.Day;
                    mes = feriado.Feriado.Month;
                    año = feriado.Feriado.Year;
                    //DateTime DiaAux = new DateTime(DateTime.Today.Year, mes, dia);//Cambia el año del registro al año actual
                    DateTime DiaAux = new DateTime(año, mes, dia);
                    FERIADOS[i] = DiaAux;
                    i++;
                }

                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                this.SOLICITUD.ConDescuentoBoolean = this.CONDESCUENTO;

                //Validar que las fechas de inicio y terminio no sean feriados ni fin de semanas
                foreach (DateTime feriado in FERIADOS)
                {
                    if (this.SOLICITUD.Inicio.Value == feriado)
                    { 
                        results.AddPropertyError("La fecha de inicio no puede ser un día feriado "); 
                    }

                    if (this.SOLICITUD.Termino.Value == feriado)
                    {
                        results.AddPropertyError("La fecha de término no puede ser un día feriado ");
                    }    
                }

                if (this.SOLICITUD.Inicio.Value.DayOfWeek == DayOfWeek.Saturday || this.SOLICITUD.Inicio.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    results.AddPropertyError("La fecha de inicio no puede ser fin de semana ");
                }

                if (this.SOLICITUD.Termino.Value.DayOfWeek == DayOfWeek.Saturday || this.SOLICITUD.Termino.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    results.AddPropertyError("La fecha de término no puede ser fin de semana ");
                }

                if (this.SOLICITUD.Inicio < DateTime.Today)
                {
                    results.AddPropertyError("La fecha de inicio no puede ser antes de hoy");
                }

                if (this.SOLICITUD.Termino < this.SOLICITUD.Inicio)
                {
                    results.AddPropertyError("Día de Término debe ser después o igual al día de Inicio ");
                }
                else
                {
                    if (this.AdministrativoDesde == "La tarde (Medio día)" && this.AdministrativoHasta == "La mañana (Medio día)")
                    {
                        this.SOLICITUD.DiasSolicitados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS) - 1;
                    }
                    else if (this.AdministrativoDesde == "La mañana (Todo el día)" && this.AdministrativoHasta == "La tarde (Todo el día)")//ok
                    {
                        this.SOLICITUD.DiasSolicitados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS);
                    }
                    else
                    {
                        this.SOLICITUD.DiasSolicitados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS) - 0.5;
                    }
                }

                if (this.SOLICITUD.DiasSolicitados > this.SOLICITUD.SaldoDias)
                {
                    results.AddPropertyError("El número de días a solicitar debe ser menor o igual a tu saldo de días ");
                }

                if (this.SOLICITUD.DiasSolicitados <= 0)
                {
                    results.AddPropertyError("El número de días a solicitar debe ser mayor a 0");
                }
            }
        }

        //Validaciones para el campo requiere prestamo en las solicitudes de vacaciones
        partial void RequierePrestamo_Validate(ScreenValidationResultsBuilder results)
        {
            if (this.RequierePrestamo == false)
            {
                try
                {
                    this.FindControl("SOLICITUDESItemProperty_Prestamo1").IsEnabled = false;
                }
                catch { }

                if (TIPOSOLICITUD == 2)// si la solicitud es del tipo vacaciones
                {
                    this.SOLICITUD.Prestamo = null; //Limpiar el valor del campo prestamo
                }
            }
            else if (this.RequierePrestamo == true)
            {
                try
                {
                    this.FindControl("SOLICITUDESItemProperty_Prestamo1").IsEnabled = true;

                    if (this.SOLICITUD.Prestamo == null)
                    {
                        //Si la casilla es verdadera, el campo debe tener algún valor
                        results.AddPropertyError("El préstamo a solicitar no puede ser vacío"); 
                    }
                }
                catch { }
            }
        }

        //Valida que la justificacion(Observaciones) para Horas extras y permiso no sean vacias
        partial void OBSERVACIONES_Validate(ScreenValidationResultsBuilder results)
        {
            if (TIPOSOLICITUD == 3 || TIPOSOLICITUD == 4)
            {
                if (this.OBSERVACIONES == null) { results.AddPropertyError("La justificación no puede quedar vacía"); }
            }
        }

        //Guarda en el campo el valor del choice list de la pantalla
        partial void AdministrativoDesde_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.SOLICITUD.AdministrativoDesde = this.AdministrativoDesde;
        }

        //Guarda en el campo el valor del choice list de la pantalla
        partial void AdministrativoHasta_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.SOLICITUD.AdministrativoHasta = this.AdministrativoHasta;
        }

        //Cierra la pantalla sin preguntar si quiere guardar cambios
        partial void CerrarPantalla_Execute()
        {
            this.Close(false);
        }

        //Para las solicitudes de horas extras donde es necesario seleccionar un empleado
        partial void SeleccionarPersonal_Execute()
        {
            this.SOLICITUD.PersonaItem1 = this.PersonalBajoJefeDeArea.SelectedItem;
            this.SOLICITUD.RutEmpleado = this.PersonalBajoJefeDeArea.SelectedItem.Rut_Persona;
            this.SOLICITUD.NombreEmpleado = this.PersonalBajoJefeDeArea.SelectedItem.NombreAD;
            this.SOLICITUD.EmailProximoDestinatario = this.PersonalBajoJefeDeArea.SelectedItem.Email;
            this.CloseModalWindow("PersonalArea");
        }

        partial void ConsultarRutUsuarioAD_Execute()
        {
            // Escriba el código aquí.
            DataWorkspace dataWorkspace = new DataWorkspace();
            ConsultarRutUsuarioADItem operation = dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();
            operation.NombreUsuario = this.Application.User.FullName;
            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();
            //this.RutUsuarioAD = operation.RutUsuario;

            if (this.Application.User.HasPermission(Permissions.SalomeEscobar) == true)
            {
                this.RutUsuarioAD = "15413075-6";//salome
            }
            else

                if (this.Application.User.HasPermission(Permissions.GustavoRubio) == true)
                {
                    this.RutUsuarioAD = "17511042-9";//gustavo
                }
                else

                    if (this.Application.User.HasPermission(Permissions.CesarRiutor) == true)
                    {
                        this.RutUsuarioAD = "17229504-5";//cesar
                    }
                    else

                        if (this.Application.User.HasPermission(Permissions.MauricioHernandez) == true)
                        {
                            this.RutUsuarioAD = "10686667-8";
                        }
                        else

                            if (this.Application.User.HasPermission(Permissions.JimenaAriza) == true)
                            {
                                this.RutUsuarioAD = "10848223-0";
                            }
                            else

                                if (this.Application.User.HasPermission(Permissions.MarceloMonsalve) == true)
                                {
                                    this.RutUsuarioAD = "12233917-3";
                                }
                                else

                                    if (this.Application.User.HasPermission(Permissions.PaulaCastro) == true)
                                    {
                                        this.RutUsuarioAD = "12833658-3";
                                    }
                                    else

                                        if (this.Application.User.HasPermission(Permissions.JanetGomez) == true)
                                        {
                                            this.RutUsuarioAD = "12855246-4";
                                        }
                                        else

                                            if (this.Application.User.HasPermission(Permissions.RodrigoLeiva) == true)
                                            {
                                                this.RutUsuarioAD = "13995715-6";
                                            }
                                            else

                                                if (this.Application.User.HasPermission(Permissions.JoseJoaquinPrat) == true)
                                                {
                                                    this.RutUsuarioAD = "14120256-1";
                                                }
                                                else

                                                    if (this.Application.User.HasPermission(Permissions.CarolinaBarrientos) == true)
                                                    {
                                                        this.RutUsuarioAD = "14335101-7";
                                                    }
                                                    else

                                                        if (this.Application.User.HasPermission(Permissions.IsraelSepulveda) == true)
                                                        {
                                                            this.RutUsuarioAD = "16114128-3";
                                                        }
                                                        else

                                                            if (this.Application.User.HasPermission(Permissions.RodrigoAstudillo) == true)
                                                            {
                                                                this.RutUsuarioAD = "16121554-6";
                                                            }
                                                            else

                                                                if (this.Application.User.HasPermission(Permissions.DanielaOportus) == true)
                                                                {
                                                                    this.RutUsuarioAD = "16191035-K";
                                                                }
                                                                else

                                                                    if (this.Application.User.HasPermission(Permissions.FlorMoraga) == true)
                                                                    {
                                                                        this.RutUsuarioAD = "16524487-7";
                                                                    }
                                                                    else

                                                                        if (this.Application.User.HasPermission(Permissions.MariaJoseVives) == true)
                                                                        {
                                                                            this.RutUsuarioAD = "16570769-9";
                                                                        }
                                                                        else

                                                                            if (this.Application.User.HasPermission(Permissions.VictoriaGallardo) == true)
                                                                            {
                                                                                this.RutUsuarioAD = "17002656-K";
                                                                            }
                                                                            else

                                                                                if (this.Application.User.HasPermission(Permissions.FrancescaTapia) == true)
                                                                                {
                                                                                    this.RutUsuarioAD = "18830554-7";
                                                                                }
                                                                                else

                                                                                    if (this.Application.User.HasPermission(Permissions.AldoPeirano) == true)
                                                                                    {
                                                                                        this.RutUsuarioAD = "6075713-5";
                                                                                    }
                                                                                    else

                                                                                        if (this.Application.User.HasPermission(Permissions.AmeliaReyes) == true)
                                                                                        {
                                                                                            this.RutUsuarioAD = "6509116-K";
                                                                                        }
                                                                                        else

                                                                                            if (this.Application.User.HasPermission(Permissions.JoseUrrutia) == true)
                                                                                            {
                                                                                                this.RutUsuarioAD = "8031707-7";
                                                                                            }
                                                                                            else

                                                                                                if (this.Application.User.HasPermission(Permissions.MarcelaEspinosa) == true)
                                                                                                {
                                                                                                    this.RutUsuarioAD = "8394703-9";
                                                                                                }
                                                                                                else

                                                                                                    if (this.Application.User.HasPermission(Permissions.MoisesArevalo) == true)
                                                                                                    {
                                                                                                        this.RutUsuarioAD = "9220822-2";//moises
                                                                                                    }
                                                                                                    else

                                                                                                        if (this.Application.User.HasPermission(Permissions.MauricioMontero) == true)
                                                                                                        {
                                                                                                            this.RutUsuarioAD = "9258364-3";
                                                                                                        }
                                                                                                        else

                                                                                                            if (this.Application.User.HasPermission(Permissions.PatriciaJofré) == true)
                                                                                                            {
                                                                                                                this.RutUsuarioAD = "9282059-9";
                                                                                                            }
                                                                                                            else

                                                                                                                if (this.Application.User.HasPermission(Permissions.XimenaEspinoza) == true)
                                                                                                                {
                                                                                                                    this.RutUsuarioAD = "9407437-1";
                                                                                                                }
                                                                                                                else

                                                                                                                    if (this.Application.User.HasPermission(Permissions.ElisaMuñoz) == true)
                                                                                                                    {
                                                                                                                        this.RutUsuarioAD = "9453159-4";
                                                                                                                    }
                                                                                                                    else

                                                                                                                        if (this.Application.User.HasPermission(Permissions.MonicaSepulveda) == true)
                                                                                                                        {
                                                                                                                            this.RutUsuarioAD = "9907954-1";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            this.RutUsuarioAD = operation.RutUsuario;
                                                                                                                        }
            
        }
      
    }
}