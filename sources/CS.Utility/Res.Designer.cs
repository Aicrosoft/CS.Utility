﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CS {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Res {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Res() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CS.Res", typeof(Res).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 Only elements allowed. 的本地化字符串。
        /// </summary>
        internal static string Config_base_elements_only {
            get {
                return ResourceManager.GetString("Config_base_elements_only", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Child nodes not allowed. 的本地化字符串。
        /// </summary>
        internal static string Config_base_no_child_nodes {
            get {
                return ResourceManager.GetString("Config_base_no_child_nodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Required attribute &apos;{0}&apos; cannot be empty. 的本地化字符串。
        /// </summary>
        internal static string Config_base_required_attribute_empty {
            get {
                return ResourceManager.GetString("Config_base_required_attribute_empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Required attribute &apos;{0}&apos; not found. 的本地化字符串。
        /// </summary>
        internal static string Config_base_required_attribute_missing {
            get {
                return ResourceManager.GetString("Config_base_required_attribute_missing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 节点值无效，属性{0}与CDATA内容至少要有一个。 的本地化字符串。
        /// </summary>
        internal static string Config_base_required_attribute_none {
            get {
                return ResourceManager.GetString("Config_base_required_attribute_none", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Unrecognized attribute &apos;{0}&apos;. Note that attribute names are case-sensitive. 的本地化字符串。
        /// </summary>
        internal static string Config_base_unrecognized_attribute {
            get {
                return ResourceManager.GetString("Config_base_unrecognized_attribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Unrecognized element. 的本地化字符串。
        /// </summary>
        internal static string Config_base_unrecognized_element {
            get {
                return ResourceManager.GetString("Config_base_unrecognized_element", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The property &apos;{0}&apos; must have value &apos;true&apos; or &apos;false&apos;. 的本地化字符串。
        /// </summary>
        internal static string Config_invalid_boolean_attribute {
            get {
                return ResourceManager.GetString("Config_invalid_boolean_attribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The &apos;{0}&apos; attribute must be set to an integer value. 的本地化字符串。
        /// </summary>
        internal static string Config_invalid_integer_attribute {
            get {
                return ResourceManager.GetString("Config_invalid_integer_attribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The &apos;{0}&apos; attribute must be specified on the &apos;{1}&apos; tag. 的本地化字符串。
        /// </summary>
        internal static string Config_missing_required_attribute {
            get {
                return ResourceManager.GetString("Config_missing_required_attribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0}--{1} 的本地化字符串。
        /// </summary>
        internal static string Demo_Format {
            get {
                return ResourceManager.GetString("Demo_Format", resourceCulture);
            }
        }
    }
}