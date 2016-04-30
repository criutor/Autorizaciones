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
    public partial class PERSONAL_DETALLES
    {
        partial void PERSONAL_DETALLES_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.Persona);

            if (this.CerrarPantallaSWITCH == true)
            {
                this.Close(true);
            }
            else
            {
                this.CerrarPantallaSWITCH = true;
            }
        }

        partial void PERSONAL_DETALLES_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.CerrarPantallaSWITCH = true;

            if (this.Persona.EsRolPrivado != true)
            {
                this.ES_JEFE_DE_AREA = this.Persona.Es_JefeDirecto;
            }
        }

        partial void CerrarPantalla_Execute()
        {
            this.Close(false);
        }

        partial void PERSONAL_DETALLES_Saving(ref bool handled)
        {
            this.Persona.Nombres = this.Persona.Nombres.ToUpper();
            this.Persona.AP_Materno = this.Persona.AP_Materno.ToUpper();
            this.Persona.AP_Paterno = this.Persona.AP_Paterno.ToUpper();

            if (this.Persona.EsRolPrivado != true )
            {
                //Si no se selecciona como jefe de área 
                if (this.Persona.Es_JefeDirecto != true )
                {
                    if (this.Persona.Division_AreaItem == null)
                    {
                        this.Persona.AreaDeTrabajo = null;
                    }
                    else { this.Persona.AreaDeTrabajo = this.Persona.Division_AreaItem.Nombre; }
                    

                    //Si era jefe de área se elimina el cargo
                    if (this.Persona.Superior_JefeDirecto.Count() > 0)
                    {
                            this.Persona.Es_JefeDirecto = false;

                            this.Persona.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;

                            this.Persona.Superior_JefeDirecto.First().Delete();
                    }
                }
                else
                //Si se selecciona como jefe de área 
                if (this.Persona.Es_JefeDirecto == true )
                {
                    this.Persona.AreaDeTrabajo = this.Persona.Division_AreaItem.Nombre;
                    //Si antes era jefe de área
                    if (this.Persona.Superior_JefeDirecto.Count() > 0 )
                    {
                        //Si el área es distinta
                        if (this.Persona.Superior_JefeDirecto.First().Division_AreaItem != this.Persona.Division_AreaItem)
                        {
                            this.Persona.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;

                            this.Persona.Superior_JefeDirecto.First().Division_AreaItem = this.Persona.Division_AreaItem;

                            this.Persona.Division_AreaItem.JefeDeArea = this.Persona.NombreAD;

                            this.Persona.Es_JefeDirecto = true;
                        }
                    }

                    //Si antes no era jefe de área
                    if (this.Persona.Superior_JefeDirecto.Count() == 0)
                    {
                        Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();

                        JefeDirecto.PersonaItem1 = this.Persona;

                        JefeDirecto.Division_AreaItem = this.Persona.Division_AreaItem;

                        this.Division_AreaItem.JefeDeArea = this.Persona.NombreAD;

                        this.Persona.Es_JefeDirecto = true;
                    }
                }
            }

            if (this.Persona.EsRolPrivado == true)
            {
                //Si el nuevo cargo no incluye ser gerente o si se borra el cargo de rol privado
                if (this.Persona.CargoRolPrivadoItem == null || this.Persona.CargoRolPrivadoItem.EsGerente != true) 
                {
                    //si era gerente se elimina el cargo de gerente
                    if (this.Persona.Es_Gerente == true && this.Persona.Superior_Gerente.Count() != 0)
                    {
                        this.Persona.Es_Gerente = false;
                        this.Persona.Es_GerenteGeneral = false;
                        this.Persona.Cargo = null;
                        this.Persona.IDGerencia = null;
                        this.Persona.AreaDeTrabajo = null;
                        this.Persona.Superior_Gerente.First().Division_GerenciaItem.Gerente = null;
                        this.Persona.Superior_Gerente.First().Delete();
                    }
                }
                //Si el nuevo cargo incluye ser gerente
                else if (this.Persona.CargoRolPrivadoItem.EsGerente == true)
                {
                    //si era gerente se actualiza el cargo de gerente
                    if (this.Persona.Es_Gerente == true && this.Persona.Superior_Gerente.Count() != 0)
                    {
                        this.Persona.Superior_Gerente.First().Division_GerenciaItem.Gerente = null;
                        this.Persona.Superior_Gerente.First().Division_GerenciaItem = this.Division_GerenciaItem;
                        this.Persona.Superior_Gerente.First().Division_GerenciaItem.Gerente = this.Persona.NombreAD;
                        this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;
                        this.Persona.IDGerencia = this.Division_GerenciaItem.Id_Gerencia;
                    }
                    //Si no es gerente crea un nuevo cargo de gerente
                    else //if (this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1 != this.Persona)
                    {
                        this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;

                        Superior_GerenteItem gerente = new Superior_GerenteItem();
                        gerente.PersonaItem1 = this.Persona;
                        gerente.Division_GerenciaItem = this.Division_GerenciaItem;
                        this.Persona.Es_Gerente = true;
                        this.Persona.Division_AreaItem = null;
                        this.Persona.EsRolPrivado = true;
                        this.Persona.AreaDeTrabajo = this.Division_GerenciaItem.Nombre;
                        this.Division_GerenciaItem.Gerente = this.Persona.NombreAD;
                        this.Persona.IDGerencia = this.Division_GerenciaItem.Id_Gerencia;
                        if (this.Division_GerenciaItem.Nombre == "GERENCIA GENERAL") { this.Persona.Es_GerenteGeneral = true; }
                    }
                }

                //Si el nuevo cargo no incluye ser subgerente
                if (this.Persona.CargoRolPrivadoItem == null || this.Persona.CargoRolPrivadoItem.EsSubgerente != true)
                {
                    //si era subgerente se elimina el cargo de subgerente
                    if (this.Persona.Es_SubGerente == true && this.Persona.Superior_SubGerente.Count() != 0)
                    {
                        this.Persona.Es_SubGerente = false;
                        this.Persona.Cargo = null;
                        this.Persona.IDGerencia_para_subgerentes = null;
                        this.Persona.AreaDeTrabajo = null;
                        this.Persona.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = null;
                        this.Persona.IDSubgerencia = null;
                        this.Persona.Superior_SubGerente.First().Delete();
                    }
                }
                //Si el nuevo cargo incluye ser subgerente
                else if (this.Persona.CargoRolPrivadoItem.EsSubgerente == true)
                {
                    //si era subgerente se actualiza el cargo de subgerente
                    if (this.Persona.Es_SubGerente == true && this.Persona.Superior_SubGerente.Count() != 0)
                    {
                        this.Persona.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = null;
                        this.Persona.Superior_SubGerente.First().Division_SubGerenciaItem = this.Division_SubGerenciaItem;
                        this.Persona.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = this.Persona.NombreAD;
                        this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;
                        this.Persona.IDSubgerencia = this.Division_SubGerenciaItem.Id_SubGerencia;
                    }
                    //Si no era subgerente crea un nuevo cargo de subgerente
                    else //if (this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1 != this.Persona)
                    {
                        this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;

                        Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                        Subgerente.PersonaItem1 = this.Persona;
                        Subgerente.Division_SubGerenciaItem = this.Division_SubGerenciaItem;
                        this.Persona.Es_SubGerente = true;
                        this.Persona.EsRolPrivado = true;
                        this.Persona.IDGerencia_para_subgerentes = this.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;
                        //IDGerencia_para_subgerentes es utilizado en el Query de Solicitud_Header en la pantalla "Master_SolicitudesPendientes",...
                        //...ya que los subgerentes no pertenecen a ninguna area, si no se guardara el id de la subgerencia, ...
                        //...los gerentes verian las solicitudes de todos los subgerentes y no solo los que les corresponden.
                        this.Persona.Division_AreaItem = null;
                        this.Persona.AreaDeTrabajo = this.Division_SubGerenciaItem.Nombre;
                        this.Division_SubGerenciaItem.SubGerente = this.Persona.NombreAD;
                        this.Persona.IDSubgerencia = this.Division_SubGerenciaItem.Id_SubGerencia;
                    }
                }

                //Si el nuevo cargo no incluye ser jefe de área
                if (this.Persona.CargoRolPrivadoItem == null || this.Persona.CargoRolPrivadoItem.EsJefeDeArea != true)
                {
                    //si era jefe de area se elimina el cargo de jefe de area
                    if (this.Persona.Es_JefeDirecto == true && this.Persona.Superior_JefeDirecto.Count() != 0)
                    {
                        this.Persona.Es_JefeDirecto = false;
                        this.Persona.Cargo = null;
                        this.Persona.AreaDeTrabajo = null;
                        this.Persona.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;
                        this.Persona.Superior_JefeDirecto.First().Delete();
                    }
                }
                //Si el nuevo cargo incluye ser jefe de area
                else if (this.Persona.CargoRolPrivadoItem.EsJefeDeArea == true)
                {
                    //si era jefe de area se actualiza el cargo de jefe de area
                    if (this.Persona.Es_JefeDirecto == true && this.Persona.Superior_JefeDirecto.Count() != 0)
                    {
                        this.Persona.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;
                        this.Persona.Superior_JefeDirecto.First().Division_AreaItem = this.Division_AreaItem;
                        this.Persona.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = this.Persona.NombreAD;
                        this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;
                    }
                    //Si no era jefe de area, crea un nuevo cargo de jefe de area
                    else //if (this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1 != this.Persona)
                    {
                        this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;

                        Superior_JefeDirectoItem JefeDeArea = new Superior_JefeDirectoItem();
                        JefeDeArea.PersonaItem1 = this.Persona;
                        JefeDeArea.Division_AreaItem = this.Division_AreaItem;
                        this.Persona.Es_JefeDirecto = true;
                        this.Persona.EsRolPrivado = true;
                        this.Persona.Division_AreaItem = this.Division_AreaItem;
                        this.Persona.AreaDeTrabajo = this.Division_AreaItem.Nombre;
                        this.Division_AreaItem.JefeDeArea = this.Persona.NombreAD;
                    }
                }

                //Si el nuevo cargo no incluye ser jefe de área ni subgerente ni gerente.
                if (this.Persona.CargoRolPrivadoItem != null && this.Persona.CargoRolPrivadoItem.EsJefeDeArea != true && this.Persona.CargoRolPrivadoItem.EsSubgerente != true && this.Persona.CargoRolPrivadoItem.EsGerente != true)
                {
                    this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;
                    this.Persona.Division_AreaItem = this.Division_AreaItem;
                    this.Persona.AreaDeTrabajo = this.Division_AreaItem.Nombre;
                }
            }
        }

        partial void PERSONAL_DETALLES_Activated()
        {
            this.FindControl("ES_JEFE_DE_AREA").IsEnabled = false;
            this.FindControl("ES_SUBGERENTE").IsEnabled = false;
            this.FindControl("ES_GERENTE").IsEnabled = false;

            if (this.Persona.EsRolPrivado == true)
            {
                this.FindControl("CargoEmpleadoRolPrivado").IsVisible = true;
                this.FindControl("Division_AreaItem").IsVisible = true;
            }

            if (this.Persona.EsRolPrivado != true)
            {
                // controles no visibles si no es rol privado.
                this.FindControl("ES_GERENTE").IsVisible = false;
                this.FindControl("ES_SUBGERENTE").IsVisible = false;
                this.FindControl("ES_JEFE_DE_AREA").IsVisible = false;
                
                this.FindControl("CargoEmpleadoNoRolPrivado").IsVisible = true;
                this.FindControl("AreaNoRolPrivado").IsVisible = true;
                this.FindControl("Persona_Es_JefeDirecto").IsVisible = true;
            }
        }

        partial void Persona_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                if (this.Persona.EsRolPrivado != true)
                {
                    if (this.Persona.Division_AreaItem == null)
                    {
                        this.IDAREA = null;
                        this.IDSUBGERENCIA = null;
                        this.IDGERENCIA = null;
                        this.Persona.Es_JefeDirecto = false;
                        this.FindControl("Persona_Es_JefeDirecto").IsEnabled = false;
                    }
                    else
                    {

                        this.IDAREA = this.Persona.Division_AreaItem.Id_Area;

                        if (this.Division_AreaItem.Division_SubGerenciaItem != null)
                        {
                            this.IDSUBGERENCIA = this.Division_AreaItem.Division_SubGerenciaItem.Id_SubGerencia;
                        }
                        else
                        {
                            this.IDSUBGERENCIA = null;
                        }

                        if (this.Division_AreaItem.Division_GerenciaItem != null)
                        {
                            this.IDGERENCIA = this.Division_AreaItem.Division_GerenciaItem.Id_Gerencia;
                        }
                        else

                            if (this.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem != null)
                            {
                                this.IDGERENCIA = this.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;
                            }

                        if (this.Persona.Es_JefeDirecto == true && this.Persona.Division_AreaItem.Superior_JefeDirecto.First() != null)
                        {
                            if (this.Persona.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1 != this.Persona)
                            {
                                results.AddPropertyError("No puede haber más de un jefe de área por área. " + this.Persona.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.NombreAD + " es el actual jefe de área de " + this.Persona.Division_AreaItem.Nombre);
                            }
                        }

                        this.FindControl("Persona_Es_JefeDirecto").IsEnabled = true;
                    }
                }

                if (this.Persona.EsRolPrivado == true)
                {
                    if (this.Persona.CargoRolPrivadoItem == null)
                    {
                        this.IDAREA = null;
                        this.IDSUBGERENCIA = null;
                        this.IDGERENCIA = null;

                        this.ES_JEFE_DE_AREA = false;
                        this.ES_SUBGERENTE = false;
                        this.ES_GERENTE = false;
                    }
                    else
                    {
                        if (this.Persona.CargoRolPrivadoItem.IDArea != null)
                        {
                            this.IDAREA = this.Persona.CargoRolPrivadoItem.IDArea;

                            if (this.Division_AreaItem.Division_SubGerenciaItem != null)
                            {
                                this.IDSUBGERENCIA = this.Division_AreaItem.Division_SubGerenciaItem.Id_SubGerencia;
                            }
                            else
                            {
                                this.IDSUBGERENCIA = null;
                            }

                            if (this.Division_AreaItem.Division_GerenciaItem != null)
                            {
                                this.IDGERENCIA = this.Division_AreaItem.Division_GerenciaItem.Id_Gerencia;
                            }
                            else

                            if (this.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem != null)
                            {
                                this.IDGERENCIA = this.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;
                            }

                            if (this.Persona.CargoRolPrivadoItem.EsJefeDeArea != true)
                            {
                                this.ES_JEFE_DE_AREA = false;
                                this.ES_SUBGERENTE = false;
                                this.ES_GERENTE = false;
                            }
                            else

                                if (this.Persona.CargoRolPrivadoItem.EsJefeDeArea == true)
                                {
                                    this.ES_JEFE_DE_AREA = true;
                                    this.ES_SUBGERENTE = false;
                                    this.ES_GERENTE = false;

                                    if (this.Division_AreaItem.Superior_JefeDirecto.First() != null)
                                    {
                                        if (this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1 != this.Persona)
                                        {
                                            results.AddPropertyError("No puede haber más de un jefe de área por área. " + this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.NombreAD + " es el actual jefe de área de " + this.Division_AreaItem.Nombre);
                                        }
                                    }
                                }

                        }
                        else

                            if (this.Persona.CargoRolPrivadoItem.IDSubgerencia != null)
                            {
                                this.IDSUBGERENCIA = this.Persona.CargoRolPrivadoItem.IDSubgerencia;

                                this.IDGERENCIA = this.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;

                                this.IDAREA = null;

                                if (this.Persona.CargoRolPrivadoItem.EsSubgerente == true)
                                {
                                    this.ES_JEFE_DE_AREA = false;
                                    this.ES_SUBGERENTE = true;
                                    this.ES_GERENTE = false;

                                    if (this.Division_SubGerenciaItem.Superior_SubGerente.First() != null)
                                    {
                                        if (this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1 != this.Persona)
                                        {
                                            results.AddPropertyError("No puede haber más de un subgerente por subgerencia. " + this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.NombreAD + " es el actual subgerente de " + this.Division_SubGerenciaItem.Nombre);
                                        }
                                    }
                                }

                            }
                            else

                                if (this.Persona.CargoRolPrivadoItem.IDGerencia != null)
                                {
                                    this.IDGERENCIA = this.Persona.CargoRolPrivadoItem.IDGerencia;

                                    this.IDSUBGERENCIA = null;

                                    this.IDAREA = null;

                                    if (this.Persona.CargoRolPrivadoItem.EsGerente == true)
                                    {
                                        this.ES_JEFE_DE_AREA = false;
                                        this.ES_SUBGERENTE = false;
                                        this.ES_GERENTE = true;

                                        if (this.Division_GerenciaItem.Superior_Gerente.First() != null)
                                        {
                                            if (this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1 != this.Persona)
                                            {
                                                results.AddPropertyError("No puede haber más de un gerente por gerencia. " + this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + " es el actual gerente de " + this.Division_GerenciaItem.Nombre);
                                            }
                                        }
                                    }
                                }
                    }
                }
            }
            catch { }
        }
    }
}