namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface IUserDataService
	{
		IEnumerable<User> GetAll();

		User GetById(Guid Id);

		User GetByUsername(string username);

		void Add(User entity);
		void Delete(User entity);
		void Update(User entity);
		User FindById(Guid Id);
		void Save();

		User GetUserByActivationToken(Guid token, bool isRecoveryPassword);

		User GetUserByBearerToken(string bearerToken);

		User GetUserByEmail(string email);
        Task<User> GetByIdAsync(Guid Id);

    }
}
