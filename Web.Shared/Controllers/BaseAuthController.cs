namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseAuthController : BaseController
    {
        private readonly UserService _userService;
        private readonly UserAuthService _userAuthService;
        private readonly UserRoleService _userRoleService;

        public BaseAuthController(UserService userService,
                                  UserAuthService userAuthService,
                                  UserRoleService userRoleService) : base() {
            this._userService = userService;
            this._userAuthService = userAuthService;
            this._userRoleService = userRoleService;
        }

        public virtual IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            return AuthenticateUser(model, null);
        }

        public virtual IActionResult AuthenticateWithBearerToken([FromQuery] string bearerToken)
        {
            User user = _userService.GetUserByBearerToken(bearerToken);

            if(user != null)
            {
                List<string> roles = _userRoleService
                    .GetRolesOfUser(user)
                    .Select(role => role.Key).ToList();

                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.Token,
                    user.Email,
                    roles 
                });
            }
            return BadRequest(new { message = "User was not found!" });
        }

        protected IActionResult AuthenticateUser(AuthenticateModel model, List<UserRoleType> allowedRoles)
        {
            try
            {
                var user = _userAuthService.Authenticate(model.Username, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Error 400: Username or password is incorrect" });

                // Store the token into db
                _userService.Update(user);

                List<UserRole> userRoles = _userRoleService.GetRolesOfUser(user);

                bool isAllowed = true;

                if(allowedRoles != null)
                {
                    isAllowed = userRoles.Exists(
                        userRole => allowedRoles.Contains(userRole.AsType())
                    );
                }

                if (!isAllowed)
                {
                    return BadRequest(new { message = "You are not allowed to log in here" });
                }

                var roles = userRoles.Select(role => role.Key).ToArray();

                return Ok(new {
                    user.Id,
                    user.Username,
                    user.Token,
                    user.Email,
                    roles
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to authenticate");
                return BadRequest(new { message = "Error in authentication..." });
            }
        }

        public virtual IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch(Exception ex)
            {
                Logger.Error(ex, "Error in trying to get users");
                return BadRequest(new { message = "Error in getting users..." });
            }
        }

        public UserAuthService UserAuthService 
        { 
            get
            {
                return this._userAuthService;
            }
        }

        public UserService UserService
        {
            get
            {
                return this._userService;
            }
        }

        public UserRoleService UserRoleService
        {
            get
            {
                return this._userRoleService;
            }
        }
    }
}
