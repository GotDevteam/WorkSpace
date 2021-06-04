using EquipmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentReceivedBackViewModel
    {
        private GOT_EquipmentContext _context;

        [JsonPropertyName("equipmentType")]
        public EquipmentTypeViewModel EquipmentType { get; set; }

        [JsonPropertyName("equipment")]
        public EquipmentViewModel Equipment { get; set; }

        [JsonPropertyName("storeInformation")]
        public StoreInformationViewModel StoreInformation { get; set; }

        [JsonPropertyName("transactionDate")]
        public DateTime TransactionDate { get; set; }

        [JsonPropertyName("warrantyEndDate")]
        public DateTime? WarrantyEndDAte { get; set; }

        [JsonPropertyName("equipmentChecklists")]
        public List<EquipmentCheckListViewModel> EquipmentChecklists { get; set; }

        [JsonPropertyName("receivedBy")]
        public string ReceivedBy { get; set; }

        public bool LoadDataForSerialNo(string SerialNo)
        {

            var equipment = _context.EquipmentLookup.Where(e => e.SerialNo == SerialNo).FirstOrDefault();
            if (equipment == null)
            {
                return false;
            }

            LoadDataForThisEquipment(equipment);
            return true;

        }

        public bool LoadDataForGotSerialNo(string GOTSerialNo)
        {
            var equipment = _context.EquipmentLookup.Where(e => e.GotSerialNo == GOTSerialNo).FirstOrDefault();
            if (equipment == null)
            {
                return false;
            }

            
            LoadDataForThisEquipment(equipment);
            return true;
        }

        public StatusResultViewModel IsOkToProceed()
        {
            StatusResultViewModel statusResult = new StatusResultViewModel();

            //validate whether the transaction already created 
            var existingtrans = _context.EquipmentTransaction.Where(et => et.SerialNo == this.Equipment.SerialNo && et.Status == true).OrderByDescending(et => et.Date).OrderByDescending(e => e.Uid).Take(1).FirstOrDefault();
            if (existingtrans != null)
            statusResult = IsOkToProceed(existingtrans);
            if (statusResult.Success == false) return statusResult;

            statusResult.Success = true;
            return statusResult;
        }
        public StatusResultViewModel IsOkToProceed(EquipmentTransaction existingtrans)
        {
            StatusResultViewModel statusResult = new StatusResultViewModel();

            if (existingtrans != null)
            {

                if (existingtrans.RecordType == 1)
                {
                    var usrname = _context.Users.Where(u => u.UserId == existingtrans.UserId).FirstOrDefault().UserName;

                    if (usrname == null)
                    {
                        statusResult.Message = "This equipment was already added to inventory on [" + existingtrans.Date + "] by user " + existingtrans.UserId;
                    }
                    else
                    {
                        statusResult.Message = "This equipment was already added to inventory on [" + existingtrans.Date + "] by user " + usrname.ToString();
                    }


                    statusResult.Success = false;
                    return statusResult;
                }

                if (existingtrans.RecordType == 11)
                {

                    var usrname = _context.Users.Where(u => u.UserId == existingtrans.UserId).FirstOrDefault().UserName;

                    if (usrname == null)
                    {
                        statusResult.Message = "This equipment was already added to repair on [" + existingtrans.Date + "] by user " + existingtrans.UserId;
                    }
                    else
                    {
                        statusResult.Message = "This equipment was already added to repair on [" + existingtrans.Date + "] by user " + usrname.ToString();
                    }


                    statusResult.Success = false;
                    return statusResult;

                }

            }
            statusResult.Success  = true;
            return statusResult;
        }

        public StatusResultViewModel SaveChangesToDataBase(GOT_EquipmentContext context)
        {
            StatusResultViewModel statusResult = new StatusResultViewModel();
            try
            {
                this._context = context;
                bool IsRepair = false;
                bool IsSerialFound = true;
                int transactionId = 0;

                if ((this.TransactionDate - DateTime.Now).Days > 0 )
                {
                    statusResult.Message = "Transaction Date[" + this.TransactionDate.ToString() + "is greater than current Datetime [" + DateTime.Now.ToString() + "]";
                    statusResult.Success = false;
                    return statusResult;
                }

                //validate whether the transaction already created 
                var existingtrans = _context.EquipmentTransaction.Where(et => et.SerialNo == this.Equipment.SerialNo && et.Status == true).OrderByDescending(et => et.Date).OrderByDescending(e => e.Uid).Take(1).FirstOrDefault();

                statusResult = IsOkToProceed(existingtrans);
                if (statusResult.Success == false)
                {
                    return statusResult;
                }
                //reset the status will set to true to in the end
                statusResult = new StatusResultViewModel();

                //last transaction is some other transaction so change the status of that to false
                if (existingtrans != null)
                    existingtrans.Status = false;
                

                EquipmentLookup eqlookup = new EquipmentLookup();
                var equipmentalreadyexists = this._context.EquipmentLookup.Where(e => e.SerialNo == this.Equipment.SerialNo).FirstOrDefault();
                if (equipmentalreadyexists == null)
                {
                    //Add new entry to equipment loop table
                    IsSerialFound = false;
                    eqlookup.SerialNo = this.Equipment.SerialNo;
                    eqlookup.GotSerialNo = this.Equipment.GOTSerialNo;
                    eqlookup.Note = this.Equipment.SerialDescription;
                    eqlookup.Note = "Created by new Web UI";
                    eqlookup.WarrantyEndDate = this.WarrantyEndDAte;
                    eqlookup.Status = true;
                    eqlookup.UserId = Common.UserID;
                    //find the equipmenttypeid 

                    var equiptypeid = this._context.EquipmentType.Where(e => e.Description == this.EquipmentType.Description).FirstOrDefault().Id;

                    eqlookup.EquipmentType = equiptypeid;

                    context.EquipmentLookup.Add(eqlookup);
                }
                else
                {
                    if (equipmentalreadyexists.GotSerialNo != null)
                    {
                        //check whether entered got serial and manuf serial are same
                        if (equipmentalreadyexists.GotSerialNo.Length > 0)
                        {
                            if (equipmentalreadyexists.GotSerialNo != this.Equipment.GOTSerialNo)
                            {
                                statusResult.Message = "Existing GOTSerial No [" + equipmentalreadyexists.GotSerialNo + "] not same as [" + this.Equipment.GOTSerialNo + "]what you entered for this Serial No";
                                statusResult.Success = false;
                                return statusResult;
                            }
                        }
                    }
                    equipmentalreadyexists.GotSerialNo = this.Equipment.GOTSerialNo;
                    equipmentalreadyexists.UpdateDate = DateTime.Now;
                    equipmentalreadyexists.UserId = Common.UserID;
                    equipmentalreadyexists.Status = true;
                    IsSerialFound = true;
                }
                this._context.SaveChanges();


                //Create receipt entry
                EquipmentTransaction equipmentreceivedBakTransaction = new EquipmentTransaction();
                equipmentreceivedBakTransaction.Date = this.TransactionDate;
                equipmentreceivedBakTransaction.Note = "Received Back Created by New Web UI";
                equipmentreceivedBakTransaction.UserId = Common.UserID;
                equipmentreceivedBakTransaction.SerialNo = this.Equipment.SerialNo;
                equipmentreceivedBakTransaction.RecordType = 5; //received back
                equipmentreceivedBakTransaction.StoreId = this.StoreInformation.StoreCode;
                equipmentreceivedBakTransaction.Status = false;

                this._context.EquipmentTransaction.Add(equipmentreceivedBakTransaction);
                this._context.SaveChanges();


                //Checklist verification
                //Create receipt entry
                EquipmentTransaction equipmentCheckListTransaction = new EquipmentTransaction();
                equipmentCheckListTransaction.Date = this.TransactionDate;
                equipmentCheckListTransaction.Note = "Check list verification";
                equipmentCheckListTransaction.UserId = Common.UserID;
                equipmentCheckListTransaction.SerialNo = this.Equipment.SerialNo;
                equipmentCheckListTransaction.RecordType = 17; //checklist verification
                equipmentCheckListTransaction.StoreId = 0000;
                equipmentCheckListTransaction.Status = false;
                this._context.EquipmentTransaction.Add(equipmentCheckListTransaction);
                this._context.SaveChanges();

                foreach (var chklist in this.EquipmentChecklists)
                {
                    EquipmentTransactionCheckList chklisttrans = new EquipmentTransactionCheckList();
                    chklisttrans.ChecklistId = chklist.ID;
                    chklisttrans.IsWorking = chklist.IsWorking;
                    chklisttrans.Notes = chklist.Description;
                    chklisttrans.TransactionId = equipmentCheckListTransaction.Uid;

                    if (chklist.IsWorking == false) IsRepair = true;

                    this._context.EquipmentTransactionCheckList.Add(chklisttrans);
                }



                if (IsRepair)
                {

                    //Create Add to inventory
                    EquipmentTransaction equipmentInInventoryTransaction = new EquipmentTransaction();
                    equipmentInInventoryTransaction.Date = this.TransactionDate;
                    equipmentInInventoryTransaction.Note = "Damaged euipment";
                    equipmentInInventoryTransaction.UserId = Common.UserID;
                    equipmentInInventoryTransaction.SerialNo = this.Equipment.SerialNo;
                    equipmentInInventoryTransaction.RecordType = 11; //Add to  inventory
                    equipmentInInventoryTransaction.StoreId = 0000;
                    equipmentInInventoryTransaction.Status = true;
                    this._context.EquipmentTransaction.Add(equipmentInInventoryTransaction);

                }
                else
                {

                    //Create Add to inventory
                    EquipmentTransaction equipmentInInventoryTransaction = new EquipmentTransaction();
                    equipmentInInventoryTransaction.Date = this.TransactionDate;
                    equipmentInInventoryTransaction.Note = "Adding to Inventory";
                    equipmentInInventoryTransaction.UserId = Common.UserID;
                    equipmentInInventoryTransaction.SerialNo = this.Equipment.SerialNo;
                    equipmentInInventoryTransaction.RecordType = 1; //Add to  inventory
                    equipmentInInventoryTransaction.StoreId = 0000;
                    equipmentInInventoryTransaction.Status = true;
                    this._context.EquipmentTransaction.Add(equipmentInInventoryTransaction);

                }

                context.SaveChanges();

                if (IsRepair)
                {
                    statusResult.Message = "Successfully, Added to repair list. The transaction id is :[" + equipmentCheckListTransaction.Uid + "]";
                }
                else
                {
                    statusResult.Message = "Successfully, Added to Inventory. The transaction id is :[" + equipmentCheckListTransaction.Uid + "]";
                }
                statusResult.Success = true;
            }
            catch(Exception ex)
            {
                Common.WriteToErrorLog(this._context, ex, "Error while saving Equipment Receive Back data", "1000", "EquipmentReceivedBackViewModel.SaveChangesToDataBase");
                
                statusResult.Message = "Sorry some unexpected error happened.Please contact the support.";
                statusResult.Success = false;
            }
            return statusResult;
        }

        public void LoadDataForThisEquipment(EquipmentLookup equipment)
        {

            this.Equipment = new EquipmentViewModel(this._context);
            this.Equipment.CopyDataFromModel(equipment);           

            this.WarrantyEndDAte = equipment.WarrantyEndDate;
            this.TransactionDate = DateTime.Now;

            try
            {
                this.StoreInformation = new StoreInformationViewModel();

                var storeId = _context.EquipmentTransaction.Where(et => et.SerialNo == equipment.SerialNo).OrderByDescending(et => et.Date).Take(1)
                        .SingleOrDefault().StoreId;
                //.Select( et => new { StoreId =  et.StoreId}).SingleOrDefault();
                if (storeId != null)
                {

                    var store = _context.StoreInfo.Where(s => s.StoreId == (int)storeId).FirstOrDefault();
                    if (store != null)
                    {
                      
                        this.StoreInformation.CopyDataFromModel((StoreInfo)store);

                    }

                }
            }
            catch(Exception ex)
            {
                //store could be blank
                this.StoreInformation = new StoreInformationViewModel();
            }

            this.EquipmentType = new EquipmentTypeViewModel("", "");
            var equipmenttype = _context.EquipmentType.Find(equipment.EquipmentType);
            if (equipmenttype != null)
            {

                this.EquipmentType = new EquipmentTypeViewModel(equipmenttype.Id.ToString(), equipmenttype.Description);

                EquipmentCheckListViewModel eqchklist = new EquipmentCheckListViewModel(this._context);
                this.EquipmentChecklists = eqchklist.LoadDataForEquipmentType(equipmenttype.Id,"ReceivedBack");

            }

        }


        public EquipmentReceivedBackViewModel()
        {
            
        }
        public EquipmentReceivedBackViewModel(GOT_EquipmentContext context)
        {
            this._context = context;
        }
        //equipmentChecklists:equipmentChecklist[];
        //receivedBy:String;
    }
}
