namespace Artnivora.Studio.Portal.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using System;

    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : BaseDashboardController
    {
        public DashboardController(UserService userService,
                            UserProfileService profileService) :
                            base(userService, profileService)
        {
        }

        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        public override IActionResult ByUserId(Guid id)
        {
            return base.ByUserId(id);
        }
    }
}
