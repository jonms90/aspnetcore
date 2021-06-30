// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#nullable enable

using System.Text.Json;

namespace Microsoft.AspNetCore.Components.Web
{
    internal static class MouseEventArgsReader
    {
        private static readonly JsonEncodedText Detail = JsonEncodedText.Encode("detail");
        private static readonly JsonEncodedText ScreenX = JsonEncodedText.Encode("screenX");
        private static readonly JsonEncodedText ScreenY = JsonEncodedText.Encode("screenY");
        private static readonly JsonEncodedText ClientX = JsonEncodedText.Encode("clientX");
        private static readonly JsonEncodedText ClientY = JsonEncodedText.Encode("clientY");
        private static readonly JsonEncodedText OffsetX = JsonEncodedText.Encode("offsetX");
        private static readonly JsonEncodedText OffsetY = JsonEncodedText.Encode("offsetY");
        private static readonly JsonEncodedText Button = JsonEncodedText.Encode("button");
        private static readonly JsonEncodedText Buttons = JsonEncodedText.Encode("buttons");
        private static readonly JsonEncodedText CtrlKey = JsonEncodedText.Encode("ctrlKey");
        private static readonly JsonEncodedText ShiftKey = JsonEncodedText.Encode("shiftKey");
        private static readonly JsonEncodedText AltKey = JsonEncodedText.Encode("altKey");
        private static readonly JsonEncodedText MetaKey = JsonEncodedText.Encode("metaKey");
        private static readonly JsonEncodedText Type = JsonEncodedText.Encode("type");

        internal static MouseEventArgs Read(JsonElement jsonElement)
        {
            var eventArgs = new MouseEventArgs();
            Read(jsonElement, eventArgs);
            return eventArgs;
        }

        internal static void Read(JsonElement jsonElement, MouseEventArgs eventArgs)
        {
            foreach (var property in jsonElement.EnumerateObject())
            {
                if (property.NameEquals(Detail.EncodedUtf8Bytes))
                {
                    eventArgs.Detail = property.Value.GetInt64();
                }
                else if (property.NameEquals(ScreenX.EncodedUtf8Bytes))
                {
                    eventArgs.ScreenX = property.Value.GetDouble();
                }
                else if (property.NameEquals(ScreenY.EncodedUtf8Bytes))
                {
                    eventArgs.ScreenY = property.Value.GetDouble();
                }
                else if (property.NameEquals(ClientX.EncodedUtf8Bytes))
                {
                    eventArgs.ClientX = property.Value.GetDouble();
                }
                else if (property.NameEquals(ClientY.EncodedUtf8Bytes))
                {
                    eventArgs.ClientY = property.Value.GetDouble();
                }
                else if (property.NameEquals(OffsetX.EncodedUtf8Bytes))
                {
                    eventArgs.OffsetX = property.Value.GetDouble();
                }
                else if (property.NameEquals(OffsetY.EncodedUtf8Bytes))
                {
                    eventArgs.OffsetY = property.Value.GetDouble();
                }
                else if (property.NameEquals(Button.EncodedUtf8Bytes))
                {
                    eventArgs.Button = property.Value.GetInt64();
                }
                else if (property.NameEquals(Buttons.EncodedUtf8Bytes))
                {
                    eventArgs.Buttons = property.Value.GetInt64();
                }
                else if (property.NameEquals(CtrlKey.EncodedUtf8Bytes))
                {
                    eventArgs.CtrlKey = property.Value.GetBoolean();
                }
                else if (property.NameEquals(ShiftKey.EncodedUtf8Bytes))
                {
                    eventArgs.ShiftKey = property.Value.GetBoolean();
                }
                else if (property.NameEquals(AltKey.EncodedUtf8Bytes))
                {
                    eventArgs.AltKey = property.Value.GetBoolean();
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
        }
    }
}
