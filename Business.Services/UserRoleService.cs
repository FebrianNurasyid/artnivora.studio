namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
	using System;
	using System.Collections.Generic;

    /// <summary>
    /// Object representing service to manage user roles
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class UserRoleService : IDisposable
	{
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		private readonly IUserRoleDataService _userRoleDataService;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleService"/> class.
		/// </summary>
		/// <param name="userRoleDataService">The user role data service.</param>
		public UserRoleService(IUserRoleDataService userRoleDataService)
		{
			_userRoleDataService = userRoleDataService;
		}

		/// <summary>
		/// Gets all user roles
		/// </summary>
		/// <returns></returns>
		public IEnumerable<UserRole> GetAll()
		{
			return _userRoleDataService.GetAll();
		}

		/// <summary>
		/// Gets the roles of the user
		/// </summary>
		/// <param name="user">The user to get the roles of.</param>
		/// <returns></returns>
		public List<UserRole> GetRolesOfUser(User user)
		{
			try
			{
				return _userRoleDataService.GetRolesOfUser(user);
			} catch(Exception exception)
			{
				Logger.Info($"Exception while fetching roles of user {user.Id}: {exception.InnerException}");
				return new List<UserRole>();
			}
		}

		/// <summary>
		/// Saves the 
		/// </summary>
		/// <param name="profile">The profile.</param>
		/// <exception cref="System.ApplicationException">Exception while saving UserProfile</exception>
		public UserRole AddRoleTypeForUser(User user, UserRoleType userRoleType)
		{
			UserRole userRole = _userRoleDataService.GetByType(userRoleType);

			UserRoles userRoles = new UserRoles
			{
				UserRole = userRole,
				User = user,
				CreationDate = DateTime.Now,
				ModifiedDate = DateTime.Now
			};

			_userRoleDataService.Add(userRoles);
			_userRoleDataService.Save();

			return userRole;
		}

		#region IDisposable Support
		private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				disposedValue = true;
			}
		}

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
		{
			Dispose(true);
		}
		#endregion
	}
}
