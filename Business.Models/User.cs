using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artnivora.Studio.Portal.Business.Models
{
	public class User
	{
		private Guid _id;
        private string _username { get; set; }
		private string _email { get; set; }
		private string _password { get; set; }
		private string _token { get; set; }		
		private DateTime _creationDate { get; set; }
		private Guid? _activation_token { get; set; }
		private bool _account_is_activated { get; set; }
		private Guid? _passwordrecoverytoken { get; set; }
		private DateTime? _passwordrecoverytokenexpirydate { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		public User()
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
		/// Gets or sets the username.
		/// </summary>
		/// <value>
		/// The username.
		/// </value>
		public string Username 
		{
			get
			{
				return this._username;
			}

			set
			{
				this._username = value;
			}
		}

		/// <summary>
		/// Gets or sets the token.
		/// </summary>
		/// <value>
		/// The token.
		/// </value>
		public string Token
		{
			get
			{
				return this._token;
			}

			set
			{
				this._token = value;
			}
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email
		{
			get
			{
				return this._email;
			}

			set
			{
				this._email = value;
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public string Password
		{
			get
			{
				return this._password;
			}

			set
			{
				this._password = value;
			}
		}

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
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
		/// Gets or sets the activation_token.
		/// </summary>
		/// <value>
		/// The activation_token.
		/// </value>
		public Guid? Activation_Token
		{
			get
			{
				return this._activation_token;
			}

			set
			{
				this._activation_token = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether account is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if [account is active]; otherwise, <c>false</c>.
		/// </value>
		public bool Account_Is_Activated
		{
			get
			{
				return this._account_is_activated;
			}
			set
			{
				this._account_is_activated = value;
			}
		}

		/// <summary>
		/// Gets or sets the PasswordRecoveryToken.
		/// </summary>
		/// <value>
		/// The PasswordRecoveryToken.
		/// </value>
		public Guid? PasswordRecoveryToken
		{
			get
			{
				return this._passwordrecoverytoken;
			}

			set
			{
				this._passwordrecoverytoken = value;
			}
		}

		/// <summary>
		/// Gets or sets the PasswordRecoveryTokenExpiryDate.
		/// </summary>
		/// <value>
		/// The PasswordRecoveryTokenExpiryDate.
		/// </value>
		public DateTime? PasswordRecoveryTokenExpiryDate
		{
			get
			{
				return this._passwordrecoverytokenexpirydate;
			}

			set
			{
				this._passwordrecoverytokenexpirydate = value;
			}
		}
	}
}
