namespace Artnivora.Studio.Portal.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Object representing volunteer function
    /// </summary>
    public class VolunteerFunction
    {
        private Guid _id;
        private string _name;
        private string _description;
        private bool _isCoreFunction;
        private IEnumerable<TargetAudience> _targetAudiences;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolunteerFunction"/> class.
        /// </summary>
        public VolunteerFunction()
        {
            this.TargetAudiences = new List<TargetAudience>();
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is core function.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is core function; otherwise, <c>false</c>.
        /// </value>
        public bool IsCoreFunction
        {
            get
            {
                return this._isCoreFunction;
            }
            set
            {
                this._isCoreFunction = value;
            }
        }

        /// <summary>
        /// Gets or sets the target audiences.
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
    }
}