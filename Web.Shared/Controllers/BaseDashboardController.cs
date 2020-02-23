using Artnivora.Studio.Portal.Business.Services;
using Artnivora.Studio.Portal.Web.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{
    public abstract class BaseDashboardController : BaseController
    {
        private readonly UserProfileService _profileService;
        private readonly UserService _userService;


        public BaseDashboardController(UserService userService, UserProfileService profileService) : base()
        {
            this._profileService = profileService;
            this._userService = userService;
        }

        public virtual IActionResult ByUserId(Guid id)
        {
            try
            {
                var res = new UserDashboard();
                var user = _userService.GetById(id);
                if (user != null)
                {
                    var userProfile = _profileService.GetByUser(user);
                    res.FirstName = userProfile.FirstName;
                    res.LastName = userProfile.LastName;
                    res.RegisteredCount = 5;
                    res.RequestCount = 4;
                    res.UnReadCount = 1;
                    res.UserProfileIsComplete = userProfile.IsComplete;
                }
                else
                {
                    return NotFound("User Not Found");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get users");
                return BadRequest(new { message = "Error in getting users..." });
            }
        }

    }
}
