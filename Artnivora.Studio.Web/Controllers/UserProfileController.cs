namespace Artnivora.Studio.Portal.Web.Controllers
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Web.Shared.Constants;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using Artnivora.Studio.Portal.Web.Shared.Utilities;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    [Authorize]
    [Route("api/[controller]")]
    public class UserProfileController : BaseUserProfileController
    {
        public UserProfileController(
            VolunteerProfileService volunteerProfileService,
            ParticipantProfileService participantProfileService,
            UserService userService,
            UserRoleService userRoleService, IHostingEnvironment env, IHttpContextAccessor httpContextAccessor, IConfiguration configuration
        ) : base(volunteerProfileService, participantProfileService, userService, userRoleService, env, httpContextAccessor, configuration) {}

        [AllowAnonymous] //TODO: remove <- this to only allow this call if a user is logged in.
        [HttpGet("[action]/{id}")]
        public override IActionResult ByUserId(Guid id)
        {
            User user = UserService.GetById(id);
            if (user != null)
            {
                List<UserRole> userRoles = UserRoleService.GetRolesOfUser(user);
                bool isParticipant = userRoles.Exists(
                    userRole => userRole.AsType() == UserRoleType.Participant
                );

                bool isVolunteer = userRoles.Exists(
                    userRole => userRole.AsType() == UserRoleType.Volunteer
                );

                if(isParticipant)
                {
                    ParticipantProfile participantProfile =
                        (ParticipantProfile) ParticipantProfileService.GetParticipantProfileByUser(user);

                    return Ok(new
                    {
                        UserProfile = participantProfile,
                    });
                }

                if(isVolunteer)
                {
                    // TODO: get the volunteer profile from the database
                    VolunteerProfile volunteerProfile =
                            (VolunteerProfile) VolunteerProfileService.GetVolunteerByUser(user);

                    return Ok(new
                    {
                        UserProfile = volunteerProfile
                    });
                }
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override async Task<IActionResult> GetAllUserProfiles() 
        {
            var userProfiles =  await ParticipantProfileService.GetAllAsync();
            return Ok(userProfiles);
        }
        
        [HttpPost("[action]")]
        public override async Task UploadProfilePicture(IFormFile file)
        {
           await base.UploadProfilePicture(file);
        }

        /// <summary>
        /// Get Participant Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ParticipantProfile</returns>
        [HttpGet("[action]")]
        public override Task<IActionResult> GetParticipantProfile()
        {
            return base.GetParticipantProfile();
        }
        /// <summary>
        /// UpdateParticipant Profile
        /// </summary>
        /// <param name="userWithUserProfile"></param>
        /// <returns></returns>
        
        [HttpPost("[action]")]
        public override Task<IActionResult> UploadParticipantProfile([FromBody]UserWithUserProfile userWithUserProfile)
        {
            return base.UploadParticipantProfile(userWithUserProfile);

        }
        /// <summary>
        /// get Base64 Images
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public override async Task GetImage() {
            await base.GetImage();
        }

        /// <summary>
        /// Get Volunteer Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>VolunteerProfile</returns>
        [HttpGet("[action]")]
        public override Task<IActionResult> GetVolunteerProfile()
        {
            return base.GetVolunteerProfile();
        }
    }
}
