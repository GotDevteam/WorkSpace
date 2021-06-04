using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentCheckListMappingViewModel
    {
        public EquipmentTypeViewModel EquipmentType { get; set; }
        public List<EquipmentCheckListViewModel> EquipmentChecklists { get; set; }

        public EquipmentCheckListMappingViewModel()
        {
            this.EquipmentType = new EquipmentTypeViewModel("","");
            this.EquipmentChecklists = new List<EquipmentCheckListViewModel>();
        }
        public EquipmentCheckListMappingViewModel(string EquipmentTypeCode, string EquipmentTypeName, List<EquipmentCheckListViewModel> equipmentCheckLists)
        {

        }
    }



}


  

