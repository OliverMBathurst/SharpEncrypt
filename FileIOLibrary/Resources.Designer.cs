﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileIOLibrary {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FileIOLibrary.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to buffer.
        /// </summary>
        internal static string buffer {
            get {
                return ResourceManager.GetString("buffer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Buffer has not been cleared by a write operation..
        /// </summary>
        internal static string BufferNotCleared {
            get {
                return ResourceManager.GetString("BufferNotCleared", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Buffer not initialized..
        /// </summary>
        internal static string BufferNotInitialized {
            get {
                return ResourceManager.GetString("BufferNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid char at index {0}..
        /// </summary>
        internal static string InvalidCharAt {
            get {
                return ResourceManager.GetString("InvalidCharAt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to path.
        /// </summary>
        internal static string path {
            get {
                return ResourceManager.GetString("path", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String not of length 8..
        /// </summary>
        internal static string WrongStringLength {
            get {
                return ResourceManager.GetString("WrongStringLength", resourceCulture);
            }
        }
    }
}
