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
    public class MasterController : BaseMasterController
    {
        
        public MasterController(MasterService masterService) :base(masterService)
        {

        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetAllDivision()
        {
            return base.GetAllDivision();
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult AddDivision([FromBody] Mst_Division mst_Division) 
        {
            return base.AddDivision(mst_Division);
        }

    }
}
