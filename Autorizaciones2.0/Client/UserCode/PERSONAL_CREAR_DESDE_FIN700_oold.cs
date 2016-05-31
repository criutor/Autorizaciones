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
    public partial class PERSONAL_CREAR_DESDE_FIN700_oold
    {
        partial void PERSONAL_CREAR_DESDE_FIN700_oold_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.
            //PersonaItem personaNueva = new PersonaItem();
        }

        partial void CrearTrabajador_Execute()
        {
            //---IDPERSONA = this.Trabajador.SelectedItem.RutTrabajador;

            PersonaItem persona = DataWorkspace.Autorizaciones_AdminsData.Persona.AddNew();
            
            
            //PersonaItem personaNueva = new PersonaItem();
            persona.Rut_Persona = "123456";
            persona.Nombres = "a";
            persona.AP_Paterno = "b";
            persona.AP_Materno = "c";
            this.Save();
            /*
            if (this.Persona.Count() == 0)//Si la persona no existe en la bd de la aplicacion....
            {

                //---PersonaItem personaNueva = new PersonaItem();

                //personaNueva.Rut_Persona = this.PersonasContratadas.SelectedItem.Rut_Persona;

                //---personaNueva.Rut_Persona = removerCeros(this.Trabajador.SelectedItem.RutTrabajador).ToString();//
                //---personaNueva.Rut_Persona = "123456";
                //---personaNueva.Rut_Persona_ConCeros = this.Trabajador.SelectedItem.RutTrabajador;//

                //---RUTSINCEROS = personaNueva.Rut_Persona;

                //---RutTrabajadorParaContratos = this.Trabajador.SelectedItem.RutTrabajador;//

                //---this.ConsultarEmailUsuarioAD_Execute();

                //---personaNueva.Email = this.Email;

                //---personaNueva.Nombres = this.Trabajador.SelectedItem.Nombres;
                //---personaNueva.Nombres = "a";
                //---personaNueva.AP_Paterno = this.Trabajador.SelectedItem.ApellidoPaterno;
                //---personaNueva.AP_Paterno = "b";
                //---personaNueva.AP_Materno = this.Trabajador.SelectedItem.ApellidoMaterno;
                //---personaNueva.AP_Materno = "c";

                //personaNueva.Division_AreaItem = this.Division_AreaItem;
                //---personaNueva.Division_AreaItem = null;

                //---personaNueva.SaldoDiasAdmins = 3.0;
                //---personaNueva.Es_Gerente = false;
                //---personaNueva.Es_JefeDirecto = false;
                //---personaNueva.Es_SubGerente = false;
                //---personaNueva.EsRolPrivado = false;
                /*---
                try //Traer el cargo de fin700
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    if (CtoT_CargoItem != null)
                    {
                        //---personaNueva.Cargo = CtoT_CargoItem.Descripcion_Cargo;
                    }

                    //---personaNueva.FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }
                
                    this.Save();
                    this.Close(true);
            }
            else
            {//Si la persona existe en la bd de la aplicacion....
                this.ShowMessageBox("Este empleado ya existe en la bd de datos de la aplicación", "ERROR", MessageBoxOption.Ok);
            }
            */
            
        }
        
        /*---
        public static string removerCeros(String RutEmpleado)
        {
            String[] RutSinCeros = new String[12];

            int largo = RutEmpleado.Length;

            int i = 0;

            while (RutEmpleado[i] == '0')
            {
                i++;
            }
            string sub = RutEmpleado.Substring(i);
            return sub;
        }
        */

        partial void PERSONAL_CREAR_DESDE_FIN700_oold_Saved()
        {
            
            //---this.Application.ShowPERSONAL_DETALLES(RUTSINCEROS);
        }

        partial void ConsultarEmailUsuarioAD_Execute()
        {
            
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarEmailUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarEmailUsuarioAD.AddNew();
            
            operation.RutUsuario = this.RUTSINCEROS;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.Email = operation.EmailUsuario;

            if (this.Email == "Valor no encontrado en Active Directory!")
            {
                try
                {

                    this.ShowMessageBox("Lo sentimos, el rut: " + RUTSINCEROS + " no coincide con ningún empleado de active directory, por favor contacta al administrador. ", "Error", MessageBoxOption.Ok);
                    this.Close(false);
                }
                catch { }
            }
        }
        
    }
}
