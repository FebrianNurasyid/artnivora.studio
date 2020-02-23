namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
	using System;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Object representing service to manage user profiles
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class UserProfileService : IDisposable
	{
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		private readonly IUserProfileDataService _userProfileDataService;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserProfileService"/> class.
		/// </summary>
		/// <param name="userProfileDataService">The user profile data service.</param>
		public UserProfileService(IUserProfileDataService userProfileDataService)
		{
			_userProfileDataService = userProfileDataService;
		}

		/// <summary>
		/// Gets all user profiles
		/// </summary>
		/// <returns></returns>
		public IEnumerable<UserProfile> GetAll()
		{
			return _userProfileDataService.GetAll();
		}

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await _userProfileDataService.GetAllAsync();
        }
        /// <summary>
        /// Gets the userprofile by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public UserProfile GetById(Guid Id)
		{
			try
			{
				return _userProfileDataService.GetById(Id);
			} catch(Exception exception)
			{
				Logger.Info($"Exception while fetching {exception.InnerException}");
				return null;
			}
		}

		/// <summary>
		/// Gets the userprofile by user id.
		/// </summary>
		/// <param name="Id">The user id.</param>
		/// <returns></returns>
		public UserProfile GetByUser(User user)
		{
			try
			{
				return _userProfileDataService.GetByUser(user);
			}
			catch (Exception exception)
			{
				Logger.Info($"Exception while fetching {exception.InnerException}");
				return null;
			}
		}

		/// <summary>
		/// Saves the specified profile.
		/// </summary>
		/// <param name="profile">The profile.</param>
		/// <exception cref="System.ApplicationException">Exception while saving UserProfile</exception>
		public void Save(UserProfile profile)
		{
			List<string> errors = new List<string>();
			try
			{
                if (profile.Id == null || profile.Id == Guid.Empty)
                {
                    _userProfileDataService.Add(profile);
                    _userProfileDataService.Save();
                }
                else
                {
                    _userProfileDataService.Update(profile);
                    _userProfileDataService.Save();
                }
			}
			catch (Exception exception)
			{
                throw new ApplicationException("Exception while saving UserProfile", exception);
			}	
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
