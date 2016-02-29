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
    public partial class Master_SolicitudesPendientes
    {
        
        partial void Master_SolicitudesPendientes_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Escriba el código aquí.

        }

        partial void Master_SolicitudesPendientes_Activated()
        {
            //--------------------------------Quitar acentos del nombre de active directory------------------------------

            string NAD = this.Application.User.FullName;
            int largo = NAD.Length;

            char[] NombreAD = new char[largo];
            int i = 0;
          
            foreach (char ch in NAD)
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
           // ----------------------------------------------------------------------------
            
            NOMBREAD = Nombreaux.ToUpper(); //****CAMBIAR POR RUT****

      
            if (Persona.Count == 0) { this.MENSAJEPersonaNoCreada(); this.Close(true); } //Si el usuario(Active directory) no ha sido asociado a un área de trabajo, el query "Persona" retornara 0 personas.   
            else
            {
                IDUsuario = Persona.First().Rut_Persona; // Filtramos que en las solicitudes no aparezcan las del mismo usuario.
                
                
                if (Persona.First().Es_Gerente == true)
                {
                    IDGERENCIA = Persona.First().Superior_GerenteQuery.First().Division_GerenciaItem.Id_Gerencia;

                    VBGERENTE = false;
                    VBSUBGERENTE = true;
                    VBJEFEDIRECTO = true;

                }
                else if (Persona.First().Es_SubGerente == true)
                {
                    IDSUBGERENCIA = Persona.First().Superior_SubGerenteQuery.First().Division_SubGerenciaItem.Id_SubGerencia;

                    
                    VBGERENTE = null;
                    VBSUBGERENTE = false;
                    VBJEFEDIRECTO = true;

                }
                else if (Persona.First().Es_JefeDirecto == true)
                {
                    IDAREA = Persona.First().Superior_JefeDirectoQuery.First().Division_AreaItem.Id_Area;

                    VBJEFEDIRECTO = false; // Filtrar las solicitudes donde el jefe directo aun no las ha aprobado(visto bueno = false)

                    if (Persona.First().Division_AreaItem.Division_SubGerenciaItem == null)
                    {
                        VBSUBGERENTE = true;
                    }
                    else {

                        IDSUBGERENCIA = Persona.First().Division_AreaItem.Division_SubGerenciaItem.Id_SubGerencia;

                        VBSUBGERENTE = false; }

                    VBGERENTE = false;
                    //IDSUBGERENCIA
                }
                else
                {
                    this.MENSAJE_NoEsUnSuperior(); this.Close(true);
                }

            }

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

        partial void DetallesSolicitud_Execute()
        {
            // Escriba el código aquí.

            if (this.Solicitud_Header.SelectedItem.Administrativo == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Administrativo.First().Id_Administrativo, 1, 2);
            }
            if (this.Solicitud_Header.SelectedItem.Vacaciones == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_Vacaciones.First().Id_Vacaciones, 2, 2);
            }
            if (this.Solicitud_Header.SelectedItem.HorasExtras == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_HorasExtras.First().Id_HorasExtras, 3, 2);
            }
            if (this.Solicitud_Header.SelectedItem.OtroPermiso == true)
            {
                this.Application.ShowSolicitudes_Ver_Aprobar_Rechazar(this.Solicitud_Header.SelectedItem.Solicitud_Detalle_OtroPermiso.First().Id_OtroPermiso, 4, 2);
            }

        }

    }
}
