// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#nullable enable

using System.Text.Json;

namespace Microsoft.AspNetCore.Components.Web
{
    internal static class KeyboardEventArgsReader
    {
        private static readonly JsonEncodedText Key = JsonEncodedText.Encode("key");
        private static readonly JsonEncodedText Code = JsonEncodedText.Encode("code");
        private static readonly JsonEncodedText Location = JsonEncodedText.Encode("location");
        private static readonly JsonEncodedText Repeat = JsonEncodedText.Encode("repeat");
        private static readonly JsonEncodedText CtrlKey = JsonEncodedText.Encode("ctrlKey");
        private static readonly JsonEncodedText ShiftKey = JsonEncodedText.Encode("shiftKey");
        private static readonly JsonEncodedText AltKey = JsonEncodedText.Encode("altKey");
        private static readonly JsonEncodedText MetaKey = JsonEncodedText.Encode("metaKey");
        private static readonly JsonEncodedText Type = JsonEncodedText.Encode("type");

        internal static KeyboardEventArgs Read(JsonElement jsonElement)
        {
            var eventArgs = new KeyboardEventArgs();
            foreach (var property in jsonElement.EnumerateObject())
            {
                if (property.NameEquals(Key.EncodedUtf8Bytes))
                {
                    eventArgs.Key = property.Value.GetString()!;
                }
                else if (property.NameEquals(Code.EncodedUtf8Bytes))
                {
                    eventArgs.Code = property.Value.GetString()!;
                }
                else if (property.NameEquals(Location.EncodedUtf8Bytes))
                {
                    eventArgs.Location = property.Value.GetSingle()!;
                }
                else if (property.NameEquals(Repeat.EncodedUtf8Bytes))
                {
                    eventArgs.Repeat = property.Value.GetBoolean();
                }
                else if (property.NameEquals(CtrlKey.EncodedUtf8Bytes))
                {
                    eventArgs.CtrlKey = property.Value.GetBoolean();
                }
                else if (property.NameEquals(AltKey.EncodedUtf8Bytes))
                {
                    eventArgs.AltKey = property.Value.GetBoolean();
                }
                else if (property.NameEquals(ShiftKey.EncodedUtf8Bytes))
                {
                    eventArgs.ShiftKey = property.Value.GetBoolean();
                }
                else if (property.NameEquals(MetaKey.EncodedUtf8Bytes))
                {
                    eventArgs.MetaKey = property.Value.GetBoolean();
                }
                else if (property.NameEquals(Type.EncodedUtf8Bytes))
                {
                    eventArgs.Type = property.Value.GetString()!;
                }
            }
            return eventArgs;
        }
    }
}
