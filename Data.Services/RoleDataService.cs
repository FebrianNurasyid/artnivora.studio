namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RoleDataService : IRoleDataSerive, IDisposable
    {
        private readonly DatabaseContext context;

        public RoleDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Mst_Role> GetAllRole()
        {
            return context.Mst_Role.ToList();
        }

        public Mst_Role GetRoleById(Guid Id)
        {
            return context.Mst_Role
                .Where(Mst_Role => Mst_Role.Id == Id)
                .FirstOrDefault();
        }

        public void Add(Mst_Role entity)
        {
            try
            {
                context.Mst_Role.Add(entity);
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

        public void Delete(Mst_Role entity)
        {
            context.Mst_Role.Remove(entity);
        }

        public void Update(Mst_Role entity)
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
