using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentCheckList
    {
        public byte Id { get; set; }
        public string CheckListName { get; set; }
        public byte? SortOrder { get; set; }
    }
}
