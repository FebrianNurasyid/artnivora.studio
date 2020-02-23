

namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{

    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Enums;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseMasterController : BaseController
    {
        private readonly MasterService _masterService;

        public BaseMasterController(MasterService masterService) : base()
        {
            this._masterService = masterService;
        }


        public virtual IActionResult GetAllDivision()
        {
            try
            {
                List<Mst_Division> mst_Divisions = _masterService.GetAllDivision().ToList();
               
                return Ok(mst_Divisions);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst division");
                return BadRequest(new { message = "Error in getting mst division..." });
            }
        }

        public virtual IActionResult AddDivision(Mst_Division mst_Division)
        {
            try
            {
                // todo save data mst division

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst division");
                return BadRequest(new { message = "Error in getting mst division..." });
            }
        }

    }
}
