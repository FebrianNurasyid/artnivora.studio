namespace Artnivora.Studio.Portal.Business.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Object representing profile of user in application
    /// </summary>
    public class UserProfile
    {
        private Guid _id;

        [ForeignKey("UserId")]
        private User _user;
        private string _salutation;
        private string _firstName;
        private string _lastName;
		private string _insertion;
		private string _maidenName;
        private DateTime? _birthdate;
        private string _profilePicture;
        private bool _telephoneListAgree;
        private bool _holidayWeekAgree;
        private bool _isComplete;
        private string _gender;

		private Address _contactAddress;
        private string _phoneNumber;
        private string _mobileNumber;
        private string _initial;

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid Id
        {
            get 
            { 
                return this._id; 
            }
            set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user identifier.
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
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Salutation
		{
			get
			{
				return this._salutation;
			}
			set
			{
				this._salutation = value;
			}
		}

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value;
			}
		}
        /// <summary>
        /// Initial
        /// </summary>
        public string Initial 
        {
            get { 
                return this._initial; 
            }
            set {
                this._initial = value;
            }
        }
		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		public string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				this._lastName = value;
			}
		}

		/// <summary>
		/// Gets or sets the insertion.
		/// </summary>
		/// <value>
		/// The initials.
		/// </value>
		public string Insertion
        {
            get
            {
                return this._insertion;
            }
            set
            {
                this._insertion = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the maiden.
        /// </summary>
        /// <value>
        /// The name of the maiden.
        /// </value>
        public string MaidenName
        {
            get
            {
                return this._maidenName;
            }
            set
            {
                this._maidenName = value;
            }
        }

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        /// <value>
        /// The birthdate.
        /// </value>
        public DateTime? Birthdate 
        { 
            get
            {
                return this._birthdate;
            }
            set
            {
                this._birthdate = value;
            }
        }

        /// <summary>
        /// Gets or sets the contact address.
        /// </summary>
        /// <value>
        /// The contact address.
        /// </value>
        [ForeignKey("ContactAddressId")]
        public Address ContactAddress
		{
			get
			{
				return this._contactAddress;
			}
			set
			{
				this._contactAddress = value;
			}
		}

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		/// <value>
		/// The phone number.
		/// </value>
		public string PhoneNumber
        {
            get
            {
                return this._phoneNumber;
            }
            set
            {
                this._phoneNumber = value;
            }
        }


        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        public string MobileNumber
        {
            get
            {
                return this._mobileNumber;
            }
            set
            {
                this._mobileNumber = value;
            }
        }
        /// <summary>
        /// get or set the profile picture userid\filename
        /// </summary>
        public string ProfilePicture
        {
            get
            {
                return this._profilePicture;
            }
            set
            {
                this._profilePicture = value;
            }
        }

        /// <summary>
        /// get or set the flag of complete on user profile
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return this._isComplete;
            }
            set
            {
                this._isComplete = value;
            }
        }

        
        public bool TelephoneListAgree
        {
            get
            {
                return this._telephoneListAgree;
            }
            set
            {
                this._telephoneListAgree = value;
            }
        }
        public bool HolidayWeekAgree
        {
            get
            {
                return this._holidayWeekAgree;
            }
            set
            {
                this._holidayWeekAgree = value;
            }
        }
        public string Gender {
            get { return this._gender; }
            set { this._gender = value; }
        }
    }
}
