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
        partial void PersonaItem_Loaded(bool succeeded)
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);
        }

        partial void PersonaItem_Changed()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);
        }

        partial void PERSONAL_DETALLES_Saved()
        {
            // Escriba el código aquí.
            this.SetDisplayNameFromEntity(this.PersonaItem);

            if (this.CerrarPantallaSWITCH == true)
            {
                this.Close(true);
            }
            else
            {
                this.CerrarPantallaSWITCH = true;
            }
        }

        partial void PersonaItem_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            try
            {
                this.PersonaItem.Cargo = this.CargoRP.Nombre;
            }
            catch { }
            */
            /*
            try
            {
                if (this.PersonaItem.EsRolPrivado == true)
                {
                    this.FindControl("CargoRP").IsVisible = true;
                }
                else { this.FindControl("Cargo").IsVisible = true; }
            }
            catch { }
            */

        }

        partial void EMAIL_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            //this.PersonaItem.Email = this.EMAIL;
        }

        partial void APELLIDOPATERNO_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItem.AP_Paterno = this.APELLIDOPATERNO;
        }

        partial void APELLIDOMATERNO_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItem.AP_Materno = this.APELLIDOMATERNO;
        }

        partial void NOMBRES_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItem.Nombres = this.NOMBRES;

        }

        partial void PERSONAL_DETALLES_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            this.CerrarPantallaSWITCH = true;

            this.APELLIDOPATERNO = this.PersonaItem.AP_Paterno;
            this.APELLIDOMATERNO = this.PersonaItem.AP_Materno;
            this.NOMBRES = this.PersonaItem.Nombres;

        }

        partial void Area_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                /*
                if (this.Area == null)
                {
                    this.FindControl("ES_JEFE_DE_AREA").IsEnabled = false;
                    this.ES_JEFE_DE_AREA = false;
                }
                else
                {
                    this.FindControl("ES_JEFE_DE_AREA").IsEnabled = true;

                }
                */
                this.PersonaItem.Division_AreaItem = this.Area;
            }
            catch { }
            
        }

        partial void Gerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                /*
                if (this.Gerencia == null && this.Subgerencia == null)
                {
                    results.AddPropertyError("Debes marcar por lo menos una casilla");
                }

                if (this.Gerencia == null)
                {
                    this.FindControl("ES_GERENTE").IsEnabled = false;
                    this.ES_GERENTE = false;
                }
                else
                {
                    this.FindControl("ES_GERENTE").IsEnabled = true;
                }
                */

                this.IDGERENCIA = this.Gerencia.Id_Gerencia;
                this.IDSUBGERENCIA = null;
                this.Subgerencia = null;
                this.Area = null;
            }
            catch { }

                
        }

        partial void Subgerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                /*
                if (this.Subgerencia == null)
                {
                    this.FindControl("ES_SUBGERENTE").IsEnabled = false;
                    this.ES_SUBGERENTE = false;
                }
                else
                {
                    this.FindControl("ES_SUBGERENTE").IsEnabled = true;

                }
                */

                this.IDSUBGERENCIA = this.Subgerencia.Id_SubGerencia;
                //this.Area = null;
            }
            catch { }
        }
        /*
        partial void ES_GERENTE_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {


                if (this.Gerencia.Superior_Gerente.Count() > 0 && this.ES_GERENTE == true)
                {
                    if (this.Gerencia.Superior_Gerente.First().PersonaItem1.Rut_Persona != this.PersonaItem.Rut_Persona)
                    {
                        results.AddPropertyError("Esta gerencia ya tiene un gerente");
                    }
                }
                else
                {
                    if (this.ES_GERENTE == false && this.Subgerencia == null)
                    {
                        results.AddPropertyError("Debes marcar por lo menos una casilla");
                    }

                    if (this.ES_GERENTE == false && this.ES_SUBGERENTE == false)
                    {
                        results.AddPropertyError("Debes marcar por lo menos una casilla");
                    }
                    
                    if (this.ES_GERENTE == true && this.ES_SUBGERENTE == true)
                    {
                        results.AddPropertyError("No puedes marcar más una casilla");
                    }
                    else

                        if (this.ES_GERENTE == true && this.ES_JEFE_DE_AREA == true)
                        {
                            results.AddPropertyError("No puedes marcar más una casilla");
                        }
                }
            }
            catch { }
        }

        partial void ES_SUBGERENTE_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                if (this.Subgerencia.Superior_SubGerente.Count() > 0 && this.ES_SUBGERENTE == true)
                {
                    if (this.Subgerencia.Superior_SubGerente.First().PersonaItem1.Rut_Persona != this.PersonaItem.Rut_Persona)
                    {
                        results.AddPropertyError("Esta subgerencia ya tiene un subgerente");
                    }
                }
                else
                {
                    if (this.ES_SUBGERENTE == false && this.Gerencia == null)
                    {
                        results.AddPropertyError("Debes marcar por lo menos una casilla");
                    }
                    
                    if (this.ES_SUBGERENTE == true && this.ES_GERENTE == true)
                    {
                        results.AddPropertyError("No puedes marcar más una casilla");
                    }

                    if (this.ES_SUBGERENTE == true && this.ES_JEFE_DE_AREA == true)
                    {
                        results.AddPropertyError("No puedes marcar más una casilla");
                    }
                }
            }
            catch { }
        }
        
        partial void ES_JEFE_DE_AREA_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                if (this.Area.Superior_JefeDirecto.Count() > 0 && this.ES_JEFE_DE_AREA == true)
                {
                    if (this.Area.Superior_JefeDirecto.First().PersonaItem1.Rut_Persona != this.PersonaItem.Rut_Persona)
                    {
                        results.AddPropertyError("Esta área ya tiene un jefe de área");
                    }
                }
                else
                {
                    if (this.ES_JEFE_DE_AREA == true && this.ES_GERENTE == true)
                    {
                        results.AddPropertyError("No puedes marcar más una casilla");
                    }

                    if (this.ES_SUBGERENTE == true && this.ES_JEFE_DE_AREA == true)
                    {
                        results.AddPropertyError("No puedes marcar más una casilla");
                    }
                }
            }
            catch { }
        }
        */
        partial void PERSONAL_DETALLES_Activated()
        {
            // Escriba el código aquí.
            if (this.PersonaItem.EsRolPrivado == true)
            {
                this.FindControl("GerenciaGerente").IsVisible = true;
                this.FindControl("SubgerenciaSubgerente").IsVisible = true;
            }
            else          
            {
                this.FindControl("GerenciaGerente").IsVisible = true;
                this.FindControl("AreaJefedearea").IsVisible = true;
                this.FindControl("botonesgerente").IsVisible = false;
                //this.FindControl("ES_GERENTE").IsVisible = false;
            }

            
            if (this.PersonaItem.Es_Gerente == true)
            {
                //this.ES_GERENTE = true;
                this.Gerencia = this.PersonaItem.Superior_Gerente.First().Division_GerenciaItem;

            }else
                if (this.PersonaItem.Es_SubGerente == true)
                {
                    //this.ES_SUBGERENTE = true;
                
                    this.Subgerencia = this.PersonaItem.Superior_SubGerente.First().Division_SubGerenciaItem;

                }else
                    if (this.PersonaItem.Es_JefeDirecto == true)
                    {
                        //this.ES_JEFE_DE_AREA = true;

                        this.Area = this.PersonaItem.Superior_JefeDirecto.First().Division_AreaItem;
                    }
                    else
                    {
                        this.Area = this.PersonaItem.Division_AreaItem;
                    }

            
        }

        partial void CerrarPantalla_Execute()
        {
            // Escriba el código aquí.
            this.Close(false);
        }

        partial void PERSONAL_DETALLES_Saving(ref bool handled)
        {
            /*
            // Escriba el código aquí.
            if (this.ES_GERENTE == true)
            {
                this.PersonaItem.Cargo = "GERENTE DE " + this.Gerencia.Nombre;
            }

            if (this.ES_SUBGERENTE == true)
            {
                this.PersonaItem.Cargo = "SUBGERENTE DE " + this.Subgerencia.Nombre;
            }

            if (this.ES_GERENTE == true)
            {
                Superior_GerenteItem gerente = new Superior_GerenteItem();
                gerente.PersonaItem1 = this.PersonaItem;
                gerente.Division_GerenciaItem = this.Gerencia;
                this.PersonaItem.Es_Gerente = true;
                this.PersonaItem.Division_AreaItem = null;
                this.PersonaItem.EsRolPrivado = true;
                this.PersonaItem.AreaDeTrabajo = this.Gerencia.Nombre;

                //this.Gerencia.Gerente = this.PersonaItem.AP_Paterno.ToUpper() + " " + this.PersonaItem.AP_Materno.ToUpper() + ", " + this.PersonaItem.Nombres.ToUpper();
                this.Gerencia.Gerente = this.PersonaItem.NombreAD;
                if (this.Gerencia.Nombre == "GERENCIA GENERAL") { this.PersonaItem.Es_GerenteGeneral = true; }
            }
            else

                if (this.ES_SUBGERENTE == true)
                {
                    Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                    Subgerente.PersonaItem1 = this.PersonaItem;
                    Subgerente.Division_SubGerenciaItem = this.Subgerencia;
                    this.PersonaItem.Es_SubGerente = true;
                    this.PersonaItem.EsRolPrivado = true;
                    this.PersonaItem.IDGerencia_para_subgerentes = this.Subgerencia.Division_GerenciaItem.Id_Gerencia;
                    //IDGerencia_para_subgerentes es utilizado en el Query de Solicitud_Header en la pantalla "Master_SolicitudesPendientes",...
                    //...ya que los subgerentes no pertenecen a ninguna area, si no se guardara el id de la subgerencia, ...
                    //...los gerentes verian las solicitudes de todos los subgerentes y no solo los que les corresponden.
                    this.PersonaItem.Division_AreaItem = null;
                    this.PersonaItem.AreaDeTrabajo = this.Subgerencia.Nombre;

                    //this.Subgerencia.SubGerente = this.PersonaItem.AP_Paterno.ToUpper() + " " + this.PersonaItem.AP_Materno.ToUpper() + ", " + this.PersonaItem.Nombres.ToUpper();
                    this.Subgerencia.SubGerente = this.PersonaItem.NombreAD;
                }
                else

                    if (this.ES_JEFE_DE_AREA == true)
                    {
                        Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                        JefeDirecto.PersonaItem1 = this.PersonaItem;
                        JefeDirecto.Division_AreaItem = this.Area;

                        this.PersonaItem.Division_AreaItem = this.Area; 

                        this.Area.JefeDeArea = this.PersonaItem.NombreAD;


                        this.PersonaItem.AreaDeTrabajo = this.Area.Nombre;

                        this.PersonaItem.Es_JefeDirecto = true;

                        this.PersonaItem.EsRolPrivado = false;
                    }
                    else
                    {
                        try
                        {
                            this.PersonaItem.Division_AreaItem = this.Area;
                            this.PersonaItem.AreaDeTrabajo = this.Area.Nombre;
                        }
                        catch { }
                    }
            */
            if (this.PersonaItem.Es_Gerente != true && this.PersonaItem.Es_JefeDirecto != true && this.PersonaItem.Es_SubGerente != true)
            {
                try
                {
                    this.PersonaItem.Division_AreaItem = this.Area;
                    this.PersonaItem.AreaDeTrabajo = this.Area.Nombre;
                }
                catch { }
            }
        }

        partial void SeleccionarComoGerente_Execute()
        {
            // Escriba el código aquí.

            if (this.Gerencia.Superior_Gerente.Count() == 0)
            {

                this.PersonaItem.Cargo = "GERENTE DE " + this.Gerencia.Nombre;

                Superior_GerenteItem gerente = new Superior_GerenteItem();
                gerente.PersonaItem1 = this.PersonaItem;
                gerente.Division_GerenciaItem = this.Gerencia;
                this.PersonaItem.Es_Gerente = true;
                this.PersonaItem.Division_AreaItem = null;
                this.PersonaItem.EsRolPrivado = true;
                this.PersonaItem.AreaDeTrabajo = this.Gerencia.Nombre;

                //this.Gerencia.Gerente = this.PersonaItem.AP_Paterno.ToUpper() + " " + this.PersonaItem.AP_Materno.ToUpper() + ", " + this.PersonaItem.Nombres.ToUpper();
                this.Gerencia.Gerente = this.PersonaItem.NombreAD;
                if (this.Gerencia.Nombre == "GERENCIA GENERAL") { this.PersonaItem.Es_GerenteGeneral = true; }

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de gerente asignado con exito");
            }
            else
            {
                this.ShowMessageBox(this.Gerencia.Superior_Gerente.First().PersonaItem1.NombreAD + " es el actual gerente.","ACCIÓN DENEGADA",MessageBoxOption.Ok);
            }
        }

        partial void SeleccionarComoSubgerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Subgerencia.Superior_SubGerente.Count() == 0)
            {
                this.PersonaItem.Cargo = "SUBGERENTE DE " + this.Subgerencia.Nombre;

                Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                Subgerente.PersonaItem1 = this.PersonaItem;
                Subgerente.Division_SubGerenciaItem = this.Subgerencia;
                this.PersonaItem.Es_SubGerente = true;
                this.PersonaItem.EsRolPrivado = true;
                this.PersonaItem.IDGerencia_para_subgerentes = this.Subgerencia.Division_GerenciaItem.Id_Gerencia;
                //IDGerencia_para_subgerentes es utilizado en el Query de Solicitud_Header en la pantalla "Master_SolicitudesPendientes",...
                //...ya que los subgerentes no pertenecen a ninguna area, si no se guardara el id de la subgerencia, ...
                //...los gerentes verian las solicitudes de todos los subgerentes y no solo los que les corresponden.
                this.PersonaItem.Division_AreaItem = null;
                this.PersonaItem.AreaDeTrabajo = this.Subgerencia.Nombre;

                //this.Subgerencia.SubGerente = this.PersonaItem.AP_Paterno.ToUpper() + " " + this.PersonaItem.AP_Materno.ToUpper() + ", " + this.PersonaItem.Nombres.ToUpper();
                this.Subgerencia.SubGerente = this.PersonaItem.NombreAD;

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de subgerente asignado con exito");
            }
            else
            {
                this.ShowMessageBox(this.Subgerencia.Superior_SubGerente.First().PersonaItem1.NombreAD + " es el actual subgerente.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
        }

        partial void SeleccionarComoJefeDeArea_Execute()
        {
            // Escriba el código aquí.

            if (this.Area.Superior_JefeDirecto.Count() == 0)
            {
                Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                JefeDirecto.PersonaItem1 = this.PersonaItem;
                JefeDirecto.Division_AreaItem = this.Area;

                this.PersonaItem.Division_AreaItem = this.Area;

                this.Area.JefeDeArea = this.PersonaItem.NombreAD;


                this.PersonaItem.AreaDeTrabajo = this.Area.Nombre;

                this.PersonaItem.Es_JefeDirecto = true;

                this.PersonaItem.EsRolPrivado = false;

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de jefe de área asignado con exito");
            }
            else
            {
                this.ShowMessageBox(this.Area.Superior_JefeDirecto.First().PersonaItem1.NombreAD + " es el actual jefe de área.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
        }

        partial void QuitarComoGerente_Execute()
        {
            // Escriba el código aquí.
            //if (this.Division_Gerencia.SelectedItem.Nombre == "GERENCIA GENERAL") { this.Division_Gerencia.SelectedItem.Superior_Gerente.First().PersonaItem1.Es_GerenteGeneral = false; }

            this.PersonaItem.Es_Gerente = false;

            this.PersonaItem.Cargo = null;

            this.PersonaItem.Superior_Gerente.First().Division_GerenciaItem.Gerente = null;

            this.PersonaItem.Superior_Gerente.First().Delete();

            this.CerrarPantallaSWITCH = false;

            this.Save();

            this.ShowMessageBox("Cargo de gerente eliminado con exito");
        }

        partial void QuitarComoSubgerente_Execute()
        {
            // Escriba el código aquí.
            this.PersonaItem.Es_SubGerente = false;

            this.PersonaItem.Cargo = null;

            this.PersonaItem.IDGerencia_para_subgerentes = null;

            this.PersonaItem.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = null;

            this.PersonaItem.Superior_SubGerente.First().Delete();

            this.CerrarPantallaSWITCH = false;

            this.Save();

            this.ShowMessageBox("Cargo de subgerente eliminado con exito");
        }

        partial void QuitarComoJefeDeArea_Execute()
        {
            // Escriba el código aquí.
            this.PersonaItem.Es_JefeDirecto = false;

            this.PersonaItem.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;

            this.PersonaItem.Superior_JefeDirecto.First().Delete();

            this.CerrarPantallaSWITCH = false;

            this.Save();

            this.ShowMessageBox("Cargo de jefe de área eliminado con exito");
        }

        partial void QuitarComoGerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.PersonaItem.Superior_Gerente.Count() == 0) { result = false; }
        }

        partial void QuitarComoSubgerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.PersonaItem.Superior_SubGerente.Count() == 0) { result = false; }
        }

        partial void QuitarComoJefeDeArea_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.PersonaItem.Superior_JefeDirecto.Count() == 0) { result = false; }
        }

        partial void SeleccionarComoGerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Gerencia == null) 
            { 
                result = false; 
            }

            if (this.PersonaItem.Superior_Gerente.Count() > 0)
            {
                result = false;
            }

            if (this.PersonaItem.Superior_SubGerente.Count() > 0)
            {
                result = false;
            }
        }

        partial void SeleccionarComoSubgerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Subgerencia == null)
            {
                result = false;
            }

            if (this.PersonaItem.Superior_SubGerente.Count() > 0)
            {
                result = false;

            }

            if (this.PersonaItem.Superior_Gerente.Count() > 0)
            {
                result = false;
            }
        }

        partial void SeleccionarComoJefeDeArea_CanExecute(ref bool result)
        {
            // Escriba el código aquí.

            if (this.Area == null)
            {
                result = false;
            }

            if (this.PersonaItem.Superior_JefeDirecto.Count() > 0)
            {
                result = false;

            }
        }

        partial void QuitarCargo_Execute()
        {
            // Escriba el código aquí.
            if (this.PersonaItem.Es_Gerente == true )
            {
                if (this.PersonaItem.Superior_Gerente.First().Division_GerenciaItem.Nombre == "GERENCIA GENERAL") 
                {
                    this.PersonaItem.Es_GerenteGeneral = false; 
                }

                this.PersonaItem.Es_Gerente = false;

                this.PersonaItem.Cargo = null;

                this.PersonaItem.Superior_Gerente.First().Division_GerenciaItem.Gerente = null;

                this.PersonaItem.Superior_Gerente.First().Delete();

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de gerente eliminado con exito");

            }else
                if (this.PersonaItem.Es_SubGerente == true )
                {
                    this.PersonaItem.Es_SubGerente = false;

                    this.PersonaItem.Cargo = null;

                    this.PersonaItem.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = null;

                    this.PersonaItem.Superior_SubGerente.First().Delete();

                    this.CerrarPantallaSWITCH = false;

                    this.Save();

                    this.ShowMessageBox("Cargo de subgerente eliminado con exito");

                }else
                    if (this.PersonaItem.Es_JefeDirecto == true)
                    {
                        this.PersonaItem.Es_JefeDirecto = false;

                        this.PersonaItem.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;

                        this.PersonaItem.Superior_JefeDirecto.First().Delete();

                        this.CerrarPantallaSWITCH = false;

                        this.Save();

                        this.ShowMessageBox("Cargo de jefe de área eliminado con exito");
                    }

        }

        partial void QuitarCargo_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.PersonaItem.Es_Gerente != true && this.PersonaItem.Es_SubGerente != true && this.PersonaItem.Es_JefeDirecto != true) 
            {
                result = false; 
            }
        }

        
    }
}