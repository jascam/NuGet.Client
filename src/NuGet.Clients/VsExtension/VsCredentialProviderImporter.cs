﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using NuGet.Credentials;
using NuGet.PackageManagement.UI;
using NuGet.PackageManagement.VisualStudio;
using NuGet.VisualStudio;

namespace NuGetVSExtension
{
    /// <summary>
    /// Find all MEF imports for IVsCredentialProvider, and handle inserting fallback provider
    /// for Dev14
    /// </summary>
    public class VsCredentialProviderImporter
    {
        private readonly DTE _dte;
        private readonly Action<string> _errorDelegate;
        private readonly Func<ICredentialProvider> _fallbackProviderFactory;
        private readonly Action _initializer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dte">DTE instance, used to determine the Visual Studio version.</param>
        /// <param name="fallbackProviderFactory">Factory method used to create a fallback provider for
        /// Dev14 in case a VSTS credential provider can not be imported.</param>
        /// <param name="errorDelegate">Used to write error messages to the user.</param>
        public VsCredentialProviderImporter(
            EnvDTE.DTE dte,
            Func<ICredentialProvider> fallbackProviderFactory,
            Action<string> errorDelegate) : this(dte, fallbackProviderFactory, errorDelegate, null)
        {
        }

        /// <summary>
        /// Constructor, allows changing initializer Action for testing purposes
        /// </summary>
        /// <param name="dte">DTE instance, used to determine the Visual Studio version.</param>
        /// <param name="fallbackProviderFactory">Factory method used to create a fallback provider for
        /// Dev14 in case a VSTS credential provider can not be imported.</param>
        /// <param name="errorDelegate">Used to write error messages to the user.</param>
        /// <param name="initializer">Init method used to supply MEF imports. Should only
        /// be supplied by tests.</param>
        public VsCredentialProviderImporter (
            EnvDTE.DTE dte,
            Func<ICredentialProvider> fallbackProviderFactory,
            Action<string> errorDelegate,
            Action initializer)
        {
            if (dte == null)
            {
                throw new ArgumentNullException(nameof(dte));
            }

            if (fallbackProviderFactory == null)
            {
                throw new ArgumentNullException(nameof(fallbackProviderFactory));
            }

            if (errorDelegate == null)
            {
                throw new ArgumentNullException(nameof(errorDelegate));
            }

            _dte = dte;
            _fallbackProviderFactory = fallbackProviderFactory;
            _errorDelegate = errorDelegate;
            _initializer = initializer ?? Initialize;
        }

        [ImportMany(typeof(IVsCredentialProvider))]
        public IEnumerable<IVsCredentialProvider> ImportedProviders { get; set; }

        /// <summary>
        /// Plugin providers are entered loaded the same way as other nuget extensions,
        /// matching any extension named CredentialProvider.*.exe.
        /// </summary>
        /// <returns>An enumeration of plugin providers</returns>
        public IReadOnlyCollection<ICredentialProvider> GetProviders()
        {
            this._initializer();
            var results = new List<ICredentialProvider>();

            if (ImportedProviders != null)
            {
                foreach (var importedProvider in ImportedProviders)
                {
                    results.Add(new VsCredentialProviderAdapter(importedProvider));
                }
            }

            // Dev15+ will provide a credential provider for VSTS.
            // If we are in Dev14, and no imported VSTS provider is found,
            // then fallback on the built-in VisualStudioAccountProvider
            if (!results.Any() && IsDev14)
            {
                // Handle any type load exception constructing the provider
                try
                {
                    results.Add(this._fallbackProviderFactory());
                }
                catch (Exception e) when (e is BadImageFormatException || e is FileLoadException)
                {
                    this._errorDelegate(string.Format(Resources.VsCredentialProviderImporter_LoadErrorFormat, e.Message));
                }
            }

            return results;
        }

        private void Initialize()
        {
            var componentModel = ServiceLocator.GetGlobalService<SComponentModel, IComponentModel>();

            // ensure we satisfy our imports and access DTE on the UI thread
            NuGetUIThreadHelper.JoinableTaskFactory.Run(
                async delegate
                {
                    await NuGetUIThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                    componentModel?.DefaultCompositionService.SatisfyImportsOnce(this);
                    Version = _dte.Version;
                });
        }

        public string Version { get; set; } = string.Empty;

        private bool IsDev14 => Version.StartsWith("14.");
    }
}
