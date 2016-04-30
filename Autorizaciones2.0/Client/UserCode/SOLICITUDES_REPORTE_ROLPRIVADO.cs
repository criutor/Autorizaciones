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
    public partial class SOLICITUDES_REPORTE_ROLPRIVADO
    {
        partial void LimpiarFechas_Execute()
        {
            // Escriba el código aquí.
            this.FechaSolicitudHasta = null;
            this.FechaSolicitudDesde = null;
        }

        partial void ConsultarGerencia_Execute()
        {
            // Escriba el código aquí.
            if (ADMINISTRATIVO == false && VACACIONES == false)
            {
                this.ShowMessageBox("Debes seleccionar por lo menos un tipo de solicitudes");
            }
            else
            {
                if (this.Gerencia != null)
                {
                    Id_Gerencia = this.Gerencia.Id_Gerencia;
                    Id_SubGerencia = -1;
                    this.NúmeroDeSolicitudesGerencia = this.SOLICITUDES.Count();
                }
                else { this.ShowMessageBox("Primero debes escoger una gerencia"); }
            }
        }

        partial void ConsultarSubGerencia_Execute()
        {
            // Escriba el código aquí.
            if (ADMINISTRATIVO == false && VACACIONES == false)
            {
                this.ShowMessageBox("Debes seleccionar por lo menos un tipo de solicitudes");
            }
            else
            {
                if (this.SubGerencia != null)
                {
                    Id_SubGerencia = this.SubGerencia.Id_SubGerencia;
                    Id_Gerencia = -1;
                    this.NúmeroDeSolicitudesSubGerencia = this.SOLICITUDES.Count();
                }
                else { this.ShowMessageBox("Primero debes escoger una subgerencia"); }
            }
        }

    }
}
