using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Songify.Simple.Helpers
{
    public class SlugifyParameterTransformer:IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
            {
                return null;
            }

            // return Regex.Replace(
            //     value.ToString() ?? string.Empty, "([a-z])([A-Z])",
            //     "$1-$2").ToLower();
            
            return Regex.Replace(
                value.ToString() ?? string.Empty, "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)).ToLower();
        }
    }
}