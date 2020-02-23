namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;

    public interface IUserRoleDataService
	{
		IEnumerable<UserRole> GetAll();

		List<UserRole> GetRolesOfUser(User user);

		void Save();

		void Add(UserRoles userRoles);

		UserRole GetByType(UserRoleType userRoleType);
	}
}
