namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Enums;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents base HVB MVC User Controller
    /// </summary>
    /// <seealso cref="Artnivora.Studio.Portal.Web.Shared.Controllers.BaseController" />
    public abstract class BaseUserController : BaseController
    {
        private readonly UserService _userService;
        private readonly UserRoleService _userRoleService;
        private readonly ParticipantProfileService _participantProfileService;
        private readonly VolunteerProfileService _volunteerProfileService;
        private readonly EmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUserController"/> class.
        /// </summary>
        /// <param name="volunteerProfileService">The volunteer profile service.</param>
        /// <param name="participantProfileService">The participant profile service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="userRoleService">The user role service.</param>
        /// <param name="emailService">The email service.</param>
        public BaseUserController(VolunteerProfileService volunteerProfileService,
                                  ParticipantProfileService participantProfileService,
                                  UserService userService,
                                  UserRoleService userRoleService,
                                  EmailService emailService) : base()
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._volunteerProfileService = volunteerProfileService;
            this._participantProfileService = participantProfileService;
            this._emailService = emailService;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Return object containing user objects if available.</returns>
        public virtual IActionResult GetAll()
        {
            try
            {
                List<User> users = _userService.GetAll().ToList();
                Logger.Info($"Has found ${users.Count} entities");

                List<User> usersWithoutPassword = new List<User>();
                foreach (User user in users)
                {
                    Logger.Info($"Has found ${user.Email}");
                    usersWithoutPassword.Add(user.WithoutPassword());
                }
                return Ok(usersWithoutPassword);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get users");
                return BadRequest(new { message = "Error in getting users..." });
            }
        }

        /// <summary>
        /// Gets user by identifier
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return object containing user object if available.</returns>
        public virtual IActionResult ById(Guid id)
        {
            try
            {
                User foundUser = _userService.GetById(id);
                if (foundUser != null)
                {
                    return Ok(foundUser.WithoutPassword());
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get user by id.");
                return BadRequest(new { message = "Error in getting user by id..." });
            }
        }

        /// <summary>
        /// Registers the specified new user.
        /// </summary>
        /// <param name="entity">The user entity.</param>
        /// <param name="userroletype">The userroletype.</param>
        /// <returns></returns>
        public virtual IActionResult Register([FromBody] UserWithUserProfile userWithUserProfile, [FromQuery] string userroletype)
        {
            try
            {
                User userInformation = userWithUserProfile.User;
                UserProfile userProfile = userWithUserProfile.UserProfile;

                List<string> errors = new List<string>();

                // Add new random guid for activation_token 
                userInformation.Activation_Token = Guid.NewGuid();

                // Save user object:
                // Fix this: errors.AddRange(_userService.Save(user));
                User savedUser = _userService.Save(userInformation);
                // Save user profile object:
                bool isParticpant = true;

                if (!string.IsNullOrEmpty(userroletype) && userroletype != UserRoleType.Participant.GetDescription())
                    isParticpant = false;

                if (isParticpant)
                {
                    _userRoleService.AddRoleTypeForUser(savedUser, UserRoleType.Participant);    // UserProfile already saved in here                       

                    ParticipantProfile participantProfile = new ParticipantProfile();

                    // basic user profile
                    participantProfile.UserProfile = new UserProfile();
                    participantProfile.UserProfile.FirstName = userProfile.FirstName;
                    participantProfile.UserProfile.LastName = userProfile.LastName;
                    participantProfile.UserProfile.Insertion = userProfile.Insertion;
                    participantProfile.UserProfile.MaidenName = userProfile.MaidenName;
                    participantProfile.UserProfile.MobileNumber = userProfile.MobileNumber;
                    participantProfile.UserProfile.Salutation = userProfile.Salutation;
                    participantProfile.UserProfile.PhoneNumber = userProfile.PhoneNumber;
                    participantProfile.UserProfile.Birthdate = userProfile.Birthdate;
                    participantProfile.UserProfile.ContactAddress = userProfile.ContactAddress;
                    participantProfile.UserProfile.Gender = userProfile.Gender ?? "";
                    participantProfile.UserProfile.User = savedUser;


                    _participantProfileService.Save(participantProfile);
                }
                else
                {
                    _userRoleService.AddRoleTypeForUser(savedUser, UserRoleType.Volunteer); // UserProfile already saved in here

                    VolunteerProfile volunteerProfile = new VolunteerProfile();

                    // basic user profile
                    volunteerProfile.UserProfile = new UserProfile();
                    volunteerProfile.UserProfile.FirstName = userProfile.FirstName;
                    volunteerProfile.UserProfile.LastName = userProfile.LastName;
                    volunteerProfile.UserProfile.Insertion = userProfile.Insertion;
                    volunteerProfile.UserProfile.MaidenName = userProfile.MaidenName;
                    volunteerProfile.UserProfile.MobileNumber = userProfile.MobileNumber;
                    volunteerProfile.UserProfile.Salutation = userProfile.Salutation;
                    volunteerProfile.UserProfile.PhoneNumber = userProfile.PhoneNumber;
                    volunteerProfile.UserProfile.Birthdate = userProfile.Birthdate;
                    volunteerProfile.UserProfile.ContactAddress = userProfile.ContactAddress;
                    volunteerProfile.UserProfile.Gender = userProfile.Gender ?? "";
                    volunteerProfile.UserProfile.User = savedUser;

                    _volunteerProfileService.Save(volunteerProfile);
                }

                // Send activation mail to new users
                _emailService.SendEmail(savedUser, EmailType.ActivationAccount);
                if (errors.Count == 0)
                {
                    return Ok(savedUser);
                }
                return BadRequest(new { errors });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to register new user.");
                return BadRequest(new { message = "Error in trying to register new user." });
            }
        }

        /// <summary>
        /// Activates the user.
        /// </summary>
        /// <param name="activatetoken">The activation token.</param>
        /// <returns>Returns 200 code or a 404 not found if no activation available</returns>
        public virtual IActionResult TokenConfirmation([FromQuery] Guid token, bool isRecoveryPassword)
        {
            try
            {
                User foundUser = _userService.GetUserByActivationToken(token, isRecoveryPassword);
                bool validToken = true;
                // TODO : Add validation here whether the activation token still valid or not                               

                if (foundUser != null)
                {
                    if (isRecoveryPassword)
                        validToken = foundUser.PasswordRecoveryTokenExpiryDate > DateTime.Now ? true : false;

                    return Ok(validToken);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to activate new user.");
                return BadRequest(new { message = "Error in trying to activate new user." });
            }
        }

        /// <summary>
        /// Method to call to start Forgot password proces.
        /// </summary>
        /// <param name="email">The email of the user that forgot his password</param>
        /// <returns>Returns 200 if proces is started, otherwise error.</returns>
        public virtual IActionResult ForgotPassword([FromQuery] string email)
        {
            try
            {
                User user = _userService.GetUserByEmail(email);
                List<string> errors = new List<string>();

                if (user != null)
                {
                    // Add new random guid for password recovery token
                    user.PasswordRecoveryToken = Guid.NewGuid();
                    user.PasswordRecoveryTokenExpiryDate = DateTime.Now.AddDays(1);

                    // Save user object:
                    errors.AddRange(_userService.Update(user));

                    // Send notification reset password to user
                    _emailService.SendEmail(user, EmailType.ResetPasswordAccount);

                    if (errors.Count > 0)
                    {
                        return BadRequest(new { errors });
                    }
                    return Ok();
                }
                errors.Add("Email address was not found");
                return NotFound(new { errors });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to process forgotpassword process.");
                return BadRequest(new { message = "Error in trying to process forgotpassword process." });
            }
        }

        /// <summary>
        /// Configures the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isRecoveryPassword">if set to <c>true</c> [is recovery password].</param>
        /// <returns></returns>
        public virtual IActionResult ConfigurePassword([FromBody] User user, [FromQuery] bool isRecoveryPassword)
        {
            try
            {
                User foundUser = _userService.GetUserByActivationToken(user.Activation_Token.Value, isRecoveryPassword);

                List<string> errors = new List<string>();

                if (foundUser != null)
                {
                    if (isRecoveryPassword) // if reset password
                    {
                        foundUser.PasswordRecoveryToken = null;
                        foundUser.PasswordRecoveryTokenExpiryDate = null;
                    }
                    else
                    {
                        foundUser.Activation_Token = null;
                        foundUser.Account_Is_Activated = true;
                    }
                    foundUser.Password = SecurePasswordHasher.Hash(user.Password);
                    // Save user object:
                    errors.AddRange(_userService.Update(foundUser));
                    if (errors.Count > 0)
                        return BadRequest(new { errors });

                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to configure password.");
                return BadRequest(new { message = "Error in trying to configure password." });
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
        /// Gets the email service.
        /// </summary>
        /// <value>
        /// The email service.
        /// </value>
        public EmailService EmailService
        {
            get { return this._emailService; }
        }

        #endregion
    }
}