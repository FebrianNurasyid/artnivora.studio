namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ThemaDataService : IThemaDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public ThemaDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Mst_Thema> GetAllThema()
        {
            return context.Mst_Thema.ToList();
        }

        public void Add(Mst_Thema entity)
        {
            try
            {
                context.Mst_Thema.Add(entity);
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

        public Mst_Thema GetThemaById(Guid Id)
        {
            return context.Mst_Thema
                .Where(Mst_Thema => Mst_Thema.Id == Id)
                .FirstOrDefault();
        }
        public void Delete(Mst_Thema entity)
        {
            context.Mst_Thema.Remove(entity);
        }

        public void Update(Mst_Thema entity)
        {
            context.Entry(entity).State = EntityState.Modified;
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

    }
}
