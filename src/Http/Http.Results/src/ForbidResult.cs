// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Http.Result
{
    internal sealed partial class ForbidResult : IResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ForbidResult"/>.
        /// </summary>
        public ForbidResult()
            : this(Array.Empty<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ForbidResult"/> with the
        /// specified authentication scheme.
        /// </summary>
        /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
        public ForbidResult(string authenticationScheme)
            : this(new[] { authenticationScheme })
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ForbidResult"/> with the
        /// specified authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
        public ForbidResult(IList<string> authenticationSchemes)
            : this(authenticationSchemes, properties: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ForbidResult"/> with the
        /// specified <paramref name="properties"/>.
        /// </summary>
        /// <param name="properties"><see cref="AuthenticationProperties"/> used to perform the authentication
        /// challenge.</param>
        public ForbidResult(AuthenticationProperties? properties)
            : this(Array.Empty<string>(), properties)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ForbidResult"/> with the
        /// specified authentication scheme and <paramref name="properties"/>.
        /// </summary>
        /// <param name="authenticationScheme">The authentication schemes to challenge.</param>
        /// <param name="properties"><see cref="AuthenticationProperties"/> used to perform the authentication
        /// challenge.</param>
        public ForbidResult(string authenticationScheme, AuthenticationProperties? properties)
            : this(new[] { authenticationScheme }, properties)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ForbidResult"/> with the
        /// specified authentication schemes and <paramref name="properties"/>.
        /// </summary>
        /// <param name="authenticationSchemes">The authentication scheme to challenge.</param>
        /// <param name="properties"><see cref="AuthenticationProperties"/> used to perform the authentication
        /// challenge.</param>
        public ForbidResult(IList<string> authenticationSchemes, AuthenticationProperties? properties)
        {
            AuthenticationSchemes = authenticationSchemes;
            Properties = properties;
        }

        /// <summary>
        /// Gets or sets the authentication schemes that are challenged.
        /// </summary>
        public IList<string> AuthenticationSchemes { get; init; }

        /// <summary>
        /// Gets or sets the <see cref="AuthenticationProperties"/> used to perform the authentication challenge.
        /// </summary>
        public AuthenticationProperties? Properties { get; init; }

        /// <inheritdoc />
        public async Task ExecuteAsync(HttpContext httpContext)
        {
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<ForbidResult>>();

            Log.ForbidResultExecuting(logger, AuthenticationSchemes);

            if (AuthenticationSchemes != null && AuthenticationSchemes.Count > 0)
            {
                for (var i = 0; i < AuthenticationSchemes.Count; i++)
                {
                    await httpContext.ForbidAsync(AuthenticationSchemes[i], Properties);
                }
            }
            else
            {
                await httpContext.ForbidAsync(Properties);
            }
        }

        private static partial class Log
        {
            public static void ForbidResultExecuting(ILogger logger, IList<string> authenticationSchemes)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    ForbidResultExecuting(logger, authenticationSchemes.ToArray());
                }
            }

            [LoggerMessage(1, LogLevel.Information, "Executing ChallengeResult with authentication schemes ({Schemes}).", EventName = "ChallengeResultExecuting", SkipEnabledCheck = true)]
            private static partial void ForbidResultExecuting(ILogger logger, string[] schemes);
        }

    }
}
