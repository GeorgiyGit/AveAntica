﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Server.Domain.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to External auth error.
        /// </summary>
        internal static string ExternalAuth {
            get {
                return ResourceManager.GetString("ExternalAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image bad request.
        /// </summary>
        internal static string ImageBadRequest {
            get {
                return ResourceManager.GetString("ImageBadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image not found.
        /// </summary>
        internal static string ImageNotFound {
            get {
                return ResourceManager.GetString("ImageNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image coudn`t be saved.
        /// </summary>
        internal static string ImageSave {
            get {
                return ResourceManager.GetString("ImageSave", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image type is incorrect.
        /// </summary>
        internal static string ImageType {
            get {
                return ResourceManager.GetString("ImageType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Language not found.
        /// </summary>
        internal static string LanguageNotFound {
            get {
                return ResourceManager.GetString("LanguageNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product bad id.
        /// </summary>
        internal static string ProductBadId {
            get {
                return ResourceManager.GetString("ProductBadId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ProductBadRequest.
        /// </summary>
        internal static string ProductBadRequest {
            get {
                return ResourceManager.GetString("ProductBadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product Type(status) not found.
        /// </summary>
        internal static string StatusNotFound {
            get {
                return ResourceManager.GetString("StatusNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User Id is incorrect.
        /// </summary>
        internal static string UserBadId {
            get {
                return ResourceManager.GetString("UserBadId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User creation failed.
        /// </summary>
        internal static string UserCreationFailed {
            get {
                return ResourceManager.GetString("UserCreationFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User is not exist.
        /// </summary>
        internal static string UserExists {
            get {
                return ResourceManager.GetString("UserExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User is not authorized.
        /// </summary>
        internal static string UserNotAuthorized {
            get {
                return ResourceManager.GetString("UserNotAuthorized", resourceCulture);
            }
        }
    }
}
