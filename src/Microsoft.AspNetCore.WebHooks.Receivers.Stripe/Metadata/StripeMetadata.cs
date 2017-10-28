// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.WebHooks.Properties;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.WebHooks.Metadata
{
    /// <summary>
    /// An <see cref="IWebHookMetadata"/> service containing metadata about the Stripe receiver.
    /// </summary>
    public class StripeMetadata : WebHookMetadata, IWebHookRequestMetadataService, IWebHookSecurityMetadata
    {
        /// <summary>
        /// Instantiates a new <see cref="StripeMetadata"/> instance.
        /// </summary>
        /// <param name="configuration">
        /// The <see cref="IConfiguration"/> used to initialize <see cref="VerifyCodeParameter"/>.
        /// </param>
        public StripeMetadata(IConfiguration configuration)
            : base(StripeConstants.ReceiverName)
        {
            var configurationRoot = configuration as IConfigurationRoot;
            if (configurationRoot == null)
            {
                var message = string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.General_ArgumentMustImplement,
                    typeof(IConfigurationRoot));
                throw new ArgumentException(message, nameof(configuration));
            }

            VerifyCodeParameter = configurationRoot.IsTrue(StripeConstants.DirectWebHookConfigurationKey);
        }

        // IWebHookRequestMetadataService...

        /// <inheritdoc />
        public WebHookBodyType BodyType => WebHookBodyType.Json;

        /// <inheritdoc />
        public bool UseHttpContextModelBinder => true;

        // IWebHookSecurityMetadata...

        /// <inheritdoc />
        public bool VerifyCodeParameter { get; }

        /// <inheritdoc />
        public bool ShortCircuitGetRequests => false;

        /// <inheritdoc />
        public WebHookGetRequest WebHookGetRequest => null;
    }
}
