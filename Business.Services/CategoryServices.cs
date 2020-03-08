namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class CategoryServices : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ICategoryDataService _ICategoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDataService">The user data service.</param>
        public CategoryServices(ICategoryDataService categoryDataService)
        {
            _ICategoryDataService = categoryDataService;
        }

        public IEnumerable<Mst_Category> GetAllCategory()
        {
            return _ICategoryDataService.GetAllCategory();
        }

        public Mst_Category GetCategoryById(Guid Id)
        {
            try
            {
                return _ICategoryDataService.GetCategoryById(Id);
            }
            catch (Exception exception)
            {
                Logger.Info($"Exception while fetching {exception.InnerException}");
                return null;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
