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
    public partial class PERSONAL_CREAR_DESDE_FIN700
    {
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

            /*
            char[] RutSinCeros = new char[12];
            int i = 0;
            
            while (RutEmpleado[i] == '0')
            {
                i++;
            }

            int j = 0;

            while (i < 12)
            {

                RutSinCeros[j] = RutEmpleado[i];
                i++; j++;

            }
            */

            //string Nombreaux = new string(RutSinCeros);

            return sub;
            
        }

        partial void CrearTrabajador_Execute()
        {
            
                       
            IDPERSONA = this.PersonasContratadas.SelectedItem.Rut_Persona;
            
            if (this.Persona.Count() == 0)//Si la persona no existe en la bd de la aplicacion....
            {
                
                PersonaItem personaNueva = new PersonaItem();

                //personaNueva.Rut_Persona = this.PersonasContratadas.SelectedItem.Rut_Persona;





                personaNueva.Rut_Persona = removerCeros(this.PersonasContratadas.SelectedItem.Rut_Persona).ToString();//

                personaNueva.Rut_Persona_ConCeros = this.PersonasContratadas.SelectedItem.Rut_Persona;//

                RUTSINCEROS = personaNueva.Rut_Persona;

                RutTrabajadorParaContratos = this.PersonasContratadas.SelectedItem.Rut_Persona;//

                this.ConsultarEmailUsuarioAD_Execute();

                
                    personaNueva.Email = this.Email;
                

                personaNueva.Nombres = this.PersonasContratadas.SelectedItem.Nombres;
                personaNueva.AP_Paterno = this.PersonasContratadas.SelectedItem.AP_Paterno;
                personaNueva.AP_Materno = this.PersonasContratadas.SelectedItem.AP_Materno;
                
                //personaNueva.Division_AreaItem = this.Division_AreaItem;
                personaNueva.Division_AreaItem = null;

                personaNueva.SaldoDiasAdmins = 3.0;
                personaNueva.Es_Gerente = false;
                personaNueva.Es_JefeDirecto = false;
                personaNueva.Es_SubGerente = false;
                personaNueva.EsRolPrivado = false;

                try //Traer el cargo de fin700
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    personaNueva.Cargo = CtoT_CargoItem.Descripcion_Cargo;

                    personaNueva.FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }

                try
                {
                    this.Save();
                    this.Close(true);
                }
                catch { }

                    
            }
            else {//Si la persona existe en la bd de la aplicacion....

                this.ShowMessageBox("Este empleado ya existe en la bd de datos de la aplicación", "ERROR", MessageBoxOption.Ok);

                /*
                try//Actualizar cargo...
                {
                    CODIGOCARGO = this.ContratoPorRut.Last().CargoEmpleado;

                    this.Persona.First().Cargo = CtoT_CargoItem.Descripcion_Cargo;

                    Persona.First().FechaVigencia = this.ContratoPorRut.Last().FechaVigencia;
                }
                catch { }

                if (this.CodigoPantalla == 1)//Actualizar area...
                {

                    this.Persona.First().Division_AreaItem = this.Division_AreaItem;
                }
                */
                /*
                if (this.CodigoPantalla == 2)
                {
                    if (this.Persona.First().Es_JefeDirecto == true)// exite en la bd y es jda
                    {
                        this.Persona.First().Superior_JefeDirecto.First().Division_AreaItem = this.Division_AreaItem;
                    }
                    else {// existe en la bd y no es jda aun

                        Superior_JefeDirectoItem JefeDirecto = new Superior_JefeDirectoItem();
                        JefeDirecto.PersonaItem1 = this.Persona.First();
                        JefeDirecto.Division_AreaItem = this.Division_AreaItem;
                        this.Division_AreaItem.JefeDeArea = this.Persona.First().NombreAD;
                        this.Persona.First().Es_JefeDirecto = true;
                    }

                    this.Persona.First().Division_AreaItem = this.Division_AreaItem;
                }
                */
            }



            
        }

        partial void PERSONAL_CREAR_DESDE_FIN700_Saved()
        {
            // Escriba el código aquí.
            this.Application.ShowPERSONAL_DETALLES(RUTSINCEROS);
        }

        partial void ConsultarEmailUsuarioAD_Execute()
        {
            // Escriba el código aquí.
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
