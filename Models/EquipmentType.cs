using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentType
    {
        public byte Id { get; set; }
        public string Description { get; set; }
        public byte SortOrder { get; set; }
        public byte? Type2 { get; set; }
        public int Uid { get; set; }

        //public EquipmentLookup eqLookup { get; set; }
    }
}
