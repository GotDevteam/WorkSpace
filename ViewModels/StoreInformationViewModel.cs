using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class StoreInformationViewModel
    {
        public int? StoreCode { get; set; }
        public int? CustomerStoreCode { get; set; }
        public string StoreName {get;set;}


        public StoreInformationViewModel()
        {
            this.StoreCode =0;
            this.CustomerStoreCode = 0;
            this.StoreName = "";
        }
        public void CopyDataFromModel(Models.StoreInfo storeinfodm)
        {
            this.StoreCode = storeinfodm.StoreId;
            this.CustomerStoreCode = storeinfodm.StoreId1;
            this.StoreName = storeinfodm.StoreName;

        }
    }
}
