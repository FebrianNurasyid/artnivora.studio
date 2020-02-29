using System;
using System.Collections.Generic;

namespace Artnivora.Studio.Portal.Web.Shared.ViewModels
{
    public class ResponseProductions
    {        
        public List<ResponseProduction> Productions { get; set; }
    }

    public class ResponseProduction
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Themes { get; set; }
        public string Concept { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public string UploadedStatus { get; set; }
        public ResponseProductionAttacment Attacment { get; set; }
    }

    public class ResponseProductionAttacment
    {
        public string Id { get; set; }
        public string ProductionAttachementId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
