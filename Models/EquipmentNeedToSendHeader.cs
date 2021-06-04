using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentNeedToSendHeader
    {
        public int Uid { get; set; }
        public int? StoreId { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingLabel { get; set; }
        public decimal? TotalWeight { get; set; }
        public string TrackingNumber { get; set; }
        public string ProcessingStatus { get; set; }
        public int? ProcessingUserId { get; set; }
        public string Note { get; set; }
        public DateTime? ChecklistCompletedDate { get; set; }
    }
}
