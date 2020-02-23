

namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MasterService : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMasterDataService _masterDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDataService">The user data service.</param>
        public MasterService(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        public IEnumerable<Mst_Division> GetAllDivision()
        {
            return _masterDataService.GetAllDivision();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
