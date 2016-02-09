using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Security.Server;
namespace LightSwitchApplication
{
    public partial class Autorizaciones_AdminsDataService
    {
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
            string[] porPalabrasNombre = entity.Nombres.Split(new Char[] { ' ' });
            string[] porPalabrasAPP = entity.AP_Paterno.Split(new Char[] { ' ' });
            string[] porPalabrasAPM = entity.AP_Materno.Split(new Char[] { ' ' });
            entity.NombreAD = porPalabrasAPP[0].ToUpper() + " " + porPalabrasAPM[0].ToUpper() + ", " + porPalabrasNombre[0].ToUpper();
            entity.NombreAD = removerSignosAcentos(entity.NombreAD);
        }
    }
}
