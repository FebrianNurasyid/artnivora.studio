using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models.Production
{
    public class ProductionAttachments
    {

        private Guid _id;
        private Guid _productionId;
        private Guid _productionAttachementId;
        private Production _production;
        private ProductionAttachment _attachment;

        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public Guid ProductionId
        {
            get
            {
                return this._productionId;
            }
            set
            {
                this._productionId = value;
            }
        }
        public Guid ProductionAttachementId
        {
            get
            {
                return this._productionAttachementId;
            }
            set
            {
                this._productionAttachementId = value;
            }
        }

        public Production Production
        {
            get
            {
                return this._production;
            }
            set
            {
                this._production = value;
            }
        }
        public ProductionAttachment ProductionAttachment
        {
            get
            {
                return this._attachment;
            }
            set
            {
                this._attachment = value;
            }
        }
    }
}
