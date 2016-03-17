using System;
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
        partial void SOLICITUDES_NUEVA_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            this.AdministrativoDesde = "La mañana (Todo el día)";
            this.AdministrativoHasta = "La tarde (Todo el día)"; 

            // Parametro de busqueda de la persona
            NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper();
            //NOMBREAD = "RUBIO FLORES, GUSTAVO";
            // Guarda el rut del usuario
            RUTUSUARIO = this.PersonaPorNombreAD.First().Rut_Persona;

            //Instanciar el objeto solicitud 
            this.SOLICITUD = new SOLICITUDESItem();
            this.SOLICITUD.FechaSolicitud = DateTime.Now;
            this.SOLICITUD.Rechazada = false;
            this.SOLICITUD.Completada = false;
            this.SOLICITUD.Cancelada = false;
            this.SOLICITUD.Estado = "Siendo procesada";
            this.SOLICITUD.VB_Empleado = true;
            
            //Si la solicitude es de horas extras, la persona es null hasta que se escoja una de la lista
            if (TIPOSOLICITUD == 3) { this.SOLICITUD.PersonaItem1 = null; }
                else{this.SOLICITUD.PersonaItem1 = this.PersonaPorNombreAD.First();}

            if (this.PersonaPorNombreAD.First().Es_Gerente == true)
            {
                //this.IDGERENCIA = this.PersonaPorNombreAD.First().Superior_Gerente.First().Division_GerenciaItem.Id_Gerencia;
                this.SOLICITUD.Departamento = " Gerencia de " + this.PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;

                this.SOLICITUD.VB_Gerente = true;
                //this.SOLICITUD.VB_SubGerente = true;
                //this.SOLICITUD.VB_JefeDirecto = true;
                this.SOLICITUD.Completada = true;
                this.SOLICITUD.Estado = "Aprobada por el Gerente";
            }
            else
                if (this.PersonaPorNombreAD.First().Es_SubGerente == true)
                {
                    //this.IDSUBGERENCIA = this.PersonaPorNombreAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Id_SubGerencia;
                    this.SOLICITUD.Departamento = " SubGerencia de " + this.PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre;
                    this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Division_GerenciaItem.Nombre;

                    this.SOLICITUD.VB_Gerente = false;
                    this.SOLICITUD.VB_SubGerente = true;
                    //this.SOLICITUD.VB_JefeDirecto = true;

                    if (this.PersonaPorNombreAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() == 0) 
                    {
                        this.SOLICITUD.Completada = true; //si no hay un gerente
                        this.SOLICITUD.Estado = "Aprobada por el Sub Gerente";
                    }
                }
                else
                    if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true)
                    {
                        this.IDAREA = this.PersonaPorNombreAD.First().Superior_JefeDirecto.First().Division_AreaItem.Id_Area;
                        this.SOLICITUD.Departamento = this.PersonaPorNombreAD.First().Division_AreaItem.Nombre;
                        this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Nombre;
                        this.SOLICITUD.VB_JefeDirecto = true;

                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem != null && this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                        {
                            // Si hay subgerencia y subgerente
                            this.SOLICITUD.VB_SubGerente = false; 
                        }
                        else
                        {
                            if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                this.SOLICITUD.VB_Gerente = false; // Si hay gerente
                            }
                            else
                            {

                                this.ShowMessageBox("Debe haber como mínimo 2 superiores asociados a tu área de trabajo para evaluar tu solicitud, por favor contacta al administrador", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
                                this.Close(false);
                            }

                        }

                    }
                    else// Para los empleados que no tienen ningun cargo de supervisión
                    {
                        int contarSuperiores = 0;

                        this.SOLICITUD.Departamento = this.PersonaPorNombreAD.First().Division_AreaItem.Nombre;
                        this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Nombre;


                        //contar superiores
                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Superior_JefeDirecto.Count() != 0)
                        {
                            contarSuperiores = contarSuperiores + 1;
                        }

                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                        {
                            if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                            {
                                contarSuperiores = contarSuperiores + 1;
                                
                            }

                            if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                contarSuperiores = contarSuperiores + 1;
                            }
                            else if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                            {
                                contarSuperiores = contarSuperiores + 1;
                            }
                        }
                                
                        
  
                        

                        //setiar vb's
                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Superior_JefeDirecto.Count() != 0)
                        {
                            this.SOLICITUD.VB_JefeDirecto = false; //si hay jefe de área
                            
                        }
                        else

                            if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem != null)
                            {
                                if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.Count() != 0)
                                {
                                    this.SOLICITUD.VB_SubGerente = false; // Si hay subgerente
                                    
                                }
                            }
                            else

                                if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                {
                                    this.SOLICITUD.VB_Gerente = false; // Si hay gerente
                                    
                                }
                                else

                                    if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Superior_Gerente.Count() != 0)
                                    {
                                        this.SOLICITUD.VB_Gerente = false; // Si hay gerente
                                    }

                        if(contarSuperiores < 2)
                        {
                            // debe tener por lo menos 2 superiores para poder crear una solicitud
                            this.ShowMessageBox("Debe haber como mínimo 2 superiores asociados a tu área de trabajo para evaluar tu solicitud, por favor contacta al administrador", "ACCIÓN DENEGADA", MessageBoxOption.Ok);

                            this.Close(false);
                        }
                        contarSuperiores = 0;
                    }

            //Instanciar el objeto detalle de solicitud dependiendo del caso
            if (TIPOSOLICITUD == 1)
            {
                this.SOLICITUD.Administrativo = true;
                this.SOLICITUD.Titulo = "Día administrativo";
                this.SOLICITUD.SaldoDias = this.PersonaPorNombreAD.First().SaldoDiasAdmins.Value;
            }
            else if (TIPOSOLICITUD == 2)
            {
                this.SOLICITUD.Vacaciones = true;
                this.SOLICITUD.Titulo = "Vacaciones";
            }
            else if (TIPOSOLICITUD == 3)
            {
                this.SOLICITUD.HorasExtras = true;
                this.SOLICITUD.Titulo = "Horas Extras";
                this.SOLICITUD.VB_Empleado = false;
            }
            else if (TIPOSOLICITUD == 4)
            {
                this.SOLICITUD.OtroPermiso = true;
                this.SOLICITUD.Titulo = "Permiso";   
            }


            this.SOLICITUD.Inicio = DateTime.Today;
            this.SOLICITUD.Termino = DateTime.Today;

            if (TIPOSOLICITUD == 3)
            {
                this.SOLICITUD.Termino = null;
            }

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUD;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";
            this.NUEVOESTADO.MensajeBy = this.PersonaPorNombreAD.First().NombreAD;
            this.NUEVOESTADO.CreadoAt = DateTime.Now;
            //this.NUEVOESTADO.NombreCortoEstado = "Siendo procesada";
            
        }

        partial void SOLICITUDES_NUEVA_Activated()
        {
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

        //Quitar acentos del nombre de active directory.
        public static string removerSignosAcentos(String conAcentos)
        {
            int largo = conAcentos.Length;
            char[] NombreAD = new char[largo];
            int i = 0;

            foreach (char ch in conAcentos)
            {
                char val = ch;

                switch (val)
                {
                    case 'á':
                    case 'Á':
                        val = 'A'; break;
                    case 'é':
                    case 'É':
                        val = 'E'; break;
                    case 'í':
                    case 'Í':
                        val = 'I'; break;
                    case 'ó':
                    case 'Ó':
                        val = 'O'; break;
                    case 'ú':
                    case 'Ú':
                        val = 'U'; break;
                }
                NombreAD[i] = val;
                i++;
            }

            string Nombreaux = new string(NombreAD);

            return Nombreaux.ToUpper();
        }

        partial void SOLICITUDES_NUEVA_Saved()
        {
            // Escriba el código aquí.
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
            /*
            if (e.PropertyName.Equals("Termino"))
            {
                TimeSpan diferenciaDias = this.Solicitud_Detalle_Vacaciones.Termino - this.Solicitud_Detalle_Vacaciones.Inicio;
                this.Solicitud_Detalle_Vacaciones.NumeroDias = diferenciaDias.Days;

            }
             */
            if (e.PropertyName.Equals("Inicio"))
            {

                InvocarSaldoVacaciones = true;
            }
        }

        //Se conecta con el stored procedure que consulta el saldo de vacaciones
        partial void ConsultarSaldo_Execute()
        {
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarSaldoVacacionesItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarSaldoVacaciones.AddNew();

            operation.Fecha = this.SOLICITUD.Inicio.Value;
            //operation.Rut = this.PersonaItem.Rut_Persona;
            operation.Rut = "0017511042-9"; //Gustavo
            //operation.Contrato = this.ContratoPorRut.Last().Contrato;
            operation.Contrato = 2063;//Gustavo

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.SOLICITUD.SaldoDias = operation.Saldo;
        }

        //Calcula los días laborales entre dos fechas descontando los feriados.
        public static int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Día de Término debe ser mayor al día de Inicio " + lastDay);

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

        partial void SOLICITUD_Validate(ScreenValidationResultsBuilder results)
        {
     
                if (InvocarSaldoVacaciones == true) { 
                    this.ConsultarSaldo_Execute();
                    InvocarSaldoVacaciones = false;
                }
                

                if (TIPOSOLICITUD == 2)// si la solicitud es del tipo vacaciones
                {
                    this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                    if (this.SOLICITUD.Prestamo.HasValue)
                    {
                        if (this.SOLICITUD.Prestamo.Value < 0 || this.SOLICITUD.Prestamo.Value > 50)
                        {
                            results.AddPropertyError("El prestamo debe ser entre 0 y 50");
                        }
 
                    }
                

                    //GUARDAR LOS REGISTROS DE FERIADOS EN UN ARREGLO

                    DateTime[] FERIADOS = new DateTime[50];//DateTime[] FERIADOS = new DateTime[] { };
                    int dia; int mes; int i = 0;

                    foreach (FeriadosItem feriado in this.Feriados)
                    {

                        dia = feriado.Feriado.Day;
                        mes = feriado.Feriado.Month;
                        DateTime DiaAux = new DateTime(DateTime.Today.Year, mes, dia);//Cambia el año del registro al año actual
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
                        this.SOLICITUD.NumeroDiasTomados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS);
                    }

                    if (this.SOLICITUD.Inicio <= DateTime.Today)
                    {
                        results.AddPropertyError("Día de Inicio debe ser después de hoy");
                    }

                    if (this.SOLICITUD.NumeroDiasTomados > this.SOLICITUD.SaldoDias)
                    {
                        results.AddPropertyError("El número de días a solicitar debe ser menor o igual a tu SALDO DE DÍAS");
                    }



                }
            


            if (TIPOSOLICITUD == 3)// si la solicitud es del tipo horas extras
            {
                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;
                this.SOLICITUD.Colacion = this.COLACION;
                this.SOLICITUD.Taxi = this.TAXI;

                if (this.SOLICITUD.HorasAutorizadas > 2)
                {

                    results.AddPropertyError("El máximo de horas extras a trabajar es 2");

                }
                else { this.SOLICITUD.HorasAutorizadas = this.SOLICITUD.HorasAutorizadas; }

                if (this.SOLICITUD.HorasAutorizadas < 0 || this.SOLICITUD.HorasAutorizadas == null)
                {

                    results.AddPropertyError("Las horas autorizadas deben ser mayor a 0");

                }
                else { this.SOLICITUD.HorasAutorizadas = this.SOLICITUD.HorasAutorizadas; }

                if (this.SOLICITUD.Inicio < DateTime.Today)
                {

                    results.AddPropertyError("La fecha de realización debe ser a partir de hoy");

                }


                //if (COLACION == true) { this.SOLICITUDESItemProperty.Colacion = true; } else { this.SOLICITUDESItemProperty.Colacion = false; }
                //if (TAXI == true) { this.SOLICITUDESItemProperty.Taxi = true; } else { this.SOLICITUDESItemProperty.Taxi = false; }

            }

            if (TIPOSOLICITUD == 4)// si la solicitud es del tipo otro permiso
            {
                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                //GUARDAR LOS REGISTROS DE FERIADOS EN UN ARREGLO

                DateTime[] FERIADOS = new DateTime[50];//DateTime[] FERIADOS = new DateTime[] { };
                int dia; int mes; int i = 0;

                foreach (FeriadosItem feriado in this.Feriados)
                {

                    dia = feriado.Feriado.Day;
                    mes = feriado.Feriado.Month;
                    DateTime DiaAux = new DateTime(DateTime.Today.Year, mes, dia);//Cambia el año del registro al año actual
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

                if (this.SOLICITUD.Inicio <= DateTime.Today)
                {

                    results.AddPropertyError("La fecha de inicio debe ser después de hoy");

                }


                if (this.SOLICITUD.Termino < this.SOLICITUD.Inicio)
                {

                    results.AddPropertyError("La fecha de término debe ser después de la fecha de inicio");

                }else
                    {
                        this.SOLICITUD.NumeroDiasTomados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS);
                    }

            }


            if (TIPOSOLICITUD == 1)// si la solicitud es del tipo dia administrativo
            {
                //GUARDAR LOS REGISTROS DE FERIADOS EN UN ARREGLO

                DateTime[] FERIADOS = new DateTime[50];//DateTime[] FERIADOS = new DateTime[] { };
                int dia; int mes; int i = 0;

                foreach (FeriadosItem feriado in this.Feriados)
                {

                    dia = feriado.Feriado.Day;
                    mes = feriado.Feriado.Month;
                    DateTime DiaAux = new DateTime(DateTime.Today.Year, mes, dia);//Cambia el año del registro al año actual
                    FERIADOS[i] = DiaAux;

                    i++;

                }

                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                this.SOLICITUD.ConDescuento = this.CONDESCUENTO;

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

                if (this.SOLICITUD.Inicio <= DateTime.Today)
                {

                    results.AddPropertyError("La fecha de inicio debe ser después de hoy");

                }

                if (this.SOLICITUD.Termino < this.SOLICITUD.Inicio)
                {

                    results.AddPropertyError("Día de Término debe ser después o igual al día de Inicio ");

                }
                else
                {
                    if (this.AdministrativoDesde == "La tarde (Medio día)" && this.AdministrativoHasta == "La mañana (Medio día)")
                    {
                        this.SOLICITUD.NumeroDiasTomados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS) - 1;
                    }
                    else if (this.AdministrativoDesde == "La mañana (Todo el día)" && this.AdministrativoHasta == "La tarde (Todo el día)")//ok
                    {
                        this.SOLICITUD.NumeroDiasTomados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS);
                    }
                    else
                    {

                        this.SOLICITUD.NumeroDiasTomados = BusinessDaysUntil(this.SOLICITUD.Inicio.Value, this.SOLICITUD.Termino.Value, FERIADOS) - 0.5;
                    }
                }

                if (this.SOLICITUD.NumeroDiasTomados > this.SOLICITUD.SaldoDias)
                {
                    results.AddPropertyError("El número de días a solicitar debe ser menor o igual a tu saldo de días ");
                }

                if (this.SOLICITUD.NumeroDiasTomados <= 0)
                {
                    results.AddPropertyError("El número de días a solicitar debe ser mayor a 0");
                }
            }
        }

        //Para las solicitudes de vacaciones
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
                        results.AddPropertyError("El préstamo a solicitar no puede ser vacio"); //Si la casilla es verdadera, el campo debe tener algún valor
                    }
                }
                catch { }
            }
        }

        //Para las solicitudes de horas extras donde es necesario selecciónar un empleado
        partial void SeleccionarPersonal_Execute()
        {
            this.SOLICITUD.PersonaItem1 = this.PersonalBajoJefeDeArea.SelectedItem;
            this.CloseModalWindow("PersonalArea");
        }

        partial void OBSERVACIONES_Validate(ScreenValidationResultsBuilder results)
        {
            if (TIPOSOLICITUD == 3 || TIPOSOLICITUD == 4)// horas extras u otra solicitud necesitan ser justificadas
            {
                if (this.OBSERVACIONES == null) { results.AddPropertyError("La justificación no puede quedar vacia"); }
            }
        }

        partial void AdministrativoDesde_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.SOLICITUD.AdministrativoDesde = this.AdministrativoDesde;
        }

        partial void AdministrativoHasta_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.SOLICITUD.AdministrativoHasta = this.AdministrativoHasta;
        }

        partial void CerrarPantalla_Execute()
        {
            // Escriba el código aquí.
            this.Close(false);
        }



    }
}