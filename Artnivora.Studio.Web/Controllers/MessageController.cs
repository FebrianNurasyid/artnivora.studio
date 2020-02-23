namespace Artnivora.Studio.Portal.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using System;
    using Microsoft.AspNetCore.Http;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : BaseMessageBoxController
    {
        public MessageController(MessageBoxService messageBoxService, IHttpContextAccessor httpContextAccessor, 
                                IConfiguration configuration) :
                               base(messageBoxService, httpContextAccessor,configuration)
        {
        }

        [HttpGet("[action]")]
        public override IActionResult Inbox(int offset, int limit)
        {
            return base.Inbox(offset, limit);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{mailBoxId}")]
        public override IActionResult GetMessageById(Guid mailBoxId)
        {
            return base.GetMessageById(mailBoxId);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult SetMailBoxAsRead([FromBody]SetMailboxAsRead mailBoxId)
        {
            return base.SetMailBoxAsRead(mailBoxId);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override async Task<IActionResult> AddMessageAttachment(IFormFile file)
        {
            return await base.AddMessageAttachment(file);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{attachmentId}")]
        public override IActionResult GetMessageAttachmentById(Guid attachmentId)
        {
            return base.GetMessageAttachmentById(attachmentId);
        }
    }
}