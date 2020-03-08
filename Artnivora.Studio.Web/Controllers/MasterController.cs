namespace Artnivora.Studio.Web.Controllers
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [Authorize]
    [Route("api/[controller]")]
    public class MasterController : BaseMasterController
    {
        public MasterController(MasterService masterService,CategoryServices categoryServices) :

            base(masterService,categoryServices)
        {

        }

        //Get data master division//
        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetAllDivision()
        {
            return base.GetAllDivision();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetDivisionById([FromQuery] Guid id)
        {
            return base.GetDivisionById(id);
        }

        //get data  master category
        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetAllCategory()
        {
            return base.GetAllCategory();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetCategoryById([FromQuery] Guid id)
        {
            return base.GetCategoryById(id);
        }

        //get data  master Role
        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetAllRole()
        {
            return base.GetAllRole();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetRoleById([FromQuery] Guid id)
        {
            return base.GetRoleById(id);
        }

        //get data master thema//
        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetAllThema()
        {
            return base.GetAllThema();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public override IActionResult GetThemaById([FromQuery] Guid id)
        {
            return base.GetThemaById(id);
        }

        //post All Master add//
        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult AddCategory([FromBody] Mst_Category Mst_Category,[FromQuery] Guid id) 
        {
            return base.AddCategory(Mst_Category,id);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult AddDivision([FromBody] List<Mst_Division> mst_Division,[FromQuery] Guid id)
        {
            return base.AddDivision(mst_Division,id);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult AddRole([FromBody] Mst_Role Mst_Role, [FromQuery] Guid id)
        {
            return base.AddRole(Mst_Role, id);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public override IActionResult AddThema([FromBody] Mst_Thema Mst_Thema, [FromQuery] Guid id)
        {
            return base.AddThema(Mst_Thema, id);
        }

        //DELETE:  All master
        [AllowAnonymous]
        [HttpDelete("{action}")]
        public override IActionResult DeleteDivision([FromQuery] Guid Id)
        {
            return base.DeleteDivision(Id);
        }

        [AllowAnonymous]
        [HttpDelete("{action}")]
        public override IActionResult DeleteCategory([FromQuery] Guid Id)
        {
            return base.DeleteCategory(Id);
        }

        [AllowAnonymous]
        [HttpDelete("{action}")]
        public override IActionResult DeleteRole([FromQuery] Guid Id)
        {
            return base.DeleteRole(Id);
        }

        [AllowAnonymous]
        [HttpDelete("{action}")]
        public override IActionResult DeleteThema([FromQuery] Guid Id)
        {
            return base.DeleteThema(Id);
        }
    }
}
