﻿using System;
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
        

        partial void Master_SolicitudesPendientes_CanRun(ref bool result)
        {
            
            // Establece el resultado en el valor del campo deseado

            

            //Autorizaciones_AdminsData aut = new Autorizaciones_AdminsData();
            
            //List<PersonaItem> personas = new List<PersonaItem>();

            //aut.Persona.First().Es_Gerente = false;
            
            
                  


        }

        partial void Convenios_Colectivos_CrearListar_CanRun(ref bool result)
        {
            // Establece el resultado en el valor del campo deseado

            //result = this.User.HasPermission(Permissions.VerConveniosScreen);

        }

        partial void Solicitudes_Crear_CanRun(ref bool result, string IDPersona, int TIPOSOLICITUD)
        {
            // Establece el resultado en el valor del campo deseado
            
        }
    }
}
