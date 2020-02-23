using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models
{
    /// <summary>
    /// Represents address of person or other entity
    /// </summary>
    public class Address
    {
		private Guid _id;

		private string _street;
        private string _houseNumber;
        private string _zipcode;
        private string _city;
        private string _country;

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
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
		/// Gets or sets the street.
		/// </summary>
		/// <value>
		/// The street.
		/// </value>
		public string Street
        {
            get
            {
                return this._street;
            }
            set
            {
                this._street = value;
            }
        }

        /// <summary>
        /// Gets or sets the house number.
        /// </summary>
        /// <value>
        /// The house no.
        /// </value>
        public string HouseNumber
        {
            get
            {
                return this._houseNumber;
            }
            set
            {
                this._houseNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        /// <value>
        /// The zipcode.
        /// </value>
        public string ZipCode
        {
            get
            {
                return this._zipcode;
            }
            set
            {
                this._zipcode = value;
            }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
            }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;
            }
        }
    }
}
