using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Web.Shared.Constants
{
    public static class GlobalConstants
    {
        public static string PHOTO_PATH = $"\\App\\photos";
        
        public static string ATTACHMENT_PATH = $"\\App\\attachment";
        
        public static string ATTACHMENT_PATH_PROD_TEMP = $"\\App\\ProductionTemp";
        
        public static string ATTACHMENT_PATH_PROD = $"\\App\\Production";

        public static string ATTACHMENT_PATH_ILLUSTRATION = $"{ATTACHMENT_PATH_PROD}\\Illustration";

        public static string ATTACHMENT_PATH_LOGO = $"{ATTACHMENT_PATH_PROD}\\Logo";
    }
}
