namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ParticipantProfileDataService : IParticipantProfileDataService, IDisposable
    {
        private readonly DatabaseContext context;
        private bool disposed = false;


        public ParticipantProfileDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public void Add(ParticipantProfile entity)
        {
            try
            {
                context.ParticipantProfile.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(IParticipantProfile entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(IParticipantProfile entity)
        {
            // TODO Add logic to delete participant profile data
        }

        public IEnumerable<IParticipantProfile> GetAll()
        {
            // TODO Add logic to get all participant profiles 

            return new List<ParticipantProfile>();
        }

        public IParticipantProfile GetByUser(User user)
        {
            var userProfile = context.UserProfile
                .Where(x => x.User.Id == user.Id)
                .Include(c => c.User)
                .Include(c => c.ContactAddress)
                .FirstOrDefault();

            if (userProfile != null) {
                ParticipantProfile participantProfile = context.ParticipantProfile
                    .Where(x => x.UserProfile == userProfile)
                    .FirstOrDefault();

                // This adds the userprofile and the address to the participantProfile
                participantProfile.UserProfile = userProfile;
                return participantProfile;
            }

            return null;

        }

        public IParticipantProfile GetById(Guid id)
        {
            var prof = context.ParticipantProfile
                .Where(partiProfile => partiProfile.Id == id)
                .FirstOrDefault();
            return prof;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
