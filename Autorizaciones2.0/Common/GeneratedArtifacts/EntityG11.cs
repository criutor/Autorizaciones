﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication
{
    #region Entities
    
    /// <summary>
    /// No hay ninguna descripción modelada
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    public sealed partial class Solicitud_Estados_VacacionesItem : global::Microsoft.LightSwitch.Framework.Base.EntityObject<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass>
    {
        #region Constructors
    
        /// <summary>
        /// Inicializa una nueva instancia de la entidad Solicitud_Estados_VacacionesItem.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Solicitud_Estados_VacacionesItem()
            : this(null)
        {
        }
    
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Solicitud_Estados_VacacionesItem(global::Microsoft.LightSwitch.Framework.EntitySet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem> entitySet)
            : base(entitySet)
        {
            global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.Initialize(this);
        }
    
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Estados_VacacionesItem_Created();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Estados_VacacionesItem_AllowSaveWithErrors(ref bool result);
    
        #endregion
    
        #region Private Properties
        
        /// <summary>
        /// Obtiene el objeto Application para esta aplicación. El objeto Application proporciona acceso a pantallas activas, métodos para abrir pantallas y acceso al usuario actual.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::Microsoft.LightSwitch.IApplication<global::LightSwitchApplication.DataWorkspace> Application
        {
            get
            {
                return global::LightSwitchApplication.Application.Current;
            }
        }
        
        /// <summary>
        /// Obtiene el área de trabajo de datos contenedora. Dicha área proporciona acceso a todos los orígenes de datos de la aplicación.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::LightSwitchApplication.DataWorkspace DataWorkspace
        {
            get
            {
                return (global::LightSwitchApplication.DataWorkspace)this.Details.EntitySet.Details.DataService.Details.DataWorkspace;
            }
        }
        
        #endregion
    
        #region Public Properties
    
        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public int Id_Estados_vacaciones
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Id_Estados_vacaciones);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Id_Estados_vacaciones_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Id_Estados_vacaciones_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Id_Estados_vacaciones_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Observaciones
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Observaciones);
            }
            set
            {
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.SetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Observaciones, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Observaciones_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Observaciones_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Observaciones_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string TituloObservacion
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.TituloObservacion);
            }
            set
            {
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.SetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.TituloObservacion, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void TituloObservacion_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void TituloObservacion_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void TituloObservacion_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem Solicitud_Detalle_VacacionesItem
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_VacacionesItem);
            }
            set
            {
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.SetValue(this, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_VacacionesItem, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Detalle_VacacionesItem_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Detalle_VacacionesItem_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Detalle_VacacionesItem_Changed();

        #endregion
    
        #region Details Class
    
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public sealed class DetailsClass : global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem,
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass,
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.IImplementation,
                global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySet,
                global::Microsoft.LightSwitch.Details.Framework.EntityCommandSet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass>,
                global::Microsoft.LightSwitch.Details.Framework.EntityMethodSet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass>>
        {
    
            static DetailsClass()
            {
                var initializeEntry = global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Id_Estados_vacaciones;
            }
    
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private static readonly global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass>.Entry
                __Solicitud_Estados_VacacionesItemEntry = new global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass>.Entry(
                    global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.__Solicitud_Estados_VacacionesItem_CreateNew,
                    global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.__Solicitud_Estados_VacacionesItem_Created,
                    global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.__Solicitud_Estados_VacacionesItem_AllowSaveWithErrors);
            private static global::LightSwitchApplication.Solicitud_Estados_VacacionesItem __Solicitud_Estados_VacacionesItem_CreateNew(global::Microsoft.LightSwitch.Framework.EntitySet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem> es)
            {
                return new global::LightSwitchApplication.Solicitud_Estados_VacacionesItem(es);
            }
            private static void __Solicitud_Estados_VacacionesItem_Created(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
            {
                e.Solicitud_Estados_VacacionesItem_Created();
            }
            private static bool __Solicitud_Estados_VacacionesItem_AllowSaveWithErrors(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
            {
                bool result = false;
                e.Solicitud_Estados_VacacionesItem_AllowSaveWithErrors(ref result);
                return result;
            }
    
            public DetailsClass() : base()
            {
            }
    
            public new global::Microsoft.LightSwitch.Details.Framework.EntityCommandSet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass> Commands
            {
                get
                {
                    return base.Commands;
                }
            }
    
            public new global::Microsoft.LightSwitch.Details.Framework.EntityMethodSet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass> Methods
            {
                get
                {
                    return base.Methods;
                }
            }
    
            public new global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySet Properties
            {
                get
                {
                    return base.Properties;
                }
            }
    
            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class PropertySet : global::Microsoft.LightSwitch.Details.Framework.Base.EntityPropertySet<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass>
            {
    
                public PropertySet() : base()
                {
                }
    
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, int> Id_Estados_vacaciones
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Id_Estados_vacaciones) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, int>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string> Observaciones
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Observaciones) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string> TituloObservacion
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.TituloObservacion) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem> Solicitud_Detalle_VacacionesItem
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_VacacionesItem) as global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem>;
                    }
                }
                
            }
    
            #pragma warning disable 109
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
            public interface IImplementation : global::Microsoft.LightSwitch.Internal.IEntityImplementation
            {
                new int Id_Estados_vacaciones { get; }
                new string Observaciones { get; set; }
                new string TituloObservacion { get; set; }
                new global::Microsoft.LightSwitch.Internal.IEntityImplementation Solicitud_Detalle_VacacionesItem { get; set; }
            }
            #pragma warning restore 109
    
            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal class PropertySetProperties
            {
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, int>.Entry
                    Id_Estados_vacaciones = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, int>.Entry(
                        "Id_Estados_vacaciones",
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Id_Estados_vacaciones_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Id_Estados_vacaciones_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Id_Estados_vacaciones_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Id_Estados_vacaciones_GetImplementationValue,
                        null,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Id_Estados_vacaciones_OnValueChanged);
                private static void _Id_Estados_vacaciones_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, int>.Data> c, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, object sf)
                {
                    c(d, ref d._Id_Estados_vacaciones, sf);
                }
                private static bool _Id_Estados_vacaciones_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    bool result = false;
                    e.Id_Estados_vacaciones_IsReadOnly(ref result);
                    return result;
                }
                private static void _Id_Estados_vacaciones_Validate(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.Id_Estados_vacaciones_Validate(r);
                }
                private static int _Id_Estados_vacaciones_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d)
                {
                    return d.ImplementationEntity.Id_Estados_vacaciones;
                }
                private static void _Id_Estados_vacaciones_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    e.Id_Estados_vacaciones_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Entry
                    Observaciones = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Entry(
                        "Observaciones",
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Observaciones_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Observaciones_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Observaciones_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Observaciones_GetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Observaciones_SetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Observaciones_OnValueChanged);
                private static void _Observaciones_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Data> c, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, object sf)
                {
                    c(d, ref d._Observaciones, sf);
                }
                private static bool _Observaciones_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    bool result = false;
                    e.Observaciones_IsReadOnly(ref result);
                    return result;
                }
                private static void _Observaciones_Validate(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.Observaciones_Validate(r);
                }
                private static string _Observaciones_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d)
                {
                    return d.ImplementationEntity.Observaciones;
                }
                private static void _Observaciones_SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, string v)
                {
                    d.ImplementationEntity.Observaciones = v;
                }
                private static void _Observaciones_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    e.Observaciones_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Entry
                    TituloObservacion = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Entry(
                        "TituloObservacion",
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._TituloObservacion_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._TituloObservacion_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._TituloObservacion_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._TituloObservacion_GetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._TituloObservacion_SetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._TituloObservacion_OnValueChanged);
                private static void _TituloObservacion_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Data> c, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, object sf)
                {
                    c(d, ref d._TituloObservacion, sf);
                }
                private static bool _TituloObservacion_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    bool result = false;
                    e.TituloObservacion_IsReadOnly(ref result);
                    return result;
                }
                private static void _TituloObservacion_Validate(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.TituloObservacion_Validate(r);
                }
                private static string _TituloObservacion_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d)
                {
                    return d.ImplementationEntity.TituloObservacion;
                }
                private static void _TituloObservacion_SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, string v)
                {
                    d.ImplementationEntity.TituloObservacion = v;
                }
                private static void _TituloObservacion_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    e.TituloObservacion_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem>.Entry
                    Solicitud_Detalle_VacacionesItem = new global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem>.Entry(
                        "Solicitud_Detalle_VacacionesItem",
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_GetCoreImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_GetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_SetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_Refresh,
                        global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_VacacionesItem_OnValueChanged);
                private static void _Solicitud_Detalle_VacacionesItem_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem>.Data> c, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, object sf)
                {
                    c(d, ref d._Solicitud_Detalle_VacacionesItem, sf);
                }
                private static bool _Solicitud_Detalle_VacacionesItem_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    bool result = false;
                    e.Solicitud_Detalle_VacacionesItem_IsReadOnly(ref result);
                    return result;
                }
                private static void _Solicitud_Detalle_VacacionesItem_Validate(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.Solicitud_Detalle_VacacionesItem_Validate(r);
                }
                private static global::Microsoft.LightSwitch.Internal.IEntityImplementation _Solicitud_Detalle_VacacionesItem_GetCoreImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d)
                {
                    return d.ImplementationEntity.Solicitud_Detalle_VacacionesItem;
                }
                private static global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem _Solicitud_Detalle_VacacionesItem_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d)
                {
                    return d.GetImplementationValue<global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem.DetailsClass>(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_VacacionesItem, ref d._Solicitud_Detalle_VacacionesItem);
                }
                private static void _Solicitud_Detalle_VacacionesItem_SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem v)
                {
                    d.SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_VacacionesItem, ref d._Solicitud_Detalle_VacacionesItem, (i, ev) => i.Solicitud_Detalle_VacacionesItem = ev, v);
                }
                private static void _Solicitud_Detalle_VacacionesItem_Refresh(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass d)
                {
                    d.RefreshNavigationProperty(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_VacacionesItem, ref d._Solicitud_Detalle_VacacionesItem);
                }
                private static void _Solicitud_Detalle_VacacionesItem_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_VacacionesItem e)
                {
                    e.Solicitud_Detalle_VacacionesItem_Changed();
                }
    
            }
    
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, int>.Data _Id_Estados_vacaciones;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Data _Observaciones;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, string>.Data _TituloObservacion;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_VacacionesItem, global::LightSwitchApplication.Solicitud_Estados_VacacionesItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_VacacionesItem>.Data _Solicitud_Detalle_VacacionesItem;
            
        }
    
        #endregion
    }
    
    #endregion
}
