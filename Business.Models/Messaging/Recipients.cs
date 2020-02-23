using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models.Messaging
{
    public class Recipients
    {
        private Guid _id;
        private Guid _messageId;
        private Guid _messageContactId;


        private Message _message;
        private MessageContact _messageContact;
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
        public Guid MessageContactId
        {
            get
            {
                return this._messageContactId;
            }
            set
            {
                this._messageContactId = value;
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
        public MessageContact MessageContact
        {
            get
            {
                return this._messageContact;
            }
            set
            {
                this._messageContact = value;
            }
        }
    }
}
