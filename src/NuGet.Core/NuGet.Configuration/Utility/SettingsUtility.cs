﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet.Common;
using System.Globalization;

namespace NuGet.Configuration
{
    public static class SettingsUtility
    {
        public const string ConfigSection = "config";
        public const string GlobalPackagesFolderKey = "globalPackagesFolder";
        public const string GlobalPackagesFolderEnvironmentKey = "NUGET_PACKAGES";
        public const string HttpCacheFolderKey = "httpCacheFolder";
        public const string HttpCacheFolderEnvironmentKey = "NUGET_HTTP_CACHE";
        public const string RepositoryPathKey = "repositoryPath";
        public static readonly string DefaultGlobalPackagesFolderPath = "packages" + Path.DirectorySeparatorChar;

        public static string GetRepositoryPath(ISettings settings)
        {
            var path = settings.GetValue(ConfigSection, RepositoryPathKey, isPath: true);
            if (!String.IsNullOrEmpty(path))
            {
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return path;
        }

        public static string GetDecryptedValue(ISettings settings, string section, string key, bool isPath = false)
        {
            if (String.IsNullOrEmpty(section))
            {
                throw new ArgumentException(Resources.Argument_Cannot_Be_Null_Or_Empty, "section");
            }

            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Resources.Argument_Cannot_Be_Null_Or_Empty, "key");
            }

            var encryptedString = settings.GetValue(section, key, isPath);
            if (encryptedString == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(encryptedString))
            {
                return String.Empty;
            }
            return EncryptionUtility.DecryptString(encryptedString);
        }

        public static void SetEncryptedValue(ISettings settings, string section, string key, string value)
        {
            if (String.IsNullOrEmpty(section))
            {
                throw new ArgumentException(Resources.Argument_Cannot_Be_Null_Or_Empty, "section");
            }

            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Resources.Argument_Cannot_Be_Null_Or_Empty, "key");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (String.IsNullOrEmpty(value))
            {
                settings.SetValue(section, key, String.Empty);
            }
            else
            {
                var encryptedString = EncryptionUtility.EncryptString(value);
                settings.SetValue(section, key, encryptedString);
            }
        }

        /// <summary>
        /// Retrieves a config value for the specified key
        /// </summary>
        /// <param name="settings">The settings instance to retrieve </param>
        /// <param name="key">The key to look up</param>
        /// <param name="decrypt">Determines if the retrieved value needs to be decrypted.</param>
        /// <param name="isPath">Determines if the retrieved value is returned as a path.</param>
        /// <returns>Null if the key was not found, value from config otherwise.</returns>
        public static string GetConfigValue(ISettings settings, string key, bool decrypt = false, bool isPath = false)
        {
            return decrypt ?
                GetDecryptedValue(settings, ConfigSection, key, isPath) :
                settings.GetValue(ConfigSection, key, isPath);
        }

        /// <summary>
        /// Sets a config value in the setting.
        /// </summary>
        /// <param name="settings">The settings instance to store the key-value in.</param>
        /// <param name="key">The key to store.</param>
        /// <param name="value">The value to store.</param>
        /// <param name="encrypt">Determines if the value needs to be encrypted prior to storing.</param>
        public static void SetConfigValue(ISettings settings, string key, string value, bool encrypt = false)
        {
            if (encrypt == true)
            {
                SetEncryptedValue(settings, ConfigSection, key, value);
            }
            else
            {
                settings.SetValue(ConfigSection, key, value);
            }
        }

        /// <summary>
        /// Deletes a config value from settings
        /// </summary>
        /// <param name="settings">The settings instance to delete the key from.</param>
        /// <param name="key">The key to delete.</param>
        /// <returns>True if the value was deleted, false otherwise.</returns>
        public static bool DeleteConfigValue(ISettings settings, string key)
        {
            return settings.DeleteValue(ConfigSection, key);
        }

        public static string GetGlobalPackagesFolder(ISettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var path = Environment.GetEnvironmentVariable(GlobalPackagesFolderEnvironmentKey);
            if (string.IsNullOrEmpty(path))
            {
                // Environment variable for globalPackagesFolder is not set.
                path = settings.GetValue(ConfigSection, GlobalPackagesFolderKey, isPath: true);
            }

            if (!string.IsNullOrEmpty(path))
            {
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                return path;
            }

            path = Path.Combine(NuGetEnvironment.GetFolderPath(NuGetFolderPath.NuGetHome), DefaultGlobalPackagesFolderPath);

            return path;
        }

        public static string GetHttpCacheFolder(ISettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            
            return NuGetEnvironment.GetFolderPath(NuGetFolderPath.HttpCacheDirectory);
        }

        /// <summary>
        /// The DefaultPushSource can be:
        /// - An absolute URL
        /// - An absolute file path
        /// - A relative file path
        /// - The name of a registered source from a config file
        /// </summary>
        public static string GetDefaultPushSource(ISettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            string source = settings.GetValue(ConfigurationConstants.Config, ConfigurationConstants.DefaultPushSource, isPath: false);

            Uri sourceUri = UriUtility.TryCreateSourceUri(source, UriKind.RelativeOrAbsolute);
            if (sourceUri != null && !sourceUri.IsAbsoluteUri)
            {
                // For non-absolute sources, it could be the name of a config source, or a relative file path.
                IPackageSourceProvider sourceProvider = new PackageSourceProvider(settings);
                IEnumerable<PackageSource> allSources = sourceProvider.LoadPackageSources();

                if (!allSources.Any(s => s.IsEnabled && s.Name.Equals(source, StringComparison.OrdinalIgnoreCase)))
                {
                    // It wasn't the name of a source, so treat it like a relative file path
                    source = settings.GetValue(ConfigurationConstants.Config, ConfigurationConstants.DefaultPushSource, isPath: true);
                }
            }

            return source;
        }

        private static string GetPathFromEnvOrConfig(string envVarName, string configKey, ISettings settings)
        {
            var path = Environment.GetEnvironmentVariable(envVarName);

            if (!string.IsNullOrEmpty(path))
            {
                if (!Path.IsPathRooted(path))
                {
                    var message = String.Format(CultureInfo.CurrentCulture, Resources.RelativeEnvVarPath, envVarName, path);
                    throw new NuGetConfigurationException(message);
                }
            }
            else
            {
                path = Path.Combine(NuGetEnvironment.GetFolderPath(NuGetFolderPath.NuGetHome), DefaultGlobalPackagesFolderPath);
            }

            if (!string.IsNullOrEmpty(path))
            {
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return path;
        }
    }
}
