namespace Artnivora.Studio.Portal.Business.Models.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IParticipantProfile
    {
        Guid Id { get; set; }
        string HealthcareProviderId { get; set; }
    }
}
