using System;

namespace Artnivora.Studio.Portal.Business.Models
{
    /// <summary>
    /// Represents user role entity
    /// </summary>
    public class UserRole
    {
		private Guid _id;
		private string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        public UserRole()
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
		/// Gets or sets the key.
		/// </summary>
		/// <value>
		/// The street.
		/// </value>
		public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
            }
        }

		public UserRoleType AsType()
		{
			return (UserRoleType) Enum.Parse(typeof(UserRoleType), Key);
		}
    }
}
