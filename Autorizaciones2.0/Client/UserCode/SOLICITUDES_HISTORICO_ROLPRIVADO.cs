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
    public partial class SOLICITUDES_HISTORICO_ROLPRIVADO
    {
        partial void LimpiarFechas_Execute()
        {
            // Escriba el código aquí.
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.SolicitudesConFiltro.Load();
        }

        partial void TodosLosEmpleados_Execute()
        {
            // Escriba el código aquí.
            this.EmpleadoFiltroSolicitudes = null;
            this.NombreEmpleadoSeleccionado = null;
        }

        partial void SeleccionarEmpleado_Execute()
        {
            // Escriba el código aquí.
            try
            {
                this.EmpleadoFiltroSolicitudes = this.Persona.SelectedItem.Rut_Persona;
                this.NombreEmpleadoSeleccionado = this.Persona.SelectedItem.NombreAD;
            }
            catch { }

            this.CloseModalWindow("Empleados");
        }

        partial void FiltroEstados_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (FiltroEstados == "Rechazada")
            {

                this.Completada = false;
                this.Rechazada = true;
                this.Cancelada = false;
                this.Caducada = false;
                this.Rebajada = false;
            }
            else
                if (FiltroEstados == "Aprobada")
                {

                    this.Completada = true;
                    this.Rechazada = false;
                    this.Cancelada = false;
                    this.Caducada = false;
                    this.Rebajada = false;
                }
                else
                    if (FiltroEstados == "En aprobacion")
                    {

                        this.Completada = false;
                        this.Rechazada = false;
                        this.Cancelada = false;
                        this.Caducada = false;
                        this.Rebajada = false;
                    }
                    else
                        if (FiltroEstados == "Cancelada")
                        {

                            this.Completada = false;
                            this.Rechazada = false;
                            this.Cancelada = true;
                            this.Caducada = false;
                            this.Rebajada = false;
                        }
                        else
                            if (FiltroEstados == "Anulada")
                            {

                                this.Completada = false;
                                this.Rechazada = false;
                                this.Cancelada = false;
                                this.Caducada = true;
                                this.Rebajada = false;
                            }
                            else
                                if (FiltroEstados == "Rebajada")
                                {

                                    this.Completada = false;
                                    this.Rechazada = false;
                                    this.Cancelada = false;
                                    this.Caducada = false;
                                    this.Rebajada = true;
                                }
                                else
                                    if (FiltroEstados == "Todas")
                                    {
                                        //this.Cancelada = null;

                                        //this.FechaSolicitudDesde = null;
                                        //this.FechaSolicitudHasta = null;

                                        this.Administrativo = true;
                                        this.Vacaciones = true;
                                        this.OtroPermiso = true;
                                        this.HorasExtras = true;
                                        this.FiltroEstados = null;

                                        this.Completada = false;
                                        this.Rechazada = false;
                                        this.Cancelada = false;
                                        this.Caducada = null;
                                        this.Rebajada = null;
                                        //this.Solicitud_Header.Load();
                                    }
        }

        partial void SOLICITUDES_HISTORICO_ROLPRIVADO_Activated()
        {
            // Escriba el código aquí.
            this.TodosLosEmpleados_Execute();

            this.FiltroEstados = "Todas";
        }
    }
}
