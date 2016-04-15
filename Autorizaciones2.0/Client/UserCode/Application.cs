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
    public partial class Application
    {

        partial void ADMINISTRAR_DIVISIONES_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_EMPLEADOS_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_FERIADOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_CONVENIOS_COLECTIVOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_CARGOS_ROL_PRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_ADMINISTRATIVOS_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_HORASEXTRAS_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_PERMISOS_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_VACACIONES_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_HISTORICO_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_HISTORICO_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //result = this.User.HasPermission(Permissions.AdminRolPrivado);
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void PROCESOS_PERIODICOS_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_APROBACIÓN_CanRun(ref bool result)
        {
            //Puede entrar a esta pantalla solo si tiene un cargo de superior

            //Consultar el rut del user.
            DataWorkspace dataWorkspace = new DataWorkspace();
            
            ConsultarRutUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();

            operation.NombreUsuario = User.FullName;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();
            
            //Consultar el rol del user

            DataWorkspace dataWorkspace2 = new DataWorkspace();

            var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut(operation.RutUsuario).Execute();        
            //var Persona = (from o in dataWorkspace2.Autorizaciones_AdminsData.Persona where o.Rut_Persona == operation.RutUsuario select o);
            
            if (Persona.First().Es_Gerente != true && Persona.First().Es_SubGerente != true && Persona.First().Es_JefeDirecto != true)
            {
                result = false;
            }
            else { result = true; }           
        }

        partial void SOLICITUDES_A_CARGO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //Puede entrar a esta pantalla solo si tiene un cargo de superior

            //Consultar el rut del user.
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarRutUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();

            operation.NombreUsuario = User.FullName;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            //Consultar el rol del user

            DataWorkspace dataWorkspace2 = new DataWorkspace();

            var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut(operation.RutUsuario).Execute();
            //var Persona = (from o in dataWorkspace2.Autorizaciones_AdminsData.Persona where o.Rut_Persona == operation.RutUsuario select o);

            if (Persona.First().Es_Gerente != true && Persona.First().Es_SubGerente != true && Persona.First().Es_JefeDirecto != true)
            {
                result = false;
            }
            else { result = true; }  
        }

        partial void PROCESOS_PERIODICOS_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //result = this.User.HasPermission(Permissions.AdminRolPrivado);
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_EMPLEADOS_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //result = this.User.HasPermission(Permissions.AdminRolPrivado);
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_ADMINISTRATIVOS_RP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //result = this.User.HasPermission(Permissions.AdminRolPrivado);
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_VACACIONES_RP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //result = this.User.HasPermission(Permissions.AdminRolPrivado);
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REPORTE_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_NOTIFICACIONES_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            //result = this.User.HasPermission(Permissions.AdminRolPrivado);
            result = this.User.HasPermission(Permissions.SecurityAdministration);
        }
    }
}
