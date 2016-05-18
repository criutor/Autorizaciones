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
    public partial class CARGOS_ROL_PRIVADO_NUEVO
    {
        partial void CARGOS_ROL_PRIVADO_NUEVO_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.CargoRolPrivadoItemProperty = new CargoRolPrivadoItem();
            this.FindControl("CargoRolPrivadoItemProperty_EsGerente").IsEnabled = false;
            this.FindControl("CargoRolPrivadoItemProperty_EsSubgerente").IsEnabled = false;
            this.FindControl("Gerencia").IsEnabled = false;
            this.FindControl("Subgerencia").IsEnabled = false;
        }

        partial void Área_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                if (this.CargoRolPrivadoItemProperty.EsGerente == false && this.CargoRolPrivadoItemProperty.EsSubgerente == false && this.CargoRolPrivadoItemProperty.EsJefeDeArea == false)
                {
                    this.FindControl("CargoRolPrivadoItemProperty_EsGerente").IsEnabled = true;
                    this.FindControl("CargoRolPrivadoItemProperty_EsSubgerente").IsEnabled = true;
                    this.FindControl("CargoRolPrivadoItemProperty_EsJefeDeArea").IsEnabled = true;
                    this.FindControl("Gerencia").IsEnabled = false;
                    this.FindControl("Subgerencia").IsEnabled = false;
                    this.FindControl("Área").IsEnabled = true;
                    this.Subgerencia = null;//Limpia el campo subgerencia
                    this.Gerencia = null;//Limpia el campo subgerencia
                }

                if (this.Área.Division_SubGerenciaItem == null)
                {
                    this.Subgerencia = null;

                    if (this.Área.Division_GerenciaItem != null)//Muestra a que gerencia pertenece el área
                    {
                        this.Gerencia = this.Área.Division_GerenciaItem;
                    }
                }
                else
                {
                    this.Subgerencia = this.Área.Division_SubGerenciaItem;//Muestra a que subgerencia pertenece el área

                    if (this.Área.Division_SubGerenciaItem.Division_GerenciaItem != null)//Muestra a que gerencia pertenece el área
                    {
                        this.Gerencia = this.Área.Division_SubGerenciaItem.Division_GerenciaItem;
                    }
                }
            }catch { }
        }

        partial void IDAREA_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                this.IDAREA = this.Área.Id_Area;//Actualizar el parámetro de búsqueda
            }
            catch { }
        }

        partial void IDSUBGERENCIA_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                this.IDSUBGERENCIA = this.Subgerencia.Id_SubGerencia;//Actualizar el parámetro de búsqueda
            }
            catch { }
        }

        partial void IDGERENCIA_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                this.IDGERENCIA = this.Gerencia.Id_Gerencia;//Actualizar el parámetro de búsqueda
            }
            catch { }
        }

        partial void CARGOS_ROL_PRIVADO_NUEVO_Saving(ref bool handled)//Guarda los valores seleccionados
        {
            try
            {
                this.CargoRolPrivadoItemProperty.Nombre = this.CargoRolPrivadoItemProperty.Nombre.ToUpper();

                if (this.CargoRolPrivadoItemProperty.EsGerente == true)
                {
                    this.CargoRolPrivadoItemProperty.IDGerencia = this.Gerencia.Id_Gerencia;
                }

                if (this.CargoRolPrivadoItemProperty.EsSubgerente == true)
                {
                    this.CargoRolPrivadoItemProperty.IDSubgerencia = this.Subgerencia.Id_SubGerencia;
                }

                if (this.CargoRolPrivadoItemProperty.EsJefeDeArea == true || (this.CargoRolPrivadoItemProperty.EsJefeDeArea == false && this.CargoRolPrivadoItemProperty.EsSubgerente == false && CargoRolPrivadoItemProperty.EsGerente == false))
                {
                    this.CargoRolPrivadoItemProperty.IDArea = this.Área.Id_Area;
                }
            }
            catch { }
        }

        partial void CARGOS_ROL_PRIVADO_NUEVO_Saved()
        {
            this.Close(false);
        }

        partial void CargoRolPrivadoItemProperty_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                // results.AddPropertyError("<Mensaje de error>");
                if (this.CargoRolPrivadoItemProperty.EsGerente == true)
                {
                    //Desabilita los campos
                    this.FindControl("CargoRolPrivadoItemProperty_EsSubgerente").IsEnabled = false;
                    this.FindControl("CargoRolPrivadoItemProperty_EsJefeDeArea").IsEnabled = false;

                    this.FindControl("Subgerencia").IsEnabled = false;
                    this.FindControl("Área").IsEnabled = false;

                    this.FindControl("Gerencia").IsEnabled = true;

                    this.Área = null;//Limpia el campo área
                    this.Subgerencia = null;//Limpia el campo subgerencia

                    if (this.Gerencia == null)
                    {
                        results.AddPropertyError("Debe escoger una gerencia");
                    }

                    if (this.CargoRolPrivadoGerente.First() != this.CargoRolPrivadoItemProperty)//Error si ya existe otro cargo con la misma asignación de supervisión
                    {
                        if (this.CargoRolPrivadoGerente.Count() != 0)
                        {
                            results.AddPropertyError("Ya existe el cargo ' " + CargoRolPrivadoGerente.First().Nombre + " ' el cual tiene asignado 'es gerente' para ' " + this.Gerencia.Nombre + " '. No puede haber más de un cargo con la asignación de gerente para la misma gerencia.");
                        }
                    }
                }
                else

                    if (this.CargoRolPrivadoItemProperty.EsSubgerente == true)
                    {

                        //Desabilita los campos
                        this.FindControl("CargoRolPrivadoItemProperty_EsGerente").IsEnabled = false;
                        this.FindControl("CargoRolPrivadoItemProperty_EsJefeDeArea").IsEnabled = false;

                        this.FindControl("Gerencia").IsEnabled = false;
                        this.FindControl("Área").IsEnabled = false;

                        this.FindControl("Subgerencia").IsEnabled = true;

                        if (this.Subgerencia == null)
                        {
                            results.AddPropertyError("Debe escoger una subgerencia");
                        }

                        if (this.CargoRolPrivadoSubgerente.First() != this.CargoRolPrivadoItemProperty)//Error si ya existe otro cargo con la misma asignación de supervisión
                        {
                            if (this.CargoRolPrivadoSubgerente.Count() != 0)
                            {
                                results.AddPropertyError("Ya existe el cargo ' " + CargoRolPrivadoSubgerente.First().Nombre + " ' el cual tiene asignado 'es subgerente' para ' " + this.Subgerencia.Nombre + " '. No puede haber más de un cargo con la asignación de subgerente para la misma subgerencia.");
                            }
                        }

                        this.Área = null;//Limpia el campo área
                        this.Gerencia = this.Subgerencia.Division_GerenciaItem;//Muestra a que gerencia pertenece el área
                    }
                    else

                        if (this.CargoRolPrivadoItemProperty.EsJefeDeArea == true)
                        {
                            //Desabilita los campos
                            this.FindControl("CargoRolPrivadoItemProperty_EsGerente").IsEnabled = false;
                            this.FindControl("CargoRolPrivadoItemProperty_EsSubgerente").IsEnabled = false;

                            this.FindControl("Gerencia").IsEnabled = false;
                            this.FindControl("Subgerencia").IsEnabled = false;

                            this.FindControl("Área").IsEnabled = true;

                            if (this.Área == null)
                            {
                                results.AddPropertyError("Debe escoger una área");
                            }

                            if (this.CargoRolPrivadoJefedearea.First() != this.CargoRolPrivadoItemProperty)//Error si ya existe otro cargo con la misma asignación de supervisión
                            {
                                if (this.CargoRolPrivadoJefedearea.Count() != 0)
                                {
                                    results.AddPropertyError("Ya existe el cargo ' " + CargoRolPrivadoJefedearea.First().Nombre + " ' el cual tiene asignado 'es Jefe de área' para ' " + this.Área.Nombre + " '. No puede haber más de un cargo con la asignación de jefe de área para la misma área.");
                                }
                            }
                        }
            }
            catch { }
        }



    }
}