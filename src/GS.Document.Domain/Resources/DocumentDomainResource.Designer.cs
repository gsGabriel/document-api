﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS.Document.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DocumentDomainResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DocumentDomainResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GS.Document.Domain.Resources.DocumentDomainResource", typeof(DocumentDomainResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to Tipo de conteúdo não permitido..
        /// </summary>
        internal static string ContentTypeNotAllowed {
            get {
                return ResourceManager.GetString("ContentTypeNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O tipo de conteúdo deve ser preenchido..
        /// </summary>
        internal static string ContentTypeRequired {
            get {
                return ResourceManager.GetString("ContentTypeRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O identificador do cliente deve ser preenchido..
        /// </summary>
        internal static string CustomerIdRequired {
            get {
                return ResourceManager.GetString("CustomerIdRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Um documento com este nome já existe..
        /// </summary>
        internal static string ExistingDocument {
            get {
                return ResourceManager.GetString("ExistingDocument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O nome do arquivo deve ser preenchido..
        /// </summary>
        internal static string FileNameRequired {
            get {
                return ResourceManager.GetString("FileNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O caminho do arquivo deve ser preenchido..
        /// </summary>
        internal static string PathRequired {
            get {
                return ResourceManager.GetString("PathRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Uma ou mais falhas de validação ocorreram: {0}.
        /// </summary>
        internal static string ValidationFailure {
            get {
                return ResourceManager.GetString("ValidationFailure", resourceCulture);
            }
        }
    }
}
