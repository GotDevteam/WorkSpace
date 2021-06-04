using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentTransaction
    {
        public int Uid { get; set; }
        public string SerialNo { get; set; }
        public byte? RecordType { get; set; }
        public int? StoreId { get; set; }
        public DateTime? Date { get; set; }
        public short? ReasonId { get; set; }
        public string Note { get; set; }
        public string SerialNo1 { get; set; }
        public short? UserId { get; set; }
        public DateTime? AddDate { get; set; }
        public bool? Status { get; set; }
        public bool? Followup { get; set; }
    }
}
