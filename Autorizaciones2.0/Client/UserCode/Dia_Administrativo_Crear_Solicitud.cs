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
    public partial class Dia_Administrativo_Crear_Solicitud
    {
        partial void Dia_Administrativo_Crear_Solicitud_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {

            Solicitud_HeaderItem solicitud = new Solicitud_HeaderItem();
            solicitud.FechaSolicitud = DateTime.Today;
            solicitud.NombreTrabajador = PersonaItem.NombreAD;
            solicitud.Fechaingreso = DateTime.Today; // cambiar por la tabla contrato!!!
            solicitud.PersonaItem1 = PersonaItem;
            solicitud.Administrativo = true;


            if (PersonaItem.Es_Gerente == true) { solicitud.Departamento = " Gerencia de " + PersonaItem.Superior_GerenteQuery.First().Division_GerenciaItem.Nombre; solicitud.Gerencia = PersonaItem.Superior_GerenteQuery.First().Division_GerenciaItem.Nombre; }
            else
                if (PersonaItem.Es_SubGerente == true) { solicitud.Departamento = " SubGerencia de " + PersonaItem.Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre; solicitud.Gerencia = PersonaItem.Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Nombre; }
                else
                {
                    solicitud.Departamento = PersonaItem.Division_AreaItem.Nombre;
                    solicitud.Gerencia = PersonaItem.Division_AreaItem.Division_GerenciaItem.Nombre;
                }
            
            this.Solicitud_Detalle_AdministrativoItemProperty = new Solicitud_Detalle_AdministrativoItem();
            this.Solicitud_Detalle_AdministrativoItemProperty.Solicitud_HeaderItem = solicitud;
            this.Solicitud_Detalle_AdministrativoItemProperty.SaldoDias = this.PersonaItem.SaldoDiasAdmins.Value;

            this.Estados_Solicitud = new Solicitud_Estados_AdministrativoItem();
            this.Estados_Solicitud.Solicitud_Detalle_AdministrativoItem = Solicitud_Detalle_AdministrativoItemProperty;
            this.Estados_Solicitud.Titulo = "LA SOLICITUD HA SIDO CREADO POR:";
           
        }

        partial void Dia_Administrativo_Crear_Solicitud_Saved()
        {
            // Escriba el código aquí.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.Solicitud_Detalle_AdministrativoItemProperty);
        }
    }
}