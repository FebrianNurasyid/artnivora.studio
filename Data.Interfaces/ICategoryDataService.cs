namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface ICategoryDataService
    {
        IEnumerable<Mst_Category> GetAllCategory();
        //Mst_Category GetById(Guid Id);
        Mst_Category GetCategoryById(Guid Id);
        void Add(Mst_Category entity);
        void Delete(Mst_Category entity);
        void Update(Mst_Category entity);
        void Save();     
        //void DeleteDiv(Mst_Division entity);
    }
}

