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

namespace LightSwitchApplication
{
    public partial class SOLICITUDES_NUEVA
    {
        partial void SOLICITUDES_NUEVA_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Parametro de busqueda de la persona
            //NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper();
            NOMBREAD = "RUBIO FLORES, GUSTAVO";
            // Guarda el rut del usuario
            RUTUSUARIO = this.PersonaPorNombreAD.First().Rut_Persona;
            //Instanciar el objeto solicitud 
            this.SOLICITUD = new SOLICITUDESItem();
            this.SOLICITUD.FechaSolicitud = DateTime.Today;

            //Si la solicitude es de horas extras, la persona es null hasta que se escoja una de la lista
            if (TIPOSOLICITUD == 3)
            { this.SOLICITUD.PersonaItem1 = null; }
            else { 
                this.SOLICITUD.PersonaItem1 = this.PersonaPorNombreAD.First(); 
            }
            
            this.SOLICITUD.Rechazada = false;
            this.SOLICITUD.Completada = false;
            this.SOLICITUD.Cancelada = false;
            this.SOLICITUD.Estado = "Siendo procesada";
            //Por defecto el empleado acepta las solicitudes que el mismo crea, las horas extras necesitan aprobación
            this.SOLICITUD.VB_Empleado = true;

            if (this.PersonaPorNombreAD.First().Es_Gerente == true)
            {
                //this.IDGERENCIA = this.PersonaPorNombreAD.First().Superior_Gerente.First().Division_GerenciaItem.Id_Gerencia;

                //Agreaga un nombre de departamento
                this.SOLICITUD.Departamento = " Gerencia de " + this.PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                //Agrega un nombre de gerencia
                this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;

                this.SOLICITUD.VB_Gerente = true;

                this.SOLICITUD.VB_SubGerente = true;

                this.SOLICITUD.VB_JefeDirecto = true;

                this.SOLICITUD.Completada = true;

            }
            else
                if (this.PersonaPorNombreAD.First().Es_SubGerente == true)
                {

                    //this.IDSUBGERENCIA = this.PersonaPorNombreAD.First().Superior_SubGerente.First().Division_SubGerenciaItem.Id_SubGerencia;
                    //Agreaga un nombre de departamento
                    this.SOLICITUD.Departamento = " SubGerencia de " + this.PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre;
                    //Agrega un nombre de gerencia
                    this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Division_GerenciaItem.Nombre;

                    this.SOLICITUD.VB_Gerente = false;

                    this.SOLICITUD.VB_SubGerente = true;

                    this.SOLICITUD.VB_JefeDirecto = true;
                }
                else
                    if (this.PersonaPorNombreAD.First().Es_JefeDirecto == true)
                    {
                        this.IDAREA = this.PersonaPorNombreAD.First().Superior_JefeDirecto.First().Division_AreaItem.Id_Area;

                        this.SOLICITUD.Departamento = this.PersonaPorNombreAD.First().Division_AreaItem.Nombre;

                        this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Nombre;

                        this.SOLICITUD.VB_Gerente = false;
                           
                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem == null)
                        {
                            //this.SOLICITUD.VB_SubGerente = true;
                            this.SOLICITUD.VB_SubGerente = null;
                        }
                        else { this.SOLICITUD.VB_SubGerente = false; }

                        this.SOLICITUD.VB_JefeDirecto = true;

                    }
                    else// Para los empleados que no tienen ningun cargo de supervisión
                    {
                        //Agreaga un nombre de departamento
                        this.SOLICITUD.Departamento = this.PersonaPorNombreAD.First().Division_AreaItem.Nombre;
                        //Agrega un nombre de gerencia
                        this.SOLICITUD.Gerencia = this.PersonaPorNombreAD.First().Division_AreaItem.Division_GerenciaItem.Nombre;

                        //Declarar las aprobaciones necesarias de los superiores
                        this.SOLICITUD.VB_JefeDirecto = false; // Indica que necesita aprobacion de jefe directo

                        if (this.PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem == null) // si no pertenece a una subgerencia, indica que no necesita aprobacion de subgerente, de lo contrario, si la necesita
                        {
                            // indica que no necesita aprobacion de un subgerente
                            //this.SOLICITUD.VB_SubGerente = true; // Utilizamos true en vez de null por que el query de solicitudes pendientes no reconoce el filtro por null(no hay problema en si el usuario tiene otro cargo de supervision ya que en este nivel vbJefeDirecto es = false).
                            this.SOLICITUD.VB_SubGerente = null;
                        }
                        else { this.SOLICITUD.VB_SubGerente = false; }// indica que si necesita aprobacion de un subgerente

                        //this.SOLICITUD.VB_Gerente = true;//Gerente solo aprueba solicitudes de horas extras
                        this.SOLICITUD.VB_Gerente = null;

                    }

            //Instanciar el objeto detalle de solicitud dependiendo del caso
            if (TIPOSOLICITUD == 1)
            {
                this.SOLICITUD.Administrativo = true;
                this.SOLICITUD.Titulo = "Día administrativo";
                this.SOLICITUD.SaldoDias = this.PersonaPorNombreAD.First().SaldoDiasAdmins.Value;
                this.SOLICITUD.Inicio = DateTime.Today;
                this.SOLICITUD.Termino = DateTime.Today;

            }
            else if (TIPOSOLICITUD == 2)
            {
                this.SOLICITUD.Vacaciones = true;
                this.SOLICITUD.Titulo = "Vacaciones";
                this.SOLICITUD.Inicio = DateTime.Today;
                this.SOLICITUD.Termino = DateTime.Today;

            }
            else if (TIPOSOLICITUD == 3)
            {
                this.SOLICITUD.Inicio = DateTime.Today;
                this.SOLICITUD.HorasExtras = true;
                this.SOLICITUD.Titulo = "Horas Extras";
                this.SOLICITUD.VB_Empleado = false;
                //Gerente siempre debe aprobar solicitudes de horas extras
                this.SOLICITUD.VB_Gerente = false;
                //El usuario no puede solicitar horas extras para si mismmo, debe escoger un nuevo empleado de una lista
                //this.SOLICITUDESItemProperty.PersonaItem1 = null;


            }
            else if (TIPOSOLICITUD == 4)
            {
                this.SOLICITUD.OtroPermiso = true;
                this.SOLICITUD.Titulo = "Permiso";
                this.SOLICITUD.Inicio = DateTime.Today;
                this.SOLICITUD.Termino = DateTime.Today;
            }

            this.NUEVOESTADO = new ESTADOSItem();
            this.NUEVOESTADO.SOLICITUDESItem = this.SOLICITUD;
            this.NUEVOESTADO.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";
            this.NUEVOESTADO.MensajeBy = this.PersonaPorNombreAD.First().NombreAD;
            this.NUEVOESTADO.CreadoAt = DateTime.Now;
            
        }

        partial void SOLICITUDES_NUEVA_Activated()
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

        partial void ConsultarSaldo_Execute()//Se ejecuta en la solicitud de vacaciones
        {
                        /*
            this.PersonaItem.FechaInicioVacaciones = this.Solicitud_Detalle_Vacaciones.Inicio;
            this.Solicitud_Detalle_Vacaciones.ConsultarSaldo = true;
            this.Save();
            No se puede aplicar por que no se puede calcular la diferencia de dias sin saber los feriados 
            */

            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarSaldoVacacionesItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarSaldoVacaciones.AddNew();

            operation.Fecha = this.SOLICITUD.Inicio.Value;
            //operation.Rut = this.PersonaItem.Rut_Persona;
            operation.Rut = "0017511042-9";
            //operation.Contrato = this.ContratoPorRut.Last().Contrato;
            operation.Contrato = 2063;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.SOLICITUD.SaldoDias = operation.Saldo;

        }

        //Calcula los dias laborales entre vacaciones
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
            // results.AddPropertyError("<Mensaje de error>");
            
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
                this.NUEVOESTADO.Observaciones = this.OBSERVACIONES;

                this.SOLICITUD.NumeroDiasTomados = this.DiasAdministrativosSolicitados;
                this.SOLICITUD.ConDescuento = this.CONDESCUENTO;

                if (this.SOLICITUD.Inicio <= DateTime.Today)
                {

                    results.AddPropertyError("La fecha de inicio debe ser después de hoy");

                }


                if (this.SOLICITUD.Termino < this.SOLICITUD.Inicio)
                {

                    results.AddPropertyError("Día de Término debe ser después o igual al día de Inicio ");

                }

                if (this.SOLICITUD.NumeroDiasTomados > this.SOLICITUD.SaldoDias)
                {

                    results.AddPropertyError("El número de días a solicitar debe ser menor o igual a tu SALDO DE DÍAS ");

                }

            }

        }

        partial void RequierePrestamo_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");

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

        partial void SeleccionarPersonal_Execute()
        {
            // Escriba el código aquí.

            this.SOLICITUD.PersonaItem1 = this.PersonalBajoJefeDeArea.SelectedItem;
            this.CloseModalWindow("PersonalArea");

        }

        partial void OBSERVACIONES_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");

            if (TIPOSOLICITUD == 3 || TIPOSOLICITUD == 4)// horas extras u otra solicitud
            {
                if (this.OBSERVACIONES == null) { results.AddPropertyError("La justificación no puede quedar vacia"); }
            }

        }


    }
}