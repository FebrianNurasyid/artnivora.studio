namespace Artnivora.Studio.Portal.Data.Services
{

    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryDataService : ICategoryDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public CategoryDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Mst_Category> GetAllCategory()
        {
            return context.Mst_Category.ToList();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Mst_Category GetCategoryById(Guid Id)
        {
            return context.Mst_Category
                .Where(Mst_Category => Mst_Category.Id == Id)
                .FirstOrDefault();
        }

        public void Add(Mst_Category entity)
        {
            try
            {
                context.Mst_Category.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Delete(Mst_Category entity)
        {
            context.Mst_Category.Remove(entity);
        }

        public void Update(Mst_Category entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }


    }
}
