﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artnivora.Studio.Portal.Business.Models
{

    public class Mst_Division
    {

        private Guid _id;
        private string _DivisionName { get; set; }
        private DateTime _CretedDate { get; set; }
        private string _CretedBy { get; set; }
        private DateTime _ModifiedDate { get; set; }
        private string _ModifiedBy { get; set; }

        public Mst_Division()
        {

        }
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

        public string DivisionName
        {
            get
            {
                return this._DivisionName;
            }

            set
            {
                this._DivisionName = value;
            }
        }

        public DateTime CretedDate { get { return this._CretedDate; } set { this._CretedDate = value; } }

        public string CretedBy { get { return this._CretedBy; } set { this._CretedBy = value; } }

        public DateTime ModifiedDate { get { return this._ModifiedDate; } set { this._ModifiedDate = value; } }

        public string ModifiedBy { get { return this._ModifiedBy; } set { this._ModifiedBy = value; } }

    }
}
