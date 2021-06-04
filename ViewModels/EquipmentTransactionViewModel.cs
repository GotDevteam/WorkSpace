//using System;
//using System.Collections.Generic;
//using System.Linq;
using System;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentTransactionViewModel
    {
        public string GOTSerialNo { get; set; }
        public string SerialNo { get; set; }

        public int Uid { get; set; }
        public DateTime? TransactionDate { get; set; }

        public int EquipmentTypeID { get; set; }

        public string EquipmentTypeDesc { get; set; }

        public byte? RecordType { get; set; }

        public string RecordTypeDescription { get; set; }

        public int? StoreID { get; set; }

        public string StoreName { get; set; }

        public int ReasonID { get; set; }

        public string ReasonDescription { get; set; }

        public int? UserID { get; set; }

        public string UserName { get; set; }

        public bool? Status { get; set; }

        public DateTime? LastStatusDate { get; set; }

        public string Location { get; set; }

        public string Grind { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        public string Notes { get; set; }

    }
}
