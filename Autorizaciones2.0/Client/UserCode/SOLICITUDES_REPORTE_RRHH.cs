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
    public partial class SOLICITUDES_REPORTE_RRHH
    {
        partial void Gerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            try
            {
                Id_Gerencia = this.Gerencia.Id_Gerencia;
                Id_GerenciaDiv = this.Gerencia.Id_Gerencia;
                Id_SubGerencia = -1;
                Id_Area = -1;
                //this.NúmeroDeSolicitudesGerencia = this.SOLICITUDES.Count();
                this.SubGerencia = null;
                this.Área = null;
            }
            catch { }
             * */
        }

        partial void Division_SubGerencia_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            try
            {
                Id_SubGerencia = this.SubGerencia.Id_SubGerencia;
                Id_Area = -1;
                Id_Gerencia = -1;
                //this.NúmeroDeSolicitudesSubGerencia = this.SOLICITUDES.Count();
                //this.Área = null;
            }
            catch { }
             
             */
        }

        partial void Division_Area_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            try
            {
                Id_Area = this.Área.Id_Area;
                Id_SubGerencia = -1;
                Id_Gerencia = -1;
                //this.NúmeroDeSolicitudesArea = this.SOLICITUDES.Count();
            }
            catch { }
             * */
        }

        partial void ConsultarGerencia_Execute()
        {
            // Escriba el código aquí.
            if (ADMINISTRATIVO == false && VACACIONES == false && OTROPERMISO == false && HORASEXTRAS == false)
            {
                this.ShowMessageBox("Debes seleccionar por lo menos un tipo de solicitudes");
            }
            else
            {
                if (this.Gerencia != null)
                {
                    Id_Gerencia = this.Gerencia.Id_Gerencia;
                    Id_SubGerencia = -1;
                    Id_Area = -1;

                    this.NúmeroDeSolicitudesGerencia = this.SOLICITUDES.Count();
                }
                else { this.ShowMessageBox("Primero debes escoger una gerencia"); }
            }
        }

        partial void ConsultarSubgerencia_Execute()
        {
            // Escriba el código aquí.
            if (ADMINISTRATIVO == false && VACACIONES == false && OTROPERMISO == false && HORASEXTRAS == false)
            {
                this.ShowMessageBox("Debes seleccionar por lo menos un tipo de solicitudes");
            }
            else
            {
                if (this.SubGerencia != null)
                {
                    Id_SubGerencia = this.SubGerencia.Id_SubGerencia;
                    Id_Area = -1;
                    Id_Gerencia = -1;

                    this.NúmeroDeSolicitudesSubGerencia = this.SOLICITUDES.Count();
                }
                else { this.ShowMessageBox("Primero debes escoger una subgerencia"); }
            }
        }

        partial void ConsultarArea_Execute()
        {
            // Escriba el código aquí.
            if (ADMINISTRATIVO == false && VACACIONES == false && OTROPERMISO == false && HORASEXTRAS == false)
            {
                this.ShowMessageBox("Debes seleccionar por lo menos un tipo de solicitudes");
            }
            else
            {
                if (this.Área != null)
                {
                    Id_Area = this.Área.Id_Area;
                    Id_SubGerencia = -1;
                    Id_Gerencia = -1;

                    this.NúmeroDeSolicitudesArea = this.SOLICITUDES.Count();
                }
                else { this.ShowMessageBox("Primero debes escoger un área"); }
            }
        }

        partial void FechaSolicitudDesde_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            /*
            if(this.FechaSolicitudDesde > this.FechaSolicitudHasta)
            {
                results.AddPropertyError("Las fechas deben ser en orden cronológico.");
            }

            if (this.FechaSolicitudDesde == null &&  this.FechaSolicitudHasta == null)
            {
                results.AddPropertyError("Las fechas no pueden estar en blanco");
            }
            */
        }

        partial void LimpiarFechas_Execute()
        {
            // Escriba el código aquí.
            this.FechaSolicitudHasta = null;
            this.FechaSolicitudDesde = null;

            //this.SOLICITUDES.Load();
        }
           
    }
}
