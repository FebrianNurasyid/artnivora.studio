namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Business.Models.Production;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductionServices : IDisposable
    {
        private IProductionDataService _productionDataService;
        private IUserProfileDataService _userProfileData;

        public ProductionServices(IProductionDataService productionDataService, IUserProfileDataService userProfileData)
        {
            _productionDataService = productionDataService;
            _userProfileData = userProfileData;
        }

        /// <summary>
        /// Add production attachment
        /// </summary>
        /// <param name="productionAttachment">The content of message attachment.</param>
        /// <returns></returns>
        public void AddProductionAttachment(ProductionAttachment productionAttachment)
        {
            List<string> errors = new List<string>();
            try
            {
                _productionDataService.AddProductionAttachment(productionAttachment);
                _productionDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving UserProfile", exception);
            }
        }

        /// <summary>
        /// Get attachment production by id
        /// </summary>
        /// <param name="attachmentId">The attachment paroduction identifier.</param>
        /// <returns></returns>
        public ProductionAttachment GetProductionAttachmentById(Guid attachmentId)
        {
            return _productionDataService.GetProductionAttachmentById(attachmentId);
        }

        public void SaveProduction(Production production)
        {           
            _productionDataService.SaveProduction(production);
            _productionDataService.Save();
        }

        /// <summary>
        /// Get productions by filter status
        /// </summary>
        /// <param name="filtered">The status production identifier.</param>
        /// <returns></returns>
        public IEnumerable<Production> GetProductions(string filtered)
        {
            return _productionDataService.GetProductions(filtered);
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
