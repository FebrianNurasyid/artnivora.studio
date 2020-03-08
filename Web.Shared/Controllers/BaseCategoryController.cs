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

    public abstract class BaseCategoryController : BaseController
    {
        private readonly CategoryServices _CategoryServices;

        public BaseCategoryController(CategoryServices categoryServices) : base()
        {
            this._CategoryServices = categoryServices;
        }

        public virtual IActionResult GetAllCategory()
        {
            try
            {
                List<Mst_Category> Mst_Category = _CategoryServices.GetAllCategory().ToList();

                return Ok(Mst_Category);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst division");
                return BadRequest(new { message = "Error in getting mst division..." });
            }
        }
    }
}
