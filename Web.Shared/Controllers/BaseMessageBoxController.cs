namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Web.Shared.Constants;
    using Artnivora.Studio.Portal.Web.Shared.Utilities;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseMessageBoxController : BaseController
    {
        private readonly MessageBoxService _messageBoxService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Guid? _userId;
        private readonly IConfiguration _configuration;

        public BaseMessageBoxController(MessageBoxService messageBoxService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base()
        {
            _messageBoxService = messageBoxService;
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.GetUserId();
            _configuration = configuration;
        }

        public virtual IActionResult Inbox(int offset = 0, int limit = 100)
        {
            try
            {
                if (_userId == null)
                    return Unauthorized();

                if (limit <= 0)
                    limit = 20;
                var path = _httpContextAccessor.GetBaseUri().LocalPath.ToString();

                var mailBoxs = _messageBoxService.GetInboxMessages(_userId.Value, limit, offset).ToList();
                int count = mailBoxs.Count();
                int prevOffset = offset - limit > 0 ? offset - limit : 0;
                int lastOffSet = count - limit > 0 ? ((((count / limit)) - (count % limit > 0 ? 0 : 1)) * limit) : 0;
                int nextOffset = count > (offset + limit) ? offset + limit : offset;

                if (count < offset)
                    mailBoxs = new List<MailBox>();

                var response = new ResponseUserMailbox()
                {
                    LinkInfo = new LinkInfo
                    {
                        TotalCount = mailBoxs.Count(),
                        Url = _httpContextAccessor.GetAbsoluteUri().PathAndQuery,
                        FirstUrl = $"{path}?offset=0&limit={limit}",
                        NextUrl = $"{path}?offset={nextOffset}&limit={limit}",
                        PrevUrl = $"{path}?offset={prevOffset}&limit={limit}",
                        LastUrl = $"{path}?offset={lastOffSet}&limit={limit}",
                    },
                    Messages = new List<ResponseMessage>()
                };
                foreach (var mb in mailBoxs.ToList())
                {
                    response.Messages.Add(new ResponseMessage()
                    {
                        Id = mb.Id.ToString(),
                        Body = mb.Message.Body,
                        Date = mb.Message.MessageSendDateTime,
                        From = mb.Message.Sender.UserFullName,
                        To = mb.MessageContact.UserFullName,
                        Subject = mb.Message.Subject,
                        IsRead = mb.IsRead
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get inbox messages");
                return BadRequest(new { message = "Error in getting messages..." });
            }
        }
        public virtual IActionResult Archived(int offset = 0, int limit = 100)
        {
            try
            {
                var messages = _messageBoxService.GetArchivedMessages(_userId.Value, limit, offset);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get archived messages");
                return BadRequest(new { message = "Error in getting archived messages..." });
            }
        }
        public virtual IActionResult SendItems(int offset = 0, int limit = 100)
        {
            try
            {
                var messages = _messageBoxService.GetMessagesOfSender(_userId.Value, limit, offset);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get send messages");
                return BadRequest(new { message = "Error in getting send messages..." });
            }
        }
        public virtual IActionResult GetMessageById(Guid id)
        {
            try
            {
                Message message = _messageBoxService.GetMessageById(id);
                byte[] bytes = Encoding.Default.GetBytes(message.Body);
                message.Body = Encoding.UTF8.GetString(bytes);


                if (message != null)
                {
                    var response = new ResponseMessage()
                    {
                        Id = message.Id.ToString(),
                        Subject = message.Subject,
                        Body = message.Body,
                        MessageSendDateTime = message.MessageSendDateTime,
                        From = message.Sender.UserFullName
                        //MessageAttachments = message.MessageAttachments,
                        //Sender = message.Sender,
                        //Recipients = message.Recipients,
                        //MailBoxes = message.MailBoxes,
                        //SenderId = message.SenderId
                    };

                    return Ok(response);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get message by id.");
                return BadRequest(new { message = "Error in getting message by id..." });
            }
        }
        public virtual IActionResult SetMailBoxAsRead(SetMailboxAsRead request)
        {
            try
            {
                _messageBoxService.SetMailBoxAsRead(request.MailboxId);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get message by id.");
                return BadRequest(new { message = "Error in getting message by id..." });
            }
        }
        public virtual async Task<IActionResult> AddMessageAttachment(IFormFile file)
        {
            MessageAttachment messageAttachment = new MessageAttachment();

            string webRootPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData));

            string target = $"\\{_configuration["AppSettings:Target"]}";

            string attachmentFolderPath = $"{webRootPath}{GlobalConstants.ATTACHMENT_PATH}{target}\\{_userId}";

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH}"))
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH}");

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH}{target}"))
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH}{target}");

            if (!Directory.Exists($"{attachmentFolderPath}"))
                Directory.CreateDirectory($"{attachmentFolderPath}");

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(file.FileName, out contentType))
                contentType = "application/octet-stream";

            var filePath = $"{attachmentFolderPath}\\{file.FileName}";
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            using (var stream = System.IO.File.Create(filePath))
                await file.CopyToAsync(stream);

            messageAttachment.FilePath = filePath;
            messageAttachment.ContentType = contentType;
            _messageBoxService.AddAttachmentMessage(messageAttachment);

            var response = new ResponseAttacmentMessage
            {
                FileName = file.FileName,
                Id = messageAttachment.Id.ToString(),
                Path = filePath
            };


            return Ok(response);
        }
        public virtual IActionResult GetMessageAttachmentById(Guid attachmentId)
        {
            try
            {
                var message = _messageBoxService.GetMessageAttachmentById(attachmentId);
                if (message != null)
                {
                    var net = new System.Net.WebClient();
                    var data = net.DownloadData(message.FilePath);
                    var content = new System.IO.MemoryStream(data);
                    var contentType = "APPLICATION/octet-stream";
                    var fileName = "something.bin";
                    return File(content, contentType, fileName);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get message by id.");
                return BadRequest(new { message = "Error in getting message by id..." });
            }
        }
    }
}
