using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artnivora.Studio.Portal.Web.Shared.Utilities
{
    public static class HttpContextUtilities
    {
        public static string GetSubject(this Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            if (System.Diagnostics.Debugger.IsAttached && httpContextAccessor.HttpContext.User == null)
            {
                return "000000000";
            }

            if (httpContextAccessor.HttpContext.User.Claims.Any(c => c.Type.Contains("identity/claims/name")))
            {
                //internal request, the initiator is in the header
                return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("identity/claims/name")).Value;
            }

            return null;
        }

        public static Uri GetAbsoluteUri(this Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            if (request.Host.Port.HasValue)
                uriBuilder.Port = request.Host.Port.Value;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        public static Uri GetBaseUri(this Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            if (request.Host.Port.HasValue)
                uriBuilder.Port = request.Host.Port.Value;
            uriBuilder.Path = request.Path.ToString();
            return uriBuilder.Uri;
        }

        public static Guid? GetUserId(this Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            if (Guid.TryParse(httpContextAccessor.HttpContext.User.Identity.Name.ToString(), out Guid id))
                return id;
            else
                return null;
        }
    }
}
