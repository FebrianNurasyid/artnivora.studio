namespace Artnivora.Studio.Portal.Business.Models
{
	using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents authentication collection of properties
    /// </summary>
    public class AuthenticateModel
	{
        private string _username;
        private string _password;

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
		public string Username 
        { 
            get { return this._username; } 
            set { this._username = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
		public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }
	}
}
