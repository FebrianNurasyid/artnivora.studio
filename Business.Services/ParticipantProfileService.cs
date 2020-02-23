namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ParticipantProfileService : UserProfileService, IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IParticipantProfileDataService _participantProfileDataService;
        private readonly IUserProfileDataService _userProfileDataService;
        private readonly IUserDataService _userDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantProfileService"/> class.
        /// </summary>
        public ParticipantProfileService(IParticipantProfileDataService participantProfileDataService,                                      
                                         IUserProfileDataService userProfileDataService, IUserDataService userDataService) : base(userProfileDataService)
        {
            _participantProfileDataService = participantProfileDataService;
            _userProfileDataService = userProfileDataService;
            _userDataService = userDataService;
        }

        #region Business service methods:

        /// <summary>
        /// Gets the userprofile by user id.
        /// </summary>
        /// <param name="Id">The user id.</param>
        /// <returns></returns>
        public IParticipantProfile GetParticipantProfileByUser(User user)
        {
            try
            {
                return _participantProfileDataService.GetByUser(user);
            }
            catch (Exception exception)
            {
                Logger.Info($"Exception while fetching {exception.InnerException}");
                return null;
            }
        }

        /// <summary>
        /// Gets the participantprofile by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public IParticipantProfile GetParticipantProfileByProfileId(Guid Id)
        {
            try
            {
                return _participantProfileDataService.GetById(Id);
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
        public void Save(ParticipantProfile profile)
        {
            try
            {
                if (profile.Id == null || profile.Id == Guid.Empty)
                {
                    _userProfileDataService.Add(profile.UserProfile);
                    _userProfileDataService.Save();
                    _participantProfileDataService.Add(profile);
                    _participantProfileDataService.Save();
                }
                else
                {
                    _userDataService.Update(profile.UserProfile.User);
                    _userDataService.Save();
                    _userProfileDataService.Update(profile.UserProfile);
                    _userProfileDataService.Save();
                    _participantProfileDataService.Update(profile);
                    _participantProfileDataService.Save();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving UserProfile", exception);
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