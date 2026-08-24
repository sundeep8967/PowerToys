// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.RegularExpressions;

namespace Microsoft.CmdPal.Ext.ClipboardHistory.Helpers;

internal static partial class DetailsMarkdownHelper
{
    public static string EscapeMarkdown(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text ?? string.Empty;
        }

        return MarkdownSpecialCharacters().Replace(text, "\\$1");
    }

    [GeneratedRegex(@"([\\`*_{}\[\]()#+\-.!>|~])")]
    private static partial Regex MarkdownSpecialCharacters();

    public static string BuildImageBody(byte[]? imageBytes, string altText)
    {
        return imageBytes is null ? string.Empty : BuildImageBodyFromBytes(imageBytes, altText);
    }

    public static string BuildImageBodyFromBytes(byte[] imageBytes, string altText)
    {
        ArgumentNullException.ThrowIfNull(imageBytes);

        var base64 = Convert.ToBase64String(imageBytes);
        return $"![{altText}](data:image/png;base64,{base64}#--x-cmdpal-fit=fit)";
    }
}
