namespace Artnivora.Studio.Portal.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class MessageBoxDataService : IMessageBoxDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public MessageBoxDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<MailBox> GetMailBoxAll()
        {
            return context.MailBox.Include(x => x.Message)
                .ToList();
        }
        public IEnumerable<MailBox> GetMailBoxByUserId(Guid userId, int limit, int offset)
        {
            return context.MailBox
                .Include(x => x.MessageContact)
                .Include(x => x.Message)
                .ThenInclude(x => x.Sender)
                .Where(x => x.UserId == userId && !x.IsArchived)
                .OrderByDescending(x => x.Message.MessageSendDateTime)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }
        public IEnumerable<MailBox> GetArchivedMessagesByUserId(Guid userId, int limit, int offset)
        {
            return context.MailBox
                .Include(x => x.MessageContact)
                .Include(x => x.Message)
                .ThenInclude(x => x.Sender)
                .Where(x => x.UserId == userId && x.IsArchived)
                .OrderByDescending(x => x.Message.MessageSendDateTime)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }
        public IEnumerable<Message> GetMessagesOfSender(Guid userId, int limit, int offset)
        {
            return context.Message
                .Where(x => x.SenderId == userId)
                .OrderByDescending(x => x.MessageSendDateTime)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }
        public MailBox GetMailBoxById(Guid Id)
        {
            return context.MailBox.Include(x => x.Message)
                .Where(x => x.Id == Id)
                .FirstOrDefault();
        }
        public void UpdateMailBox(MailBox entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<Message> GetMessageAll()
        {
            return context.Message
               .Include(x => x.Recipients).ThenInclude(x => x.MessageContact)
               .ToList();
        }
        public void AddMessage(Message entity)
        {
            try
            {
                context.Message.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void DeleteMessage(Message entity)
        {
            context.Message.Remove(entity);
        }
        public void UpdateMessage(Message entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public Message GetMessageById(Guid Id)
        {
            return context.MailBox
                .Include(x => x.Message).ThenInclude(x => x.Sender)
                .Where(x => x.Id == Id).Select(x => x.Message)
                .FirstOrDefault();
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void AddAttachmentMessage(MessageAttachment messageAttachment)
        {
            try
            {
                context.MessageAttachment.Add(messageAttachment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public MessageAttachment GetMessageAttachmentById(Guid attachmentId)
        {
            return context.MessageAttachment.Where(x => x.Id == attachmentId).FirstOrDefault();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
