namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface IRoleDataSerive
    {
        IEnumerable<Mst_Role> GetAllRole();

        /*serProfile GetById(Guid Id);*/

        Mst_Role GetRoleById(Guid Id);
        void Add(Mst_Role entity);
        void Delete(Mst_Role entity);
        void Update(Mst_Role entity);
        void Save();

        //IEnumerable<Mst_Division> GetAllCategory();
        //void DeleteDiv(Mst_Division entity);
    }
}

