namespace Artnivora.Studio.Portal.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Object representing target audience object
    /// </summary>
    public class TargetAudience
    {
        private Guid _id;
        private string _name;
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetAudience"/> class.
        /// </summary>
        public TargetAudience()
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
    }
}