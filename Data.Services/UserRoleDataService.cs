namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
	using System.Collections.Generic;
	using System.Linq;

	public class UserRoleDataService : IUserRoleDataService, IDisposable
	{
		private readonly DatabaseContext context;

		public UserRoleDataService(DatabaseContext context)
		{
			this.context = context;
		}


		IEnumerable<UserRole> IUserRoleDataService.GetAll()
		{
			return context.UserRole.ToList();
		}

		public List<UserRole> GetRolesOfUser(User user)
		{
			List<UserRole> userRolesList = context.UserRoles
				.Where(userRoles => userRoles.User == user)
				.Include(userRoles => userRoles.UserRole)
				.Select(userRole => userRole.UserRole)
				.ToList();

			return userRolesList;
		}

		public void Add(UserRoles userRoles)
		{
			context.UserRoles.Add(userRoles);
		}

		public UserRole GetByType(UserRoleType userRoleType)
		{
			return context.UserRole
				.Where(userRole => userRole.Key == userRoleType.ToString())
				.FirstOrDefault();
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
