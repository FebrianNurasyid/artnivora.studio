namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Data.Interfaces;
	using System;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Object representing service to manage system users
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class UserService : IDisposable
	{
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		private readonly IUserDataService _userDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDataService">The user data service.</param>
        public UserService(IUserDataService userDataService)
		{
			_userDataService = userDataService;
		}

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>List of users.</returns>
        public IEnumerable<User> GetAll()
		{
			return _userDataService.GetAll();
		}

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>User or null if no user was found.</returns>
        public User GetById(Guid Id)
		{
			try
			{
				return _userDataService.GetById(Id);
			} catch(Exception exception)
			{
				Logger.Info($"Exception while fetching {exception.InnerException}");
				return null;
			}
		}
        /// <summary>
        /// Gets the user by identifier Asyncronously.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>User or null if no user was found.</returns>
        public async Task<User> GetByIdAsync(Guid Id)
        {
            try
            {
                return await _userDataService.GetByIdAsync(Id);
            }
            catch (Exception exception)
            {
                Logger.Info($"Exception while fetching {exception.InnerException}");
                return null;
            }
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The saved user</returns>
        /// <exception cref="ArgumentException">User with username {entity.Username} already exists!</exception>
        public User Save(User entity)
		{
			try
			{
				entity.CreationDate = DateTime.Now;

				// For now the username is equal to the email. This is temporary!
				entity.Username = entity.Email;

				User userWithSameUsername = _userDataService.GetByUsername(entity.Username);
				if (userWithSameUsername == null)
				{
					entity.Password = HashPasswordIfPresent(entity.Password);
					_userDataService.Add(entity);
					_userDataService.Save();
					return entity;
				} else
				{
					throw new ArgumentException($"User with username {entity.Username} already exists!");
				}
			}
			catch (Exception exception)
			{
				Logger.Info($"Exception while saving User {exception.InnerException}");
			}
			return null;
		}

		private string HashPasswordIfPresent(string unhashedPassword)
		{
			if(unhashedPassword == null || unhashedPassword.Length < 2)
			{
				return "";
			}

			return SecurePasswordHasher.Hash(unhashedPassword);
		}

		// I know this is unused, but it can be used later!
		private bool ValidateUsername(string username)
		{
			if(username == null)
			{
				return false;
			}
			return _userDataService.GetByUsername(username) != null;
		}

		/// <summary>
		/// Gets the user by activation token identifier.
		/// </summary>
		/// <param name="Activation_Token">The Activation_Token.</param>
		/// <returns>User or null if no user was found.</returns>
		public User GetUserByBearerToken(string bearerToken)
		{
			try
			{
				return _userDataService.GetUserByBearerToken(bearerToken);
			}
			catch (Exception exception)
			{
				Logger.Info($"Exception while fetching {exception.InnerException}");
				return null;
			}
		}

		/// <summary>
		/// Gets the user by activation token identifier.
		/// </summary>
		/// <param name="Activation_Token">The Activation_Token.</param>
		/// <returns>User or null if no user was found.</returns>
		public User GetUserByActivationToken(Guid token,bool isRecoveryPassword)
		{
			try
			{				
				return _userDataService.GetUserByActivationToken(token,isRecoveryPassword);
			}
			catch (Exception exception)
			{
				Logger.Info($"Exception while fetching {exception.InnerException}");
				return null;
			}
		}


		/// <summary>
		/// Update the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">User with username {entity.Username} already exists!</exception>
		public List<string> Update(User entity)
		{
			List<string> errors = new List<string>();
			try
			{				
				_userDataService.Update(entity);
				_userDataService.Save();
			}
			catch (Exception exception)
			{
				Logger.Info($"Exception while update User {exception.InnerException}");
				errors.Add("Exception while update user");
			}
			return errors;
		}

		/// <summary>
		/// Gets the user by email.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns>User or null if no user was found.</returns>
		public User GetUserByEmail(string email)
		{
			try
			{
				return _userDataService.GetUserByEmail(email);
			}
			catch (Exception exception)
			{
				Logger.Info($"Exception while fetching {exception.InnerException}");
				return null;
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
