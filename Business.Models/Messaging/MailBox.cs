using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models.Messaging
{
    public class MailBox
    {
        private Guid _id;
        private Guid _userId;
        private Guid _messageId;
        private bool _isRead;
        private bool _isArchived;

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
            }
        }
        public Guid UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                this._userId = value;
            }
        }
        public Guid MessageId
        {
            get
            {
                return _messageId;
            }
            set
            {
                this._messageId = value;
            }
        }
        public bool IsRead
        {
            get
            {
                return _isRead;
            }
            set
            {
                this._isRead = value;
            }
        }
        public bool IsArchived
        {
            get
            {
                return _isArchived;
            }
            set
            {
                this._isArchived = value;
            }
        }

        public Message Message
        {
            get;
            set;
        }
        public MessageContact MessageContact { get; set; }
    }
}
