using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class EquipmentTransactionType
    {
        public byte Id { get; set; }
        public string Description { get; set; }
        public bool? InStore { get; set; }
        public bool? InGot { get; set; }
        public bool? InJanam { get; set; }
        public bool? InTransit { get; set; }
        public bool? TimeFormat { get; set; }
    }
}
