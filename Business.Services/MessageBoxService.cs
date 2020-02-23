namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents messageBox service 
    /// </summary>
    public class MessageBoxService : IDisposable
    {
        private IMessageBoxDataService _messageBoxDataService;

        public MessageBoxService(IMessageBoxDataService messageBoxDataService)
        {
            _messageBoxDataService = messageBoxDataService;
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(Message message)
        {
            var mailBoxs = new List<MailBox>();
            // First add mailbox foeach recipient
            foreach (Recipients recipient in message.Recipients)
            {
                mailBoxs.Add(new MailBox()
                {
                    UserId = recipient.MessageContact.UserId,
                    IsRead = false,
                    IsArchived = false,
                });
            }

            _messageBoxDataService.AddMessage(message);
            _messageBoxDataService.Save();
        }

        /// <summary>
        /// Archives the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ArchiveMessage(MailBox mailBox)
        {
            mailBox.IsArchived = true;
            _messageBoxDataService.UpdateMailBox(mailBox);
            _messageBoxDataService.Save();
        }

        /// <summary>
        /// Set Message as Read.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SetMailBoxAsRead(Guid mailBoxId)
        {
            var mailbox = _messageBoxDataService.GetMailBoxById(mailBoxId);
            mailbox.IsRead = true;
            _messageBoxDataService.UpdateMailBox(mailbox);
            _messageBoxDataService.Save();
        }

        /// <summary>
        /// Gets the inbox messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<MailBox> GetInboxMessages(Guid userId, int limit, int offset)
        {
            return _messageBoxDataService.GetMailBoxByUserId(userId, limit, offset);
        }

        /// <summary>
        /// Gets the archived messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<MailBox> GetArchivedMessages(Guid userId, int limit, int offset)
        {
            return _messageBoxDataService.GetArchivedMessagesByUserId(userId, limit, offset);
        }

        /// <summary>
        /// Gets the send messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Message> GetMessagesOfSender(Guid userId, int limit, int offset)
        {
            return _messageBoxDataService.GetMessagesOfSender(userId, limit, offset);
        }

        /// <summary>
        /// Get message by id
        /// </summary>
        /// <param name="id">The mail box identifier.</param>
        /// <returns></returns>
        public Message GetMessageById(Guid id)
        {
            return _messageBoxDataService.GetMessageById(id);
        }

        /// <summary>
        /// Add message attachment
        /// </summary>
        /// <param name="messageAttachment">The content of message attachment.</param>
        /// <returns></returns>
        public void AddAttachmentMessage(MessageAttachment messageAttachment)
        {
            List<string> errors = new List<string>();
            try
            {                
                _messageBoxDataService.AddAttachmentMessage(messageAttachment);
                _messageBoxDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving UserProfile", exception);
            }
        }

        /// <summary>
        /// Get attachment message by id
        /// </summary>
        /// <param name="attachmentId">The attachment message identifier.</param>
        /// <returns></returns>
        public MessageAttachment GetMessageAttachmentById(Guid attachmentId)
        {
            return _messageBoxDataService.GetMessageAttachmentById(attachmentId);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
