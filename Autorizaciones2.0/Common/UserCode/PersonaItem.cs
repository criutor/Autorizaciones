using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class PersonaItem
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

        partial void NombreCompleto_Compute(ref string result)
        {
            // Establece el resultado en el valor del campo deseado

            string[] porPalabrasNombre = this.Nombres.Split(new Char[] { ' ' });
            string[] porPalabrasAPP = this.AP_Paterno.Split(new Char[] { ' ' });
            string[] porPalabrasAPM = this.AP_Paterno.Split(new Char[] { ' ' });
            result = porPalabrasAPP[0] + " " + porPalabrasAPM[0] + ", " + porPalabrasNombre[0];
            result = removerSignosAcentos(result).ToUpper();

            
        }
    }
}
