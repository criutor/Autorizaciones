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
using System.Text.RegularExpressions;



namespace LightSwitchApplication
{
    public partial class PERSONAL_CREAR_ROL_PRIVADO
    {
        partial void PERSONAL_CREAR_ROL_PRIVADO_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            this.CerrarPantallaSWITCH = true;

            this.PersonaItemProperty = new PersonaItem();
            
            this.PersonaItemProperty.SaldoDiasAdmins = 3.0;
            //***this.PersonaItemProperty.SaldoVacaciones = 15;
            this.PersonaItemProperty.SaldoVacaciones2 = 0;
            //this.PersonaItemProperty.VacacionesSaldo = 15;
            this.PersonaItemProperty.Es_Gerente = false;
            this.PersonaItemProperty.Es_JefeDirecto = false;
            this.PersonaItemProperty.Es_SubGerente = false;
            this.PersonaItemProperty.EsRolPrivado = true;
            //this.PersonaItemProperty.RolPrivado = "Sí";

            
            this.ES_GERENTE = false;
            this.ES_SUBGERENTE = false;

            //this.CARGO = false; //variable auxiliar que sirve para validar cuando no se ha marcado ninguna casilla, es gerente o sub gerente.
            
            //this.PersonaItemProperty.Nombres = ""; // Sin esto lanza un null exception ya que nombres es Null

        }



        partial void PersonaItemProperty_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            try
            {
                this.PersonaItemProperty.Cargo = this.CargoRP.Nombre;
            }
            catch { }
            */
            try
            {
            
            this.PersonaItemProperty.Rut_Persona = this.RUN + '-' + this.Dig_Verificador;

            //this.PersonaItemProperty.Rut_Persona = this.RUN + '-' + this.Dig_Verificador;
            this.PersonaItemProperty.Email = this.Email;
            this.PersonaItemProperty.Nombres = this.Nombres.ToUpper();
            this.PersonaItemProperty.AP_Materno = this.ApellidoMaterno.ToUpper();
            this.PersonaItemProperty.AP_Paterno = this.ApellidoPaterno.ToUpper();

            string[] porPalabrasNombre = PersonaItemProperty.Nombres.Split(new Char[] { ' ' });
            string[] porPalabrasAPP = PersonaItemProperty.AP_Paterno.Split(new Char[] { ' ' });
            string[] porPalabrasAPM = PersonaItemProperty.AP_Materno.Split(new Char[] { ' ' });
            
            this.PersonaItemProperty.NombreAD = porPalabrasAPP[0].ToUpper() + " " + porPalabrasAPM[0].ToUpper() + ", " + porPalabrasNombre[0].ToUpper();

            }
            catch { }

        }

        partial void ConsultarEmailUsuarioAD_Execute()
        {
            // Escriba el código aquí.
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarEmailUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarEmailUsuarioAD.AddNew();

            //operation.RutUsuario = this.PersonaItemProperty.Rut_Persona;
            operation.RutUsuario = this.PersonaItemProperty.Rut_Persona;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.Email = operation.EmailUsuario;
        }

        partial void PERSONAL_CREAR_ROL_PRIVADO_Created()
        {
                // Detecta si se ha hecho algún cambio en la propiedad en la pantalla, si es asi, llama a la funcion ..._PropertyChanged
            
                Dispatchers.Main.BeginInvoke(() =>
                {
                    ((INotifyPropertyChanged)this.PersonaItemProperty).PropertyChanged +=
                        new PropertyChangedEventHandler(CrearNuevoRolPrivado_PropertyChanged);

                });




                  
        }

        //Detecta si se ha hecho algún cambio en el campo Inicio
        void CrearNuevoRolPrivado_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /*
            if (e.PropertyName.Equals("Termino"))
            {
                TimeSpan diferenciaDias = this.Solicitud_Detalle_Vacaciones.Termino - this.Solicitud_Detalle_Vacaciones.Inicio;
                this.Solicitud_Detalle_Vacaciones.NumeroDias = diferenciaDias.Days;

            }
             */
            if (e.PropertyName.Equals("Rut_Persona"))
            {

                InvocarEmailAD = true;
            }
        }

        partial void InvocarEmailAD_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (InvocarEmailAD == true)
            {
                this.ConsultarEmailUsuarioAD_Execute();
                InvocarEmailAD = false;
            }
        }

        partial void Dig_Verificador_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                string rutAux = this.RUN;//control que contenga rut
                int suma = 0;
                try
                {
                    for (int x = rutAux.Length - 1; x >= 0; x--)
                        suma += int.Parse(char.IsDigit(rutAux[x]) ? rutAux[x].ToString() : "0") * (((rutAux.Length - (x + 1)) % 6) + 2);
                }
                catch { }
                int numericDigito = (11 - suma % 11);
                string digito = numericDigito == 11 ? "0" : numericDigito == 10 ? "K" : numericDigito.ToString();
                string Dig = digito;
                Regex expresion = new Regex("^([0-9]+-[0-9K])$");
                string dv = this.Dig_Verificador.ToUpper();//control que contenga DV
            

            
                if (!expresion.IsMatch(rutAux) && dv != Dig)
                {
                    results.AddPropertyError("Run invalido");
                }
            }
            catch { }

            
        }

        partial void Gerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                /*
                if (this.Gerencia == null)
                {
                    this.FindControl("ES_GERENTE").IsEnabled = false;
                    this.ES_GERENTE = false;
                }
                else //if(this.ES_SUBGERENTE == false)
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
                else //if (this.ES_GERENTE == false)
                {
                    this.FindControl("ES_SUBGERENTE").IsEnabled = true;

                }
                */

                this.IDSUBGERENCIA = this.Subgerencia.Id_SubGerencia;
                //this.Area = null;
            }
            catch { }
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
                this.PersonaItemProperty.Division_AreaItem = this.Area;
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
                   if (this.Gerencia.Superior_Gerente.First().PersonaItem1.Rut_Persona != this.PersonaItemProperty.Rut_Persona )
                    {
                        results.AddPropertyError("Esta gerencia ya tiene un gerente");
                    }
                }
                else
                    
                    if (this.ES_GERENTE == true)
                    {
                        this.FindControl("ES_SUBGERENTE").IsEnabled = false;
                    }

                    if (this.ES_GERENTE == false && this.Subgerencia != null)
                    {
                        this.FindControl("ES_SUBGERENTE").IsEnabled = true;
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
            catch { }

        }
        */
        
        /*
        partial void ES_SUBGERENTE_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                

                if (this.Subgerencia.Superior_SubGerente.Count() > 0 && this.ES_SUBGERENTE == true)
                {
                    if (this.Subgerencia.Superior_SubGerente.First().PersonaItem1.Rut_Persona != this.PersonaItemProperty.Rut_Persona)
                    {
                        results.AddPropertyError("Esta subgerencia ya tiene un subgerente");
                    }
                }
                else
                {
                    
                    if (this.ES_SUBGERENTE == true )
                    {
                        this.FindControl("ES_GERENTE").IsEnabled = false;
                    }

                    if (this.ES_SUBGERENTE == false && this.Gerencia != null)
                    {
                        this.FindControl("ES_GERENTE").IsEnabled = true;
                    }
                    
                    if (this.ES_GERENTE == false && this.ES_SUBGERENTE == false)
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
        */

        /*
        partial void ES_JEFE_DE_AREA_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (this.ES_JEFE_DE_AREA == true && this.ES_GERENTE == true)
            {
                results.AddPropertyError("No puedes marcar más una casilla");
            }

            if (this.ES_SUBGERENTE == true && this.ES_JEFE_DE_AREA == true)
            {
                results.AddPropertyError("No puedes marcar más una casilla");
            }
        }
        */

        partial void PERSONAL_CREAR_ROL_PRIVADO_Saving(ref bool handled)
        {
            if (this.ES_GERENTE == true)
            {
                this.PersonaItemProperty.Cargo = "GERENTE DE " + this.Gerencia.Nombre;
            }
            else if (this.ES_SUBGERENTE == true)
            {
                this.PersonaItemProperty.Cargo = "SUBGERENTE DE " + this.Subgerencia.Nombre;
            }

            if (this.ES_GERENTE == true)
            {
                Superior_GerenteItem gerente = new Superior_GerenteItem();
                gerente.PersonaItem1 = this.PersonaItemProperty;
                gerente.Division_GerenciaItem = this.Gerencia;
                this.PersonaItemProperty.Es_Gerente = true;
                this.PersonaItemProperty.Division_AreaItem = null;
                this.PersonaItemProperty.EsRolPrivado = true;
                this.PersonaItemProperty.AreaDeTrabajo = this.Gerencia.Nombre;

                this.Gerencia.Gerente = this.PersonaItemProperty.AP_Paterno.ToUpper() + " " + this.PersonaItemProperty.AP_Materno.ToUpper() + ", " + this.PersonaItemProperty.Nombres.ToUpper();

                if (this.Gerencia.Nombre == "GERENCIA GENERAL") { this.PersonaItemProperty.Es_GerenteGeneral = true; }
            }else

            if (this.ES_SUBGERENTE == true)
            {
                Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                Subgerente.PersonaItem1 = this.PersonaItemProperty;
                Subgerente.Division_SubGerenciaItem = this.Subgerencia;
                this.PersonaItemProperty.Es_SubGerente = true;
                this.PersonaItemProperty.EsRolPrivado = true;
                this.PersonaItemProperty.IDGerencia_para_subgerentes = this.Subgerencia.Division_GerenciaItem.Id_Gerencia;
                //IDGerencia_para_subgerentes es utilizado en el Query de Solicitud_Header en la pantalla "Master_SolicitudesPendientes",...
                //...ya que los subgerentes no pertenecen a ninguna area, si no se guardara el id de la subgerencia, ...
                //...los gerentes verian las solicitudes de todos los subgerentes y no solo los que les corresponden.
                this.PersonaItemProperty.Division_AreaItem = null;
                this.PersonaItemProperty.AreaDeTrabajo = this.Subgerencia.Nombre;

                this.Subgerencia.SubGerente = this.PersonaItemProperty.AP_Paterno.ToUpper() + " " + this.PersonaItemProperty.AP_Materno.ToUpper() + ", " + this.PersonaItemProperty.Nombres.ToUpper();

            }else

            if (this.ES_JEFE_DE_AREA == true)
            {
                Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                JefeDirecto.PersonaItem1 = this.PersonaItemProperty;
                JefeDirecto.Division_AreaItem = this.Area;

                this.PersonaItemProperty.Division_AreaItem = JefeDirecto.Division_AreaItem;//no lo esta guardando!!

                this.Area.JefeDeArea = this.PersonaItemProperty.AP_Paterno.ToUpper() + " " + this.PersonaItemProperty.AP_Materno.ToUpper() + ", " + this.PersonaItemProperty.Nombres.ToUpper();


                this.PersonaItemProperty.AreaDeTrabajo = this.Area.Nombre;

                this.PersonaItemProperty.Es_JefeDirecto = true;

                this.PersonaItemProperty.EsRolPrivado = false;
            }

        }

        partial void CerrarPantalla_Execute()
        {
            // Escriba el código aquí.
            this.Close(false);
        }

        partial void PERSONAL_CREAR_ROL_PRIVADO_Saved()
        {
            // Escriba el código aquí.
            if (this.CerrarPantallaSWITCH == true)
            {
                this.Close(true);
            }
            else
            {
                this.CerrarPantallaSWITCH = true;
            }
        }


        /*
        partial void CARGO_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");

            if (this.ES_SUBGERENTE == false && this.ES_SUBGERENTE == false)
            {
                this.CARGO = false;
            }
            
            if(this.ES_SUBGERENTE == true)
            {
                this.CARGO = true;
            }

            if (this.ES_GERENTE == true)
            {
                this.CARGO = true;
            }

            if(this.CARGO == false)
            {
                results.AddPropertyError("Debes seleccionar una de las dos casillas, Es gerente o Es Subgerente");
            }

        }

        */



        /*
        partial void Email_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            if (this.PersonaItemProperty.Cargo == "Valor no encontrado en Active Directory!")
            {
                this.PersonaItemProperty.Cargo = null;
            }
    
        }
        */

        /*
        partial void EsRolPrivado_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            this.PersonaItemProperty.EsRolPrivado = this.EsRolPrivado;

            if (this.EsRolPrivado == true) {

                try
                {
                    this.FindControl("DatosRolPrivado").IsVisible = true;
                    this.FindControl("DatosNoRolPrivado").IsVisible = false;
                }
                catch { }

                this.PersonaItemProperty.Division_AreaItem = null;

                if (this.PersonaItemProperty.CargoRolPrivadoItem != null)
                {
                    this.PersonaItemProperty.Cargo = this.PersonaItemProperty.CargoRolPrivadoItem.Nombre;
                }


                if (this.PersonaItemProperty.CargoRolPrivadoItem == null)
                {
                    results.AddPropertyError("Debe escoger un cargo para rol privado");
                }

            }
            else
            {
                this.PersonaItemProperty.CargoRolPrivadoItem = null;
                this.FindControl("DatosRolPrivado").IsVisible = false; 
                this.FindControl("DatosNoRolPrivado").IsVisible = true; 
            }


        }
        */

        partial void SeleccionarComoGerente_Execute()
        {
            // Escriba el código aquí.

            if (this.Gerencia.Superior_Gerente.Count() == 0)
            {

                this.PersonaItemProperty.Cargo = "GERENTE DE " + this.Gerencia.Nombre;

                Superior_GerenteItem gerente = new Superior_GerenteItem();
                gerente.PersonaItem1 = this.PersonaItemProperty;
                gerente.Division_GerenciaItem = this.Gerencia;
                this.PersonaItemProperty.Es_Gerente = true;
                this.PersonaItemProperty.Division_AreaItem = null;
                this.PersonaItemProperty.EsRolPrivado = true;
                this.PersonaItemProperty.AreaDeTrabajo = this.Gerencia.Nombre;

                //this.Gerencia.Gerente = this.PersonaItemProperty.AP_Paterno.ToUpper() + " " + this.PersonaItemProperty.AP_Materno.ToUpper() + ", " + this.PersonaItemProperty.Nombres.ToUpper();
                this.Gerencia.Gerente = this.PersonaItemProperty.NombreAD;
                
                if (this.Gerencia.Nombre == "GERENCIA GENERAL") { this.PersonaItemProperty.Es_GerenteGeneral = true; }

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de gerente asignado con exito");
            }
            else
            {
                this.ShowMessageBox(this.Gerencia.Superior_Gerente.First().PersonaItem1.NombreAD + " es el actual gerente.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
        }

        partial void SeleccionarComoSubgerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Subgerencia.Superior_SubGerente.Count() == 0)
            {
                this.PersonaItemProperty.Cargo = "SUBGERENTE DE " + this.Subgerencia.Nombre;

                Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                Subgerente.PersonaItem1 = this.PersonaItemProperty;
                Subgerente.Division_SubGerenciaItem = this.Subgerencia;
                this.PersonaItemProperty.Es_SubGerente = true;
                this.PersonaItemProperty.EsRolPrivado = true;
                this.PersonaItemProperty.IDGerencia_para_subgerentes = this.Subgerencia.Division_GerenciaItem.Id_Gerencia;
                //IDGerencia_para_subgerentes es utilizado en el Query de Solicitud_Header en la pantalla "Master_SolicitudesPendientes",...
                //...ya que los subgerentes no pertenecen a ninguna area, si no se guardara el id de la subgerencia, ...
                //...los gerentes verian las solicitudes de todos los subgerentes y no solo los que les corresponden.
                this.PersonaItemProperty.Division_AreaItem = null;
                this.PersonaItemProperty.AreaDeTrabajo = this.Subgerencia.Nombre;

                //this.Subgerencia.SubGerente = this.PersonaItemProperty.AP_Paterno.ToUpper() + " " + this.PersonaItemProperty.AP_Materno.ToUpper() + ", " + this.PersonaItemProperty.Nombres.ToUpper();
                this.Subgerencia.SubGerente = this.PersonaItemProperty.NombreAD;

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de subgerente asignado con exito");
            }
            else
            {
                this.ShowMessageBox(this.Subgerencia.Superior_SubGerente.First().PersonaItem1.NombreAD + " es el actual subgerente.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
        }

         
        partial void SeleccionarComoGerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.CanSave == true)
            {
                if (this.Gerencia == null)
                {
                    result = false;
                }

                if (this.PersonaItemProperty.Superior_Gerente.Count() > 0)
                {
                    result = false;
                }

                if (this.PersonaItemProperty.Superior_SubGerente.Count() > 0)
                {
                    result = false;
                }
            }
            else { result = false; }
        }

        partial void SeleccionarComoSubgerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.CanSave == true)
            {

                if (this.Subgerencia == null)
                {
                    result = false;
                }

                if (this.PersonaItemProperty.Superior_SubGerente.Count() > 0)
                {
                    result = false;

                }

                if (this.PersonaItemProperty.Superior_Gerente.Count() > 0)
                {
                    result = false;
                }
            }
            else { result = false; }
        }

        partial void QuitarCargo_Execute()
        {
            // Escriba el código aquí.
            if (this.PersonaItemProperty.Es_Gerente == true)
            {
                if (this.PersonaItemProperty.Superior_Gerente.First().Division_GerenciaItem.Nombre == "GERENCIA GENERAL")
                {
                    this.PersonaItemProperty.Es_GerenteGeneral = false;
                }

                this.PersonaItemProperty.Es_Gerente = false;

                this.PersonaItemProperty.Cargo = null;

                this.PersonaItemProperty.Superior_Gerente.First().Division_GerenciaItem.Gerente = null;

                this.PersonaItemProperty.Superior_Gerente.First().Delete();

                this.CerrarPantallaSWITCH = false;

                this.Save();

                this.ShowMessageBox("Cargo de gerente eliminado con exito");

            }
            else
                if (this.PersonaItemProperty.Es_SubGerente == true)
                {
                    this.PersonaItemProperty.Es_SubGerente = false;

                    this.PersonaItemProperty.Cargo = null;

                    this.PersonaItemProperty.Superior_SubGerente.First().Division_SubGerenciaItem.SubGerente = null;

                    this.PersonaItemProperty.Superior_SubGerente.First().Delete();

                    this.CerrarPantallaSWITCH = false;

                    this.Save();

                    this.ShowMessageBox("Cargo de subgerente eliminado con exito");

                }
                else
                    if (this.PersonaItemProperty.Es_JefeDirecto == true)
                    {
                        this.PersonaItemProperty.Es_JefeDirecto = false;

                        this.PersonaItemProperty.Superior_JefeDirecto.First().Division_AreaItem.JefeDeArea = null;

                        this.PersonaItemProperty.Superior_JefeDirecto.First().Delete();

                        this.CerrarPantallaSWITCH = false;

                        this.Save();

                        this.ShowMessageBox("Cargo de jefe de área eliminado con exito");
                    }

        }

        partial void QuitarCargo_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.PersonaItemProperty.Es_Gerente != true && this.PersonaItemProperty.Es_SubGerente != true && this.PersonaItemProperty.Es_JefeDirecto != true)
            {
                result = false;
            }
        }
    }
}