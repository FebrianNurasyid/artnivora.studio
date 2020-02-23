namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IMessageBoxDataService
    {
        IEnumerable<MailBox> GetMailBoxAll();
        IEnumerable<MailBox> GetMailBoxByUserId(Guid userId, int limit, int offset);
        IEnumerable<MailBox> GetArchivedMessagesByUserId(Guid userId, int limit, int offset);
        IEnumerable<Message> GetMessagesOfSender(Guid userId, int limit, int offset);
        MailBox GetMailBoxById(Guid Id);
        void UpdateMailBox(MailBox entity);
        IEnumerable<Message> GetMessageAll();
        void AddMessage(Message entity);
        void DeleteMessage(Message entity);
        void UpdateMessage(Message entity);
        Message GetMessageById(Guid Id);
        void Save();
        void AddAttachmentMessage(MessageAttachment messageAttachment);
        MessageAttachment GetMessageAttachmentById(Guid attachmentId);

    }
}
