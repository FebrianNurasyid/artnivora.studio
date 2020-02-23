using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models.Enums
{
    /// <summary>
    /// Enumeration containing emailtypes
    /// </summary>
    public enum EmailType
    {
        [Description("Activation Account")]
        ActivationAccount = 1,
        [Description("Reset Password Account")]
        ResetPasswordAccount = 2,
    }
}
