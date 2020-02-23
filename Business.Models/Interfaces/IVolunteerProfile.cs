namespace Artnivora.Studio.Portal.Business.Models.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IVolunteerProfile
    {
        Guid Id { get; set; }

        bool IsCoreVolunteer { get; set; }

        MaritalStatusType MaritalStatus { get; set; }

        IEnumerable<TargetAudience> TargetAudiences { get; }

        IEnumerable<VolunteerFunction> VolunteerFunctions { get; }
    }
}
