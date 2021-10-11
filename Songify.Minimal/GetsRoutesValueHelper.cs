using System;
using Microsoft.AspNetCore.Http;

namespace Songify.Minimal
{
    class GetsRoutesValueHelper
    {
        public T Get<T>(HttpContext context, string paramName) where T : struct
        {
            var value = context.Request.RouteValues[paramName]?.ToString();
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(paramName));
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}