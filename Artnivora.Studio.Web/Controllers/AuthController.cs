namespace Artnivora.Studio.Portal.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using System;
    using System.Collections.Generic;

    [Authorize]
	[Route("api/[controller]")]
	public class AuthController : BaseAuthController
	{

		public AuthController(UserService userService, 
			                  UserAuthService userAuthService,
							  UserRoleService userRoleService) : base(userService, userAuthService, userRoleService)
        {
		}

		[AllowAnonymous]
		[HttpPost("Authenticate")]
        public override IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
			return base.AuthenticateUser(model, new List<UserRoleType> { UserRoleType.Participant, UserRoleType.Volunteer,UserRoleType.Admin });
		}

		[AllowAnonymous]
		[HttpPost("AuthenticateWithBearerToken")]
		public override IActionResult AuthenticateWithBearerToken([FromQuery] string bearerToken)
		{
			return base.AuthenticateWithBearerToken(bearerToken);
		}

		// TODO: Only use the proper role(s) here!
		[HttpGet]
		[Authorize(Roles = "Participant, Volunteer")]
		public override IActionResult GetAll()
		{
            return base.GetAll();
		}
	}
}
