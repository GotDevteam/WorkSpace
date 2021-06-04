using EquipmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class GoingOutEquipmentDetails
    {
        private Models.GOT_EquipmentContext _dbcontext;
        public int UID { get; set; }
        public DateTime? DateToSend { get; set; }
        public string Note { get; set; }
        public DateTime? AddDate { get; set; }
        public string AddedBy { get; set; }
        public EquipmentTypeViewModel EquipmentType { get; set; }
        public EquipmentTypeViewModel RequestedEquipmentType { get; set; }

        public EquipmentViewModel Equipment { get; set; }

        public List<EquipmentCheckListViewModel> EquipmentChecklists { get; set; }

        public bool IsChecklistCompleted { get; set; }
        public GoingOutEquipmentDetails(Models.GOT_EquipmentContext dbcontext)
        {
            this._dbcontext = dbcontext;

        }
        public GoingOutEquipmentDetails()
        {         

        }

        public  StatusResultViewModel IsValidTransactionStatus(string serialNo, int storeID, GOT_EquipmentContext context)
        {
            this._dbcontext = context;
            StatusResultViewModel status = new StatusResultViewModel();

            Models.EquipmentTransaction equipTrans = this._dbcontext.EquipmentTransaction.Where(et => et.SerialNo == serialNo && et.Status == true).SingleOrDefault();
            bool continueprocess = false;

            if (equipTrans != null)
            {
                status.Success = false;
                switch (equipTrans.RecordType)
                {
                    case 1:
                        continueprocess = true;
                        break;
                    case 2:
                        status.Message = "This equipment is currently in service.";
                        break;
                    case 6:
                        status.Message = "This equipment is currently in repair.";
                        break;
                    case 7:
                        status.Message = "This equipment is already set for going out to store id [" + equipTrans.StoreId + "]";
                        break;
                    case 8:
                        status.Message = "Already sent out to store id [" + equipTrans.StoreId + "]";
                        break;
                    case 9:
                        status.Message = "This equipment is already assigned to a store.";
                        break;
                    case 10:
                        status.Message = "This equipment is in lost status .";
                        break;
                    case 11:
                        status.Message = "This equipment is in damaged status .";
                        break;
                    case 14:
                        status.Message = "This equipment is in recycle status .";
                        break;
                    default:
                        status.Message = "This equipment is not in Inventory status";
                        break;

                }

            }
            else
            {
                status.Message = "Unable to determine the current status of Inventory as there is no data in equipment transaction for this serial no";
            }
            if (!continueprocess)
            {
                status.Success = false;
                return status;
            }
            else
            {
                foreach (Models.EquipmentNeedToSend needtosend in this._dbcontext.EquipmentNeedToSend.Where(nts => nts.SerialNo == serialNo && nts.Status == true && nts.StoreId != storeID))
                {

                    status.Message = "This equipment is already picked for store id[" + needtosend.StoreId.ToString() + "].";
                    status.Success = false;
                    return status;

                }

                status.Success = true;
                return status;
            }
        }

        public StatusResultViewModel LoadDataForGotSerialNo(string gotSerialNo, int storeId)
        {
            StatusResultViewModel status = new StatusResultViewModel();

            if (this.Equipment == null) this.Equipment = new EquipmentViewModel(this._dbcontext);
            if (!(this.Equipment.LoadDataForGotSerialNo(gotSerialNo)))
            {
                status.Message = "Unable to locate the GOT serial No [" + gotSerialNo + "]";
                status.Success = false;
                return status;
            }
                

            if (this.EquipmentType == null) this.EquipmentType = new EquipmentTypeViewModel(this._dbcontext);
            if (!this.EquipmentType.LoadDataForID(Convert.ToByte(this.Equipment.EquipmentTypeID)))
            {
                status.Message = " There is no equipment type specified for this GOT serial no [" + gotSerialNo + "]";
                status.Success = false;
                return status;
            }

            status = IsValidTransactionStatus(Equipment.SerialNo, storeId, this._dbcontext);

            if (status.Success == false) return status;
            status.Success = false;

            
            
            
            EquipmentCheckListViewModel eqchkvm = new EquipmentCheckListViewModel(this._dbcontext);
            this.EquipmentChecklists = eqchkvm.LoadDataForEquipmentType(Convert.ToByte(this.EquipmentType.Code), "GoingOut");

            status.Message = "Loaded successfully";
            status.Success = true;
            return status;
            
        }

    }
}
