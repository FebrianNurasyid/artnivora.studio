namespace Artnivora.Studio.Portal.Business.Services
{
	using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
	using System.Text;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.Extensions.Options;
	using Microsoft.IdentityModel.Tokens;

	/// <summary>
	/// Object representing service to manage user authentication
	/// </summary>
	public class UserAuthService
	{

		private readonly AppSettings _appSettings;
		private readonly IUserDataService _userDataService;
		private readonly IUserRoleDataService _userRoleDataService;

		public UserAuthService(
			IOptions<AppSettings> appSettings, 
			IUserDataService userDataService,
			IUserRoleDataService userRoleDataService
		) 
		{
			_appSettings = appSettings.Value;
			this._userDataService = userDataService;
			this._userRoleDataService = userRoleDataService;
		}

		public User Authenticate(string username, string password)
		{
			var user = _userDataService.GetByUsername(username);

			// return null if user not found
			if (user == null)
				return null;

			var isSamePasswordAsHashed = SecurePasswordHasher.Verify(password, user.Password);
			
			if (!isSamePasswordAsHashed)
				return null;

			List<UserRole> userRoles = _userRoleDataService.GetRolesOfUser(user);
			string rolesAsString = string.Join(",",
				userRoles.Select(userRole => userRole.Key)
			);
			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, rolesAsString),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			user.Token = tokenHandler.WriteToken(token);

			return user;
		}
	}
}
