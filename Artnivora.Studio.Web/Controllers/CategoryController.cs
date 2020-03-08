namespace Artnivora.Studio.Web.Controllers
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : BaseCategoryController
    {

        public CategoryController(CategoryServices categoryServices) : base(categoryServices)
        {

        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetAllCategory()
        {
            return base.GetAllCategory();
        }
    }
}
