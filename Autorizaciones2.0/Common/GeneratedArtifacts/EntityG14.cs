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
    public sealed partial class ConsultarEmailUsuarioADItem : global::Microsoft.LightSwitch.Framework.Base.EntityObject<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass>
    {
        #region Constructors
    
        /// <summary>
        /// Inicializa una nueva instancia de la entidad ConsultarEmailUsuarioADItem.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ConsultarEmailUsuarioADItem()
            : this(null)
        {
        }
    
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ConsultarEmailUsuarioADItem(global::Microsoft.LightSwitch.Framework.EntitySet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem> entitySet)
            : base(entitySet)
        {
            global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.Initialize(this);
        }
    
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void ConsultarEmailUsuarioADItem_Created();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void ConsultarEmailUsuarioADItem_AllowSaveWithErrors(ref bool result);
    
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
        public int id
        {
            get
            {
                return global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.GetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.id);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void id_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void id_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void id_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RutUsuario
        {
            get
            {
                return global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.GetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.RutUsuario);
            }
            set
            {
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.SetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.RutUsuario, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void RutUsuario_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void RutUsuario_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void RutUsuario_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string NombreUsuario
        {
            get
            {
                return global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.GetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.NombreUsuario);
            }
            set
            {
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.SetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.NombreUsuario, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NombreUsuario_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NombreUsuario_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NombreUsuario_Changed();

        /// <summary>
        /// No hay ninguna descripción modelada
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string EmailUsuario
        {
            get
            {
                return global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.GetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.EmailUsuario);
            }
            set
            {
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.SetValue(this, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.EmailUsuario, value);
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void EmailUsuario_IsReadOnly(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void EmailUsuario_Validate(global::Microsoft.LightSwitch.EntityValidationResultsBuilder results);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void EmailUsuario_Changed();

        #endregion
    
        #region Details Class
    
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public sealed class DetailsClass : global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem,
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass,
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.IImplementation,
                global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySet,
                global::Microsoft.LightSwitch.Details.Framework.EntityCommandSet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass>,
                global::Microsoft.LightSwitch.Details.Framework.EntityMethodSet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass>>
        {
    
            static DetailsClass()
            {
                var initializeEntry = global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.id;
            }
    
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private static readonly global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass>.Entry
                __ConsultarEmailUsuarioADItemEntry = new global::Microsoft.LightSwitch.Details.Framework.Base.EntityDetails<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass>.Entry(
                    global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.__ConsultarEmailUsuarioADItem_CreateNew,
                    global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.__ConsultarEmailUsuarioADItem_Created,
                    global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.__ConsultarEmailUsuarioADItem_AllowSaveWithErrors);
            private static global::LightSwitchApplication.ConsultarEmailUsuarioADItem __ConsultarEmailUsuarioADItem_CreateNew(global::Microsoft.LightSwitch.Framework.EntitySet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem> es)
            {
                return new global::LightSwitchApplication.ConsultarEmailUsuarioADItem(es);
            }
            private static void __ConsultarEmailUsuarioADItem_Created(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
            {
                e.ConsultarEmailUsuarioADItem_Created();
            }
            private static bool __ConsultarEmailUsuarioADItem_AllowSaveWithErrors(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
            {
                bool result = false;
                e.ConsultarEmailUsuarioADItem_AllowSaveWithErrors(ref result);
                return result;
            }
    
            public DetailsClass() : base()
            {
            }
    
            public new global::Microsoft.LightSwitch.Details.Framework.EntityCommandSet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass> Commands
            {
                get
                {
                    return base.Commands;
                }
            }
    
            public new global::Microsoft.LightSwitch.Details.Framework.EntityMethodSet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass> Methods
            {
                get
                {
                    return base.Methods;
                }
            }
    
            public new global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySet Properties
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
            public sealed class PropertySet : global::Microsoft.LightSwitch.Details.Framework.Base.EntityPropertySet<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass>
            {
    
                public PropertySet() : base()
                {
                }
    
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, int> id
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.id) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, int>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string> RutUsuario
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.RutUsuario) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string> NombreUsuario
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.NombreUsuario) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>;
                    }
                }
                
                public global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string> EmailUsuario
                {
                    get
                    {
                        return base.GetItem(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties.EmailUsuario) as global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>;
                    }
                }
                
            }
    
            #pragma warning disable 109
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
            public interface IImplementation : global::Microsoft.LightSwitch.Internal.IEntityImplementation
            {
                new int id { get; }
                new string RutUsuario { get; set; }
                new string NombreUsuario { get; set; }
                new string EmailUsuario { get; set; }
            }
            #pragma warning restore 109
    
            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal class PropertySetProperties
            {
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, int>.Entry
                    id = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, int>.Entry(
                        "id",
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._id_Stub,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._id_ComputeIsReadOnly,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._id_Validate,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._id_GetImplementationValue,
                        null,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._id_OnValueChanged);
                private static void _id_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, int>.Data> c, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, object sf)
                {
                    c(d, ref d._id, sf);
                }
                private static bool _id_ComputeIsReadOnly(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    bool result = false;
                    e.id_IsReadOnly(ref result);
                    return result;
                }
                private static void _id_Validate(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.id_Validate(r);
                }
                private static int _id_GetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d)
                {
                    return d.ImplementationEntity.id;
                }
                private static void _id_OnValueChanged(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    e.id_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Entry
                    RutUsuario = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Entry(
                        "RutUsuario",
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._RutUsuario_Stub,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._RutUsuario_ComputeIsReadOnly,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._RutUsuario_Validate,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._RutUsuario_GetImplementationValue,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._RutUsuario_SetImplementationValue,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._RutUsuario_OnValueChanged);
                private static void _RutUsuario_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Data> c, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, object sf)
                {
                    c(d, ref d._RutUsuario, sf);
                }
                private static bool _RutUsuario_ComputeIsReadOnly(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    bool result = false;
                    e.RutUsuario_IsReadOnly(ref result);
                    return result;
                }
                private static void _RutUsuario_Validate(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.RutUsuario_Validate(r);
                }
                private static string _RutUsuario_GetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d)
                {
                    return d.ImplementationEntity.RutUsuario;
                }
                private static void _RutUsuario_SetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, string v)
                {
                    d.ImplementationEntity.RutUsuario = v;
                }
                private static void _RutUsuario_OnValueChanged(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    e.RutUsuario_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Entry
                    NombreUsuario = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Entry(
                        "NombreUsuario",
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._NombreUsuario_Stub,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._NombreUsuario_ComputeIsReadOnly,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._NombreUsuario_Validate,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._NombreUsuario_GetImplementationValue,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._NombreUsuario_SetImplementationValue,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._NombreUsuario_OnValueChanged);
                private static void _NombreUsuario_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Data> c, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, object sf)
                {
                    c(d, ref d._NombreUsuario, sf);
                }
                private static bool _NombreUsuario_ComputeIsReadOnly(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    bool result = false;
                    e.NombreUsuario_IsReadOnly(ref result);
                    return result;
                }
                private static void _NombreUsuario_Validate(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.NombreUsuario_Validate(r);
                }
                private static string _NombreUsuario_GetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d)
                {
                    return d.ImplementationEntity.NombreUsuario;
                }
                private static void _NombreUsuario_SetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, string v)
                {
                    d.ImplementationEntity.NombreUsuario = v;
                }
                private static void _NombreUsuario_OnValueChanged(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    e.NombreUsuario_Changed();
                }
    
                [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
                public static readonly global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Entry
                    EmailUsuario = new global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Entry(
                        "EmailUsuario",
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._EmailUsuario_Stub,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._EmailUsuario_ComputeIsReadOnly,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._EmailUsuario_Validate,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._EmailUsuario_GetImplementationValue,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._EmailUsuario_SetImplementationValue,
                        global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass.PropertySetProperties._EmailUsuario_OnValueChanged);
                private static void _EmailUsuario_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Data> c, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, object sf)
                {
                    c(d, ref d._EmailUsuario, sf);
                }
                private static bool _EmailUsuario_ComputeIsReadOnly(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    bool result = false;
                    e.EmailUsuario_IsReadOnly(ref result);
                    return result;
                }
                private static void _EmailUsuario_Validate(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e, global::Microsoft.LightSwitch.EntityValidationResultsBuilder r)
                {
                    e.EmailUsuario_Validate(r);
                }
                private static string _EmailUsuario_GetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d)
                {
                    return d.ImplementationEntity.EmailUsuario;
                }
                private static void _EmailUsuario_SetImplementationValue(global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass d, string v)
                {
                    d.ImplementationEntity.EmailUsuario = v;
                }
                private static void _EmailUsuario_OnValueChanged(global::LightSwitchApplication.ConsultarEmailUsuarioADItem e)
                {
                    e.EmailUsuario_Changed();
                }
    
            }
    
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, int>.Data _id;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Data _RutUsuario;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Data _NombreUsuario;
            
            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private global::Microsoft.LightSwitch.Details.Framework.EntityStorageProperty<global::LightSwitchApplication.ConsultarEmailUsuarioADItem, global::LightSwitchApplication.ConsultarEmailUsuarioADItem.DetailsClass, string>.Data _EmailUsuario;
            
        }
    
        #endregion
    }
    
    #endregion
}
