namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface IThemaDataService
    {
        IEnumerable<Mst_Thema> GetAllThema();

        /*serProfile GetById(Guid Id);*/

        Mst_Thema GetThemaById(Guid Id);
        void Add(Mst_Thema entity);
        void Delete(Mst_Thema entity);
        void Update(Mst_Thema entity);
        void Save();

        
        //void DeleteDiv(Mst_Division entity);
    }
}

