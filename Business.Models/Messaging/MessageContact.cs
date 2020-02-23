namespace Artnivora.Studio.Portal.Business.Models.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MessageContact
    {
        private Guid _id;
        private Guid _userId;
        private string _userFullName;

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
        public Guid UserId
        {
            get
            {
                return this._userId;
            }
            set
            {
                this._userId = value;
            }
        }
        public string UserFullName
        {
            get
            {
                return this._userFullName;
            }
            set
            {
                this._userFullName = value;
            }
        }

        public IEnumerable<MailBox> MailBoxes { get; set; }
        public IEnumerable<Message> SendMessages { get; set; }
    }
}
