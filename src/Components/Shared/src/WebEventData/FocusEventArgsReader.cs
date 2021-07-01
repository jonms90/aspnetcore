// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#nullable enable

using System.Text.Json;

namespace Microsoft.AspNetCore.Components.Web
{
    internal static class FocusEventArgsReader
    {
        private static readonly JsonEncodedText Type = JsonEncodedText.Encode("type");

        internal static FocusEventArgs Read(JsonElement jsonElement)
        {
            var eventArgs = new FocusEventArgs();
            foreach (var property in jsonElement.EnumerateObject())
            {
                if (property.NameEquals(Type.EncodedUtf8Bytes))
                {
                    eventArgs.Type = property.Value.GetString()!;
                }
            }
            return eventArgs;
        }
    }
}
