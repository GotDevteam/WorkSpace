using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentNeedToSend
    {
        public int Uid { get; set; }
        public int? StoreId { get; set; }
        public DateTime? DateToSend { get; set; }
        public string SerialNo { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
        public int? UserId { get; set; }
        public DateTime? AddDate { get; set; }
        public byte? EquipmentType { get; set; }
        public int? NeedToSendHeaderId { get; set; }
    }
}
