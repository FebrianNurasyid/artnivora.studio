namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface IUserProfileDataService
	{
		IEnumerable<UserProfile> GetAll();

		UserProfile GetById(Guid Id);

		UserProfile GetByUser(User user);

		void Add(UserProfile entity);
		void Delete(UserProfile entity);
		void Update(UserProfile entity);
		UserProfile FindById(Guid Id);
		void Save();
        Task<IEnumerable<UserProfile>> GetAllAsync();

    }
}
