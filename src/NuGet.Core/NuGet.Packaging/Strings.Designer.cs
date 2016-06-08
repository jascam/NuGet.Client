﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NuGet.Packaging {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Strings() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NuGet.Packaging.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unsupported targetFramework value &apos;{0}&apos;..
        /// </summary>
        public static string Error_InvalidTargetFramework {
            get {
                return ResourceManager.GetString("Error_InvalidTargetFramework", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to There are duplicate packages: {0}.
        /// </summary>
        public static string ErrorDuplicatePackages {
            get {
                return ResourceManager.GetString("ErrorDuplicatePackages", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Invalid allowedVersions for package id &apos;{0}&apos;: &apos;{1}&apos;.
        /// </summary>
        public static string ErrorInvalidAllowedVersions {
            get {
                return ResourceManager.GetString("ErrorInvalidAllowedVersions", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Invalid minClientVersion: &apos;{0}&apos;.
        /// </summary>
        public static string ErrorInvalidMinClientVersion {
            get {
                return ResourceManager.GetString("ErrorInvalidMinClientVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Invalid package version for package id &apos;{0}&apos;: &apos;{1}&apos;.
        /// </summary>
        public static string ErrorInvalidPackageVersion {
            get {
                return ResourceManager.GetString("ErrorInvalidPackageVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Null or empty package id.
        /// </summary>
        public static string ErrorNullOrEmptyPackageId {
            get {
                return ResourceManager.GetString("ErrorNullOrEmptyPackageId", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unable to delete temporary file &apos;{0}&apos;. Error: &apos;{1}&apos;..
        /// </summary>
        public static string ErrorUnableToDeleteFile {
            get {
                return ResourceManager.GetString("ErrorUnableToDeleteFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Failed to update file time for {0}: {1}.
        /// </summary>
        public static string FailedFileTime {
            get {
                return ResourceManager.GetString("FailedFileTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Fail to load packages.config as XML file. Please check it. .
        /// </summary>
        public static string FailToLoadPackagesConfig {
            get {
                return ResourceManager.GetString("FailToLoadPackagesConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Failed to write packages.config as XML file &apos;{0}&apos;. Error: &apos;{1}&apos;..
        /// </summary>
        public static string FailToWritePackagesConfig {
            get {
                return ResourceManager.GetString("FailToWritePackagesConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to {0} This validation error occurred in a &apos;{1}&apos; element..
        /// </summary>
        public static string InvalidNuspecElement {
            get {
                return ResourceManager.GetString("InvalidNuspecElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The nuspec contains an invalid entry &apos;{0}&apos; in package &apos;{1}&apos; ..
        /// </summary>
        public static string InvalidNuspecEntry {
            get {
                return ResourceManager.GetString("InvalidNuspecEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Installing {0} {1}..
        /// </summary>
        public static string Log_InstallingPackage {
            get {
                return ResourceManager.GetString("Log_InstallingPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to MinClientVersion already exists in packages.config.
        /// </summary>
        public static string MinClientVersionAlreadyExist {
            get {
                return ResourceManager.GetString("MinClientVersionAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Nuspec file does not exist in package..
        /// </summary>
        public static string MissingNuspec {
            get {
                return ResourceManager.GetString("MissingNuspec", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Package contains multiple nuspec files..
        /// </summary>
        public static string MultipleNuspecFiles {
            get {
                return ResourceManager.GetString("MultipleNuspecFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &apos;{0}&apos; must contain an absolute path &apos;{1}&apos;..
        /// </summary>
        public static string MustContainAbsolutePath {
            get {
                return ResourceManager.GetString("MustContainAbsolutePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Package entry already exists in packages.config. Id: {0}.
        /// </summary>
        public static string PackageEntryAlreadyExist {
            get {
                return ResourceManager.GetString("PackageEntryAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Package entry does not exists in packages.config. Id: {0}, Version: {1}.
        /// </summary>
        public static string PackageEntryNotExist {
            get {
                return ResourceManager.GetString("PackageEntryNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The &apos;{0}&apos; package requires NuGet client version &apos;{1}&apos; or above, but the current NuGet version is &apos;{2}&apos;. To upgrade NuGet, please go to http://docs.nuget.org/consume/installing-nuget.
        /// </summary>
        public static string PackageMinVersionNotSatisfied {
            get {
                return ResourceManager.GetString("PackageMinVersionNotSatisfied", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Packages node does not exists in packages.config at {0}..
        /// </summary>
        public static string PackagesNodeNotExist {
            get {
                return ResourceManager.GetString("PackagesNodeNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Package stream should be seekable.
        /// </summary>
        public static string PackageStreamShouldBeSeekable {
            get {
                return ResourceManager.GetString("PackageStreamShouldBeSeekable", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to String argument &apos;{0}&apos; cannot be null or empty.
        /// </summary>
        public static string StringCannotBeNullOrEmpty {
            get {
                return ResourceManager.GetString("StringCannotBeNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to An error occurred while updating packages.config. The file was closed before the entry could be added..
        /// </summary>
        public static string UnableToAddEntry {
            get {
                return ResourceManager.GetString("UnableToAddEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unable to parse the current NuGet client version..
        /// </summary>
        public static string UnableToParseClientVersion {
            get {
                return ResourceManager.GetString("UnableToParseClientVersion", resourceCulture);
            }
        }
    }
}
