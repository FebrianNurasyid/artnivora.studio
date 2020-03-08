using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Models
{
    public class Mst_Thema
    {

        private Guid _Id;
        private string _ThemaName { get; set; }
        private DateTime _CretedDate { get; set; }
        private string _CretedBy { get; set; }
        private DateTime _ModifiedDate { get; set; }
        private string _ModifiedBy { get; set; }
        public Mst_Thema()
        {

        }
        public Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        public string ThemaName
        {
            get
            {
                return this._ThemaName;
            }

            set
            {
                this._ThemaName = value;
            }
        }
        public DateTime CretedDate { get { return this._CretedDate; } set { this._CretedDate = value; } }
        public string CretedBy { get { return this._CretedBy; } set { this._CretedBy = value; } }
        public DateTime ModifiedDate { get { return this._ModifiedDate; } set { this._ModifiedDate = value; } }
        public string ModifiedBy { get { return this._ModifiedBy; } set { this._ModifiedBy = value; } }

    }
}

