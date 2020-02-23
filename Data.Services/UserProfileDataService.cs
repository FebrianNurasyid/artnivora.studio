namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
	using System.Collections.Generic;
	using System.Linq;
    using System.Threading.Tasks;

    public class UserProfileDataService : IUserProfileDataService, IDisposable
	{
		private readonly DatabaseContext context;

		public UserProfileDataService(DatabaseContext context)
		{
			this.context = context;
		}

		public UserProfile GetById(Guid Id)
		{
			return context.UserProfile
				.Where(userProfile => userProfile.Id == Id)
				.FirstOrDefault();
		}

		public UserProfile GetByUser(User user)
		{
			return context.UserProfile
				.Where(userProfile => userProfile.User == user)
				.FirstOrDefault();
		}

		public IEnumerable<UserProfile> GetAll()
		{
			return context.UserProfile
				.Include(userProfile => userProfile.ContactAddress)
				.ToList();
		}
        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await context.UserProfile
                .Include(userProfile => userProfile.ContactAddress)
                .ToListAsync();
        }

        public UserProfile FindById(Guid Id)
		{
			return context.UserProfile.Find(Id);
		}

		public void Add(UserProfile entity)
		{
			try
			{
				context.UserProfile.Add(entity);
			} catch(Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void Delete(UserProfile entity)
		{
			context.UserProfile.Remove(entity);
		}

		public void Update(UserProfile entity)
		{
			context.Entry(entity).State = EntityState.Modified;
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
