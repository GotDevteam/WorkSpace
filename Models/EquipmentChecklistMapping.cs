using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentChecklistMapping
    {
        public int Uid { get; set; }
        public string TransactionScreen { get; set; }
        public byte? EquipmentTypeId { get; set; }
        public byte? CheckListId { get; set; }
    }
}
