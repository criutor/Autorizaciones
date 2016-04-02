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
    public partial class PERSONAL_LISTAR_ROLPRIVADO
    {
        partial void SeleccionarPersona_Execute()
        {
            if (this.Persona.SelectedItem.Es_Gerente == true){ this.ShowMessageBox("Este empleado ya posee un cargo de gerente en: "+ this.Persona.SelectedItem.Superior_Gerente.First().Division_GerenciaItem.Nombre);}
            else if (this.Persona.SelectedItem.Es_JefeDirecto == true) { this.ShowMessageBox("Este empleado ya posee un cargo de jefe de área en: " + this.Persona.SelectedItem.Superior_JefeDirecto.First().Division_AreaItem.Nombre); }
            else if (this.Persona.SelectedItem.Es_SubGerente == true) { this.ShowMessageBox("Este empleado ya posee un cargo de subgerente en: " + this.Persona.SelectedItem.Superior_SubGerente.First().Division_SubGerenciaItem.Nombre); }
            else
            {

                if (this.codigo == 1) //Escoger un gerente por primera vez 
                {
                    Superior_GerenteItem gerente = new Superior_GerenteItem();
                    gerente.PersonaItem1 = this.Persona.SelectedItem;
                    gerente.Division_GerenciaItem = this.Division_GerenciaItem;
                    this.Persona.SelectedItem.Es_Gerente = true;
                    this.Persona.SelectedItem.Division_AreaItem = null;
                    this.Persona.SelectedItem.EsRolPrivado = true;
                    this.Persona.SelectedItem.AreaDeTrabajo = this.Division_GerenciaItem.Nombre;
                    
                    this.Division_GerenciaItem.Gerente = this.Persona.SelectedItem.NombreAD;

                    if (this.Division_GerenciaItem.Nombre == "GERENCIA GENERAL") { this.Persona.SelectedItem.Es_GerenteGeneral = true; }

                }
                else

                    if (this.codigo == 2) //Escoger un gerente después de la primera vez
                    {


                        this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.Es_Gerente = false;
                        this.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1 = this.Persona.SelectedItem;
                        this.Persona.SelectedItem.Es_Gerente = true;
                        this.Persona.SelectedItem.Division_AreaItem = null;
                        
                        this.Division_GerenciaItem.Gerente = this.Persona.SelectedItem.NombreAD;

                    }
                    else

                        if (this.codigo == 3)//Escoger un Subgerente por primera vez
                        {
                            Superior_SubGerenteItem Subgerente = new Superior_SubGerenteItem();
                            Subgerente.PersonaItem1 = this.Persona.SelectedItem;
                            Subgerente.Division_SubGerenciaItem = this.Division_SubGerenciaItem;
                            this.Persona.SelectedItem.Es_SubGerente = true;
                            this.Persona.SelectedItem.EsRolPrivado = true;
                            this.Persona.SelectedItem.IDGerencia_para_subgerentes = this.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;
                            //IDGerencia_para_subgerentes es utilizado en el Query de Solicitud_Header en la pantalla "Master_SolicitudesPendientes",...
                            //...ya que los subgerentes no pertenecen a ninguna area, si no se guardara el id de la subgerencia, ...
                            //...los gerentes verian las solicitudes de todos los subgerentes y no solo los que les corresponden.
                            this.Persona.SelectedItem.Division_AreaItem = null;
                            this.Persona.SelectedItem.AreaDeTrabajo = this.Division_SubGerenciaItem.Nombre;

                            this.Division_SubGerenciaItem.SubGerente = this.Persona.SelectedItem.NombreAD;

                        }
                        else

                            if (this.codigo == 4)//Escoger un Subgerente después de la primera vez
                            {
                                this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.Es_SubGerente = false;
                                this.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1 = this.Persona.SelectedItem;
                                this.Persona.SelectedItem.Es_SubGerente = true;
                                this.Persona.SelectedItem.IDGerencia_para_subgerentes = this.Division_SubGerenciaItem.Division_GerenciaItem.Id_Gerencia;
                                this.Persona.SelectedItem.Division_AreaItem = null;

                                this.Division_SubGerenciaItem.SubGerente = this.Persona.SelectedItem.NombreAD;

                            }
                            else

                                if (this.codigo == 5)//Escoger un jefe directo por primera vez
                                {
                                    Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                                    JefeDirecto.PersonaItem1 = this.Persona.SelectedItem;
                                    JefeDirecto.PersonaItem1.Division_AreaItem = this.Division_AreaItem;
                                    JefeDirecto.Division_AreaItem = this.Division_AreaItem;
                                    this.Persona.SelectedItem.Es_JefeDirecto = true;

                                    this.Persona.SelectedItem.AreaDeTrabajo = this.Division_AreaItem.Nombre;

                                    this.Division_AreaItem.JefeDeArea = this.Persona.SelectedItem.NombreAD;

                                }
                                else

                                    if (this.codigo == 6)//Escoger un jefe directo después de la primera vez
                                    {
                                        this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.Es_JefeDirecto = false;
                                        this.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1 = this.Persona.SelectedItem;
                                        this.Persona.SelectedItem.Es_JefeDirecto = true;
                                        this.Persona.SelectedItem.Division_AreaItem = this.Division_AreaItem;

                                        this.Division_AreaItem.JefeDeArea = this.Persona.SelectedItem.NombreAD;
                                    }

                this.Save();
                this.Close(true);
            }
        }
                      
    }
}
