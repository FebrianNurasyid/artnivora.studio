using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Web.Shared.ViewModels
{
    public class LinkInfo
    {
        public int TotalCount { get; set; }
        public string Url { get; set; }
        public string FirstUrl { get; set; }
        public string NextUrl { get; set; }
        public string PrevUrl { get; set; }
        public string LastUrl { get; set; }
    }
}
