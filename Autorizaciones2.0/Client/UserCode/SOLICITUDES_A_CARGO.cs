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
    public partial class SOLICITUDES_A_CARGO
    {

        partial void SOLICITUDES_A_CARGO_Activated()
        {
            this.ConsultarRutUsuarioAD_Execute();
            //Mostrar todas las solicitudes por defecto (Parametros de la query)
            this.TodosLosEmpleados_Execute();

            this.FiltroEstados = "Todas";

            //****CAMBIAR POR RUT****
            //NOMBREAD = removerSignosAcentos(this.Application.User.FullName).ToUpper();

            //Verificar que que tipo de usuario esta ingresando a la pantalla.
            if (this.PersonaPorRutAD.Count == 0) { this.MENSAJEPersonaNoCreada(); this.Close(true); } //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
            else
            {
                IDUsuario = PersonaPorRutAD.First().Rut_Persona; // Filtramos que en las solicitudes no aparezcan las del mismo usuario.

                if (PersonaPorRutAD.First().Es_GerenteGeneral == true)
                {
                    /*
                     * si solicitud es tipo horas extras
                     * si yo ya las he evaluado(rechazado o aprobado)
                     */

                    //IDGERENCIA = PersonaPorNombreAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                    VBGERENTEGENERAL = true;
                    SWITCHRUTGERENTE = null;//Traer a todos los gerentes(es opcional en la query)

                }else

                if (PersonaPorRutAD.First().Es_Gerente == true)
                {
                    /*
                     * si solicitud es tipo horas extras
                     * si yo ya las he evaluado(rechazado o aprobado)
                     */

                    IDGERENCIA = PersonaPorRutAD.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;
                    VBGERENTE = true;
                    SWITCHRUTGERENTE = "INVALIDAR";// No traerá a otros gerentes, ya que el rut no es valido
                }
                else if (PersonaPorRutAD.First().Es_SubGerente == true)
                {
                    /*
                     * si yo ya las he evaluado(rechazado o aprobado)
                     */

                    IDSUBGERENCIA = PersonaPorRutAD.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;
                    VBSUBGERENTE = true;
                    SWITCHRUTGERENTE = "INVALIDAR";
                }
                else if (PersonaPorRutAD.First().Es_JefeDirecto == true)
                {
                    IDAREA = PersonaPorRutAD.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;
                    VBJEFEAREA = true;
                    SWITCHRUTGERENTE = "INVALIDAR";
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
            if (FiltroEstados == "Rechazada")
            {

                this.Completada = false;
                this.Rechazada = true;
                this.Cancelada = false;
                this.Caducada = false;
                this.Rebajada = false;
            }
            else
                if (FiltroEstados == "Aprobada")
                {

                    this.Completada = true;
                    this.Rechazada = false;
                    this.Cancelada = false;
                    this.Caducada = false;
                    this.Rebajada = false;
                }
                else
                    if (FiltroEstados == "En aprobacion")
                    {

                        this.Completada = false;
                        this.Rechazada = false;
                        this.Cancelada = false;
                        this.Caducada = false;
                        this.Rebajada = false;
                    }
                    else
                        if (FiltroEstados == "Cancelada")
                        {

                            this.Completada = false;
                            this.Rechazada = false;
                            this.Cancelada = true;
                            this.Caducada = false;
                            this.Rebajada = false;
                        }
                        else
                            if (FiltroEstados == "Anulada")
                            {

                                this.Completada = false;
                                this.Rechazada = false;
                                this.Cancelada = false;
                                this.Caducada = true;
                                this.Rebajada = false;
                            }
                            else
                                if (FiltroEstados == "Rebajada")
                                {

                                    this.Completada = false;
                                    this.Rechazada = false;
                                    this.Cancelada = false;
                                    this.Caducada = false;
                                    this.Rebajada = true;
                                }
                                else
                                    if (FiltroEstados == "Todas")
                                    {

                                        //this.FechaSolicitudDesde = null;
                                        //this.FechaSolicitudHasta = null;

                                        this.Administrativo = true;
                                        this.Vacaciones = true;
                                        this.OtroPermiso = true;
                                        this.HorasExtras = true;
                                        this.FiltroEstados = null;

                                        this.Completada = null;
                                        this.Rechazada = null;
                                        this.Cancelada = null;
                                        this.Caducada = null;
                                        this.Rebajada = null;
                                        //this.Solicitud_Header.Load();
                                    }
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

            this.ShowMessageBox("Lo sentimos, esta sección es para revisar solicitudes del personal a tu cargo y tu nombre no está asociado a un cargo de Jefatura. Contacta al administrador si esto es un error", "NO POSEES UN CARGO DE JEFATURA!", MessageBoxOption.Ok);
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

        partial void LimpiarFechas_Execute()
        {
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;

            this.SOLICITUDES.Load();
        }

        partial void TodosLosEmpleados_Execute()
        {
            // Escriba el código aquí.
            this.EmpleadoFiltroSolicitudes = null;
            this.NombreEmpleadoSeleccionado = null;

            /*
            this.FechaSolicitudDesde = null;
            this.FechaSolicitudHasta = null;
            this.Administrativo = true;
            this.Vacaciones = true;
            this.OtroPermiso = true;
            this.HorasExtras = true;
            this.FiltroEstados = null;
            */
        }

        partial void ConsultarRutUsuarioAD_Execute()
        {
            // Escriba el código aquí.
            DataWorkspace dataWorkspace = new DataWorkspace();

            ConsultarRutUsuarioADItem operation =
                dataWorkspace.Autorizaciones_AdminsData.ConsultarRutUsuarioAD.AddNew();

            operation.NombreUsuario = this.Application.User.FullName;

            dataWorkspace.Autorizaciones_AdminsData.SaveChanges();

            this.RutUsuarioAD = operation.RutUsuario;
        }

    }
}
