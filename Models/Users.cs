using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Short { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; }
        public bool? Visible { get; set; }
        public string DepartmentIncomingList { get; set; }
        public bool? TrackLogin { get; set; }
        public short? MenuId { get; set; }
        public int? SwitchToUserId { get; set; }
        public int? VendorId { get; set; }
        public string SystemUserName { get; set; }
    }
}
