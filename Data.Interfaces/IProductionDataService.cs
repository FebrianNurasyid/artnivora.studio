
namespace Artnivora.Studio.Portal.Data.Interfaces
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Business.Models.Production;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IProductionDataService
    {
        void AddProductionAttachment(ProductionAttachment productionAttachment);
        ProductionAttachment GetProductionAttachmentById(Guid attachmentId);
        void SaveProduction(Production entity);
        void Save();
        IEnumerable<Production> GetProductions(string filtered);

    }
}
