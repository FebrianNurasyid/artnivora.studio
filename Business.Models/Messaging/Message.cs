namespace Artnivora.Studio.Portal.Business.Models.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Represents internal message object.
    /// </summary>
    public class Message
    {
        private Guid _id;
        private Guid _senderid;
        private string _subject;
        private string _body;
        private DateTime _messageSendDateTime;


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
        [Column("sender")]
        public Guid SenderId
        {
            get
            {
                return this._senderid;
            }
            set
            {
                this._senderid = value;
            }
        }
        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }
        public string Body
        {
            get
            {
                return this._body;
            }
            set
            {
                this._body = value;
            }
        }
        public DateTime MessageSendDateTime
        {
            get
            {
                return this._messageSendDateTime;
            }
            set
            {
                this._messageSendDateTime = value;
            }
        }

        public IEnumerable<MessageAttachments> MessageAttachments { get; set; }
        public IEnumerable<Recipients> Recipients { get; set; }
        public IEnumerable<MailBox> MailBoxes { get; set; }
        public MessageContact Sender { get; set; }
    }
}
