namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface IMasterDataService
    {
        IEnumerable<Mst_Division> GetAllDivision();

        /*serProfile GetById(Guid Id);*/

        Mst_Division GetById(Guid Id);
        void Add(Mst_Division entity);
        void Delete(Mst_Division entity);
        void Update(Mst_Division entity);        
        void Save();

        //IEnumerable<Mst_Division> GetAllCategory();
        //void DeleteDiv(Mst_Division entity);
    }
}

