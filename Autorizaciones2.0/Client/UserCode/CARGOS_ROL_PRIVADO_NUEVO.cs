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
        }

        partial void Área_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                if (this.Es_Gerente == false && this.Es_Subgerente == false && this.Es_JefeDeÁrea == false )
                {
                    this.FindControl("Es_Gerente").IsEnabled = true;
                    this.FindControl("Es_Subgerente").IsEnabled = true;
                    this.FindControl("Es_JefeDeÁrea").IsEnabled = true;
                    this.FindControl("Área").IsEnabled = true;
                }

               

                if (this.Área.Division_SubGerenciaItem == null)
                {
                    this.Subgerencia = null;
                }
                else
                {
                    this.Subgerencia = this.Área.Division_SubGerenciaItem;//Muestra a que subgerencia pertenece el área
                }

                if (this.Área.Division_GerenciaItem != null)//Muestra a que gerencia pertenece el área
                {
                    this.Gerencia = this.Área.Division_GerenciaItem;
                }

                if (this.Área.Division_SubGerenciaItem.Division_GerenciaItem != null)//Muestra a que gerencia pertenece el área
                {
                    this.Gerencia = this.Área.Division_SubGerenciaItem.Division_GerenciaItem;
                }
            }

            catch { }
        }

        partial void Es_Gerente_Validate(ScreenValidationResultsBuilder results)
        {
            if (this.Es_Gerente == true)
            {
                try
                {
                    if (this.Gerencia == null)
                    {
                        results.AddPropertyError("Debe escoger una gerencia");
                    }

                    if (this.CargoRolPrivadoGerente.First() != this.CargoRolPrivadoItemProperty)//Error si ya existe otro cargo con la misma asignación de supervisión
                    {
                        if (this.CargoRolPrivadoGerente.Count() != 0)
                        {
                            results.AddPropertyError("El cargo de ' " + CargoRolPrivadoGerente.First().Nombre + " ' ya tiene asignado el gerente para ' " + this.Gerencia.Nombre + " '. No puede haber más de un cargo con la asignación de gerente para la misma gerencia.");
                        }
                    }

                    //Desabilita los campos
                    this.FindControl("Es_Subgerente").IsEnabled = false;
                    this.FindControl("Es_JefeDeÁrea").IsEnabled = false;

                    this.FindControl("Subgerencia").IsEnabled = false;
                    this.FindControl("Área").IsEnabled = false;

                    this.FindControl("Gerencia").IsEnabled = true;

                    this.Área = null;//Limpia el campo área
                    this.Subgerencia = null;//Limpia el campo subgerencia

                }
                catch { }
            }
            
            else
            
            if (this.Es_Gerente == false)
            {
                if (this.Es_Gerente == false && this.Es_Subgerente == false && this.Es_JefeDeÁrea == false && this.Área == null)
                {
                    results.AddPropertyError("Si este cargo no es para un Gerente, Subgerente o Jefe de área, entonces debe escoger una área");
                }

                try
                {
                    this.FindControl("Gerencia").IsEnabled = false;
                    this.Gerencia = null;
                }
                catch { }
            }
            
        }

        partial void Es_Subgerente_Validate(ScreenValidationResultsBuilder results)
        {
            if (this.Es_Subgerente == true)
            {
                try
                {
                    if (this.Subgerencia == null)
                    {
                        results.AddPropertyError("Debe escoger una subgerencia");
                    }

                    if (this.CargoRolPrivadoSubgerente.First() != this.CargoRolPrivadoItemProperty)//Error si ya existe otro cargo con la misma asignación de supervisión
                    {
                        if (this.CargoRolPrivadoSubgerente.Count() != 0)
                        {
                            results.AddPropertyError("El cargo de ' " + CargoRolPrivadoSubgerente.First().Nombre + " ' ya tiene asignado el subgerente para ' " + this.Subgerencia.Nombre + " '. No puede haber más de un cargo con la asignación de subgerente para la misma subgerencia.");
                        }
                    }

                    //Desabilita los campos
                    this.FindControl("Es_Gerente").IsEnabled = false;
                    this.FindControl("Es_JefeDeÁrea").IsEnabled = false;

                    this.FindControl("Gerencia").IsEnabled = false;
                    this.FindControl("Área").IsEnabled = false;

                    this.FindControl("Subgerencia").IsEnabled = true;

                    this.Área = null;//Limpia el campo área
                    this.Gerencia = this.Subgerencia.Division_GerenciaItem;//Muestra a que gerencia pertenece el área

                }
                catch { }
            }

            
            else

            if (this.Es_Subgerente == false)
            {
                 try
                {
                    this.FindControl("Subgerencia").IsEnabled = false;
                    this.Subgerencia = null;//Limpia el campo subgerencia
                    this.Gerencia = null;//Limpia el campo gerencia
                }
                catch { }
            }
            
        }

        partial void Es_JefeDeÁrea_Validate(ScreenValidationResultsBuilder results)
        {
            if (this.Es_JefeDeÁrea == true)
            {
                try
                {
                    if (this.Área == null)
                    {
                        results.AddPropertyError("Debe escoger una área");
                    }

                    if (this.CargoRolPrivadoJefedearea.First() != this.CargoRolPrivadoItemProperty)//Error si ya existe otro cargo con la misma asignación de supervisión
                    {
                        if (this.CargoRolPrivadoJefedearea.Count() != 0)
                        {
                            results.AddPropertyError("El cargo de ' " + CargoRolPrivadoJefedearea.First().Nombre + " ' ya tiene asignado el Jefe de área para ' " + this.Área.Nombre + " '. No puede haber más de un cargo con la asignación de jefe de área para la misma área.");
                        }
                    }

                    //Desabilita los campos
                    this.FindControl("Es_Gerente").IsEnabled = false;
                    this.FindControl("Es_Subgerente").IsEnabled = false;

                    this.FindControl("Gerencia").IsEnabled = false;
                    this.FindControl("Subgerencia").IsEnabled = false;

                    this.FindControl("Área").IsEnabled = true;
                }
                catch { }
            }else

            if (this.Es_JefeDeÁrea == false)
            {
                this.FindControl("Área").IsEnabled = true;

                try
                {
                    this.FindControl("Es_Gerente").IsEnabled = true;
                    this.FindControl("Es_Subgerente").IsEnabled = true;
                }
                catch { }
            }
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
                this.CargoRolPrivadoItemProperty.EsGerente = this.Es_Gerente;
                this.CargoRolPrivadoItemProperty.EsJefeDeArea = this.Es_JefeDeÁrea;
                this.CargoRolPrivadoItemProperty.EsSubgerente = this.Es_Subgerente;

                if (this.Es_Gerente == true)
                {
                    this.CargoRolPrivadoItemProperty.IDGerencia = this.Gerencia.Id_Gerencia;
                }

                if (this.Es_Subgerente == true)
                {
                    this.CargoRolPrivadoItemProperty.IDSubgerencia = this.Subgerencia.Id_SubGerencia;
                }

                if (this.Es_JefeDeÁrea == true || (this.Es_JefeDeÁrea == false && this.Es_Subgerente == false && this.Es_Gerente == false))
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

    }
}