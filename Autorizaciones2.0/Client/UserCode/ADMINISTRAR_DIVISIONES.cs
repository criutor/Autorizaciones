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
    public partial class ADMINISTRAR_DIVISIONES
    {
        partial void SeleccionarGerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Division_Gerencia.SelectedItem.Superior_GerenteQuery.FirstOrDefault() == null)
            {

                this.Application.ShowPERSONAL_LISTAR_ROLPRIVADO(1, this.Division_Gerencia.SelectedItem.Id_Gerencia);
                
            }
            else{
                this.ShowMessageBox("Esta gerencia ya tiene un gerente asignado");
                
                }
        }

        partial void SeleccionarSubGerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Division_SubGerencia.SelectedItem.Superior_SubGerenteQuery.FirstOrDefault() == null)
            {
                this.Application.ShowPERSONAL_LISTAR_ROLPRIVADO(3, this.Division_SubGerencia.SelectedItem.Id_SubGerencia);
                
            }
            else
            {
                this.ShowMessageBox("Esta subgerencia ya tiene un subgerente asignado");
                
            }
            
        }

        partial void SeleccionarJefeDeÁrea_Execute()
        {

            if (this.Division_Area.SelectedItem.Superior_JefeDirectoQuery.FirstOrDefault() == null)
            {
                this.Application.ShowPERSONAL_LISTAR_NOROLPRIVADO(2, this.Division_Area.SelectedItem.Id_Area);
                
            }
            else
            {
                this.ShowMessageBox("Esta área ya tiene un Jefe de área asignado");
            }
        }

        partial void QuitarGerente_Execute()
        {
            // Escriba el código aquí.
            if (this.Division_Gerencia.SelectedItem.Nombre == "GERENCIA GENERAL") { this.Division_Gerencia.SelectedItem.Superior_Gerente.First().PersonaItem1.Es_GerenteGeneral = false; }
            
            this.Division_Gerencia.SelectedItem.Superior_Gerente.First().PersonaItem1.Es_Gerente = false;

            this.Division_Gerencia.SelectedItem.Superior_Gerente.First().Delete();
            
            this.Division_Gerencia.SelectedItem.Gerente = null;

            this.Save();
        }

        partial void QuitarJefeDeÁrea_Execute()
        {
            // Escriba el código aquí.
            this.Division_Area.SelectedItem.Superior_JefeDirecto.First().PersonaItem1.Es_JefeDirecto = false;

            this.Division_Area.SelectedItem.Superior_JefeDirecto.First().Delete();

            this.Division_Area.SelectedItem.JefeDeArea = null;

            this.Save();
            
        }

        partial void QuitarSubGerente_Execute()
        {
            // Escriba el código aquí.
            this.Division_SubGerencia.SelectedItem.Superior_SubGerente.First().PersonaItem1.Es_SubGerente = false;

            this.Division_SubGerencia.SelectedItem.Superior_SubGerente.First().PersonaItem1.IDGerencia_para_subgerentes = null;

            this.Division_SubGerencia.SelectedItem.Superior_SubGerente.First().Delete();

            this.Division_SubGerencia.SelectedItem.SubGerente = null;

            this.Save();
        }

        partial void QuitarGerente_CanExecute(ref bool result)
        {

            if (this.Division_Gerencia.SelectedItem == null) { result = false; }
            else
                if (this.Division_Gerencia.SelectedItem.Superior_Gerente.Count() == 0) { result = false; }

        }

        partial void QuitarJefeDeÁrea_CanExecute(ref bool result)
        {

            if (this.Division_Area.SelectedItem == null) { result = false; }
                else
                    if (this.Division_Area.SelectedItem.Superior_JefeDirecto.Count() == 0) { result = false; }

        }

        partial void QuitarSubGerente_CanExecute(ref bool result)
        {
                if (this.Division_SubGerencia.SelectedItem == null) { result = false; }
                else
                    if (this.Division_SubGerencia.SelectedItem.Superior_SubGerente.Count() == 0) { result = false; }

        }

        partial void SeleccionarJefeDeÁrea_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Division_Area.SelectedItem == null) { result = false; }
            else if (this.Division_Area.SelectedItem.Superior_JefeDirecto.Count() > 0)
            {
                result = false;

            }
        }

        partial void SeleccionarSubGerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Division_SubGerencia.SelectedItem == null) { result = false; }
            else if (this.Division_SubGerencia.SelectedItem.Superior_SubGerente.Count() > 0)
                {
                    result = false;

                }
        }

        partial void SeleccionarGerente_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Division_Gerencia.SelectedItem == null) { result = false; }
                else if (this.Division_Gerencia.SelectedItem.Superior_Gerente.Count() > 0 )
                {
                    result = false;

                }
        }

        partial void Division_GerenciaAddAndEditNew_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("NuevaGerencia");

        }

        partial void Division_SubGerenciaAddAndEditNew_Execute()
        {
            // Escriba el código aquí.
            this.OpenModalWindow("NuevaSubgerencia");
        }

        partial void Division_AreaAddAndEditNew_Execute()
        {
            // Escriba el código aquí.
            this.IDGerenciaSelected = this.Division_Gerencia.SelectedItem.Id_Gerencia;
            this.OpenModalWindow("NuevaArea");
        }

        partial void NuevaArea_Execute()
        {
            // Escriba el código aquí.
            if (NombreDivision == null) { this.ShowMessageBox("El campo 'Nombre' no puede quedar en blanco"); }
            else{

            Division_AreaItem NuevaArea = new Division_AreaItem();
            NuevaArea.Detalles = "DETALLES";
            NuevaArea.Nombre = NombreDivision.ToUpper();
            NuevaArea.Division_GerenciaItem = Division_Gerencia.SelectedItem;
            
            NuevaArea.Division_SubGerenciaItem = this.SubgerenciaCampo;

            /*
             * Al agregar este nuevo campo en la tabla, podemos filtrar las areas que no tienen gerencia y las que tienen en el 
             * mismo control de autocompletar en las ventanas crear rol privado y detalles para el control de autocompletar de las areas
             * , lo he retirado por que causo un error en produccion.
             * 
            if (NuevaArea.Division_SubGerenciaItem == null)
            { NuevaArea.ConSubgerencia = null; }
            else { NuevaArea.ConSubgerencia = true; }
            */

            this.CloseModalWindow("NuevaArea");
            this.NombreDivision = null;
            this.Save();
            }
        }

        partial void NuevaGerencia_Execute()
        {
            // Escriba el código aquí.
            if (NombreDivision == null) { this.ShowMessageBox("El campo 'Nombre' no puede quedar en blanco"); }
            else
            {
                Division_GerenciaItem NuevaGerencia = new Division_GerenciaItem();
                NuevaGerencia.Nombre = NombreDivision.ToUpper();
                this.CloseModalWindow("NuevaGerencia");
                this.NombreDivision = null;
                this.Save();
            }
        }

        partial void NuevaSubgerencia_Execute()
        {
            // Escriba el código aquí.
            if (NombreDivision == null) { this.ShowMessageBox("El campo 'Nombre' no puede quedar en blanco"); }
            else
            {
                Division_SubGerenciaItem NuevaSubgerencia = new Division_SubGerenciaItem();
                NuevaSubgerencia.Nombre = NombreDivision.ToUpper();
                NuevaSubgerencia.Division_GerenciaItem = this.Division_Gerencia.SelectedItem;
                this.CloseModalWindow("NuevaSubgerencia");
                this.NombreDivision = null;
                this.Save();
            }
        }

        partial void CancelarNuevaArea_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("NuevaArea");
            this.NombreDivision = null;
        }

        partial void CancelarNuevaGerencia_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("NuevaGerencia");
            this.NombreDivision = null;
        }

        partial void CancelarNuevaSubgerencia_Execute()
        {
            // Escriba el código aquí.
            this.CloseModalWindow("NuevaSubgerencia");
            this.NombreDivision = null;
        }

 

        partial void Division_GerenciaDeleteSelected_Execute()
        {
            // Escriba el código aquí.
            try
            {
                this.IDGerenciaSelected = this.Division_Gerencia.SelectedItem.Id_Gerencia;
            }
            catch { }

            if (this.Division_Gerencia.SelectedItem.Nombre == "GERENCIA GENERAL")
            {
                this.ShowMessageBox("GERENCIA GENERAL no puede ser eliminada", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
            else if (this.Division_Gerencia.SelectedItem.Division_Area.Count() > 0 || this.Division_Gerencia.SelectedItem.Division_SubGerencia.Count() > 0)
            {
                this.ShowMessageBox("Primero debe eliminar las áreas y subgerencias asociadas","ACCIÓN DENEGADA",MessageBoxOption.Ok);
            }
            else if (this.CargoRolPrivado.Count() > 0)
            {
                this.ShowMessageBox("Primero debe eliminar los cargos para rol privado asociados a esta gerencia.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
            else
            {
                    this.Division_Gerencia.SelectedItem.Delete();
                    this.Save();
                    this.Refresh();                
            }

            this.IDGerenciaSelected = null;
        }

        partial void Division_SubGerenciaDeleteSelected_Execute()
        {
            try
            {
                this.IDSubGerenciaSelected = this.Division_SubGerencia.SelectedItem.Id_SubGerencia;
            }
            catch { }

            if (this.Division_SubGerencia.SelectedItem.Division_Area.Count() > 0 )
            {
                this.ShowMessageBox("Primero debe eliminar las áreas asociadas", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
            else if (this.CargoRolPrivado.Count() > 0)
            {
                this.ShowMessageBox("Primero debe eliminar los cargos para rol privado asociados a esta subgerencia.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
            else
            {
                this.Division_SubGerencia.SelectedItem.Delete();
                this.Save();
                this.Refresh();
            }

            this.IDSubGerenciaSelected = null;
        }



        partial void Division_AreaDeleteSelected_Execute()
        {
            try
            {
                this.IDAreaSelected = this.Division_Area.SelectedItem.Id_Area;
            }
            catch { }

            if (this.CargoRolPrivado.Count() > 0)
            {
                this.ShowMessageBox("Primero debe eliminar los cargos para rol privado asociados a esta área.", "ACCIÓN DENEGADA", MessageBoxOption.Ok);
            }
            else
            {
                System.Windows.MessageBoxResult result = this.ShowMessageBox("Sí elimina esta área, todos los empleados asociados quedarán sin área asignada. ¿Desea continuar?", "ADVERTENCIA", MessageBoxOption.YesNo);

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (PersonaItem persona in this.Division_Area.SelectedItem.Persona)
                        {
                            persona.AreaDeTrabajo = null;
                            persona.Es_JefeDirecto = false;
                        }
                    }
                    catch { }

                    if(this.Division_Area.SelectedItem.Superior_JefeDirecto.Count() > 0)
                    {
                        this.Division_Area.SelectedItem.Superior_JefeDirecto.First().Delete();
                    }

                    this.Division_Area.SelectedItem.Delete();
                    this.Save();
                    this.Refresh();
                }
            }
            this.IDAreaSelected = null;
        }

        partial void Division_AreaDeleteSelected_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Division_Area.SelectedItem == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }

        partial void Division_GerenciaDeleteSelected_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Division_Gerencia.SelectedItem == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }


        partial void Division_SubGerenciaDeleteSelected_CanExecute(ref bool result)
        {
            // Escriba el código aquí.
            if (this.Division_SubGerencia.SelectedItem == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }

    }
}
