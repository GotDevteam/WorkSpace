using EquipmentAPI.Models;
using EquipmentAPI.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentGoingOutStoreInfoViewModel
    {
        private GOT_EquipmentContext _context;

       [Key] public string ID { get; set; }
        public int StoreID { get; set; }
        
        public string StoreName { get; set; }

        public int DaysWaiting { get; set; }

        public int NoOfEquipments { get; set; }
      
        public string ShippingMethod { get; set; }

        public string TrackingNumber { get; set; }

        public string ShippingLabel { get; set; }

        public decimal TotalWeight { get; set; }

        public string ProcessingStatus { get; set; }

        public int ProcessingUserID { get; set; }

        public string ProcessingUserName { get; set; }        

        public int UID { get; set; }

        public string Note { get; set; }

        public bool IsExpanded { get; set; }

        public bool IsReadOnly { get; set; }

        public DateTime? ChecklistCompletedDate { get; set; }

        [NotMapped()]
        public List<GoingOutEquipmentDetails> EquipmentDetails { get; set; }

        public EquipmentGoingOutStoreInfoViewModel(GOT_EquipmentContext context)
        {
            this._context = context;
        }

        public EquipmentGoingOutStoreInfoViewModel()
        {
            
        }

        public StatusResultViewModel DeleteAllGoingOutEquipments(int NeedToSendHeaderID)
        {
            StatusResultViewModel status = new StatusResultViewModel();
            EquipmentNeedToSendHeader eqpntshdr = this._context.EquipmentNeedToSendHeader.Find(NeedToSendHeaderID);
            if (eqpntshdr == null)
            {
                status.Success = false;
                status.Message = "Unable to locate the Need to send header record for the passed id [" + NeedToSendHeaderID.ToString() + "]";
                return status;
            }
            foreach (EquipmentNeedToSend nts in this._context.EquipmentNeedToSend.Where( n => n.NeedToSendHeaderId == NeedToSendHeaderID))
            {
                this._context.EquipmentNeedToSendChecklist.RemoveRange(this._context.EquipmentNeedToSendChecklist.Where(nc => nc.NeedToSendId == nts.Uid));

                //delete the transaction created for this serial no.
                EquipmentTransaction latesttrans =  this._context.EquipmentTransaction.Where(e => e.SerialNo == nts.SerialNo && e.Status == true).FirstOrDefault();
                if (latesttrans != null)
                {
                    if (latesttrans.RecordType != 7 )
                    {
                        status.Success = false;
                        status.Message = "There are other transactions happened for this serial [" + latesttrans.SerialNo + "] so you cannot delete.";
                        return status;
                    }
                    this._context.EquipmentTransaction.Remove(latesttrans);

                    EquipmentTransaction previoustrans = this._context.EquipmentTransaction.Where(e => e.SerialNo == nts.SerialNo && e.RecordType == 1).OrderByDescending(e => e.Uid).Take(1).FirstOrDefault();
                    if(previoustrans != null)
                    {
                        previoustrans.Status = true;
                    }
                }

                nts.SerialNo = "";
                nts.NeedToSendHeaderId = 0;
                nts.Status = true;
            }
            

            this._context.EquipmentNeedToSendHeader.Remove(eqpntshdr);
            this._context.SaveChanges();

            status.Success = true;
            status.Message = "Successfully deleted all the equipments for this store.";                
            return status;

        }

        public StatusResultViewModel StartGoingOutEquipmentsProcess(GOT_EquipmentContext context)
        {
            this._context = context;

            StatusResultViewModel status = new StatusResultViewModel();
            Models.EquipmentNeedToSendHeader ntsheader;

            if (this.UID > 0)
            {
                ntsheader = this._context.EquipmentNeedToSendHeader.Find(this.UID);
            }

            ntsheader = new EquipmentNeedToSendHeader();
            ntsheader.Note = this.Note;
            ntsheader.ProcessingUserId = Common.UserID;
            ntsheader.ShippingLabel = this.ShippingLabel;
            ntsheader.ShippingMethod = this.ShippingMethod;
            ntsheader.StoreId = this.StoreID;
            ntsheader.TotalWeight = this.TotalWeight;
            ntsheader.TrackingNumber = this.TrackingNumber;
            ntsheader.ProcessingStatus = "Started";

            if (this.UID == 0)
            {
                this._context.EquipmentNeedToSendHeader.Add(ntsheader);
            }

            this._context.SaveChanges();


            foreach (Models.EquipmentNeedToSend ntos in this._context.EquipmentNeedToSend.Where(nts => nts.StoreId == this.StoreID && nts.Status == true))
            {
                ntos.NeedToSendHeaderId = ntsheader.Uid;
            }
            this._context.SaveChanges();


            status.Message = ntsheader.Uid.ToString();
            status.Success = true;

            return status;
        }

        private StatusResultViewModel DataIsValid()
        {
            StatusResultViewModel status = new StatusResultViewModel();
            if (this.StoreID <= 0 )
            {
                status.Message = "Invalid store Id passed. ";
                    status.Success = false;
                return status;
            }

            if (this.EquipmentDetails.Count <= 0)
            {
                status.Message = "There is no equipment data passed.";
                status.Success = false;

                return status;
            }

            foreach (GoingOutEquipmentDetails goequipvm in this.EquipmentDetails)
            {

                if (goequipvm.Equipment == null)
                {
                    status.Message = "There is no equipment information passed.";
                    status.Success = false;
                    return status;
                }

                if (goequipvm.Equipment.SerialNo.Length == 0 )
                {
                    status.Message = "Equipment serial number cannot be blank.";
                    status.Success = false;
                    return status;
                }

                if (goequipvm.EquipmentChecklists.Count() == 0 || goequipvm.EquipmentChecklists == null )
                {
                    status.Message = "There is no checklist data found for serial no [" + goequipvm.Equipment.SerialNo + "]";
                    status.Success = false;
                    return status;
                }

                List<EquipmentChecklistMapping> chkmappinglist = this._context.EquipmentChecklistMapping.Where(m => m.TransactionScreen == "GoingOut" &&  m.EquipmentTypeId == Convert.ToInt32(goequipvm.EquipmentType.Code)).ToList();

                if (chkmappinglist.Count() != goequipvm.EquipmentChecklists.Count())
                {
                    status.Message = "The no of checklist should be [" + chkmappinglist.Count().ToString() + "], but the number verified list is [" + goequipvm.EquipmentChecklists.Count().ToString() + "]";
                    status.Success = false;
                    return status;
                }

                foreach (ViewModels.EquipmentCheckListViewModel chklistvm in goequipvm.EquipmentChecklists)
                {
                    if (chkmappinglist.Where( m => m.CheckListId == chklistvm.ID).Count() <= 0 )
                    {
                        status.Message = "Invalid checklist Id passed. Please contact support";
                        status.Success = false;
                        return status;
                    }
                    
                    if (chklistvm.IsWorking == false)
                    {

                        status.Message = "Checklist [" + chklistvm.Name + "] not checked for serial no [" + goequipvm.Equipment.SerialNo + "]";
                        status.Success = false;
                        return status;
                    }


                }

                status = goequipvm.IsValidTransactionStatus( goequipvm.Equipment.SerialNo, this.StoreID, this._context);
                if (status.Success == false) return status;
                status.Success = false;
            }


            status.Success = true;
            return status;
        }

        public StatusResultViewModel SaveGoingOutEquipments(GOT_EquipmentContext context, bool isComplete)
        {
            this._context = context;

            StatusResultViewModel status = new StatusResultViewModel();


            if (isComplete)
            {
                status = DataIsValid();
                if (status.Success == false) return status;
            }

                status.Success = false;

            Models.EquipmentNeedToSendHeader ntsheader;

            if (this.UID > 0) 
            {
                ntsheader = this._context.EquipmentNeedToSendHeader.Find(this.UID);
            }
            else
            {
                ntsheader = new EquipmentNeedToSendHeader();
            }

                
                ntsheader.Note = this.Note;                
                ntsheader.ProcessingUserId = Common.UserID;
                ntsheader.ShippingLabel = this.ShippingLabel;
                ntsheader.ShippingMethod = this.ShippingMethod;
                ntsheader.StoreId = this.StoreID;
                ntsheader.TotalWeight = this.TotalWeight;
                ntsheader.TrackingNumber = this.TrackingNumber;
                ntsheader.ProcessingStatus = isComplete ? "Completed" : "Processing";
                ntsheader.ChecklistCompletedDate = DateTime.Now;

            if (this.UID == 0)
            { 
                this._context.EquipmentNeedToSendHeader.Add(ntsheader);
            }

            this._context.SaveChanges();

            foreach (GoingOutEquipmentDetails goequipvm in this.EquipmentDetails)
            {

                Models.EquipmentNeedToSend equipNeedToSend = this._context.EquipmentNeedToSend.Find(goequipvm.UID);
                if (equipNeedToSend == null)
                {
                    status.Message = "Unable to locate the need to send Data. ";
                    status.Success = false;
                    return status;                    
                }

                equipNeedToSend.SerialNo = goequipvm.Equipment.SerialNo;
                equipNeedToSend.NeedToSendHeaderId = ntsheader.Uid;                
                equipNeedToSend.Status = isComplete ? false: true;
                
                if (goequipvm.EquipmentChecklists != null)
                {
                
                    foreach (ViewModels.EquipmentCheckListViewModel chklistvm in goequipvm.EquipmentChecklists)
                    {
                        Models.EquipmentNeedToSendChecklist ntsChklist;

                        ntsChklist = this._context.EquipmentNeedToSendChecklist.Where(nchk => nchk.NeedToSendId == equipNeedToSend.Uid && nchk.CheckListId == chklistvm.ID).SingleOrDefault();
                        if (ntsChklist == null)
                        {
                           

                            //add new record
                            ntsChklist = new EquipmentNeedToSendChecklist();
                            ntsChklist.IsWorking = chklistvm.IsWorking;
                            ntsChklist.CheckListId = chklistvm.ID;
                            ntsChklist.Note = chklistvm.Description;
                            ntsChklist.NeedToSendId = equipNeedToSend.Uid;
                            this._context.EquipmentNeedToSendChecklist.Add(ntsChklist);
                        }
                        else
                        {
                            //edit
                            ntsChklist.IsWorking = chklistvm.IsWorking;
                            ntsChklist.CheckListId = chklistvm.ID;
                            ntsChklist.Note = chklistvm.Description;
                            ntsChklist.NeedToSendId = equipNeedToSend.Uid;

                           

                        }

                    }
                    


                }
                if (isComplete)
                {
                    Models.EquipmentTransaction existingtrans;
                    existingtrans = this._context.EquipmentTransaction.Where(e => e.SerialNo == goequipvm.Equipment.SerialNo && e.Status == true).FirstOrDefault();
                    if (existingtrans != null) existingtrans.Status = false;

                    Models.EquipmentTransaction eqptrans = new EquipmentTransaction();
                    eqptrans.ReasonId = 0;
                    eqptrans.RecordType = 7;
                    eqptrans.Note = "Checklist completed and ready to send";
                    eqptrans.SerialNo = goequipvm.Equipment.SerialNo;
                    eqptrans.Status = true;
                    eqptrans.StoreId = ntsheader.StoreId;
                    eqptrans.Date = DateTime.Now;
                    eqptrans.AddDate = DateTime.Now;
                    eqptrans.UserId = Common.UserID;
                    this._context.EquipmentTransaction.Add(eqptrans);

                }

            }

 
            this._context.SaveChanges();

            status.Message =  "Store [" + ntsheader.StoreId.ToString() +  "] succesfully saved.";
            status.Success = true;
            return status;
        }


        public List<EquipmentGoingOutStoreInfoViewModel> GetGoingOutStoreList(int TabSelected, DateTime FromDate, DateTime ToDate)
        {
            List<EquipmentGoingOutStoreInfoViewModel> goingOutStoreList = new List<EquipmentGoingOutStoreInfoViewModel>();
            var Tabselectedparam = new SqlParameter()
            {
                ParameterName = "Tabselected",
                Value = TabSelected                
            };

            var FromDateParam = new SqlParameter()
            {
                ParameterName = "FromDate",
                Value = FromDate.Date
            };


            var TodateParam = new SqlParameter()
            {
                ParameterName = "ToDate",
                Value = ToDate.Date
            };


            goingOutStoreList = _context.GoingOutStoreInfoList.FromSqlRaw("exec dbo.sp_GetEquipmentGoingOutStoreInfo @Tabselected,@Fromdate, @Todate", Tabselectedparam, FromDateParam,TodateParam ).ToList();

            foreach(EquipmentGoingOutStoreInfoViewModel ego in goingOutStoreList)
            {
                //for each store get the equipments to be sent

                var needtosenddetails = from nts in _context.EquipmentNeedToSend
                                        from elkup in _context.EquipmentLookup.Where(lkp => lkp.SerialNo == nts.SerialNo).DefaultIfEmpty()
                                        from et in _context.EquipmentType.Where(etyp => etyp.Id == elkup.EquipmentType).DefaultIfEmpty()
                                        from reqet in _context.EquipmentType.Where( etyp => etyp.Id == nts.EquipmentType).DefaultIfEmpty()
                                        from usr in _context.Users.Where(u => u.UserId == nts.UserId).DefaultIfEmpty()
                                        where nts.StoreId == ego.StoreID && (nts.Status == true || nts.NeedToSendHeaderId == ego.UID)
                                        orderby nts.DateToSend
                                        select new
                                        {
                                            serialno = nts.SerialNo,
                                            gotserialno = elkup.GotSerialNo,
                                            serialdesc = elkup.Note,
                                            eqtypeid = et.Id,
                                            eqtypename = et.Description,
                                            username = usr.UserName,
                                            adddate = nts.AddDate,
                                            datetosend = nts.DateToSend,
                                            note = nts.Note,
                                            uid = nts.Uid,
                                            reqeqtypeid = reqet.Id,
                                            reqeqtypename = reqet.Description

                                        };

                EquipmentViewModel equipment;
                EquipmentTypeViewModel equipmentType;
                EquipmentTypeViewModel reqequipmentType;
                ego.EquipmentDetails = new List<GoingOutEquipmentDetails>();

                foreach (var needtosend in needtosenddetails )
                {
                    GoingOutEquipmentDetails goeqdetails = new GoingOutEquipmentDetails(this._context);

                    goeqdetails.AddDate = needtosend.adddate;
                    goeqdetails.DateToSend = needtosend.datetosend;
                    goeqdetails.Note = needtosend.note;
                    goeqdetails.UID = needtosend.uid;
                    goeqdetails.AddedBy = needtosend.username;
                    goeqdetails.AddDate = needtosend.adddate;


                    equipment = new EquipmentViewModel(this._context);
                    equipment.SerialNo = needtosend.serialno==null?"":needtosend.serialno;
                    equipment.GOTSerialNo = needtosend.gotserialno;
                    equipment.SerialDescription = needtosend.serialdesc;
                    goeqdetails.Equipment = equipment;

                    reqequipmentType = new EquipmentTypeViewModel(this._context);
                    reqequipmentType.Code = needtosend.reqeqtypeid.ToString();
                    reqequipmentType.Description = needtosend.reqeqtypename;
                    goeqdetails.RequestedEquipmentType= reqequipmentType;

                    equipmentType = new  EquipmentTypeViewModel(this._context);
                    equipmentType.Code = needtosend.eqtypeid.ToString();
                    equipmentType.Description = needtosend.eqtypename;
                    goeqdetails.EquipmentType = equipmentType;

                    ego.EquipmentDetails.Add(goeqdetails);

                    if (equipmentType != null && needtosend.eqtypeid  > 0 )
                    {
                        goeqdetails.EquipmentChecklists = new List<EquipmentCheckListViewModel>();

                        var allchecklistmappings = from nschk in _context.EquipmentNeedToSendChecklist
                                                   from chk in _context.EquipmentCheckList.Where(c => c.Id == nschk.CheckListId).DefaultIfEmpty()
                                                   where nschk.NeedToSendId == needtosend.uid 
                                                   select new
                                                   {
                                                       ChecklistID = chk.Id,
                                                       CheckListName = chk.CheckListName,
                                                       CheckListSortOrder = chk.SortOrder,
                                                       chklistnote = nschk.Note,
                                                       isworking = nschk.IsWorking
                                                   };

                        if (allchecklistmappings != null)
                        {
                            goeqdetails.EquipmentChecklists = new List<EquipmentCheckListViewModel>();
                            bool isEquipmentChecklistVerified = true;

                            foreach (var checklisitem in allchecklistmappings)
                            {
                                EquipmentCheckListViewModel equipmentCheckListViewModel = new EquipmentCheckListViewModel();
                                equipmentCheckListViewModel.Name = checklisitem.CheckListName == null?"":checklisitem.CheckListName;
                                equipmentCheckListViewModel.ID = checklisitem.ChecklistID;
                                equipmentCheckListViewModel.Description = checklisitem.chklistnote == null? "": checklisitem.chklistnote;
                                equipmentCheckListViewModel.IsWorking = checklisitem.isworking == null ? false : checklisitem.isworking;
                                equipmentCheckListViewModel.SortOrder = checklisitem.CheckListSortOrder == null ? 0 : checklisitem.CheckListSortOrder;

                                if (checklisitem.isworking == true) isEquipmentChecklistVerified = isEquipmentChecklistVerified == false ? false : true;
                                else isEquipmentChecklistVerified = false;


                                goeqdetails.EquipmentChecklists.Add(equipmentCheckListViewModel);
                            }
                            goeqdetails.IsChecklistCompleted = isEquipmentChecklistVerified;

                        }

                    }
                }                
                
            }

            return goingOutStoreList;
        }

    }
    

}


namespace EquipmentAPI.Models
{
    public partial class GOT_EquipmentContext : DbContext
    {
     
        public DbSet<EquipmentGoingOutStoreInfoViewModel> GoingOutStoreInfoList { get; set; }
    }
}
