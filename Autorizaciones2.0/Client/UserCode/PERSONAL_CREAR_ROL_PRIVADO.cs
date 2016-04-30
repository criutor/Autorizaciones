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
            this.CerrarPantallaSWITCH = true;

            this.Persona = new PersonaItem();
            this.Persona.SaldoDiasAdmins = 3.0;
            this.Persona.SaldoVacaciones2 = 0;
            this.Persona.Es_Gerente = false;
            this.Persona.Es_JefeDirecto = false;
            this.Persona.Es_SubGerente = false;
            this.Persona.EsRolPrivado = true;
        }

        partial void Persona_Validate(ScreenValidationResultsBuilder results)
        {
            try
            {
                this.Persona.Rut_Persona = this.RUN + '-' + this.Dig_Verificador;
                this.Persona.Email = this.Email;
            
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
            catch { }
        }
        
        partial void Dig_Verificador_Validate(ScreenValidationResultsBuilder results) // validador de run
        {
            try
            {
                string rutAux = this.RUN; //control que contenga rut
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
                string dv = this.Dig_Verificador.ToUpper(); //control que contenga DV

                if (!expresion.IsMatch(rutAux) && dv != Dig)
                {
                    results.AddPropertyError("Run invalido");
                }
            }
            catch { }
        }

        partial void PERSONAL_CREAR_ROL_PRIVADO_Created()// Detecta si se ha hecho algún cambio en la propiedad PersonaItemProperty en la pantalla, si es asi, llama a la funcion CrearNuevoRolPrivado_PropertyChanged
        {
                Dispatchers.Main.BeginInvoke(() =>
                {
                    ((INotifyPropertyChanged)this.Persona).PropertyChanged +=
                        new PropertyChangedEventHandler(CrearNuevoRolPrivado_PropertyChanged);
                });
        }

        void CrearNuevoRolPrivado_PropertyChanged(object sender, PropertyChangedEventArgs e)//Detecta si se ha hecho algún cambio en el campo Rut_Persona
        {
            if (e.PropertyName.Equals("Rut_Persona"))
            {
                InvocarEmailAD = true;
            }
        }

        partial void InvocarEmailAD_Validate(ScreenValidationResultsBuilder results) //Invocador para la consulta del email del usuario en active directory
        {
            if (InvocarEmailAD == true)
            {
                this.ConsultarEmailUsuarioAD_Execute();
                InvocarEmailAD = false;
            }
        }

        partial void ConsultarEmailUsuarioAD_Execute()//Consulta el email del usuario en active directory
        {
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarEmailUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarEmailUsuarioAD.AddNew();

            operation.RutUsuario = this.Persona.Rut_Persona;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.Email = operation.EmailUsuario; //retorna el email en una variable de la pantalla
        }

        partial void PERSONAL_CREAR_ROL_PRIVADO_Saving(ref bool handled)
        {
            this.Persona.Nombres = this.Persona.Nombres.ToUpper();
            this.Persona.AP_Materno = this.Persona.AP_Materno.ToUpper();
            this.Persona.AP_Paterno = this.Persona.AP_Paterno.ToUpper();

            //Crea un nuevo cargo de subgerente
            if (this.Persona.CargoRolPrivadoItem.EsGerente == true && this.Persona.CargoRolPrivadoItem.IDGerencia != null)
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
            }else

            //Crea un nuevo cargo de subgerente
            if (this.Persona.CargoRolPrivadoItem.EsSubgerente == true && this.Persona.CargoRolPrivadoItem.IDSubgerencia != null)
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
            }else

            //Crea un nuevo cargo de jefe de area
            if (this.Persona.CargoRolPrivadoItem.EsJefeDeArea == true)
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
            }else

            //Si el nuevo cargo no incluye ser jefe de área ni subgerente ni gerente.
            if (this.Persona.CargoRolPrivadoItem != null && this.Persona.CargoRolPrivadoItem.EsJefeDeArea != true && this.Persona.CargoRolPrivadoItem.EsSubgerente != true && this.Persona.CargoRolPrivadoItem.EsGerente != true)
            {
                this.Persona.Cargo = this.Persona.CargoRolPrivadoItem.Nombre;
                this.Persona.Division_AreaItem = this.Division_AreaItem;
                this.Persona.AreaDeTrabajo = this.Division_AreaItem.Nombre;
            }
        }

        partial void CerrarPantalla_Execute()
        {
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

        partial void PERSONAL_CREAR_ROL_PRIVADO_Activated()
        {
            try
            {
                //Desabilita los campos
                this.FindControl("ES_GERENTE").IsEnabled = false;
                this.FindControl("ES_SUBGERENTE").IsEnabled = false;
                this.FindControl("ES_JEFE_DE_AREA").IsEnabled = false;
            }
            catch { }
        }
    }
}