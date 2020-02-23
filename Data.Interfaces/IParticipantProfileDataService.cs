namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IParticipantProfileDataService
    {
        IEnumerable<IParticipantProfile> GetAll();

        IParticipantProfile GetById(Guid id);

        IParticipantProfile GetByUser(User user);


        void Add(ParticipantProfile entity);
        void Delete(IParticipantProfile entity);
        void Update(IParticipantProfile entity);
        void Save();
    }
}
