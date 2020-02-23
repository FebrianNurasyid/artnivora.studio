namespace Artnivora.Studio.Portal.Business.Models.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MessageAttachments
    {
        private Guid _id;
        private Guid _messageId;
        private Guid _messageAttachementId;

        private Message _message;
        private MessageAttachment _attachment;



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
        public Guid MessageId
        {
            get
            {
                return this._messageId;
            }
            set
            {
                this._messageId = value;
            }
        }
        public Guid MessaageAttachmentId
        {
            get
            {
                return this._messageAttachementId;
            }
            set
            {
                this._messageAttachementId = value;
            }
        }

        public Message Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
        public MessageAttachment Attachment
        {
            get
            {
                return this._attachment;
            }
            set
            {
                this._attachment = value;
            }
        }
    }
}
