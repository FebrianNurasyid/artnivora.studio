using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artnivora.Studio.Portal.Business.Models
{
    public class Mst_Role
    {

        private Guid _Id;
        private string _RoleName { get; set; }
        private DateTime _CretedDate { get; set; }
        private string _CretedBy { get; set; }
        private DateTime _ModifiedDate { get; set; }
        private string _ModifiedBy { get; set; }

        public Mst_Role()
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
        public string RoleName
        {
            get
            {
                return this._RoleName;
            }

            set
            {
                this._RoleName = value;
            }
        }

        public DateTime CretedDate { get { return this._CretedDate; } set { this._CretedDate = value; } }

        public string CretedBy { get { return this._CretedBy; } set { this._CretedBy = value; } }

        public DateTime ModifiedDate { get { return this._ModifiedDate; } set { this._ModifiedDate = value; } }

        public string ModifiedBy { get { return this._ModifiedBy; } set { this._ModifiedBy = value; } }

    }
}

