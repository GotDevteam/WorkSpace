using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentLookup
    {
        public int Uid { get; set; }
        public byte? EquipmentType { get; set; }
        public string SerialNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public byte? ExtendedWarrantyType { get; set; }
        public DateTime? ExtendedWarrantyStartDate { get; set; }
        public DateTime? ExtendedWarrantyEndDate { get; set; }
        public bool? Status { get; set; }
        public string Note { get; set; }
        public short? UserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? GrindProgram { get; set; }
        public byte? MenuId { get; set; }
        public string Os { get; set; }
        public string GotSerialNo { get; set; }

        //public EquipmentType eqType { get; set; }
    }
}
