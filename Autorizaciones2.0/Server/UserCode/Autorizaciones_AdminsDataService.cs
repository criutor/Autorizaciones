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
            entity.NombreAD = removerSignosAcentos(entity.NombreAD);
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
            //string destinatario = entity.EmailProximoDestinatario;
            string destinatario = "cesar.riutor@planvital.cl";
            string asunto = "TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN";
            

            LightSwitchApplication.UserCode.EnviaMail correo = new UserCode.EnviaMail();

            //Si es de solicitud de horas extras
            if (entity.HorasExtras == true)
            {
                string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ". " + "Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " en espera de su aceptación. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
            }
            //todas las otras solicitudes
            else
            {
                if (entity.VB_GerenteGeneral == false)
                {
                    string mensaje = "Estimado Gerente General" + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles. ";
                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                }

                if (entity.VB_JefeDirecto == false)
                {
                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como jefe(a) de área. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                }
                else
                    if (entity.VB_SubGerente == false)
                    {
                        string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como subgerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                        correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                    }
                    else
                        if (entity.VB_Gerente == false)
                        {
                            if(entity.PersonaItem1.Division_AreaItem != null){

                                if(entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem != null)
                                {
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                                }
                                else
                                {
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                                }

                            }else
                            {
                                if (entity.PersonaItem1.Es_SubGerente == true)
                                {
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Superior_SubGerente.First().Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                                }

                                

                                
                            
                            }
                        }
            }

            //entity.EmailProximoDestinatario = null;
        }

        
        partial void SOLICITUDES_Updated(SOLICITUDESItem entity)
        {
            if(entity.Cancelada != true){

                //string destinatario = entity.EmailProximoDestinatario;
                string destinatario = "cesar.riutor@planvital.cl";
                string asunto;


                LightSwitchApplication.UserCode.EnviaMail correo = new UserCode.EnviaMail();

                if (entity.Rechazada == true)
                {
                    asunto = "SU SOLICITUD HA SIDO RECHAZADA";
                    string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ". " + "Su solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " ha sido " + entity.Estado + ". Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                }
                else
                    if (entity.Rebajada == true)
                    {
                        asunto = "SU SOLICITUD HA SIDO REBAJADA";
                        string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ". " + "Su solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " ha sido " + entity.Estado + ". Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                        correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                    }
                    else
                        if (entity.Completada == true)
                        {
                            asunto = "SU SOLICITUD HA SIDO APROBADA POR SUS SUPERIORES ";
                            string mensaje = "Estimado(a) " + entity.PersonaItem1.NombreAD + ". " + "Su solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " ha completado todas las aprobaciones necesarias. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                            correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                        }
                        else
                            {
                                if (entity.VB_JefeDirecto == false)
                                {
                                    asunto = "TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN";
                                    string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Superior_JefeDirecto.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como jefe(a) de área. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                    correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                                }
                                else
                                    if (entity.VB_SubGerente == false)
                                    {
                                        asunto = "TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN";
                                        string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Superior_SubGerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como subgerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                        correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                                    }
                                    else
                                        if (entity.VB_Gerente == false)
                                        {
                                            asunto = "TIENE UNA SOLICITUD EN ESPERA DE SU APROBACIÓN";
                                            if (entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem != null)
                                            {
                                                string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_SubGerenciaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                                correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
                                            }
                                            else
                                            {
                                                string mensaje = "Estimado(a) " + entity.PersonaItem1.Division_AreaItem.Division_GerenciaItem.Superior_Gerente.First().PersonaItem1.NombreAD + ". Tiene una solicitud del tipo " + entity.Titulo + " con fecha de solicitud " + entity.FechaSolicitud + " a nombre de " + entity.PersonaItem1.NombreAD + " en espera de su aprobación como gerente. Por favor diríjase a http://172.17.40.45/AutorizacionesAdministrativas/ e ingrese utilizando su usuario y clave de Windows para más detalles.";
                                                correo.Mail("documentos.super@planvital.cl", destinatario, asunto, mensaje);
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
