using System.ComponentModel;

namespace Artnivora.Studio.Portal.Business.Models
{
    /// <summary>
    /// Enumeration containing User role types
    /// </summary>
    public enum UserRoleType
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Commission Member")]
        CommissionMember = 2,
        [Description("Participant")]
        Participant = 3,
        [Description("Volunteer")]
        Volunteer = 4
    }
}
