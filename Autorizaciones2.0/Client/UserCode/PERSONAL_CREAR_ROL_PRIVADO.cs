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
            this.PersonaItemProperty = new PersonaItem();
            this.PersonaItemProperty.SaldoDiasAdmins = 3.0;
            this.PersonaItemProperty.SaldoVacaciones = 15;
            this.PersonaItemProperty.Es_Gerente = false;
            this.PersonaItemProperty.Es_JefeDirecto = false;
            this.PersonaItemProperty.Es_SubGerente = false;
            this.PersonaItemProperty.EsRolPrivado = true;

            
            //this.PersonaItemProperty.Nombres = ""; // Sin esto lanza un null exception ya que nombres es Null

        }

        partial void PERSONAL_CREAR_ROL_PRIVADO_Saved()
        {
            this.Close(true);
            //Application.Current.ShowDefaultScreen(this.PersonaItemProperty);
        }

        partial void PersonaItemProperty_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                this.PersonaItemProperty.Cargo = this.CargoRP.Nombre;
            }
            catch { }

            this.PersonaItemProperty.Email = this.Email;
            this.PersonaItemProperty.Nombres = this.Nombres;
            this.PersonaItemProperty.AP_Materno = this.ApellidoMaterno;
            this.PersonaItemProperty.AP_Paterno = this.ApellidoPaterno;

            this.PersonaItemProperty.Rut_Persona = this.RUN + '-' + this.Dig_Verificador;

        }

        partial void ConsultarEmailUsuarioAD_Execute()
        {
            // Escriba el código aquí.
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarEmailUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarEmailUsuarioAD.AddNew();

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
            string dv = this.Dig_Verificador;//control que contenga DV
            try
            {
                if (!expresion.IsMatch(rutAux) && dv != Dig)
                {
                    results.AddPropertyError("Run invalido");
                }
            }
            catch { }
        }

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
    }
}