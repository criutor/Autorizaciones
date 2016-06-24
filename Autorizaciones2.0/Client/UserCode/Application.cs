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
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void ADMINISTRAR_EMPLEADOS_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void ADMINISTRAR_FERIADOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void ADMINISTRAR_CONVENIOS_COLECTIVOS_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void ADMINISTRAR_CARGOS_ROL_PRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
        }

        partial void SOLICITUDES_REBAJAR_ADMINISTRATIVOS_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_REBAJAR_HORASEXTRAS_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_REBAJAR_PERMISOS_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_REBAJAR_VACACIONES_NRP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_HISTORICO_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void PROCESOS_PERIODICOS_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void ADMINISTRAR_NOTIFICACIONES_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_REPORTE_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_REBAJAR_RRHH_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }

        partial void SOLICITUDES_APROBACION_CanRun(ref bool result)
        {
            //Puede entrar a esta pantalla solo si tiene un cargo con rol aprobador
              
                DataWorkspace dataWorkspace = new DataWorkspace();
            
                ConsultarRutUsuarioADItem operation =
                    dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();

                operation.NombreUsuario = User.FullName;

                dataWorkspace.Autorizaciones_AdminsData.SaveChanges();
            
            DataWorkspace dataWorkspace2 = new DataWorkspace();

            //var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut(operation.RutUsuario).Execute();//Original

            string RutUsuarioAD;

            if (User.HasPermission(Permissions.SalomeEscobar) == true)
            {
                RutUsuarioAD = "15413075-6";//salome
            }
            else

                if (User.HasPermission(Permissions.GustavoRubio) == true)
                {
                    RutUsuarioAD = "17511042-9";//gustavo
                }
                else

                    if (User.HasPermission(Permissions.CesarRiutor) == true)
                    {
                        RutUsuarioAD = "17229504-5";//cesar
                    }
                    else

                        if (User.HasPermission(Permissions.MauricioHernandez) == true)
                        {
                            RutUsuarioAD = "10686667-8";
                        }
                        else

                            if (User.HasPermission(Permissions.JimenaAriza) == true)
                            {
                                RutUsuarioAD = "10848223-0";
                            }
                            else

                                if (User.HasPermission(Permissions.MarceloMonsalve) == true)
                                {
                                    RutUsuarioAD = "12233917-3";
                                }
                                else

                                    if (User.HasPermission(Permissions.PaulaCastro) == true)
                                    {
                                        RutUsuarioAD = "12833658-3";
                                    }
                                    else

                                        if (User.HasPermission(Permissions.JanetGomez) == true)
                                        {
                                            RutUsuarioAD = "12855246-4";
                                        }
                                        else

                                            if (User.HasPermission(Permissions.RodrigoLeiva) == true)
                                            {
                                                RutUsuarioAD = "13995715-6";
                                            }
                                            else

                                                if (User.HasPermission(Permissions.JoseJoaquinPrat) == true)
                                                {
                                                    RutUsuarioAD = "14120256-1";
                                                }
                                                else

                                                    if (User.HasPermission(Permissions.CarolinaBarrientos) == true)
                                                    {
                                                        RutUsuarioAD = "14335101-7";
                                                    }
                                                    else

                                                        if (User.HasPermission(Permissions.IsraelSepulveda) == true)
                                                        {
                                                            RutUsuarioAD = "16114128-3";
                                                        }
                                                        else

                                                            if (User.HasPermission(Permissions.RodrigoAstudillo) == true)
                                                            {
                                                                RutUsuarioAD = "16121554-6";
                                                            }
                                                            else

                                                                if (User.HasPermission(Permissions.DanielaOportus) == true)
                                                                {
                                                                    RutUsuarioAD = "16191035-K";
                                                                }
                                                                else

                                                                    if (User.HasPermission(Permissions.FlorMoraga) == true)
                                                                    {
                                                                        RutUsuarioAD = "16524487-7";
                                                                    }
                                                                    else

                                                                        if (User.HasPermission(Permissions.MariaJoseVives) == true)
                                                                        {
                                                                            RutUsuarioAD = "16570769-9";
                                                                        }
                                                                        else

                                                                            if (User.HasPermission(Permissions.VictoriaGallardo) == true)
                                                                            {
                                                                                RutUsuarioAD = "17002656-K";
                                                                            }
                                                                            else

                                                                                if (User.HasPermission(Permissions.FrancescaTapia) == true)
                                                                                {
                                                                                    RutUsuarioAD = "18830554-7";
                                                                                }
                                                                                else

                                                                                    if (User.HasPermission(Permissions.AldoPeirano) == true)
                                                                                    {
                                                                                        RutUsuarioAD = "6075713-5";
                                                                                    }
                                                                                    else

                                                                                        if (User.HasPermission(Permissions.AmeliaReyes) == true)
                                                                                        {
                                                                                            RutUsuarioAD = "6509116-K";
                                                                                        }
                                                                                        else

                                                                                            if (User.HasPermission(Permissions.JoseUrrutia) == true)
                                                                                            {
                                                                                                RutUsuarioAD = "8031707-7";
                                                                                            }
                                                                                            else

                                                                                                if (User.HasPermission(Permissions.MarcelaEspinosa) == true)
                                                                                                {
                                                                                                    RutUsuarioAD = "8394703-9";
                                                                                                }
                                                                                                else

                                                                                                    if (User.HasPermission(Permissions.MoisesArevalo) == true)
                                                                                                    {
                                                                                                        RutUsuarioAD = "9220822-2";//moises
                                                                                                    }
                                                                                                    else

                                                                                                        if (User.HasPermission(Permissions.MauricioMontero) == true)
                                                                                                        {
                                                                                                            RutUsuarioAD = "9258364-3";
                                                                                                        }
                                                                                                        else

                                                                                                            if (User.HasPermission(Permissions.PatriciaJofré) == true)
                                                                                                            {
                                                                                                                RutUsuarioAD = "9282059-9";
                                                                                                            }
                                                                                                            else

                                                                                                                if (User.HasPermission(Permissions.XimenaEspinoza) == true)
                                                                                                                {
                                                                                                                    RutUsuarioAD = "9407437-1";
                                                                                                                }
                                                                                                                else

                                                                                                                    if (User.HasPermission(Permissions.ElisaMuñoz) == true)
                                                                                                                    {
                                                                                                                        RutUsuarioAD = "9453159-4";
                                                                                                                    }
                                                                                                                    else

                                                                                                                        if (User.HasPermission(Permissions.MonicaSepulveda) == true)
                                                                                                                        {
                                                                                                                            RutUsuarioAD = "9907954-1";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            RutUsuarioAD = operation.RutUsuario;
                                                                                                                        }

            var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut(RutUsuarioAD).Execute();


            //var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut("17229504-5").Execute();//ESTA ES PARA PRUEBAS INTERNAS
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

            ///*// ESTO ES PARA DEPLOY
                //Consultar el rut del user.
                DataWorkspace dataWorkspace = new DataWorkspace();

                ConsultarRutUsuarioADItem operation =
                    dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();

                operation.NombreUsuario = User.FullName;

                dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

                //Consultar el rol del user
            //*/

            DataWorkspace dataWorkspace2 = new DataWorkspace();
            
            //var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut(operation.RutUsuario).Execute();// Original

            string RutUsuarioAD;

            if (User.HasPermission(Permissions.SalomeEscobar) == true)
            {
                RutUsuarioAD = "15413075-6";//salome
            }
            else

                if (User.HasPermission(Permissions.GustavoRubio) == true)
                {
                    RutUsuarioAD = "17511042-9";//gustavo
                }
                else

                    if (User.HasPermission(Permissions.CesarRiutor) == true)
                    {
                        RutUsuarioAD = "17229504-5";//cesar
                    }
                    else

                        if (User.HasPermission(Permissions.MauricioHernandez) == true)
                        {
                            RutUsuarioAD = "10686667-8";
                        }
                        else

                            if (User.HasPermission(Permissions.JimenaAriza) == true)
                            {
                                RutUsuarioAD = "10848223-0";
                            }
                            else

                                if (User.HasPermission(Permissions.MarceloMonsalve) == true)
                                {
                                    RutUsuarioAD = "12233917-3";
                                }
                                else

                                    if (User.HasPermission(Permissions.PaulaCastro) == true)
                                    {
                                        RutUsuarioAD = "12833658-3";
                                    }
                                    else

                                        if (User.HasPermission(Permissions.JanetGomez) == true)
                                        {
                                            RutUsuarioAD = "12855246-4";
                                        }
                                        else

                                            if (User.HasPermission(Permissions.RodrigoLeiva) == true)
                                            {
                                                RutUsuarioAD = "13995715-6";
                                            }
                                            else

                                                if (User.HasPermission(Permissions.JoseJoaquinPrat) == true)
                                                {
                                                    RutUsuarioAD = "14120256-1";
                                                }
                                                else

                                                    if (User.HasPermission(Permissions.CarolinaBarrientos) == true)
                                                    {
                                                        RutUsuarioAD = "14335101-7";
                                                    }
                                                    else

                                                        if (User.HasPermission(Permissions.IsraelSepulveda) == true)
                                                        {
                                                            RutUsuarioAD = "16114128-3";
                                                        }
                                                        else

                                                            if (User.HasPermission(Permissions.RodrigoAstudillo) == true)
                                                            {
                                                                RutUsuarioAD = "16121554-6";
                                                            }
                                                            else

                                                                if (User.HasPermission(Permissions.DanielaOportus) == true)
                                                                {
                                                                    RutUsuarioAD = "16191035-K";
                                                                }
                                                                else

                                                                    if (User.HasPermission(Permissions.FlorMoraga) == true)
                                                                    {
                                                                        RutUsuarioAD = "16524487-7";
                                                                    }
                                                                    else

                                                                        if (User.HasPermission(Permissions.MariaJoseVives) == true)
                                                                        {
                                                                            RutUsuarioAD = "16570769-9";
                                                                        }
                                                                        else

                                                                            if (User.HasPermission(Permissions.VictoriaGallardo) == true)
                                                                            {
                                                                                RutUsuarioAD = "17002656-K";
                                                                            }
                                                                            else

                                                                                if (User.HasPermission(Permissions.FrancescaTapia) == true)
                                                                                {
                                                                                    RutUsuarioAD = "18830554-7";
                                                                                }
                                                                                else

                                                                                    if (User.HasPermission(Permissions.AldoPeirano) == true)
                                                                                    {
                                                                                        RutUsuarioAD = "6075713-5";
                                                                                    }
                                                                                    else

                                                                                        if (User.HasPermission(Permissions.AmeliaReyes) == true)
                                                                                        {
                                                                                            RutUsuarioAD = "6509116-K";
                                                                                        }
                                                                                        else

                                                                                            if (User.HasPermission(Permissions.JoseUrrutia) == true)
                                                                                            {
                                                                                                RutUsuarioAD = "8031707-7";
                                                                                            }
                                                                                            else

                                                                                                if (User.HasPermission(Permissions.MarcelaEspinosa) == true)
                                                                                                {
                                                                                                    RutUsuarioAD = "8394703-9";
                                                                                                }
                                                                                                else

                                                                                                    if (User.HasPermission(Permissions.MoisesArevalo) == true)
                                                                                                    {
                                                                                                        RutUsuarioAD = "9220822-2";//moises
                                                                                                    }
                                                                                                    else

                                                                                                        if (User.HasPermission(Permissions.MauricioMontero) == true)
                                                                                                        {
                                                                                                            RutUsuarioAD = "9258364-3";
                                                                                                        }
                                                                                                        else

                                                                                                            if (User.HasPermission(Permissions.PatriciaJofré) == true)
                                                                                                            {
                                                                                                                RutUsuarioAD = "9282059-9";
                                                                                                            }
                                                                                                            else

                                                                                                                if (User.HasPermission(Permissions.XimenaEspinoza) == true)
                                                                                                                {
                                                                                                                    RutUsuarioAD = "9407437-1";
                                                                                                                }
                                                                                                                else

                                                                                                                    if (User.HasPermission(Permissions.ElisaMuñoz) == true)
                                                                                                                    {
                                                                                                                        RutUsuarioAD = "9453159-4";
                                                                                                                    }
                                                                                                                    else

                                                                                                                        if (User.HasPermission(Permissions.MonicaSepulveda) == true)
                                                                                                                        {
                                                                                                                            RutUsuarioAD = "9907954-1";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            RutUsuarioAD = operation.RutUsuario;
                                                                                                                        }

            var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut(RutUsuarioAD).Execute();

            //var Persona = dataWorkspace2.Autorizaciones_AdminsData.PersonaPorRut("17229504-5").Execute();//ESTA ES PARA PRUEBAS INTERNAS
            //var Persona = (from o in dataWorkspace2.Autorizaciones_AdminsData.Persona where o.Rut_Persona == operation.RutUsuario select o);
            
            if (Persona.First().Es_Gerente != true && Persona.First().Es_SubGerente != true && Persona.First().Es_JefeDirecto != true)
            {
                result = false;
            }
            else { result = true; }  
        }

        partial void SOLICITUDES_HISTORICO_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
            //result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void PROCESOS_PERIODICOS_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
            //result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void ADMINISTRAR_EMPLEADOS_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
            //result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_ADMINISTRATIVOS_RP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
            //result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REBAJAR_VACACIONES_RP_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
            //result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        
        partial void ADMINISTRAR_NOTIFICACIONES_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
            //result = this.User.HasPermission(Permissions.SecurityAdministration);
        }

        partial void SOLICITUDES_REPORTE_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
        }

        partial void SOLICITUDES_REBAJAR_ROLPRIVADO_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRolPrivado);
        }

        partial void SOLICITUDES_HORASEXTRAS_REPORTEFIN700_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado
            result = this.User.HasPermission(Permissions.AdminRRHH);
        }
        
    }
}
