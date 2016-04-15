using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Security.Server;

//using Microsoft.VisualBasic;
//using System.Collections;
//using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;

using ActiveDirectoryLookup;

namespace LightSwitchApplication
{
    public partial class Autorizaciones_AdminsDataService
    {
        //Quitar los acentos del nombre para dejar en formato Active Directory
        public static string removerSignosAcentos(String texto)
        {
            string consignos = "áàäéèëíìïóòöúùuÁÀÄÉÈËÍÌÏÓÒÖÚÙÜ";
            string sinsignos = "aaaeeeiiiooouuuAAAEEEIIIOOOUUU";

            StringBuilder textoSinAcentos = new StringBuilder(texto.Length);
            int indexConAcento;
            foreach (char caracter in texto)
            {
                indexConAcento = consignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(sinsignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
        }

        partial void Persona_Inserted(PersonaItem entity)
        {
            //Ordenar nombre para dejar en formato Active Directory
            entity.Nombres = entity.Nombres.ToUpper();
            entity.AP_Materno = entity.AP_Materno.ToUpper();
            entity.AP_Paterno = entity.AP_Paterno.ToUpper();

            string[] porPalabrasNombre = entity.Nombres.Split(new Char[] { ' ' });
            string[] porPalabrasAPP = entity.AP_Paterno.Split(new Char[] { ' ' });
            string[] porPalabrasAPM = entity.AP_Materno.Split(new Char[] { ' ' });
            entity.NombreAD = porPalabrasAPP[0].ToUpper() + " " + porPalabrasAPM[0].ToUpper() + ", " + porPalabrasNombre[0].ToUpper();
            //entity.NombreAD = removerSignosAcentos(entity.NombreAD);
        }
        
        partial void ConsultarSaldoVacaciones_Inserting(ConsultarSaldoVacacionesItem entity)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                string connectionStringName = this.DataWorkspace.Fin700v60Data.Details.Name;
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

                string procedure = "dbo.SicasSaldoVacaciones";
                using (SqlCommand command = new SqlCommand(procedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@EmpId", 1));
                    command.Parameters.Add(new SqlParameter("@RutTrabajador", entity.Rut));
                    command.Parameters.Add(new SqlParameter("@Contrato", entity.Contrato));
                    command.Parameters.Add(new SqlParameter("@Fecha", entity.Fecha));

                    command.Parameters.Add("@DiasDevengados", SqlDbType.Float).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@DiasGanados", SqlDbType.Float).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@DiasTomadosVacaciones", SqlDbType.Float).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@SaldoVacaciones", SqlDbType.Float).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    entity.Saldo = (double)(command.Parameters["@SaldoVacaciones"].Value);
                    //connection.Close();
                }
            } this.Details.DiscardChanges();
        }
        
        partial void SOLICITUDES_Inserted(SOLICITUDESItem entity)
        {
            string destinatario = entity.EmailProximoDestinatario;
            string asunto = "Tiene una solicitud en espera de su aprobación";
            
            LightSwitchApplication.UserCode.EnviaMail correo = new UserCode.EnviaMail();

            //Si es de solicitud de horas extras
            if (entity.PersonaItem1.Es_GerenteGeneral == true)
            {
                string mensaje = "Estimado(a) Administrador(a) Rol Privado:\n" + "Una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " ha completado todas las aprobaciones necesarias.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla..";
                asunto = "Solicitud aprobada por todos los superiores correspondientes";
                
                DataWorkspace dataWorkspace = new DataWorkspace();
                var CorreoNotificacion = (from o in dataWorkspace.Autorizaciones_AdminsData.CorreosDeAvisos where o.Nombre == "Administrador(a) Rol Privado" select o);

                if (CorreoNotificacion.First().Email != null)
                {
                    correo.Mail(CorreoNotificacion.First().Email, asunto, mensaje);
                }
            }

            //Si es de solicitud de horas extras
            if (entity.HorasExtras == true)
            {
                string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ":\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " en espera de su aceptación.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                correo.Mail(destinatario, asunto, mensaje);
            }
            //todas las otras solicitudes
            else
            {
                if (entity.VB_GerenteGeneral == false)
                {
                    string mensaje = "Estimado Gerente General:\n" + "Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                    correo.Mail(destinatario, asunto, mensaje);
                }

                if (entity.VB_JefeDirecto == false)
                {
                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.NombreAD + ":\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como jefe(a) de área.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                    correo.Mail(destinatario, asunto, mensaje);
                }
                else
                    if (entity.VB_SubGerente == false)
                    {
                        string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.NombreAD + ":\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como subgerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                        correo.Mail(destinatario, asunto, mensaje);
                    }
                    else
                        if (entity.VB_Gerente == false)
                        {
                            if(entity.PersonaItem1.Division_AreaItem != null)
                            {

                                if(entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem != null)
                                {
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ":\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                                    correo.Mail(destinatario, asunto, mensaje);
                                }
                                else
                                {
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ":\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                                    correo.Mail(destinatario, asunto, mensaje);
                                }
                            }else
                            {
                                if (entity.PersonaItem1.Es_SubGerente == true)
                                {
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ":\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                                    correo.Mail(destinatario, asunto, mensaje); 
                                }
                            }
                        }
            }
        }
        
        partial void SOLICITUDES_Updated(SOLICITUDESItem entity)
        {
            if (entity.Cancelada != true)
            {
                string destinatario = entity.EmailProximoDestinatario;
                string asunto;

                LightSwitchApplication.UserCode.EnviaMail correo = new UserCode.EnviaMail();

                if (entity.Rechazada == true)
                {
                    asunto = "Su solicitud ha sido rechazada";
                    string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ".\n" + "Su solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " ha sido " + entity.Estado + ".\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                    correo.Mail(destinatario, asunto, mensaje);
                }
                else
                    if (entity.Rebajada == true)
                    {
                        asunto = "Su solicitud ha sido rebajada";
                        string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ".\n" + "Su solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " ha sido " + entity.Estado + ".\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                        correo.Mail(destinatario, asunto, mensaje);
                    }
                    else
                        if (entity.Completada == true)
                        {
                            asunto = "Su solicitud ha sido aprobada por sus superiores";
                            string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ".\n" + "Su solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " ha completado todas las aprobaciones necesarias.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla..";
                            correo.Mail(destinatario, asunto, mensaje);

                            if (entity.PersonaItem1.EsRolPrivado == true)
                            {
                                mensaje = "Estimado(a) Administrador(a) Rol Privado:\n" + "Una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " ha completado todas las aprobaciones necesarias.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla..";
                                asunto = "Solicitud aprobada por todos los superiores correspondientes";

                                DataWorkspace dataWorkspace = new DataWorkspace();
                                var CorreoNotificacion = (from o in dataWorkspace.Autorizaciones_AdminsData.CorreosDeAvisos where o.Nombre == "Administrador(a) Rol Privado" select o);

                                if (CorreoNotificacion.First().Email != null)
                                {
                                    correo.Mail(CorreoNotificacion.First().Email, asunto, mensaje);
                                }
                            }
                            else
                            {
                                mensaje = "Estimado(a) Administrador(a) RR.HH:\n" + "Una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " ha completado todas las aprobaciones necesarias.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla..";
                                asunto = "Solicitud aprobada por todos los superiores correspondientes";
                                
                                DataWorkspace dataWorkspace = new DataWorkspace();
                                var CorreoNotificacion = (from o in dataWorkspace.Autorizaciones_AdminsData.CorreosDeAvisos where o.Nombre == "Administrador(a) Recursos Humanos" select o);
                                
                                if (CorreoNotificacion.First().Email != null)
                                {
                                    correo.Mail(CorreoNotificacion.First().Email, asunto, mensaje);
                                }
                            }

                            if(entity.HorasExtras == true)
                            {
                                if (entity.Colacion == true && entity.Taxi == true)
                                {
                                    mensaje = "Estimado(a):\n" + "Una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " ha completado todas las aprobaciones necesarias.\nLa fecha de realización es para el " + entity.Inicio.Value + " y tanto TAXI como COLACIÓN han sido requeridos.\n\nEmail generado automáticamente. No responder a esta casilla..";
                                }
                                else if (entity.Colacion == true)
                                {
                                    mensaje = "Estimado(a):\n" + "Una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " ha completado todas las aprobaciones necesarias.\nLa fecha de realización es para el " + entity.Inicio.Value + " y se ha requerido TAXI.\n\nEmail generado automáticamente. No responder a esta casilla..";
                                }
                                else if ( entity.Taxi == true)
                                {
                                    mensaje = "Estimado(a):\n" + "Una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " ha completado todas las aprobaciones necesarias.\nLa fecha de realización es para el " + entity.Inicio.Value + " y se ha requerido COLACIÓN .\n\nEmail generado automáticamente. No responder a esta casilla..";
                                }

                                asunto = "Solicitud aprobada por todos los superiores correspondientes";

                                DataWorkspace dataWorkspace = new DataWorkspace();
                                var CorreoNotificacion = (from o in dataWorkspace.Autorizaciones_AdminsData.CorreosDeAvisos where o.Nombre == "Notificar pedir colación y/o taxi" select o);

                                if (CorreoNotificacion.First().Email != null)
                                {
                                    correo.Mail(CorreoNotificacion.First().Email, asunto, mensaje);
                                }
                            }
                        }
                        else
                        {
                            if (entity.VB_JefeDirecto == false)
                            {
                                asunto = "Tiene una solicitud en espera de su aprobación";
                                string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.NombreAD + ".\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como jefe(a) de área.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                                correo.Mail(destinatario, asunto, mensaje);
                            }
                            else
                                if (entity.VB_SubGerente == false)
                                {
                                    asunto = "Tiene una solicitud en espera de su aprobación";
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.NombreAD + ".\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como subgerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                                    correo.Mail(destinatario, asunto, mensaje);
                                }
                                else
                                    if (entity.VB_Gerente == false)
                                    {
                                        asunto = "Tiene una solicitud en espera de su aprobación";
                                        if (entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem != null)
                                        {
                                            string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ".\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla.";
                                            correo.Mail(destinatario, asunto, mensaje);
                                        }
                                        else
                                        {
                                            string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ".\nTiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente.\nPor favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows a través de Internet explorer para más detalles.\n\nEmail generado automáticamente. No responder a esta casilla."; 
                                            correo.Mail(destinatario, asunto, mensaje);
                                        }
                                    }
                        }
                //entity.EmailProximoDestinatario = null;
            }
        }

        //Consultar rut usando nombre de AD
        partial void ConsultarRutUsuarioAD_Inserting(ConsultarRutUsuarioADItem entity)
        {
            string[] props = { "displayname", "mail", "employeeID" };

            //var propResults = ActiveDirectoryInfo.UserPropertySearchByName(entity.NombreUsuario, "LDAP://afpplanvital.cl", props);
            //entity.RutUsuario = propResults["employeeID"];

            var propResults = ActiveDirectoryInfo.UserPropertySearchByName(entity.NombreUsuario, "LDAP://afpplanvital.cl", props);

            entity.RutUsuario = propResults["employeeID"];

            this.Details.DiscardChanges();
        }

        //Consultar email usando el rut
        partial void ConsultarEmailUsuarioAD_Inserting(ConsultarEmailUsuarioADItem entity)
        {
            string[] props = { "displayname", "mail", "employeeID" };

            //var propResults = ActiveDirectoryInfo.UserPropertySearchByName(entity.NombreUsuario, "LDAP://afpplanvital.cl", props);
            //entity.RutUsuario = propResults["employeeID"];

            var propResults = ActiveDirectoryInfo.UserPropertyBuscarPorRut(entity.RutUsuario, "LDAP://afpplanvital.cl", props);

            entity.EmailUsuario = propResults["mail"];

            this.Details.DiscardChanges();
        }
    }
}
