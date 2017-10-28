// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using Microsoft.AspNetCore.WebHooks;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="IConfigurationRoot"/>.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class WebHookReceiverConfigurationRootExtensions
    {
        /// <summary>
        /// Gets the secret key <see cref="IConfigurationSection"/> with the given <paramref name="sectionKey"/> and
        /// <paramref name="id"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfigurationRoot"/>.</param>
        /// <param name="sectionKey">
        /// The key (relative to <see cref="WebHookConstants.ReceiverConfigurationSectionKey"/>) of the
        /// <see cref="IConfigurationSection"/> containing the receiver-specific
        /// <see cref="WebHookConstants.SecretKeyConfigurationKeySectionKey"/> <see cref="IConfigurationSection"/>.
        /// Typically this is the name of the receiver e.g. <c>github</c>.
        /// </param>
        /// <param name="id">
        /// The (potentially empty) key (relative to <see cref="WebHookConstants.ReceiverConfigurationSectionKey"/>,
        /// <paramref name="sectionKey"/> and <see cref="WebHookConstants.SecretKeyConfigurationKeySectionKey"/>) of
        /// the <see cref="IConfigurationSection"/> to return. This allows an <see cref="IWebHookReceiver"/> to support
        /// multiple WebHook senders with individual
        /// configurations.
        /// </param>
        /// <returns>
        /// The secret key <see cref="IConfigurationSection"/> with the given <paramref name="sectionKey"/> and
        /// <paramref name="id"/>. <see langword="null"/> if the <see cref="IConfigurationSection"/> does not exist.
        /// </returns>
        public static IConfigurationSection GetSecretKeys(
            this IConfigurationRoot configuration,
            string sectionKey,
            string id)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (sectionKey == null)
            {
                throw new ArgumentNullException(nameof(sectionKey));
            }

            if (string.IsNullOrEmpty(id))
            {
                id = WebHookConstants.DefaultIdConfigurationKey;
            }

            // Look up configuration value for these keys.
            var key = ConfigurationPath.Combine(
                WebHookConstants.ReceiverConfigurationSectionKey,
                sectionKey,
                WebHookConstants.SecretKeyConfigurationKeySectionKey,
                id);

            var section = configuration.GetSection(key);
            if (!section.Exists())
            {
                return null;
            }

            return section;
        }

        /// <summary>
        /// Returns an indication secret key configuration values exist for <paramref name="sectionKey"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfigurationRoot"/>.</param>
        /// <param name="sectionKey">
        /// The key (relative to <see cref="WebHookConstants.ReceiverConfigurationSectionKey"/>) of the
        /// <see cref="IConfigurationSection"/> containing the receiver-specific
        /// <see cref="WebHookConstants.SecretKeyConfigurationKeySectionKey"/> <see cref="IConfigurationSection"/>.
        /// Typically this is the name of the receiver e.g. <c>github</c>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if secret key configuration values exist in the receiver-specific
        /// <see cref="WebHookConstants.SecretKeyConfigurationKeySectionKey"/> <see cref="IConfigurationSection"/>;
        /// <see langword="false"/> otherwise.
        /// </returns>
        public static bool HasSecretKeys(this IConfigurationRoot configuration, string sectionKey)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (sectionKey == null)
            {
                throw new ArgumentNullException(nameof(sectionKey));
            }

            var key = ConfigurationPath.Combine(
                WebHookConstants.ReceiverConfigurationSectionKey,
                sectionKey,
                WebHookConstants.SecretKeyConfigurationKeySectionKey);

            return configuration.GetSection(key).Exists();
        }
    }
}
