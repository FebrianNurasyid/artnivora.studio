namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class MasterService : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMasterDataService _masterDataService;
        private readonly ICategoryDataService _categoryDataService;
        private readonly IRoleDataSerive _roleDataService;
        private readonly IThemaDataService _themaDataService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDataService">The user data service.</param>
        public MasterService(IMasterDataService masterDataService, ICategoryDataService categoryDataService, IRoleDataSerive roleDataService,IThemaDataService themaDataService)
        {
            _masterDataService = masterDataService;
            _categoryDataService = categoryDataService;
            _roleDataService = roleDataService;
            _themaDataService = themaDataService;
            
        }

        //get all data master //
        public IEnumerable<Mst_Division> GetAllDivision()
        {
            return _masterDataService.GetAllDivision();
        }
        public IEnumerable<Mst_Thema> GetAllThema()
        {
            return _themaDataService.GetAllThema();
        }
        public IEnumerable<Mst_Role> GetAllRole()
        {
            return _roleDataService.GetAllRole();
        }
        public IEnumerable<Mst_Category> GetAllCategory()
        {
            return _categoryDataService.GetAllCategory();
        }

        //post all master data (save & update)
        public Mst_Division Save(Mst_Division Mst_Division)
        {
            try
            {

                _masterDataService.Add(Mst_Division);
                _masterDataService.Save();
                return Mst_Division;

            }  
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving UserProfile", exception);
            }
        }
        public void UpdateDivision(Mst_Division Mst_Division)
        {
            try
            {
                _masterDataService.Update(Mst_Division);
                _masterDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while update divison", exception);
            }
        }
        public void SaveCategory(Mst_Category Mst_Category)
        {
            try
            {
                _categoryDataService.Add(Mst_Category);
                _categoryDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving category", exception);
            }
        }
        public void UpdateCategory(Mst_Category Mst_Category)
        {
            try
            {
                _categoryDataService.Update(Mst_Category);
                _categoryDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while update category", exception);
            }
        }
        public Mst_Role SaveRole(Mst_Role Mst_Role)
        {
            try
            {
                _roleDataService.Add(Mst_Role);
                _roleDataService.Save();
                return Mst_Role;
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving Role", exception);
            }
        }
        public void UpdateRole(Mst_Role Mst_Role)
        {
            try
            {
                _roleDataService.Update(Mst_Role);
                _roleDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while update Role", exception);
            }
        }
        public Mst_Thema SaveThema(Mst_Thema Mst_Thema)
        {
            try
            {
                _themaDataService.Add(Mst_Thema);
                _themaDataService.Save();
                return Mst_Thema;
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while saving Role", exception);
            }
        }
        public void UpdateThema(Mst_Thema Mst_Thema)
        {
            try
            {
                _themaDataService.Update(Mst_Thema);
                _themaDataService.Save();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Exception while update Role", exception);
            }
        }
        public void DeleteDivision(Mst_Division mst_Division)
        {
            try
            {
                _masterDataService.Delete(mst_Division);
                _masterDataService.Save();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Exception while saving UserProfile", ex);
            }

        }
        public void DeleteCategory(Mst_Category Mst_Category)
        {
            try
            {
                _categoryDataService.Delete(Mst_Category);
                _categoryDataService.Save();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Exception while saving UserProfile", ex);
            }

        }
        public void DeleteRole(Mst_Role mst_role)
        {
            try
            {
                _roleDataService.Delete(mst_role);
                _roleDataService.Save();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Exception while saving Role ", ex);
            }

        }
        public void DeleteThema(Mst_Thema Mst_Thema)
        {
            try
            {
                _themaDataService.Delete(Mst_Thema);
                _themaDataService.Save();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Exception while saving Role ", ex);
            }

        }

        //get all data master  ByID//
        public Mst_Category GetCategoryById(Guid id)
        {
            return _categoryDataService.GetCategoryById(id);
        }
        public Mst_Role GetRoleById(Guid id)
        {
            return _roleDataService.GetRoleById(id);
        }
        public Mst_Thema GetThemaById(Guid id)
        {
            return _themaDataService.GetThemaById(id);
        }
        public Mst_Division GetById(Guid Id)
        {
            try
            {
                return _masterDataService.GetById(Id);
            }
            catch (Exception exception)
            {
                Logger.Info($"Exception while fetching {exception.InnerException}");
                return null;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
