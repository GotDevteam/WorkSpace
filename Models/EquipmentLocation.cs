using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentLocation
    {
        public int Uid { get; set; }
        public byte? EquipmentType { get; set; }
        public string SerialNo { get; set; }
        public int? StoreId { get; set; }
        public string Location { get; set; }
        public byte? Status { get; set; }
        public DateTime? StartService { get; set; }
        public DateTime? EndService { get; set; }
        public string Notes { get; set; }
    }
}
