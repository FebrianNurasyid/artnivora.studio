namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Enums;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Artnivora.Studio.Portal.Web.Shared.Constants;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using Artnivora.Studio.Portal.Web.Shared.Utilities;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Represents base HVB MVC User Profile Controller
    /// </summary>
    /// <seealso cref="Artnivora.Studio.Portal.Web.Shared.Controllers.BaseController" />
    public abstract class BaseUserProfileController : BaseController
    {
        private readonly UserService _userService;
        private readonly ParticipantProfileService _participantProfileService;
        private readonly VolunteerProfileService _volunteerProfileService;
        private readonly UserRoleService _userRoleService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUserController"/> class.
        /// </summary>
        /// <param name="volunteerProfileService">The volunteer profile service.</param>
        /// <param name="participantProfileService">The participant profile service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="userRoleService">The user role service.</param>
        /// <param name="emailService">The email service.</param>
        public BaseUserProfileController(VolunteerProfileService volunteerProfileService,
                                  ParticipantProfileService participantProfileService,
                                  UserService userService,
                                  UserRoleService userRoleService, IHostingEnvironment hostingEnvironment,
                                  IHttpContextAccessor httpContextAccessor,
                                  IConfiguration configuration) : base()
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._volunteerProfileService = volunteerProfileService;
            this._participantProfileService = participantProfileService;
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the profile information of the user
        /// </summary>
        /// <returns>Return object containing user profile infoirmation if available.</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        public abstract IActionResult ByUserId(Guid id);

        /// <summary>
        /// Get All User Profiles
        /// </summary>
        /// <returns></returns>
        public abstract Task<IActionResult> GetAllUserProfiles();
        /// <summary>
        /// Upload Profile Picture
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual async Task UploadProfilePicture(IFormFile file)
        {
            string webRootPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData));

            var userId = _httpContextAccessor.GetSubject();
            var user = _userService.GetById(new Guid(userId));
            var userProfile = _participantProfileService.GetByUser(user);

            if(userProfile.ProfilePicture != null)
            {
                if (System.IO.File.Exists(userProfile.ProfilePicture))
                {
                    System.IO.File.Delete(userProfile.ProfilePicture);
                }
            }

            string target = $"\\{_configuration["AppSettings:Target"]}";

            string pictureFolderPath = $"{webRootPath}{GlobalConstants.PHOTO_PATH}{target}\\{userId}";


            if (!Directory.Exists($"{webRootPath}{GlobalConstants.PHOTO_PATH}"))
            {
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.PHOTO_PATH}");
            }

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.PHOTO_PATH}{target}"))
            {
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.PHOTO_PATH}{target}");
            }

            if (!Directory.Exists($"{pictureFolderPath}"))
            {
                Directory.CreateDirectory($"{pictureFolderPath}");
            }

            var filePath = $"{pictureFolderPath}\\{file.FileName}";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            userProfile.ProfilePicture = filePath;
            _participantProfileService.Save(userProfile);

            byte[] imageByte = await System.IO.File.ReadAllBytesAsync(filePath);
            await Response.Body.WriteAsync(imageByte, 0, imageByte.Length);
        }

        public virtual async Task<IActionResult> GetParticipantProfile()
        {
            try
            {
                var userId = _httpContextAccessor.GetSubject();
                var user = _userService.GetById(new Guid(userId));

                return Ok(_participantProfileService.GetParticipantProfileByUser(user));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error get ParticipantProfile.");
                return BadRequest(new { message = "Error in trying to get participant profile." });
            }
        }

        /// <summary>
        /// Post Participant Profile
        /// </summary>
        /// <param name="userWithUserProfile"></param>
        /// <returns>Participant Profile</returns>
        public virtual async Task<IActionResult> UploadParticipantProfile(UserWithUserProfile userWithUserProfile)
        {
            try
            {
                var userId = _httpContextAccessor.GetSubject();
                UserProfile userProfile = userWithUserProfile.UserProfile;
                User userInformation = userWithUserProfile.User;

                List<string> errors = new List<string>();

                var user = _userService.GetById(new Guid(userId));
                //if (userWithUserProfile)
                //{
                    ParticipantProfile participantProfile = (ParticipantProfile)_participantProfileService.GetParticipantProfileByUser(user);

                    //// basic user profile table
                    participantProfile.UserProfile.FirstName = userProfile.FirstName;
                    participantProfile.UserProfile.LastName = userProfile.LastName;
                    participantProfile.UserProfile.Insertion = userProfile.Insertion;
                    participantProfile.UserProfile.Initial = userProfile.Initial;
                    participantProfile.UserProfile.MaidenName = userProfile.MaidenName;
                    participantProfile.UserProfile.MobileNumber = userProfile.MobileNumber;
                    participantProfile.UserProfile.Salutation = userProfile.Salutation;
                    participantProfile.UserProfile.PhoneNumber = userProfile.PhoneNumber;
                    participantProfile.UserProfile.Birthdate = userProfile.Birthdate;
                    participantProfile.UserProfile.ContactAddress = userProfile.ContactAddress;
                    participantProfile.UserProfile.HolidayWeekAgree = userProfile.HolidayWeekAgree;
                    participantProfile.UserProfile.TelephoneListAgree = userProfile.TelephoneListAgree;
                    participantProfile.UserProfile.Gender = userProfile.Gender;
                    participantProfile.UserProfile.User.Email = userInformation.Email;
                    

                    //participant table
                    //participantProfile.HealthcareProviderId = userWithUserProfile.ParticipantProfile.HealthcareProviderId;

                    _participantProfileService.Save(participantProfile);
                    return Ok(participantProfile);
                //}

                //VolunteerProfile volunteerProfile = (VolunteerProfile)_volunteerProfileService.GetVolunteerByUser(user);

                ////// basic user profile table
                //volunteerProfile.UserProfile.FirstName = userProfile.FirstName;
                //volunteerProfile.UserProfile.LastName = userProfile.LastName;
                //volunteerProfile.UserProfile.Insertion = userProfile.Insertion;
                //volunteerProfile.UserProfile.Initial = userProfile.Initial;
                //volunteerProfile.UserProfile.MaidenName = userProfile.MaidenName;
                //volunteerProfile.UserProfile.MobileNumber = userProfile.MobileNumber;
                //volunteerProfile.UserProfile.Salutation = userProfile.Salutation;
                //volunteerProfile.UserProfile.PhoneNumber = userProfile.PhoneNumber;
                //volunteerProfile.UserProfile.Birthdate = userProfile.Birthdate;
                //volunteerProfile.UserProfile.ContactAddress = userProfile.ContactAddress;
                //volunteerProfile.UserProfile.HolidayWeekAgree = userProfile.HolidayWeekAgree;
                //volunteerProfile.UserProfile.TelephoneListAgree = userProfile.TelephoneListAgree;
                //volunteerProfile.UserProfile.User.Email = userInformation.Email;

                ////participant table
                ////volunteerProfile.MaritalStatus = userWithUserProfile.VolunteerProfile.MaritalStatus;


                //_volunteerProfileService.Save(volunteerProfile);
                //return Ok(volunteerProfile);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in Updating Participant Profile.");
                return BadRequest(new { message = "Error in Updating Participant Profile." });
            }
        }

        public virtual async Task GetImage()
        {
            var userId = _httpContextAccessor.GetSubject();
            var user = _userService.GetById(new Guid(userId));
            var userProfile = _volunteerProfileService.GetByUser(user);
            if(userProfile != null && userProfile.ProfilePicture != null && userProfile.ProfilePicture.Length > 1)
            {
                string webRootPath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData));
                string target = $"\\{_configuration["AppSettings:Target"]}";

                string filePath = $"{webRootPath}{GlobalConstants.PHOTO_PATH}{target}\\{userProfile.ProfilePicture}";
                byte[] imageByte = await System.IO.File.ReadAllBytesAsync(filePath);
                await Response.Body.WriteAsync(imageByte, 0, imageByte.Length);
            }
        }

        public virtual async Task<IActionResult> GetVolunteerProfile()
        {
            try
            {
                var userId = _httpContextAccessor.GetSubject();
                var user = _userService.GetById(new Guid(userId));

                return Ok(_volunteerProfileService.GetVolunteerProfileById(user.Id));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error get Volunteer Profile.");
                return BadRequest(new { message = "Error in trying to get participant profile." });
            }
        }

        #region Service Properties:

        /// <summary>
        /// Gets the volunteer profile service.
        /// </summary>
        /// <value>
        /// The volunteer profile service.
        /// </value>
        public VolunteerProfileService VolunteerProfileService
        {
            get { return this._volunteerProfileService; }
        }

        /// <summary>
        /// Gets the participant profile service.
        /// </summary>
        /// <value>
        /// The participant profile service.
        /// </value>
        public ParticipantProfileService ParticipantProfileService
        {
            get { return this._participantProfileService; }
        }

        /// <summary>
        /// Gets the user service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        public UserService UserService
        {
            get { return this._userService; }
        }

        /// <summary>
        /// Gets the user role service.
        /// </summary>
        /// <value>
        /// The user role service.
        /// </value>
        public UserRoleService UserRoleService
        {
            get { return this._userRoleService; }
        }
        /// <summary>
        /// Get Netcore Hosting Environment
        /// </summary>
        public IHostingEnvironment HostingEnvironment
        {
            get { return this._hostingEnvironment; }
        }
        public IHttpContextAccessor HttpContextAccessor
        {
            get { return this._httpContextAccessor; }
        }
        #endregion
    }
}