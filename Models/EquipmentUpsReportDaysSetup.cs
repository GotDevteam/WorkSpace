using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentUpsReportDaysSetup
    {
        public byte? Priority { get; set; }
        public string CheckName { get; set; }
        public string Description { get; set; }
        public byte? ReportDays { get; set; }
        public byte? MaxDays { get; set; }
    }
}
