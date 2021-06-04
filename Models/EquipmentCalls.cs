using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentCalls
    {
        public int Uid { get; set; }
        public int? StoreId { get; set; }
        public string SerialNo { get; set; }
        public DateTime? CallDate { get; set; }
        public byte[] CallMessage1 { get; set; }
        public short? UserId { get; set; }
        public string CallMessage { get; set; }
    }
}
