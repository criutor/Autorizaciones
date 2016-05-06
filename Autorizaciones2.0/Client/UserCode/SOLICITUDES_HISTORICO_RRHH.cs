using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Text;
using System.Collections;
using System.Security.Permissions;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using Microsoft.LightSwitch.Application;
using Microsoft.LightSwitch.Threading;
using Microsoft.LightSwitch.Client;


namespace LightSwitchApplication
{
    public partial class SOLICITUDES_HISTORICO_RRHH
    {
        partial void LimpiarFechas_Execute()
        {
            // Escriba el código aquí.
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.SolicitudesConFiltro.Load();
        }

        partial void FiltroEstados_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (FiltroEstados == "Rechazada") {

                this.Completada = false;
                this.Rechazada = true;
                this.Cancelada = false;
                this.Caducada = false;
                this.Rebajada = false; 
            }
            else
                if (FiltroEstados == "Aprobada") {

                    this.Completada = true;
                    this.Rechazada = false;
                    this.Cancelada = false;
                    this.Caducada = false;
                    this.Rebajada = false; 
                }
                else
                    if (FiltroEstados == "En aprobacion") {

                        this.Completada = false;
                        this.Rechazada = false;
                        this.Cancelada = false;
                        this.Caducada = false;
                        this.Rebajada = false; 
                    }
                    else
                        if (FiltroEstados == "Cancelada") {

                            this.Completada = false;
                            this.Rechazada = false;
                            this.Cancelada = true;
                            this.Caducada = false;
                            this.Rebajada = false; 
                        }
                        else
                            if (FiltroEstados == "Anulada") {

                                this.Completada = false;
                                this.Rechazada = false;
                                this.Cancelada = false;
                                this.Caducada = true;
                                this.Rebajada = false; 
                            }
                            else
                                if (FiltroEstados == "Rebajada") {

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

        partial void SOLICITUDES_HISTORICO_RRHH_Activated()
        {
            // Escriba el código aquí.
            this.TodosLosEmpleados_Execute();
            
            this.FiltroEstados = "Todas";
        }

        partial void Gerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                Id_Gerencia = this.Gerencia.Id_Gerencia;
                Id_SubGerencia = -1;
                Id_Area = -1;
                this.SubGerencia = null;
                this.Área = null;
            }
            catch { }
        }

        partial void SubGerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                Id_SubGerencia = this.SubGerencia.Id_SubGerencia;
                Id_Area = -1;
                Id_Gerencia = -1;
                this.Gerencia = null;
                this.Área = null;
            }
            catch { }
        }

        partial void Área_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                Id_Area = this.Área.Id_Area;
                Id_SubGerencia = -1;
                Id_Gerencia = -1;
                this.SubGerencia = null;
                this.Gerencia = null;
            }
            catch { }
        }

        partial void SolicitudesConFiltro_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.NumDeSolicitudes = this.SolicitudesConFiltro.Count();
        }

        partial void LimpiarDivisiones_Execute()
        {
            // Escriba el código aquí.
            this.SubGerencia = null;
            this.Gerencia = null;
            this.Área = null;
            //this.FiltroEstados = "Todas";
            this.TodosLosEmpleados_Execute();
            this.SolicitudesConFiltro.Load();
        }
    }
}
