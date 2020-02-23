using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Web.Shared.ViewModels
{
    public class ResponseUserMailbox
    {
        public LinkInfo LinkInfo { get; set; }
        public List<ResponseMessage> Messages { get; set; }
    }

    public class ResponseMessage
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }

        public DateTime MessageSendDateTime { get; set; }
    }

    public class ResponseAttacmentMessage
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }        
    }
}
