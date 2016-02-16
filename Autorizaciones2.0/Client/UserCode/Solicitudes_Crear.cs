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

            Solicitud_HeaderItem solicitud = new Solicitud_HeaderItem();
            solicitud.FechaSolicitud = DateTime.Today;
            solicitud.NombreTrabajador = PersonaItem.NombreAD;
            solicitud.Fechaingreso = DateTime.Today; // cambiar por la tabla contrato!!!
            solicitud.PersonaItem1 = PersonaItem;
            
            solicitud.Rechazada = false;
            solicitud.Completada = false;
            //solicitud.Administrativo = false;
            //solicitud.OtroPermiso = false;
            //solicitud.Vacaciones = false;
            //solicitud.HorasExtras = false;
            
            if (PersonaItem.Es_Gerente == true) { solicitud.Departamento = " Gerencia de " + PersonaItem.Superior_GerenteQuery.First().Division_GerenciaItem.Nombre; solicitud.Gerencia = PersonaItem.Superior_GerenteQuery.First().Division_GerenciaItem.Nombre; }
            else
                if (PersonaItem.Es_SubGerente == true) { solicitud.Departamento = " SubGerencia de " + PersonaItem.Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre; solicitud.Gerencia = PersonaItem.Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre; }
                else
                {
                    solicitud.Departamento = PersonaItem.Division_AreaItem.Nombre;
                    solicitud.Gerencia = PersonaItem.Division_AreaItem.Division_GerenciaItem.Nombre;
                }



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

        /*
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
            if (e.PropertyName.Equals("Termino"))
            {
                TimeSpan diferenciaDias = this.Solicitud_Detalle_Vacaciones.Termino - this.Solicitud_Detalle_Vacaciones.Inicio;
                this.Solicitud_Detalle_Vacaciones.NumeroDias = diferenciaDias.Days;

            }
            if (e.PropertyName.Equals("Inicio"))
            {
                TimeSpan diferenciaDias = this.Solicitud_Detalle_Vacaciones.Termino - this.Solicitud_Detalle_Vacaciones.Inicio;
                this.Solicitud_Detalle_Vacaciones.NumeroDias = diferenciaDias.Days;

            }
        }*/
        

        partial void Solicitudes_Crear_Saved()
        {

            this.Close(true);
 
        }

        partial void ConsultarSaldo_Execute()
        {
                        /*
            this.PersonaItem.FechaInicioVacaciones = this.Solicitud_Detalle_Vacaciones.Inicio;
            this.Solicitud_Detalle_Vacaciones.ConsultarSaldo = true;
            this.Save();
            */

            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarSaldoVacacionesItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarSaldoVacaciones.AddNew();

            operation.Fecha = this.Solicitud_Detalle_Vacaciones.Inicio;
            operation.Rut = "0017511042-9";
            operation.Contrato = 2063;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.Solicitud_Detalle_Vacaciones.SALDO = operation.Saldo;

            this.CloseModalWindow("SeleccionarDiaInicio");

        }

    }
}