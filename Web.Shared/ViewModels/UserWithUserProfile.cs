using Artnivora.Studio.Portal.Business.Models;

namespace Artnivora.Studio.Portal.Web.Shared.ViewModels
{
	public class UserWithUserProfile
	{
		private User _user;

		private UserProfile _userProfile;

		private VolunteerProfile _volunteerProfile;

		private ParticipantProfile _participantProfile;
        private bool _isParticipant;
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User
		{
			get
			{
				return this._user;
			}
			set
			{
				this._user = value;
			}
		}

		/// <summary>
		/// Gets or sets the user profile.
		/// </summary>
		/// <value>
		/// The user profile.
		/// </value>
		public UserProfile UserProfile
		{
			get
			{
				return this._userProfile;
			}
			set
			{
				this._userProfile = value;
			}
		}

		/// <summary>
		/// Gets or sets the volunteer profile.
		/// </summary>
		/// <value>
		/// The volunteer profile.
		/// </value>
		public VolunteerProfile VolunteerProfile
		{
			get
			{
				return this._volunteerProfile;
			}
			set
			{
				this._volunteerProfile = value;
			}
		}

		/// <summary>
		/// Gets or sets the participant profile.
		/// </summary>
		/// <value>
		/// The participant profile.
		/// </value>
		public ParticipantProfile ParticipantProfile
		{
			get
			{
				return this._participantProfile;
			}
			set
			{
				this._participantProfile = value;
			}
		}

        public bool IsParticipant {
            get { return this._isParticipant; }
            set { this._isParticipant = value; }
        }
	}
}
