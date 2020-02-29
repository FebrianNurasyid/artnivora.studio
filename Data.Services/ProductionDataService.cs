namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Business.Models.Production;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ProductionDataService : IProductionDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public ProductionDataService(DatabaseContext context)
        {
            this.context = context;
        }


        public void AddProductionAttachment(ProductionAttachment productionAttachment)
        {
            try
            {
                context.ProductionAttachment.Add(productionAttachment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public ProductionAttachment GetProductionAttachmentById(Guid attachmentId)
        {
            return context.ProductionAttachment.Where(x => x.Id == attachmentId).FirstOrDefault();
        }

        public void SaveProduction(Production entity)
        {
            try
            {
                context.Production.Add(entity);
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

        public IEnumerable<Production> GetProductions(string filtered)
        {
            return context.Production.Include(x => x.ProductionAttachments).ThenInclude(y => y.ProductionAttachment)
                .Where(x => x.Status == filtered).ToList();
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
