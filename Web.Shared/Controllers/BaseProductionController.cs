namespace Artnivora.Studio.Portal.Web.Shared.Controllers
{
    using Artnivora.Studio.Portal.Business.Models.Messaging;
    using Artnivora.Studio.Portal.Business.Models.Production;
    using Artnivora.Studio.Portal.Business.Services;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Artnivora.Studio.Portal.Web.Shared.Constants;
    using Artnivora.Studio.Portal.Web.Shared.Utilities;
    using Artnivora.Studio.Portal.Web.Shared.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseProductionController : BaseController
    {
        private readonly ProductionServices _productionServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Guid? _userId;
        private readonly IConfiguration _configuration;
        private IUserProfileDataService _userProfileData;
        private readonly IUserDataService _userDataService;

        public BaseProductionController(ProductionServices productionServices, IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration, IUserProfileDataService userProfileData, IUserDataService userDataService) : base()
        {
            _productionServices = productionServices;
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.GetUserId();
            _configuration = configuration;
            _userProfileData = userProfileData;
            _userDataService = userDataService;
        }

        public virtual async Task<IActionResult> AddProductionAttachment(IFormFile file)
        {
            ProductionAttachment productionAttachment = new ProductionAttachment();

            string webRootPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData));

            string target = $"\\{_configuration["AppSettings:Target"]}";

            string attachmentFolderPath = $"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}{target}";

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}"))
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}");

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}{target}"))
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}{target}");

            if (!Directory.Exists($"{attachmentFolderPath}"))
                Directory.CreateDirectory($"{attachmentFolderPath}");

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(file.FileName, out contentType))
                contentType = "application/octet-stream";

            string userIdPath = $"{_userId.Value}";
            var attachmentPictureDirectory = $"{attachmentFolderPath}\\{userIdPath}";
            var attachmentFilePath = $"{attachmentPictureDirectory}\\{file.FileName}";

            if (System.IO.File.Exists(attachmentFilePath))
            {
                System.IO.File.Delete(attachmentFilePath);
            }

            if (!Directory.Exists($"{attachmentPictureDirectory}"))
            {
                Directory.CreateDirectory($"{attachmentPictureDirectory}");
            }

            using (var stream = System.IO.File.Create(attachmentFilePath))
                await file.CopyToAsync(stream);

            string filePathToSave = $"{userIdPath}\\{file.FileName}";
            productionAttachment.FilePath = filePathToSave;
            productionAttachment.ContentType = contentType;
            productionAttachment.FileName = file.FileName;

            _productionServices.AddProductionAttachment(productionAttachment);

            var response = new ResponseAttacmentMessage
            {
                FileName = file.FileName,
                Id = productionAttachment.Id.ToString(),
                Path = GetAbsoluteAttachmentPath(attachmentFilePath, "temp")
            };

            return Ok(response);
        }

        public virtual IActionResult GetProductionAttachmentById(Guid attachmentId, string type)
        {
            try
            {
                var productionAttachment = _productionServices.GetProductionAttachmentById(attachmentId);
                if (productionAttachment != null)
                {
                    var net = new System.Net.WebClient();
                    var data = net.DownloadData(GetAbsoluteAttachmentPath(productionAttachment.FilePath, type));
                    var content = new MemoryStream(data);
                    var contentType = productionAttachment.ContentType;
                    return File(content, contentType);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get attachment by id.");
                return BadRequest(new { message = "Error in getting attachment by id..." });
            }
        }

        public virtual IActionResult SaveProduction(Production production,Guid id)
        {
            try
            {
                var user = _userDataService.GetById(_userId.Value);
                if (id == Guid.Empty)
                {
                    production.Id = Guid.NewGuid();
                    production.CreatedDate = DateTime.Now;
                    production.CreatedBy = user.Username;

                    // save real attachment for production task
                    var attachmentId = production.ProductionAttachments.FirstOrDefault().ProductionAttachementId;
                    var productionAttachment = _productionServices.GetProductionAttachmentById(attachmentId);
                    var realAttachment = SaveRealAttachment(GetAbsoluteAttachmentPath(productionAttachment.FilePath,"temp"), production, productionAttachment);
                    DeleteTempAttachment(productionAttachment.Id);

                    // setup new production prop
                    List<ProductionAttachments> productionAttachments = new List<ProductionAttachments>();
                    productionAttachments.Add(new ProductionAttachments { ProductionAttachementId = realAttachment.Id });
                    Production productionToSave = new Production()
                    {
                        Id = production.Id,
                        Title = production.Title,
                        Category = production.Category,
                        Themes = production.Themes,
                        Concept = production.Concept,
                        Status = production.Status,
                        CreatedBy = production.CreatedBy,
                        CreatedDate = production.CreatedDate
                    };
                    productionToSave.ProductionAttachments = productionAttachments;

                    _productionServices.SaveProduction(productionToSave);
                }
                else
                {
                    var prod = _productionServices.GetProdById(id);
                    prod.Title = production.Title;
                    prod.Category = production.Category;
                    prod.Themes = production.Themes;
                    prod.Concept = production.Concept;
                    prod.ModifiedBy = user?.Username;
                    prod.ModifiedDate = DateTime.Now;
                    prod.Status = production.Status;
                    prod.Remark = production.Remark;

                    if (!string.IsNullOrEmpty(production.UploadedStatus))
                    {
                        prod.UploadedStatus = production.UploadedStatus;
                        prod.UploadedDate = DateTime.Now;
                        prod.UploadedBy = user?.Username;
                    }

                    ProductionAttachment realAttachment = new ProductionAttachment();
                    if (prod.ProductionAttachments.FirstOrDefault().ProductionAttachment.Id != production.ProductionAttachments.FirstOrDefault().ProductionAttachementId)
                    {
                        //delete Previous attachment
                        DeletePreviousAttachment(prod);
                        // save real attachment for production task
                        var attachmentId = production.ProductionAttachments.FirstOrDefault().ProductionAttachementId;
                        var productionAttachment = _productionServices.GetProductionAttachmentById(attachmentId);
                        realAttachment = SaveRealAttachment(GetAbsoluteAttachmentPath(productionAttachment.FilePath, "temp"), production, productionAttachment);
                        //delete Temp attachment
                        DeleteTempAttachment(productionAttachment.Id);
                    }
                    else
                        realAttachment.Id = production.ProductionAttachments.FirstOrDefault().ProductionAttachementId;

                    // setup new production attachment
                    List<ProductionAttachments> productionAttachments = new List<ProductionAttachments>();
                    productionAttachments.Add(new ProductionAttachments { ProductionAttachementId = realAttachment.Id });
                    prod.ProductionAttachments = productionAttachments;

                    _productionServices.UpdateProduction(prod);

                }
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying save production.");
                return BadRequest(new { message = "Error in save production..." });
            }
        }

        private ProductionAttachment SaveRealAttachment(string sourceAttachmentFilePath, Production production, ProductionAttachment productionAttachment)
        {
            ProductionAttachment realProductionAttachment = new ProductionAttachment();

            string webRootPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData));

            string target = $"\\{_configuration["AppSettings:Target"]}";

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}"))
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}");

            if (!Directory.Exists($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}{target}\\{production.Category}"))
                Directory.CreateDirectory($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}{target}\\{production.Category}");

            string monthYearPath = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year.ToString();
            string attachmentFolderPath = $"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}{target}\\{production.Category}\\{monthYearPath}";

            if (!Directory.Exists($"{attachmentFolderPath}"))
                Directory.CreateDirectory($"{attachmentFolderPath}");

            string fileName = Guid.NewGuid().ToString() + "_" + productionAttachment.FileName;

            var attachmentDirectory = $"{attachmentFolderPath}";
            var attachmentFilePath = $"{attachmentDirectory}\\{fileName}";

            if (System.IO.File.Exists(attachmentFilePath))
                System.IO.File.Delete(attachmentFilePath);

            if (!Directory.Exists($"{attachmentDirectory}"))
                Directory.CreateDirectory($"{attachmentDirectory}");

            System.IO.File.Copy(sourceAttachmentFilePath, attachmentFilePath, true);

            realProductionAttachment.FilePath = $"{production.Category}\\{monthYearPath}\\{fileName}";
            realProductionAttachment.ContentType = productionAttachment.ContentType;
            realProductionAttachment.FileName = productionAttachment.FileName;
            _productionServices.AddProductionAttachment(realProductionAttachment);

            return realProductionAttachment;
        }

        private string GetAbsoluteAttachmentPath(string attachment, string type)
        {
            string webRootPath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData));
            string target = $"\\{_configuration["AppSettings:Target"]}";
            if (type == "temp")
                return $"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}{target}\\{attachment}";
            else
                return $"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}{target}\\{attachment}";
        }

        public virtual IActionResult GetProductions(string filtered)
        {
            try
            {
                var productions = _productionServices.GetProductions(filtered);
                if (productions != null)
                {
                    var response = new ResponseProductions
                    {
                        Productions = new List<ResponseProduction>()
                    };

                    foreach (var pd in productions.ToList())
                    {
                        var addToProd = new ResponseProduction
                        {
                            Id = pd.Id.ToString(),
                            Title = pd.Title,
                            Category = pd.Category,
                            Themes = pd.Themes,
                            Concept = pd.Concept,
                            Status = pd.Status,
                            CreatedBy = pd.CreatedBy,
                            CreatedDate = pd.CreatedDate,
                            UploadedBy = pd.UploadedBy,
                            UploadedDate = pd.UploadedDate,
                            UploadedStatus = pd.UploadedStatus,
                            ModifiedBy = pd.ModifiedBy,
                            ModifiedDate = pd.ModifiedDate,
                            Remark = pd.Remark,
                            Attacment = new ResponseProductionAttacment
                            {
                                Id = pd.ProductionAttachments.FirstOrDefault().Id.ToString(),
                                ProductionAttachementId = pd.ProductionAttachments.FirstOrDefault().ProductionAttachementId.ToString(),
                                FileName = pd.ProductionAttachments.FirstOrDefault().ProductionAttachment.FileName
                            }
                        };
                        response.Productions.Add(addToProd);
                    }
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get productions data.");
                return BadRequest(new { message = "Error in getting productions data..." });
            }
        }

        public virtual IActionResult GetProdById(Guid id)
        {
            try
            {
                var foundProd = _productionServices.GetProdById(id);
                if (foundProd != null)
                {
                    var response = new ResponseProduction()
                    {
                        Id = foundProd.Id.ToString(),
                        Concept = foundProd.Concept,
                        Category = foundProd.Category,
                        Title = foundProd.Title,
                        Themes = foundProd.Themes,
                        Status = foundProd.Status,
                        Remark = foundProd.Remark,
                        UploadedStatus = foundProd.UploadedStatus,
                        Attacment = new ResponseProductionAttacment
                        {
                            Id = foundProd.ProductionAttachments.FirstOrDefault().Id.ToString(),
                            ProductionAttachementId = foundProd.ProductionAttachments.FirstOrDefault().ProductionAttachementId.ToString(),
                            FileName = foundProd.ProductionAttachments.FirstOrDefault().ProductionAttachment.FileName,
                            FilePath = foundProd.ProductionAttachments.FirstOrDefault().ProductionAttachment.FilePath,
                        }
                    };
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in trying to get production by id.");
                return BadRequest(new { message = "Error in getting production by id..." });
            }
        }

        private void DeletePreviousAttachment(Production prod)
        {
            string webRootPath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData));

            string target = $"\\{_configuration["AppSettings:Target"]}";

            if (prod.ProductionAttachments.Any())
            {
                var originalPath = $"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD}{target}\\{prod.ProductionAttachments.FirstOrDefault().ProductionAttachment.FilePath}";

                if (System.IO.File.Exists(originalPath))
                    System.IO.File.Delete(originalPath);

                _productionServices.DeleteAttachment(prod.ProductionAttachments.FirstOrDefault().ProductionAttachment);
            }
        }

        private void DeleteTempAttachment(Guid attachmentId)
        {
            var productionAttachment = _productionServices.GetProductionAttachmentById(attachmentId);

            string webRootPath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData));
            string target = $"\\{_configuration["AppSettings:Target"]}";                        

            DirectoryInfo directoryInfo = new DirectoryInfo($"{webRootPath}{GlobalConstants.ATTACHMENT_PATH_PROD_TEMP}{target}\\{_userId.Value}");

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }            

            _productionServices.DeleteAttachment(productionAttachment);
        }
    }
}
