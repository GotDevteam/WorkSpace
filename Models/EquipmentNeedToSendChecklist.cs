using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentNeedToSendChecklist
    {
        public int Uid { get; set; }
        public int? NeedToSendId { get; set; }
        public int? CheckListId { get; set; }
        public bool? IsWorking { get; set; }
        public string Note { get; set; }
    }
}
