using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentUpsDashBoardWarningDateLog
    {
        public int Uid { get; set; }
        public int? StoreId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? AddDate { get; set; }
        public int? UserId { get; set; }
    }
}
