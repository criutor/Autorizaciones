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
    public partial class SOLICITUDES_REALIZADAS
    {

        partial void SOLICITUDES_REALIZADAS_Activated()
        {
            //Mostrar todas las solicitudes por defecto (Parametros de la query)
            this.FECHADESDE = null;
            this.FECHAHASTA = null;
            this.ADMINISTRATIVO = true;
            this.VACACIONES = true;
            this.OTROPERMISO = true;
            this.HORASEXTRAS = true;

            //****CAMBIAR POR RUT****
            NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper();

            //Verificar que que tipo de usuario esta ingresando a la pantalla.
            if (this.PersonaPorNombreAD.Count == 0) { this.MENSAJEPersonaNoCreada(); this.Close(true); } //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
            else
            {
                Rut_Persona = PersonaPorNombreAD.First().Rut_Persona; // Filtramos que en las solicitudes no aparezcan las del mismo usuario.


                if (PersonaPorNombreAD.First().Es_Gerente == true)
                {
                    IDGERENCIA = PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                }
                else if (PersonaPorNombreAD.First().Es_SubGerente == true)
                {
                    IDSUBGERENCIA = PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;
                }
                else if (PersonaPorNombreAD.First().Es_JefeDirecto == true)
                {
                    IDAREA = PersonaPorNombreAD.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;
                }
                else
                {
                    this.MENSAJE_NoEsUnSuperior(); this.Close(true);
                }

            }

        }

        //Quitar acentos del nombre de active directory
        public static string removerSignosAcentos(String conAcentos)
        {
            int largo = conAcentos.Length;
            char[] NombreAD = new char[largo];
            int i = 0;

            foreach (char ch in conAcentos)
            {
                char val = ch;

                switch (val)
                {
                    case 'á':
                    case 'Á':
                        val = 'A'; break;
                    case 'é':
                    case 'É':
                        val = 'E'; break;
                    case 'í':
                    case 'Í':
                        val = 'I'; break;
                    case 'ó':
                    case 'Ó':
                        val = 'O'; break;
                    case 'ú':
                    case 'Ú':
                        val = 'U'; break;
                }
                NombreAD[i] = val;
                i++;
            }

            string Nombreaux = new string(NombreAD);

            return Nombreaux.ToUpper();
        }

        partial void MENSAJE_NoEsUnSuperior_Execute()
        {
            // Escriba el código aquí.

            this.ShowMessageBox("Lo sentimos, esta sección es para aprobar o rechazar solicitudes y tu nombre no está asociado a un cargo de Jefatura. Contacta al administrador si esto es un error", "NO POSEES UN CARGO DE JEFATURA!", MessageBoxOption.Ok);
        }

        partial void MENSAJEPersonaNoCreada_Execute()
        {
            // Escriba el código aquí.
            this.ShowMessageBox("Lo sentimos, tu perfil no aparece en nuestra base de datos. Contacta al administrador", "USUARIO NO ENCONTRADO!", MessageBoxOption.Ok);

        }

        partial void SolicitudesConFiltro_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                this.EmpleadoFiltroSolicitudes = this.PersonalBajoMiSupervision.SelectedItem.Rut_Persona;
                this.NombreEmpleadoSeleccionado = this.PersonalBajoMiSupervision.SelectedItem.NombreAD;
            }
            catch { }
        }

        partial void SeleccionarEmpleado_Execute()
        {
            this.CloseModalWindow("Empleados");
        }

        partial void LimpiarFiltros_Execute()
        {
            this.FECHADESDE = null;
            this.FECHAHASTA = null;
        }


        

    }
}
