using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentCheckListViewModel
    {

        private Models.GOT_EquipmentContext _dbcontext;
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsWorking { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }
        public EquipmentCheckListViewModel()
        {

        }
        public EquipmentCheckListViewModel(Models.GOT_EquipmentContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public List<EquipmentCheckListViewModel> LoadDataForEquipmentType(byte? equipmentTypeID, string screenName)
        {
            List<EquipmentCheckListViewModel> EquipmentChecklists;
            EquipmentChecklists = new List<EquipmentCheckListViewModel>();

            var allchecklistmappings = from emap in _dbcontext.EquipmentChecklistMapping
                                       join chk in _dbcontext.EquipmentCheckList on emap.CheckListId equals chk.Id
                                       where emap.TransactionScreen == screenName
                                       && emap.EquipmentTypeId == equipmentTypeID
                                       && chk.Id == emap.CheckListId
                                       orderby chk.SortOrder
                                       select new
                                       {
                                           ChecklistID = chk.Id,
                                           CheckListName = chk.CheckListName,
                                           CheckListSortOrder = chk.SortOrder

                                       };

            if (allchecklistmappings != null)
            {
                

                foreach (var checklisitem in allchecklistmappings)
                {
                    EquipmentCheckListViewModel equipmentCheckListViewModel = new EquipmentCheckListViewModel();
                    equipmentCheckListViewModel.Name = checklisitem.CheckListName;
                    equipmentCheckListViewModel.ID = checklisitem.ChecklistID;
                    equipmentCheckListViewModel.Description = "";
                    equipmentCheckListViewModel.IsWorking = false;
                    equipmentCheckListViewModel.SortOrder = checklisitem.CheckListSortOrder;
                    EquipmentChecklists.Add(equipmentCheckListViewModel);
                }
            }

            return EquipmentChecklists;

        }
    }
}
