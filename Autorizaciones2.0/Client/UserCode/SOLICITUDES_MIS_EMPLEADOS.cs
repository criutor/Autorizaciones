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
    public partial class SOLICITUDES_MIS_EMPLEADOS
    {

        partial void SOLICITUDES_MIS_EMPLEADOS_Activated()
        {
            //Mostrar todas las solicitudes por defecto (Parametros de la query)

            this.TodasLasSolicitudes_Execute();
            this.FECHADESDE = null;
            this.FECHAHASTA = null;
            this.ADMINISTRATIVO = true;
            this.VACACIONES = true;
            this.OTROPERMISO = true;
            this.HORASEXTRAS = true;
            this.FiltroEstados = null;
            this.FALSAS = false;
            this.VERDADERAS = true;
            //this.Solicitud_Header.Load();

            //this.TodasLasSolicitudes_Execute();

            //****CAMBIAR POR RUT****
            NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper();

            //Verificar que que tipo de usuario esta ingresando a la pantalla.
            if (this.PersonaPorNombreAD.Count == 0) { this.MENSAJEPersonaNoCreada(); this.Close(true); } //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
            else
            {
                IDUsuario = PersonaPorNombreAD.First().Rut_Persona; // Filtramos que en las solicitudes no aparezcan las del mismo usuario.


                if (PersonaPorNombreAD.First().Es_Gerente == true)
                {
                    /*
                     * si solicitud es tipo horas extras
                     * si yo ya las he evaluado(rechazado o aprobado)
                     */

                    IDGERENCIA = PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                    VBGERENTE = true;
                }
                else if (PersonaPorNombreAD.First().Es_SubGerente == true)
                {
                    /*
                     * si yo ya las he evaluado(rechazado o aprobado)
                     */

                    IDSUBGERENCIA = PersonaPorNombreAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;
                    VBSUBGERENTE = true;
                }
                else if (PersonaPorNombreAD.First().Es_JefeDirecto == true)
                {
                    /*
                     * si yo ya las he evaluado(rechazado o aprobado)
                     */

                    IDAREA = PersonaPorNombreAD.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;
                    VBJEFEAREA = true;
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

        partial void FiltroEstados_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");

            //Si se escoge alguna de las tres opciones de búsqueda, no aplicar los filtros FALSAS ni VERDADERAS
            if (FiltroEstados != null)
            {
                this.FALSAS = null; this.VERDADERAS = null; //VBGERENTE = null; VBSUBGERENTE = null; VBJEFEDIRECTO = null;
            }
            //Al cambiar la opción se cambian los filtros
            if (FiltroEstados == "Rechazadas") { 
                
                /*
                 * Por mi o despues que yo las haya aprobado                 
                 */
                this.Rechazada = true; this.Completada = false; this.Cancelada = false; }
            else
                if (FiltroEstados == "Aprobadas") {

               /*
               * solo si yo participé en la aprobación                 
               */
                    this.Completada = true; this.Rechazada = false; this.Cancelada = false;  }
                else
                    if (FiltroEstados == "Abiertas") {
               /*
               * solo si yo ya la aprobé                 
               */
                        this.Rechazada = false; this.Completada = false; this.Cancelada = false; }
                    else
                        if (FiltroEstados == "Canceladas") {

                            /*
                            * despues que yo las haya aprobado               
                            */

                            this.Rechazada = false; this.Completada = false; this.Cancelada = true;  }
                        else
            if (FiltroEstados == "Todos los estados") 
                        {
                        
                            this.FECHADESDE = null;
                            this.FECHAHASTA = null;
                            this.ADMINISTRATIVO = true;
                            this.VACACIONES = true;
                            this.OTROPERMISO = true;
                            this.HORASEXTRAS = true;
                            this.FiltroEstados = null;
                            this.FALSAS = false;
                            this.VERDADERAS = true;
                            //this.Solicitud_Header.Load();
                        }

            /*else
                if (RechazadaAprobadaAbierta == "Falta mi aprobación") 
                { 
                    this.Rechazada = false; this.Completada = false;

                    if (PersonaPorNombreAD.First().Es_Gerente == true)
                    {
                        //VBGERENTE = false;
                        VBSUBGERENTE = true;
                        VBJEFEDIRECTO = true;
                    }
                    else if (PersonaPorNombreAD.First().Es_SubGerente == true)
                    {
                        //VBGERENTE = false;
                        VBSUBGERENTE = false;
                        VBJEFEDIRECTO = true;
                    }
                    else if (PersonaPorNombreAD.First().Es_JefeDirecto == true)
                    {
                        VBJEFEDIRECTO = false; // Filtrar las solicitudes donde el jefe directo aun no las ha aprobado(visto bueno = false)

                        if (PersonaPorNombreAD.First().Division_AreaItem.Division_SubGerenciaItem == null)
                        {
                            VBSUBGERENTE = true;
                        }
                        else { VBSUBGERENTE = false; }

                        //VBGERENTE = false;

                    }
                }*/

        }


        /*
        partial void MasDetalles_Execute()
        {
            // Escriba el código aquí.
            if (this.Solicitud_Header.SelectedItem.Administrativo == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Administrativo.First().Id_Administrativo, 1, 3);
            }
            if (this.Solicitud_Header.SelectedItem.Vacaciones == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Vacaciones.First().Id_Vacaciones, 2, 3);
            }
            if (this.Solicitud_Header.SelectedItem.HorasExtras == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_HorasExtras.First().Id_HorasExtras, 3, 3);
            }
            if (this.Solicitud_Header.SelectedItem.OtroPermiso == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_OtroPermiso.First().Id_OtroPermiso, 4, 3);
            }

        }
        */

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
        /*
        partial void Persona_Validate(ScreenValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Mensaje de error>");
            try
            {
                this.EmpleadoFiltroSolicitudes = this.Persona.SelectedItem.Rut_Persona;
                this.NombreEmpleadoSeleccionado = this.Persona.SelectedItem.NombreAD;
            }
            catch { }
        }
        */
        partial void SeleccionarEmpleado_Execute()
        {
            // Escriba el código aquí.

            try
            {
                this.EmpleadoFiltroSolicitudes = this.Persona.SelectedItem.Rut_Persona;
                this.NombreEmpleadoSeleccionado = this.Persona.SelectedItem.NombreAD;
            }
            catch { }

            this.CloseModalWindow("Empleados");
        }

        partial void LimpiarFiltros_Execute()
        {
            // Escriba el código aquí.
            this.FiltroEstados = null;
            this.FECHADESDE = null;
            this.FECHAHASTA = null;
            //this.ADMINISTRATIVO = false;
            //this.VACACIONES = false;
            //this.OTROPERMISO = false;
            //this.HORASEXTRAS = false;
            this.FALSAS = false;
            this.VERDADERAS = true;
            //this.SolicitudesPorPersona.Load();

        }

        partial void TodasLasSolicitudes_Execute()
        {
            // Escriba el código aquí.
            this.EmpleadoFiltroSolicitudes = null;
            this.NombreEmpleadoSeleccionado = null;

        }

    }
}
