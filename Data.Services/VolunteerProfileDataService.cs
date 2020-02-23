namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VolunteerProfileDataService : IVolunteerProfileDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public VolunteerProfileDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public void Add(IVolunteerProfile entity)
        {
            try
            {
                context.VolunteerProfile.Add((VolunteerProfile)entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(IVolunteerProfile entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(IVolunteerProfile entity)
        {
            // TODO Add logic to delete volunteer profile
        }

        public IEnumerable<IVolunteerProfile> GetAll()
        {
            // TODO Add logic to get all volunteer profiles 

            return new List<VolunteerProfile>();
        }


        public IVolunteerProfile GetByUser(User user)
        {
            var userProfile = context.UserProfile
                .Where(x => x.User.Id == user.Id)
                .Include(c => c.User)
                .Include(c => c.ContactAddress)
                .FirstOrDefault();

            if (userProfile != null)
            {
                VolunteerProfile volunteerProfile = context.VolunteerProfile
                    .Where(x => x.UserProfile == userProfile)
                    .FirstOrDefault();

                // This adds the userprofile and the address to the participantProfile
                volunteerProfile.UserProfile = userProfile;
                return volunteerProfile;
            }

            return null;
        }
        
        public IVolunteerProfile GetById(Guid id)
        {
            var userProfile = context.UserProfile
                .Where(x => x.User.Id == id)
                .Include(c => c.User)
                .Include(c => c.ContactAddress)
                .FirstOrDefault();

            if (userProfile != null)
            {
                VolunteerProfile volunteerProfile = context.VolunteerProfile
                    .Where(x => x.UserProfile == userProfile)
                    .FirstOrDefault();

                // This adds the userprofile and the address to the Volunteer Profile
                volunteerProfile.UserProfile = userProfile;
                return volunteerProfile;
            }

            return null;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

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
