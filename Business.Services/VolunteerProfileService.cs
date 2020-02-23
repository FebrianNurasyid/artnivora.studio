namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object representing business service to manage volunteer profiles
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class VolunteerProfileService : UserProfileService, IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IVolunteerProfileDataService _volunteerProfileDataService;
        private readonly IVolunteerFunctionDataService _volunteerFunctionDataService;
        private readonly IUserProfileDataService _userProfileDataService;
        private readonly IUserDataService _userDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolunteerProfileService"/> class.
        /// </summary>
        public VolunteerProfileService(
                                       IVolunteerProfileDataService volunteerProfileDataService, 
                                       IVolunteerFunctionDataService volunteerFunctionDataService,
                                       IUserProfileDataService userProfileDataService, IUserDataService userDataService) : base(userProfileDataService)
        {
            _volunteerProfileDataService = volunteerProfileDataService;
            _userProfileDataService = userProfileDataService;
            _userDataService = userDataService;
        }

        #region Business service methods:

        /// <summary>
        /// Gets the volunteer profile by user id.
        /// </summary>
        /// <param name="Id">The user id.</param>
        /// <returns></returns>
        public IVolunteerProfile GetVolunteerByUser(User user)
        {
            try
            {
                return _volunteerProfileDataService.GetByUser(user);
            }
            catch (Exception exception)
            {
                Logger.Info($"Exception while fetching {exception.InnerException}");
                return null;
            }
        }

        /// <summary>
        /// Gets the volunteerprofile by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public IVolunteerProfile GetVolunteerProfileById(Guid Id)
        {
            try
            {
                return _volunteerProfileDataService.GetById(Id);
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
        /// <exception cref="System.ArgumentNullException">profile</exception>
        public void Save(VolunteerProfile profile)
        {
            try
            {
                if (profile.Id == null || profile.Id == Guid.Empty)
                {
                    _userProfileDataService.Add(profile.UserProfile);
                    _userProfileDataService.Save();
                    _volunteerProfileDataService.Add(profile);
                    _volunteerProfileDataService.Save();
                }
                else
                {
                    _userDataService.Update(profile.UserProfile.User);
                    _userDataService.Save();
                    _userProfileDataService.Update(profile.UserProfile);
                    _userProfileDataService.Save();
                    _volunteerProfileDataService.Update(profile);
                    _volunteerProfileDataService.Save();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving UserProfile", exception);
            }
        }

        /// <summary>
        /// Gets the available volunteer functions.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">profile</exception>
        public IEnumerable<VolunteerFunction> GetAvailableVolunteerFunctions(VolunteerProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            using(VolunteerFunctionService service = new VolunteerFunctionService(_volunteerFunctionDataService))
            {
                var functions = service.GetAllFunctions(profile.TargetAudiences);
                var listFunctions = new List<VolunteerFunction>(functions);

                if (!profile.IsCoreVolunteer) {
                    foreach (var function in functions)
                    {
                        if (function.IsCoreFunction)
                        {
                            listFunctions.Remove(function);
                        }
                    }
                }

                return listFunctions;
            }            
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        #endregion
    }
}