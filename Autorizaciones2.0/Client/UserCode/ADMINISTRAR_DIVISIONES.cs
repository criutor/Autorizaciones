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
            NuevaArea.Nombre = NombreDivision;
            NuevaArea.Division_GerenciaItem = Division_Gerencia.SelectedItem;
            
            NuevaArea.Division_SubGerenciaItem = this.SubgerenciaCampo;
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
                NuevaGerencia.Nombre = NombreDivision;
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
                NuevaSubgerencia.Nombre = NombreDivision;
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




       
    }
}
