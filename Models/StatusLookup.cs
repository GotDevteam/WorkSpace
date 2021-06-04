using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class StatusLookup
    {
        public int Uid { get; set; }
        public byte? StatusId { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public byte? Sort { get; set; }
        public string Note { get; set; }
    }
}
