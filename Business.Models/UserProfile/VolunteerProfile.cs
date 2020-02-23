namespace Artnivora.Studio.Portal.Business.Models
{
    using Artnivora.Studio.Portal.Business.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Object representing volunteer profile
    /// </summary>
    public class VolunteerProfile : IVolunteerProfile
    {
        private bool _isCoreVolunteer;
        private MaritalStatusType _maritalStatus;
        private IEnumerable<TargetAudience> _targetAudiences;
        private IEnumerable<VolunteerFunction> _volunteerFunctions;
        private Guid _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolunteerProfile"/> class.
        /// </summary>
        /// <param name="isCoreVolunteer">if set to <c>true</c> [is core volunteer].</param>
        public VolunteerProfile()
        {
        }


        public Guid Id { get { return this._id; } set { this._id = value; } }

        [ForeignKey("UserProfileId")]
        private UserProfile _userProfile;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is core volunteer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is core volunteer; otherwise, <c>false</c>.
        /// </value>
        public bool IsCoreVolunteer
        {
            get
            {
                return this._isCoreVolunteer;
            }
            set
            {
                this._isCoreVolunteer = value;
            }
        }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>
        /// The marital status.
        /// </value>
        public MaritalStatusType MaritalStatus
        {
            get
            {
                return this._maritalStatus;
            }
            set
            {
                this._maritalStatus = value;
            }
        }

        /// <summary>
        /// Gets the target audiences.
        /// </summary>
        /// <value>
        /// The target audiences.
        /// </value>
        public IEnumerable<TargetAudience> TargetAudiences
        {
            get
            {
                return this._targetAudiences;
            }
            internal set
            {
                this._targetAudiences = value;
            }
        }

        /// <summary>
        /// Gets the volunteer functions.
        /// </summary>
        /// <value>
        /// The volunteer functions.
        /// </value>
        public IEnumerable<VolunteerFunction> VolunteerFunctions
        {
            get
            {
                return this._volunteerFunctions;
            }
            internal set
            {
                this._volunteerFunctions = value;
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

        /// <summary>
        /// Adds the target audience.
        /// </summary>
        /// <param name="targetAudience">The target audience.</param>
        /// <exception cref="ArgumentNullException">targetAudience</exception>
        //public void AddTargetAudience(TargetAudience targetAudience)
        //{
        //    if (targetAudience == null)
        //        throw new ArgumentNullException("targetAudience");

        //    var targetAudiences = new List<TargetAudience>(this.TargetAudiences);
        //    if (targetAudiences.Contains(targetAudience)) return;

        //    // If volunteer is core volunteer, he/she can only have one target audience, so current audiences will be cleared. 
        //    // So only one target audience can be active.
        //    if (this.IsCoreVolunteer) targetAudiences.Clear();

        //    targetAudiences.Add(targetAudience);
        //    this.TargetAudiences = targetAudiences;

        //    // Clear volunteer functions, because target audiences have changed.
        //    var volunteerFunctions = new List<VolunteerFunction>();
        //    this.VolunteerFunctions = volunteerFunctions;
        //}

        /// <summary>
        /// Adds the volunteer function.
        /// </summary>
        /// <param name="volunteerFunction">The volunteer function.</param>
        /// <exception cref="ArgumentNullException">volunteerFunction</exception>
        /// <exception cref="ApplicationException">Cannot add core volunteer function to none-core volunteer.</exception>
        //public void AddVolunteerFunction(VolunteerFunction volunteerFunction)
        //{
        //    if (volunteerFunction == null)
        //        throw new ArgumentNullException("volunteerFunction");

        //    if (!this.IsCoreVolunteer && volunteerFunction.IsCoreFunction)
        //        throw new ApplicationException("Cannot add core volunteer function to none-core volunteer.");

        //    var volunteerFunctions = new List<VolunteerFunction>(this.VolunteerFunctions);
        //    if (volunteerFunctions.Contains(volunteerFunction)) return;

        //    volunteerFunctions.Add(volunteerFunction);
        //    this.VolunteerFunctions = volunteerFunctions;
        //}
    }
}