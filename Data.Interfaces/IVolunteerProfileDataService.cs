namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IVolunteerProfileDataService
    {
        IEnumerable<IVolunteerProfile> GetAll();

        IVolunteerProfile GetById(Guid id);

        IVolunteerProfile GetByUser(User user);

        void Add(IVolunteerProfile entity);
        void Delete(IVolunteerProfile entity);
        void Update(IVolunteerProfile entity);
        void Save();
    }
}
