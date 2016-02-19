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
    public partial class Solicitudes_Crear
    {

        partial void Solicitudes_Crear_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            //Instanciar el objeto solicitud header

            Solicitud_HeaderItem solicitud = new Solicitud_HeaderItem();
            solicitud.FechaSolicitud = DateTime.Today;
            solicitud.NombreTrabajador = PersonaItem.NombreAD;
            solicitud.Fechaingreso = DateTime.Today; // cambiar por la tabla contrato!!!
            solicitud.PersonaItem1 = PersonaItem;
            solicitud.Rechazada = false;
            solicitud.Completada = false;
   
            if (PersonaItem.Es_Gerente == true) { 
                //Agreaga un nombre de departamento
                solicitud.Departamento = " Gerencia de " + PersonaItem.Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;
                //Agrega un nombre de gerencia
                solicitud.Gerencia = PersonaItem.Superior_GerenteQuery.First().Division_GerenciaItem.Nombre;

                solicitud.VB_Gerente = true;

                solicitud.VB_SubGerente = true;

                solicitud.VB_JefeDirecto = true;
            }
            else
                if (PersonaItem.Es_SubGerente == true) {
                    //Agreaga un nombre de departamento
                    solicitud.Departamento = " SubGerencia de " + PersonaItem.Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre;
                    //Agrega un nombre de gerencia
                    solicitud.Gerencia = PersonaItem.Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre;

                    solicitud.VB_Gerente = false;

                    solicitud.VB_SubGerente = true;
          
                    solicitud.VB_JefeDirecto = true;
                }
                else
                    if (PersonaItem.Es_JefeDirecto == true)
                    {
                        solicitud.Departamento = PersonaItem.Division_AreaItem.Nombre;
                        
                        solicitud.Gerencia = PersonaItem.Division_AreaItem.Division_GerenciaItem.Nombre;

                        solicitud.VB_Gerente = false;

                        if (this.PersonaItem.Division_AreaItem.Division_SubGerenciaItem == null) 
                        {
                            solicitud.VB_SubGerente = true; 
                        }
                        else { solicitud.VB_SubGerente = false; }

                        solicitud.VB_JefeDirecto = true;

                    }else// Para los empleados que no tienen ningun cargo de supervisión
                        {   
                            //Agreaga un nombre de departamento
                            solicitud.Departamento = PersonaItem.Division_AreaItem.Nombre;
                            //Agrega un nombre de gerencia
                            solicitud.Gerencia = PersonaItem.Division_AreaItem.Division_GerenciaItem.Nombre;

                            //Declarar las aprobaciones necesarias de los superiores
                            solicitud.VB_JefeDirecto = false; // Indica que necesita aprobacion de jefe directo

                            if (this.PersonaItem.Division_AreaItem.Division_SubGerenciaItem == null) // si no pertenece a una subgerencia, indica que no necesita aprobacion de subgerente, de lo contrario, si la necesita
                            {
                                // indica que no necesita aprobacion de un subgerente
                                solicitud.VB_SubGerente = true; // Utilizamos true en vez de null por que el query de solicitudes pendientes no reconoce el filtro por null(no hay problema en si el usuario tiene otro cargo de supervision ya que en este nivel vbJefeDirecto es = false).
                            }
                            else { solicitud.VB_SubGerente = false; }// indica que si necesita aprobacion de un subgerente

                                solicitud.VB_Gerente = false; // indica que si necesita aprobacion de un gerente

                        }

            //Instanciar el objeto detalle de solicitud dependiendo del caso
            if (TIPOSOLICITUD == 1)
            {

                solicitud.Administrativo = true;
                solicitud.Titulo = "Día administrativo";

                this.Solicitud_Detalle_DiaAdministrativo = new Solicitud_Detalle_AdministrativoItem();
                this.Solicitud_Detalle_DiaAdministrativo.Solicitud_HeaderItem = solicitud;
                this.Solicitud_Detalle_DiaAdministrativo.SaldoDias = this.PersonaItem.SaldoDiasAdmins.Value;

                this.Estados_Solicitud_DiaAdministrativo = new Solicitud_Estados_AdministrativoItem();
                this.Estados_Solicitud_DiaAdministrativo.Solicitud_Detalle_AdministrativoItem = Solicitud_Detalle_DiaAdministrativo;
                this.Estados_Solicitud_DiaAdministrativo.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";

            }
            else if (TIPOSOLICITUD == 2)
            {

                solicitud.Vacaciones = true;
                solicitud.Titulo = "Vacaciones";

                this.Solicitud_Detalle_Vacaciones = new Solicitud_Detalle_VacacionesItem();
                this.Solicitud_Detalle_Vacaciones.Solicitud_HeaderItem = solicitud;

                this.Estados_Solicitud_Vacaciones = new Solicitud_Estados_VacacionesItem();
                this.Estados_Solicitud_Vacaciones.Solicitud_Detalle_VacacionesItem = Solicitud_Detalle_Vacaciones;
                this.Estados_Solicitud_Vacaciones.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";
                this.Estados_Solicitud_Vacaciones.MensajeBy = this.Application.User.FullName;

            }
            else if (TIPOSOLICITUD == 3)
            {

                solicitud.HorasExtras = true;
                solicitud.Titulo = "Horas Extras";

                this.Solicitud_Detalles_HorasExtras = new Solicitud_Detalle_HorasExtrasItem();
                this.Solicitud_Detalles_HorasExtras.Solicitud_HeaderItem = solicitud;

                this.Estados_Solicitud_HorasExtras = new Solicitud_Estados_HorasExtrasItem();
                this.Estados_Solicitud_HorasExtras.Solicitud_Detalle_HorasExtrasItem = Solicitud_Detalles_HorasExtras;
                this.Estados_Solicitud_HorasExtras.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";

            }
            else if (TIPOSOLICITUD == 4)
            {

                solicitud.OtroPermiso = true;
                solicitud.Titulo = "Permiso";

                this.Solicitud_Detalle_OtroPermiso = new Solicitud_Detalle_OtroPermisoItem();
                this.Solicitud_Detalle_OtroPermiso.Solicitud_HeaderItem = solicitud;


                this.Estados_Solicitud_OtroPermiso = new Solicitud_Estados_OtroPermisoItem();
                this.Estados_Solicitud_OtroPermiso.Solicitud_Detalle_OtroPermisoItem = Solicitud_Detalle_OtroPermiso;
                this.Estados_Solicitud_OtroPermiso.TituloObservacion = "LA SOLICITUD HA SIDO CREADA POR:";

            }

        }

        partial void Solicitudes_Crear_Activated()
        {
            //Activar los controles dependiendo del tipo de solicitud
            if (TIPOSOLICITUD == 1)
            {

                this.FindControl("Solicitud_Detalle_DiaAdministrativo").IsVisible = true;
                this.FindControl("Estados_Solicitud_DiaAdministrativo").IsVisible = true;
            }
            else

                if (TIPOSOLICITUD == 2)
                {

                    this.FindControl("Solicitud_Detalle_Vacaciones").IsVisible = true;
                    this.FindControl("Estados_Solicitud_Vacaciones").IsVisible = true;

                }
                else

                    if (TIPOSOLICITUD == 3)
                    {

                        this.FindControl("Solicitud_Detalles_HorasExtras").IsVisible = true;
                        this.FindControl("Estados_Solicitud_HorasExtras").IsVisible = true;
                    }
                    else

                        if (TIPOSOLICITUD == 4)
                        {

                            this.FindControl("Solicitud_Detalle_OtroPermiso").IsVisible = true;
                            this.FindControl("Estados_Solicitud_OtroPermiso").IsVisible = true;
                        }
        }

        
        partial void Solicitudes_Crear_Created()
        {
            
            // Detecta si se ha hecho algún cambio en la pantalla
            Dispatchers.Main.BeginInvoke(() =>
            {
                ((INotifyPropertyChanged)this.Solicitud_Detalle_Vacaciones).PropertyChanged +=
                    new PropertyChangedEventHandler(CrearNuevaSolicitud_PropertyChanged);

            });
            
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
        

        partial void Solicitudes_Crear_Saved()
        {

            this.Close(true);
 
        }

        partial void ConsultarSaldo_Execute()//Se ejecuta en la solicitude de vacaciones
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

            operation.Fecha = this.Solicitud_Detalle_Vacaciones.Inicio;
            operation.Rut = "0017511042-9";
            operation.Contrato = 2063;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.Solicitud_Detalle_Vacaciones.SALDO = operation.Saldo;

            

        }

        partial void Solicitud_Detalle_Vacaciones_Validate(ScreenValidationResultsBuilder results)
        {

            if (InvocarSaldoVacaciones == true) { this.ConsultarSaldo_Execute(); }
            InvocarSaldoVacaciones = false;
        }


    }
}