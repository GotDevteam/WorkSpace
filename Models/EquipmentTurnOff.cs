using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentTurnOff
    {
        public int Uid { get; set; }
        public string SerialNo { get; set; }
        public int? StoreId { get; set; }
        public short? UserId { get; set; }
        public DateTime? AddDate { get; set; }
        public string Note { get; set; }
        public DateTime? TurnOffDate { get; set; }
        public DateTime? TurnOnDate { get; set; }
        public bool? Done { get; set; }
    }
}
