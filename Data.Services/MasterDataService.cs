

namespace Artnivora.Studio.Portal.Data.Services
{

    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MasterDataService : IMasterDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public MasterDataService(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Mst_Division> GetAllDivision()
        {
            return context.Mst_Division.ToList();
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
