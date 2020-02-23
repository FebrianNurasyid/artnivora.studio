using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models
{
    /// <summary>
    /// Enumeration containing marital status
    /// </summary>
    [Flags]
    public enum MaritalStatusType 
    {
        Married = 1,
        InRelation = 2,
        Single = 3,
        Other = 9,
    }
}
