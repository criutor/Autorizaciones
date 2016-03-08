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

        partial void ConsultarInfoUsuarioAD_Inserting(ConsultarInfoUsuarioADItem entity)
        {
            string[] props = {  "displayname","mail","employeeID"};

            var propResults = ActiveDirectoryInfo.UserPropertySearchByName(entity.NombreUsuario, "LDAP://afpplanvital.cl", props);

            entity.EmailUsuario = propResults["mail"];
            entity.RutUsuario = propResults["employeeID"];

            this.Details.DiscardChanges();
        }


    }
}
