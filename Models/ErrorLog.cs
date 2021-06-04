using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class ErrorLog
    {
        public int Uid { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? AddDate { get; set; }
        public int? UserId { get; set; }
        public string Ipaddress { get; set; }
        public string BrowserType { get; set; }
        public string BrowserVersion { get; set; }
        public string ErrorCode { get; set; }
        public string FunctionName { get; set; }
        public string RequestData { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
