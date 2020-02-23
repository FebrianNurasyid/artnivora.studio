namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
	using System.Collections.Generic;
	using System.Linq;
    using System.Threading.Tasks;

    public class UserDataService : IUserDataService, IDisposable
	{
		private readonly DatabaseContext context;

		public UserDataService(DatabaseContext context)
		{
			this.context = context;
		}
        public async Task<User> GetByIdAsync(Guid Id)
        {
            return await context.User
                .Where(user => user.Id == Id)
                .FirstOrDefaultAsync();
        }

        public User GetById(Guid Id)
		{
			return context.User
				.Where(user => user.Id == Id)
				.FirstOrDefault();
		}
	
		public User GetByUsername(string username)
		{
			return context.User.FirstOrDefault(x => x.Username == username);
		}

		public IEnumerable<User> GetAll()
		{
			return context.User
				.ToList();
		}

		public User FindById(Guid Id)
		{
			return context.User
				.Where(user => user.Id == Id)
				.FirstOrDefault();
		}

		public void Add(User entity)
		{
			try
			{
				context.User.Add(entity);
			} catch(Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void Delete(User entity)
		{
			context.User.Remove(entity);
		}

		public void Update(User entity)
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

		public User GetUserByActivationToken(Guid token, bool isRecoveryPassword)
		{
			if (isRecoveryPassword)
			{
				return context.User
					.Where(user => user.PasswordRecoveryToken == token)
					.FirstOrDefault();
			}
			else
			{
				return context.User
					.Where(user => user.Activation_Token == token)
					.FirstOrDefault();
			}
		}

		public User GetUserByBearerToken(string bearerToken)
		{
			return context.User
				.Where(user => user.Token == bearerToken)
				.FirstOrDefault();
		}

		public User GetUserByEmail(string email)
		{
			return context.User
				.Where(user => user.Email == email)
				.FirstOrDefault();
		}
	}
}
