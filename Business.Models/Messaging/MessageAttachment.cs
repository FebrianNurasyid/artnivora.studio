namespace Artnivora.Studio.Portal.Business.Models.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents message attachment object.
    /// </summary>
    public class MessageAttachment
    {
        private Guid _id;
        private string _filePath;
        private string _contentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageAttachment"/> class.
        /// </summary>
        public MessageAttachment()
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

        public IEnumerable<MessageAttachments> Attachments { get; set; }
    }
}
