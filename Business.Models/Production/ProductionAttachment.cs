namespace Artnivora.Studio.Portal.Business.Models.Production
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductionAttachment
    {
        private Guid _id;
        private string _filePath;
        private string _contentType;
        private string _fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionAttachment"/> class.
        /// </summary>
        public ProductionAttachment()
        {
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The file path.
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
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath
        {
            get
            {
                return this._filePath;
            }
            set
            {
                this._filePath = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType
        {
            get
            {
                return this._contentType;
            }
            set
            {
                this._contentType = value;
            }
        }

        /// <summary>
        /// Gets or sets the file name of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }

        public IEnumerable<ProductionAttachments> Attachments { get; set; }
    }
}
