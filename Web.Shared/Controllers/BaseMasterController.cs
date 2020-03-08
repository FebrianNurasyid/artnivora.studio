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
        private readonly CategoryServices _categoryServices;
        //private readonly RoleDataService _roleDataService;
        public BaseMasterController(MasterService masterService,
            CategoryServices categoryServices) : base()
        {
            this._masterService = masterService;
            this._categoryServices = categoryServices;
           
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
        public virtual IActionResult GetDivisionById(Guid id)
        {
            try
            {
                Mst_Division mst_Division = _masterService.GetById(id);
                if (mst_Division == null) 
                    return NotFound("not found");

                return Ok(mst_Division);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst division");
                return BadRequest(new { message = "Error in getting mst division..." });
            }
        }
        public virtual IActionResult GetAllCategory()
        {
            try
            {
                List<Mst_Category> Mst_Category = _categoryServices.GetAllCategory().ToList();

                return Ok(Mst_Category);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst category");
                return BadRequest(new { message = "Error in getting mst category..." });
            }
        }
        public virtual IActionResult GetCategoryById(Guid id)
        {
            try
            {
                Mst_Category mst_Category = _categoryServices.GetCategoryById(id);
                if (mst_Category == null)
                    return NotFound("not found");

                return Ok(mst_Category);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst category");
                return BadRequest(new { message = "Error in getting mst category..." });
            }
        }
        public virtual IActionResult GetAllRole()
        {
            try
            {
                List<Mst_Role> Mst_Role = _masterService.GetAllRole().ToList();

                return Ok(Mst_Role);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst Role");
                return BadRequest(new { message = "Error in getting mst Role..." });
            }
        }
        public virtual IActionResult GetRoleById(Guid id)
        {
            try
            {
                Mst_Role mst_Role = _masterService.GetRoleById(id);
                if (mst_Role == null)
                    return NotFound("not found");

                return Ok(mst_Role);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst Role");
                return BadRequest(new { message = "Error in getting mst Role..." });
            }
        }
        public virtual IActionResult GetAllThema()
        {
            try
            {
                List<Mst_Thema> mst_Thema = _masterService.GetAllThema().ToList();

                return Ok(mst_Thema);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst thema");
                return BadRequest(new { message = "Error in getting mst thema..." });
            }
        }
        public virtual IActionResult GetThemaById(Guid id)
        {
            try
            {
                Mst_Thema mst_Thema = _masterService.GetThemaById(id);
                if (mst_Thema == null)
                    return NotFound("not found");

                return Ok(mst_Thema);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst Role");
                return BadRequest(new { message = "Error in getting mst Role..." });
            }
        }
        public virtual IActionResult AddDivision(List<Mst_Division> mst_Division,Guid divisionId)
        {
            try
            {
                // todo save data mst division
                if (divisionId== Guid.Empty)
                {
                    //save
                    //mst_Division.Id = mst_Division.Id;
                    foreach(var item in mst_Division)
                    {
                        Mst_Division mstDivSave = new Mst_Division();

                        mstDivSave.ModifiedDate = DateTime.Now;
                        mstDivSave.ModifiedBy = "System";
                        mstDivSave.CretedDate = DateTime.Now;
                        mstDivSave.CretedBy = "System";
                        mstDivSave.DivisionName = item.DivisionName;
                        _masterService.Save(mstDivSave);
                    }
                    return Ok();
                }
                else
                {
                    var mstDiv = _masterService.GetById(divisionId);
                    if (mstDiv != null)
                    {
                        // update 
                        mstDiv.DivisionName = mst_Division.FirstOrDefault()?.DivisionName;
                        mstDiv.ModifiedDate = DateTime.Now;
                        mstDiv.ModifiedBy = "System"; //get user data                   
                        _masterService.UpdateDivision(mstDiv);
                        return Ok(mstDiv);
                    }
                    else
                        return NotFound();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst division");
                return BadRequest(new { message = "Error in getting mst division..." });
            }
        }
        public virtual IActionResult AddCategory(Mst_Category Mst_Category, Guid categoryid)
        {
            try
            {
                // todo save data  Mst_Category
                if (categoryid == Guid.Empty)
                {                                 
                    Mst_Category.CretedDate = DateTime.Now;
                    Mst_Category.CretedBy = "System"; // get username data 
                    Mst_Category.ModifiedDate = DateTime.Now;
                    Mst_Category.ModifiedBy = "System"; // get username data 

                    _masterService.SaveCategory(Mst_Category);
                    return Ok(Mst_Category);
                }
                else
                {
                    var mstCat = _masterService.GetCategoryById(categoryid);
                    if (mstCat != null)
                    {
                        // update 
                        mstCat.CategoryName = Mst_Category.CategoryName;
                        mstCat.ModifiedDate = DateTime.Now;
                        mstCat.ModifiedBy = "System"; //get user data                   
                        _masterService.UpdateCategory(mstCat);
                        return Ok(mstCat);
                    }
                    else
                        return NotFound();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst category");
                return BadRequest(new { message = "Error in getting mst category..." });
            }
        }
        public virtual IActionResult AddRole(Mst_Role Mst_Role, Guid roleid)
        {
            try
            {
                if (roleid == Guid.Empty)
                {
                    Mst_Role.CretedDate = DateTime.Now;
                    Mst_Role.CretedBy = "System"; // get username data 
                    Mst_Role.ModifiedDate = DateTime.Now;
                    Mst_Role.ModifiedBy = "System"; // get username data 

                    _masterService.SaveRole(Mst_Role);
                    return Ok(Mst_Role);
                }
                else
                {
                    var mstrole = _masterService.GetRoleById(roleid);
                    if (mstrole != null)
                    {
                        // update 
                        mstrole.RoleName =Mst_Role.RoleName;
                        mstrole.ModifiedDate = DateTime.Now;
                        mstrole.ModifiedBy = "System"; //get user data                   
                        _masterService.UpdateRole(mstrole);
                        return Ok(mstrole);
                    }
                    else
                        return NotFound();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst role");
                return BadRequest(new { message = "Error in getting mst role..." });
            }
        }
        public virtual IActionResult AddThema(Mst_Thema Mst_Thema, Guid themaid)
        {
            try
            {
                if (themaid == Guid.Empty)
                {
                    Mst_Thema.CretedDate = DateTime.Now;
                    Mst_Thema.CretedBy = "System"; // get username data 
                    Mst_Thema.ModifiedDate = DateTime.Now;
                    Mst_Thema.ModifiedBy = "System"; // get username data 

                    _masterService.SaveThema(Mst_Thema);
                    return Ok(Mst_Thema);
                }
                else
                {
                    var mstthema = _masterService.GetThemaById(themaid);
                    if (mstthema != null)
                    {
                        // update 
                        mstthema.ThemaName = Mst_Thema.ThemaName;
                        mstthema.ModifiedDate = DateTime.Now;
                        mstthema.ModifiedBy = "System"; //get user data                   
                        _masterService.UpdateThema(mstthema);
                        return Ok(mstthema);
                    }
                    else
                        return NotFound();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get mst thema");
                return BadRequest(new { message = "Error in getting mst role..." });
            }
        }
        public virtual IActionResult DeleteDivision(Guid Id)
        {
            List<string> errors = new List<string>();

            var mst_Division = _masterService.GetById(Id);
            if (mst_Division == null)
            {
                errors.Add("Division not found");
                return NotFound(new { errors });
            }
            else
            {
                _masterService.DeleteDivision(mst_Division);
                return Ok("Data Division Saved succesfully");
            }
        }
        public virtual IActionResult DeleteCategory(Guid Id)
        {
            List<string> errors = new List<string>();

            var Mst_Category = _masterService.GetCategoryById(Id);
            if (Mst_Category == null)
            {
                errors.Add("Category not found");
                return NotFound(new { errors });
            }
            else
            {
                _masterService.DeleteCategory(Mst_Category);
                return Ok("Data Category Saved succesfully");
            }
        }
        public virtual IActionResult DeleteRole(Guid Id)
        {
            List<string> errors = new List<string>();

            var mst_role = _masterService.GetRoleById(Id);
            if (mst_role == null)
            {
                errors.Add("Role not found");
                return NotFound(new { errors });
            }
            else
            {
                _masterService.DeleteRole(mst_role);
                return Ok("Data Role Saved succesfully");
            }
        }
        public virtual IActionResult DeleteThema(Guid Id)
        {
            List<string> errors = new List<string>();

            var Mst_Thema = _masterService.GetThemaById(Id);
            if (Mst_Thema == null)
            {
                errors.Add("Thema not found");
                return NotFound(new { errors });
            }
            else
            {
                _masterService.DeleteThema(Mst_Thema);
                return Ok("Data Thema Delete succesfully");
            }
        }
    }
}
