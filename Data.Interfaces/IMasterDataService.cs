namespace Artnivora.Studio.Portal.Data.Interfaces
{


    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    public interface IMasterDataService
    {
        IEnumerable<Mst_Division> GetAllDivision();

    }



}

