namespace Artnivora.Studio.Portal.Data.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;

    public class VolunteerFunctionDataService : IVolunteerFunctionDataService, IDisposable
    {
        private readonly DatabaseContext context;

        public VolunteerFunctionDataService(DatabaseContext context)
        {
            this.context = context;
        }

        IEnumerable<VolunteerFunction> IVolunteerFunctionDataService.Get()
        {
            throw new System.NotImplementedException();
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
