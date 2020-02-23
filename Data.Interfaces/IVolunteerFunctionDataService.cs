namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface representing volunteerfunction dataservice
    /// </summary>
    public interface IVolunteerFunctionDataService
    {
        IEnumerable<VolunteerFunction> Get();        
    }
}
