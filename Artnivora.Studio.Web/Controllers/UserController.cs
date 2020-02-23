namespace Artnivora.Studio.Portal.Web.Controllers
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseUserController
    {
        public UserController(VolunteerProfileService volunteerProfileService,
                              ParticipantProfileService participantProfileService,
                              UserService userService,
                              UserRoleService userRoleService,
                              EmailService emailService) :
                base(volunteerProfileService,
                     participantProfileService,
                     userService,
                     userRoleService,
                     emailService)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public override IActionResult GetAll()
        {
            return base.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        public override IActionResult ById(Guid id)
        {
            return base.ById(id);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult Register([FromBody] UserWithUserProfile userWithUserProfile, [FromQuery] string userroletype)
        {
            return base.Register(userWithUserProfile, userroletype);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult TokenConfirmation([FromQuery] Guid token, [FromQuery] bool isRecoveryPassword)
        {
            return base.TokenConfirmation(token,isRecoveryPassword);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult ForgotPassword([FromQuery] string email)
        {
            return base.ForgotPassword(email);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult ConfigurePassword([FromBody] User user, [FromQuery] bool isRecoveryPassword)
        {
            return base.ConfigurePassword(user, isRecoveryPassword);
        }
    }
}
