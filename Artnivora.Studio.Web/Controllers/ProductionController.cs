namespace Artnivora.Studio.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Web.Shared.Controllers;
    using System;
    using Microsoft.AspNetCore.Http;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Artnivora.Studio.Portal.Business.Models.Production;
    using Artnivora.Studio.Portal.Data.Interfaces;

    [Authorize]
    [Route("api/[controller]")]
    public class ProductionController : BaseProductionController
    {
        public ProductionController(ProductionServices productionServices, 
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUserProfileDataService userProfileData, IUserDataService userDataService) :
                              base(productionServices, httpContextAccessor, configuration,userProfileData,userDataService)
        {
        }

        
        [HttpPost("[action]")]
        public override async Task<IActionResult> AddProductionAttachment(IFormFile file)
        {
            return await base.AddProductionAttachment(file);
        }
        
        [HttpGet("[action]/{attachmentId}")]
        public override IActionResult GetProductionAttachmentById(Guid attachmentId)
        {
            return base.GetProductionAttachmentById(attachmentId);
        }

        [HttpPost("[action]")]
        public override IActionResult SaveProduction([FromBody]Production production)
        {
            return base.SaveProduction(production);
        }

        [HttpGet("[action]/{filtered}")]
        public override IActionResult GetProductions(string filtered)
        {
            return base.GetProductions(filtered);
        }

    }
}
