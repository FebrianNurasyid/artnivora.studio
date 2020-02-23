namespace Artnivora.Studio.Portal.Business.Models
{
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Object representing profile of participant
    /// </summary>
    /// <seealso cref="Artnivora.Studio.Portal.Business.Models.ParticipantProfile" />
    public class ParticipantProfile : IParticipantProfile
    {
        private string _healthcareProviderId;
        
        private Guid _id;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantProfile"/> class.
        /// </summary>
        public ParticipantProfile()
        {
            
        }
        public Guid Id 
        {
            get { return this._id; }
            set { this._id = value; }
        }

        
        [ForeignKey("UserProfileId")]
        private UserProfile _userProfile;
        /// <summary>
        /// Gets or sets the healthcare provider identifier.
        /// </summary>
        /// <value>
        /// The healthcare provider identifier.
        /// </value>
        public string HealthcareProviderId
        {
            get
            {
                return this._healthcareProviderId;
            }
            set
            {
                this._healthcareProviderId = value;
            }
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
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
    }
}