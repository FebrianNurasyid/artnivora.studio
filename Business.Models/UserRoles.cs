using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artnivora.Studio.Portal.Business.Models
{
    /// <summary>
    /// Represents user-user role entity
    /// </summary>
    public class UserRoles
    {
		private Guid _id;

		[ForeignKey("UserId")]
		private User _user;

		[ForeignKey("UserRoleId")]
		private UserRole _userRole;

		private DateTime _creationDate;

		private DateTime _modifiedDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoles"/> class.
        /// </summary>
        public UserRoles()
        {

        }

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
		/// Gets or sets the createdDate.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public DateTime CreationDate
		{
			get
			{
				return this._creationDate;
			}
			set
			{
				this._creationDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the modifiedDate.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public DateTime ModifiedDate
		{
			get
			{
				return this._modifiedDate;
			}
			set
			{
				this._modifiedDate = value;
			}
		}


		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The identifier.
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
		/// Gets or sets the user role.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public UserRole UserRole
		{
			get
			{
				return this._userRole;
			}
			set
			{
				this._userRole = value;
			}
		}
	}
}
