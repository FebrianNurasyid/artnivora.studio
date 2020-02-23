namespace Artnivora.Studio.Portal.Business.Models.Production
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Represents internal production object.
    /// </summary>
    public class Production
    {
        private Guid _id;
        private string _title;
        private string _category;
        private string _themes;
        private string _concept;
        private string _status;        
        private DateTime _createdDate;
        private string _createdBy;
        private DateTime? _modifiedDate;
        private string _modifiedBy;
        private DateTime? _uploadedDate;
        private string _uploadedBy;
        private string _uploadedStatus;

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
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = value;
            }
        }
        public string Themes
        {
            get
            {
                return this._themes;
            }
            set
            {
                this._themes = value;
            }
        }
        public string Concept
        {
            get
            {
                return this._concept;
            }
            set
            {
                this._concept = value;
            }
        }
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return this._createdDate;
            }
            set
            {
                this._createdDate = value;
            }
        }
        public string CreatedBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
            }
        }
        public DateTime? ModifiedDate
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
        public string ModifiedBy
        {
            get
            {
                return this._modifiedBy;
            }
            set
            {
                this._modifiedBy = value;
            }
        }
        public DateTime? UploadedDate
        {
            get
            {
                return this._uploadedDate;
            }
            set
            {
                this._uploadedDate = value;
            }
        }
        public string UploadedBy
        {
            get
            {
                return this._uploadedBy;
            }
            set
            {
                this._uploadedBy = value;
            }
        }
        public string UploadedStatus
        {
            get
            {
                return this._uploadedStatus;
            }
            set
            {
                this._uploadedStatus = value;
            }
        }
        public IEnumerable<ProductionAttachments> ProductionAttachments { get; set; }
    }
}
