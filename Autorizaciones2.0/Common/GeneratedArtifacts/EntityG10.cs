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
    public sealed partial class Solicitud_Estados_OtroPermisoItem : global::Microsoft.LightSwitch.Framework.Base.EntityObject<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass>
    {
        #region Constructors
    
        /// <summary>
        /// Inicializa una nueva instancia de la entidad Solicitud_Estados_OtroPermisoItem.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Solicitud_Estados_OtroPermisoItem()
            : this(null)
        {
        }
    
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Solicitud_Estados_OtroPermisoItem(global::Microsoft.LightSwitch.Framework.EntitySet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem> entitySet)
            : base(entitySet)
        {
            global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.Initialize(this);
        }
    
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Estados_OtroPermisoItem_Created();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Estados_OtroPermisoItem_AllowSaveWithErrors(ref bool result);
    
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
        public int Id_Estados_OtroPermiso
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Id_Estados_OtroPermiso);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Id_Estados_OtroPermiso_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Id_Estados_OtroPermiso_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Id_Estados_OtroPermiso_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Observaciones
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Observaciones);
            }
            set
            {
                global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.SetValue(this, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Observaciones, value);
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
        public global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem Solicitud_Detalle_OtroPermisoItem
        {
            get
            {
                return global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.GetValue(this, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_OtroPermisoItem);
            }
            set
            {
                global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.SetValue(this, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_OtroPermisoItem, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Detalle_OtroPermisoItem_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Detalle_OtroPermisoItem_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Solicitud_Detalle_OtroPermisoItem_Changed();

        #endregion
    
        #region Details Class
    
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public sealed class DetailsClass : global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<
                global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem,
                global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass,
                global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.IImplementation,
                global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySet,
                global::Microsoft.LightSwitch.Details.Framework.EntityCommandSet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass>,
                global::Microsoft.LightSwitch.Details.Framework.EntityMethodSet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass>>
        {
    
            static DetailsClass()
            {
                var initializeEntry = global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Id_Estados_OtroPermiso;
            }
    
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private static readonly global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass>.Entry
                __Solicitud_Estados_OtroPermisoItemEntry = new global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass>.Entry(
                    global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.__Solicitud_Estados_OtroPermisoItem_CreateNew,
                    global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.__Solicitud_Estados_OtroPermisoItem_Created,
                    global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.__Solicitud_Estados_OtroPermisoItem_AllowSaveWithErrors);
            private static global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem __Solicitud_Estados_OtroPermisoItem_CreateNew(global::Microsoft.LightSwitch.Framework.EntitySet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem> es)
            {
                return new global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem(es);
            }
            private static void __Solicitud_Estados_OtroPermisoItem_Created(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
            {
                e.Solicitud_Estados_OtroPermisoItem_Created();
            }
            private static bool __Solicitud_Estados_OtroPermisoItem_AllowSaveWithErrors(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
            {
                bool result = false;
                e.Solicitud_Estados_OtroPermisoItem_AllowSaveWithErrors(ref result);
                return result;
            }
    
            public DetailsClass() : base()
            {
            }
    
            public new global::Microsoft.LightSwitch.Details.Framework.EntityCommandSet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass> Commands
            {
                get
                {
                    return base.Commands;
                }
            }
    
            public new global::Microsoft.LightSwitch.Details.Framework.EntityMethodSet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass> Methods
            {
                get
                {
                    return base.Methods;
                }
            }
    
            public new global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySet Properties
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
            public sealed class PropertySet : global::Microsoft.LightSwitch.Details.Framework.Base.EntityPropertySet<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass>
            {
    
                public PropertySet() : base()
                {
                }
    
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, int> Id_Estados_OtroPermiso
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Id_Estados_OtroPermiso) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, int>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, string> Observaciones
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Observaciones) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, string>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem> Solicitud_Detalle_OtroPermisoItem
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_OtroPermisoItem) as global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem>;
                    }
                }
                
            }
    
            #pragma warning disable 109
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
            public interface IImplementation : global::Microsoft.LightSwitch.Internal.IEntityImplementation
            {
                new int Id_Estados_OtroPermiso { get; }
                new string Observaciones { get; set; }
                new global::Microsoft.LightSwitch.Internal.IEntityImplementation Solicitud_Detalle_OtroPermisoItem { get; set; }
            }
            #pragma warning restore 109
    
            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal class PropertySetProperties
            {
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, int>.Entry
                    Id_Estados_OtroPermiso = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, int>.Entry(
                        "Id_Estados_OtroPermiso",
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Id_Estados_OtroPermiso_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Id_Estados_OtroPermiso_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Id_Estados_OtroPermiso_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Id_Estados_OtroPermiso_GetImplementationValue,
                        null,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Id_Estados_OtroPermiso_OnValueChanged);
                private static void _Id_Estados_OtroPermiso_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, int>.Data> c, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d, object sf)
                {
                    c(d, ref d._Id_Estados_OtroPermiso, sf);
                }
                private static bool _Id_Estados_OtroPermiso_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
                {
                    bool result = false;
                    e.Id_Estados_OtroPermiso_IsReadOnly(ref result);
                    return result;
                }
                private static void _Id_Estados_OtroPermiso_Validate(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.Id_Estados_OtroPermiso_Validate(r);
                }
                private static int _Id_Estados_OtroPermiso_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d)
                {
                    return d.ImplementationEntity.Id_Estados_OtroPermiso;
                }
                private static void _Id_Estados_OtroPermiso_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
                {
                    e.Id_Estados_OtroPermiso_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, string>.Entry
                    Observaciones = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, string>.Entry(
                        "Observaciones",
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Observaciones_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Observaciones_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Observaciones_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Observaciones_GetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Observaciones_SetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Observaciones_OnValueChanged);
                private static void _Observaciones_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, string>.Data> c, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d, object sf)
                {
                    c(d, ref d._Observaciones, sf);
                }
                private static bool _Observaciones_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
                {
                    bool result = false;
                    e.Observaciones_IsReadOnly(ref result);
                    return result;
                }
                private static void _Observaciones_Validate(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.Observaciones_Validate(r);
                }
                private static string _Observaciones_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d)
                {
                    return d.ImplementationEntity.Observaciones;
                }
                private static void _Observaciones_SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d, string v)
                {
                    d.ImplementationEntity.Observaciones = v;
                }
                private static void _Observaciones_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
                {
                    e.Observaciones_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem>.Entry
                    Solicitud_Detalle_OtroPermisoItem = new global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem>.Entry(
                        "Solicitud_Detalle_OtroPermisoItem",
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_Stub,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_ComputeIsReadOnly,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_Validate,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_GetCoreImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_GetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_SetImplementationValue,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_Refresh,
                        global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties._Solicitud_Detalle_OtroPermisoItem_OnValueChanged);
                private static void _Solicitud_Detalle_OtroPermisoItem_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem>.Data> c, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d, object sf)
                {
                    c(d, ref d._Solicitud_Detalle_OtroPermisoItem, sf);
                }
                private static bool _Solicitud_Detalle_OtroPermisoItem_ComputeIsReadOnly(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
                {
                    bool result = false;
                    e.Solicitud_Detalle_OtroPermisoItem_IsReadOnly(ref result);
                    return result;
                }
                private static void _Solicitud_Detalle_OtroPermisoItem_Validate(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.Solicitud_Detalle_OtroPermisoItem_Validate(r);
                }
                private static global::Microsoft.LightSwitch.Internal.IEntityImplementation _Solicitud_Detalle_OtroPermisoItem_GetCoreImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d)
                {
                    return d.ImplementationEntity.Solicitud_Detalle_OtroPermisoItem;
                }
                private static global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem _Solicitud_Detalle_OtroPermisoItem_GetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d)
                {
                    return d.GetImplementationValue<global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem.DetailsClass>(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_OtroPermisoItem, ref d._Solicitud_Detalle_OtroPermisoItem);
                }
                private static void _Solicitud_Detalle_OtroPermisoItem_SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem v)
                {
                    d.SetImplementationValue(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_OtroPermisoItem, ref d._Solicitud_Detalle_OtroPermisoItem, (i, ev) => i.Solicitud_Detalle_OtroPermisoItem = ev, v);
                }
                private static void _Solicitud_Detalle_OtroPermisoItem_Refresh(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass d)
                {
                    d.RefreshNavigationProperty(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass.PropertySetProperties.Solicitud_Detalle_OtroPermisoItem, ref d._Solicitud_Detalle_OtroPermisoItem);
                }
                private static void _Solicitud_Detalle_OtroPermisoItem_OnValueChanged(global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem e)
                {
                    e.Solicitud_Detalle_OtroPermisoItem_Changed();
                }
    
            }
    
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, int>.Data _Id_Estados_OtroPermiso;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, string>.Data _Observaciones;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityReferenceProperty<global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem, global::LightSwitchApplication.Solicitud_Estados_OtroPermisoItem.DetailsClass, global::LightSwitchApplication.Solicitud_Detalle_OtroPermisoItem>.Data _Solicitud_Detalle_OtroPermisoItem;
            
        }
    
        #endregion
    }
    
    #endregion
}
