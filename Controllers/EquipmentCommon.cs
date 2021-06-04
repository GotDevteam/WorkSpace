using EquipmentAPI.BaseClasses;
using EquipmentAPI.Models;
using EquipmentAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EquipmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentCommon : GOTControllerBase
    {
        private readonly GOT_EquipmentContext _context;

        public EquipmentCommon(GOT_EquipmentContext context)
        {
            _context = context;
        }


        // GET: api/<EquipmentCommon>
        [HttpGet("Test")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StatusResultViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            StatusResultViewModel statusResultViewModel = new StatusResultViewModel();
            statusResultViewModel.Message = "hitting equipmentmanger website successfully !!!!";
            statusResultViewModel.Success = true;
            return Ok(statusResultViewModel);

        }


        // GET: api/<EquipmentCommon>
        [HttpGet("GetEquipmentChecklistMapping/{screen}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EquipmentCheckListMappingViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string screen)
        {
            List<ViewModels.EquipmentCheckListMappingViewModel> equipmentChecklistMappingviewmodels = new List<EquipmentCheckListMappingViewModel>();

           

            var allchecklistmappings = from emap in _context.EquipmentChecklistMapping
                                       join et in _context.EquipmentType on   emap.EquipmentTypeId equals  et.Id
                                       join chk in _context.EquipmentCheckList on emap.CheckListId equals chk.Id
                                       where emap.TransactionScreen == screen
                                       && et.Id == emap.EquipmentTypeId
                                       && chk.Id == emap.CheckListId                                       
                                       select new
                                       {
                                           EquipmentCode = et.Id.ToString(),
                                           EquipmentDesc = et.Description,
                                           ChecklistID = chk.Id,
                                           CheckListName = chk.CheckListName,
                                           CheckListSortOrder = chk.SortOrder

                                       };

            EquipmentCheckListMappingViewModel equipmentCheckListMappingViewModel = new EquipmentCheckListMappingViewModel();
            string lastAddedData = string.Empty;
            foreach (var chkmapping in allchecklistmappings)
            {

                if (lastAddedData == string.Empty || lastAddedData != chkmapping.EquipmentDesc)
                {
                    equipmentCheckListMappingViewModel = new EquipmentCheckListMappingViewModel();
                    EquipmentTypeViewModel equipmentType = new EquipmentTypeViewModel("","");
                    equipmentType.Description = chkmapping.EquipmentDesc;
                    equipmentType.Code = chkmapping.EquipmentCode.ToString();
                    equipmentCheckListMappingViewModel.EquipmentType = equipmentType;

                }

                ViewModels.EquipmentCheckListViewModel checklistvm = new EquipmentCheckListViewModel();
                checklistvm.Description = string.Empty;
                checklistvm.ID = chkmapping.ChecklistID;
                checklistvm.IsWorking = false;
                checklistvm.Name = chkmapping.CheckListName;
                checklistvm.SortOrder = chkmapping.CheckListSortOrder;

                equipmentCheckListMappingViewModel.EquipmentChecklists.Add(checklistvm);

                if (lastAddedData == string.Empty || lastAddedData != chkmapping.EquipmentDesc)
                {
                    equipmentChecklistMappingviewmodels.Add(equipmentCheckListMappingViewModel);
                }
                lastAddedData = chkmapping.EquipmentDesc;
            }


            return Ok(equipmentChecklistMappingviewmodels);

        }


        // GET: api/<EquipmentCommon>
        [HttpGet("GetEquipmentByGOTSerialNo/{GOTserialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViewModels.EquipmentViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEquipmentByGOTSerialNo(string GOTserialNo)
        {
            ViewModels.EquipmentViewModel equipment = new EquipmentViewModel(this._context);

            if (equipment.LoadDataForGotSerialNo(GOTserialNo) == false)
            {
                return BadRequest("Unable to load the data for the entered  Serial No[" + GOTserialNo + "].");

            }           

            return Ok(equipment);

        }


 

        // GET: api/<EquipmentCommon>
        [HttpGet("GetGoingOutEquipmentByGOTSerialNo/{GOTserialNo}/{SerialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GoingOutEquipmentDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGoingOutEquipmentByGOTSerialNo(string GOTserialNo, int SerialNo)
        {
            ViewModels.GoingOutEquipmentDetails goequpdetvm = new GoingOutEquipmentDetails(this._context);

            StatusResultViewModel status = goequpdetvm.LoadDataForGotSerialNo(GOTserialNo,SerialNo);
            if (status.Success == false)
            {
                return NotFound(status.Message);
            }

            return Ok(goequpdetvm);

        }


        // GET: api/<EquipmentCommon>
        [HttpGet("GeEquipmentTypeBySerialNo/{GOTserialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EquipmentTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GeEquipmentTypeBySerialNo(string serialNo)
        {
            ViewModels.EquipmentTypeViewModel equipmenttype = new EquipmentTypeViewModel(this._context);

            if (equipmenttype.LoadDataForSerialNo(serialNo) == false)
            {
                return NotFound();
            }
         
            return Ok(equipmenttype);

        }



        // GET: api/<EquipmentCommon>
        [HttpGet("GeReceivedBackByGOTSerialNo/{GOTserialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EquipmentReceivedBackViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GeReceivedBackByGOTSerialNo(string GOTserialNo)
        {
            ViewModels.EquipmentReceivedBackViewModel equipmentReceivedBack = new EquipmentReceivedBackViewModel(this._context);

            if (equipmentReceivedBack.LoadDataForGotSerialNo(GOTserialNo) == false)
            {
                return BadRequest("Unable to load the data for the entered  Serial No[" + GOTserialNo + "]. You can still add to Inventory / Repair");
                

            }

            StatusResultViewModel statusresult = equipmentReceivedBack.IsOkToProceed();
            if (statusresult.Success == false) return NotFound(statusresult.Message);

            return Ok(equipmentReceivedBack);

        }

        

        // GET: api/<EquipmentCommon>
        [HttpGet("GeReceivedBackBySerialNo/{serialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EquipmentReceivedBackViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public IActionResult GeReceivedBackBySerialNo(string serialNo)
        {
            ViewModels.EquipmentReceivedBackViewModel equipmentReceivedBack = new EquipmentReceivedBackViewModel(this._context);

            if (equipmentReceivedBack.LoadDataForSerialNo(serialNo) == false)
                {
                return BadRequest("Unable to load the data for the entered  Serial No[" + serialNo + "]. You can still add to Inventory / Repair");
            }

            StatusResultViewModel statusresult = equipmentReceivedBack.IsOkToProceed();
            if (statusresult.Success == false) NotFound(statusresult.Message);

            return Ok(equipmentReceivedBack);

        }

        // GET: api/<EquipmentCommon>
        [HttpGet("GetAllTransactionsForGOTSerialNo/{gotSerialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EquipmentTransactionViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTransactionsGOTForSerialNo(string gotSerialNo)
        {
            string serialNo = string.Empty;
            serialNo = this._context.EquipmentLookup.Where(e => e.GotSerialNo == gotSerialNo).SingleOrDefault().SerialNo;

            return GetAllTransactionsForSerialNo(serialNo);
        }

        [HttpGet("GetPagedEquipmentAdo")]
        public IActionResult GetPagedEquipmentAdo(int StoreId, string Filter, string SortOrder, int PageNumber, int PageSize)
        {
            var result = this._context.EquipmentTransaction.AsQueryable();
            //
            //_context.CreaTeCo
            var pagination = "ORDER BY Serial_No OFFSET " + PageNumber.ToString() + " Rows FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";

            var sql = "select lookup.*,eqType.Description eqTypeDescription, LastTran.Record_Type, DateTran.Date lastStatusDate, " +
                      "tranType.Description lastKnownStatus, storeInfo.Store_Name storeName " +
                      "from Equipment_Lookup lookup " +
                      "inner join equipment_type eqType on eqType.UID = lookup.Equipment_Type " +
                      "inner join(select*, ROW_NUMBER() over (PARTITION BY Serial_no order by date desc) as rn from Equipment_Transaction) as LastTran " +
                      "on lookup.Serial_No = LastTran.Serial_No and LastTran.rn = 1 " +
                      "inner join (select Serial_no,Record_Type, Status,Date, ROW_NUMBER() over (PARTITION BY Serial_no order by date desc, uid desc) as rn from Equipment_Transaction) as DateTran " +
                      "on lookup.Serial_No = DateTran.Serial_No and DateTran.rn = 1 and DateTran.Record_Type = 2 and DateTran.Status = 1 " +
                      "inner join Equipment_Transaction_Type tranType on LastTran.Record_Type = tranType.ID " +
                      "inner join store_info storeInfo on storeInfo.Store_ID = LastTran.Store_ID "
                      + pagination;

            

            String connectString = this._context.Database.GetDbConnection().ConnectionString;
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                connection.Open();

                da.Fill(ds);

            }

            //result = ;
            //return result.ToArray();
            return Ok(ds.Tables[0]);
        }


        [HttpGet("GetPagedEquipment")]
        public IActionResult GetPagedEquipment(int StoreId, string Filter, string SortOrder, int PageNumber, int PageSize)
        {
            //var result = this._context.EquipmentTransaction.AsQueryable();
            //
            var sql = "select lookup.*, LastTran.Record_Type, LastTran.Date from Equipment_Lookup lookup "+
                        " inner join Equipment_Transaction trans on trans.Serial_No = lookup.Serial_No "+
                        "inner join(select*, ROW_NUMBER() over (PARTITION BY Serial_no order by date desc) as rn from Equipment_Transaction) as LastTran"+
                            "on trans.Serial_No = LastTran.Serial_No and LastTran.rn = 1 ";

            //var result = _context.Database.ExecuteSqlRaw(sql);
            //var result = _context.Database.FromSqlRaw<string>(sql);

            var result = from eqTran in _context.EquipmentTransaction
                         from eqLookup in _context.EquipmentLookup.Where(tbl => tbl.SerialNo == eqTran.SerialNo)
                         from eqType in _context.EquipmentType.Where(tbl => tbl.Uid == eqLookup.EquipmentType).DefaultIfEmpty()
                         select new
                         {
                             serialno = eqLookup.SerialNo,
                             GOTSerialNo = eqLookup.GotSerialNo,
                             equipmenttypedesc = eqType.Description,
                             lastStatusDate = "01/01/2005",
                             lastKnownStatus = "", 
                             location = "",
                             grind = "",
                             purchaseDate ="",
                             warrentyEndDate = "",
                             StoreId = eqTran.StoreId,
                             note = eqTran.Note
                         };
            
            
            if (StoreId > 0)
            {
                result = result.Where(eq => eq.StoreId == StoreId);
            }

            result = result.Skip(--PageNumber).Take(PageSize);

            if (SortOrder == "desc") {
                result.OrderByDescending(eq => eq.StoreId);
            }
            else
            {
                result.OrderBy(eq => eq.StoreId);
            }

            //var eqList = result.ToList();

            
            /*
            eqList.Select(o => new dto{ 
                e
            });
            */
            //return result.ToArray();
            return Ok(result);
        }

        // GET: api/<EquipmentCommon>
        [HttpGet("GetAllTransactionsForSerialNo/{serialNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EquipmentTransactionViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTransactionsForSerialNo(string SerialNo)
        {
            List<ViewModels.EquipmentTransactionViewModel> equipmenttransvm = new List<EquipmentTransactionViewModel>();

            var alltransactions = from etran in _context.EquipmentTransaction
                                  from elkup in _context.EquipmentLookup.Where(lkp => lkp.SerialNo == etran.SerialNo).DefaultIfEmpty()
                                  from et in _context.EquipmentType.Where(etyp => etyp.Id == elkup.EquipmentType).DefaultIfEmpty()
                                  from si in _context.StoreInfo.Where(st => st.StoreId == etran.StoreId).DefaultIfEmpty()
                                  from usr in _context.Users.Where(u => u.UserId == etran.UserId).DefaultIfEmpty()
                                  from rcdtype in _context.EquipmentTransactionType.Where(rt => rt.Id == etran.RecordType).DefaultIfEmpty()
                                  from rsn in _context.EquipmentTransactionReason.Where(et => et.Id == etran.ReasonId).DefaultIfEmpty()
                                  where etran.SerialNo == SerialNo
                                  orderby etran.Date descending, etran.Uid descending
                                  select new
                                  {
                                      serialno = etran.SerialNo,
                                      GOTSerialNo = elkup.GotSerialNo,
                                      TransactionId = etran.Uid == null ? 0 : etran.Uid,
                                      TransactionDate = etran.Date,
                                      TransactionNote = etran.Note,
                                      Userid = etran.UserId,
                                      UserName = usr.UserName,
                                      equipmentTypeid = et.Id == null ? 0 : et.Id,
                                      equipmenttypedesc = et.Description,
                                      storeid = si.StoreId == null ? 0 : si.StoreId,
                                      storename = si.StoreName,
                                      recordType = etran.RecordType == null ? 0 : etran.RecordType,
                                      recordtypedesc = rcdtype.Description,
                                      reasonid = etran.ReasonId == null ? 0 : etran.ReasonId,
                                      reasondesc = rsn.Description,
                                      status = etran.Status
                                  };

            
            foreach (var et in alltransactions)
            {
                ViewModels.EquipmentTransactionViewModel eq = new EquipmentTransactionViewModel();

                eq.SerialNo = et.serialno;
                eq.GOTSerialNo = et.GOTSerialNo;
                eq.Uid = et.TransactionId;
                eq.TransactionDate = et.TransactionDate;
                eq.Notes = et.TransactionNote;
                eq.UserID = et.Userid;
                eq.UserName = et.UserName;
                eq.EquipmentTypeID = et.equipmentTypeid;
                eq.EquipmentTypeDesc = et.equipmenttypedesc;
                eq.StoreID = et.storeid;
                eq.StoreName = et.storename;
                eq.RecordType = et.recordType;
                eq.RecordTypeDescription = et.recordtypedesc;
                eq.ReasonID = (int)et.reasonid;
                eq.ReasonDescription = et.reasondesc;
                eq.Status = et.status;
                equipmenttransvm.Add(eq);
            }

            return Ok(equipmenttransvm);

        }

        // GET: api/<EquipmentCommon>
        [HttpGet("GetAllEquipmentTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EquipmentCheckListMappingViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllEquipmentTypes()
        {
            List<ViewModels.EquipmentTypeViewModel> equipmentTypesvm = new List<EquipmentTypeViewModel>();


            var equipmentypes = _context.EquipmentType.OrderBy(e => e.Description).ToList();
            foreach (var equipmenttype in equipmentypes)
            {
                ViewModels.EquipmentTypeViewModel eq = new EquipmentTypeViewModel(equipmenttype.Id.ToString(), equipmenttype.Description);
                equipmentTypesvm.Add(eq);
            }

              
            return Ok(equipmentTypesvm);

        }


        // POST: api/EquipmentLookups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("PostEquipmentReceivedBack")]
        public StatusResultViewModel PostEquipmentReceivedBack(EquipmentReceivedBackViewModel equipmentreceivedbackvm)
        {
            

            StatusResultViewModel statusmessage = equipmentreceivedbackvm.SaveChangesToDataBase(this._context);
            return statusmessage;

            //return CreatedAtAction("GetEquipmentLookup", new { id = equipmentreceivedbackvm.Equipment.SerialNo }, equipmentreceivedbackvm);
        }


        // POST: api/EquipmentLookups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("PostEquipmentGoingOutStartProcess")]
        public StatusResultViewModel PostEquipmentGoingOutStartProcess(EquipmentGoingOutStoreInfoViewModel equipmentgoingoutvm)
        {

            StatusResultViewModel statusmessage = equipmentgoingoutvm.StartGoingOutEquipmentsProcess(this._context);
            return statusmessage;

            //return CreatedAtAction("GetEquipmentLookup", new { id = equipmentreceivedbackvm.Equipment.SerialNo }, equipmentreceivedbackvm);
        }

        // POST: api/EquipmentLookups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("PostEquipmentGoingOutInProcess")]
        public StatusResultViewModel PostEquipmentGoingOutInProcess(EquipmentGoingOutStoreInfoViewModel equipmentgoingoutvm)
        {

            StatusResultViewModel statusmessage = equipmentgoingoutvm.SaveGoingOutEquipments(this._context,false);
            return statusmessage;

            //return CreatedAtAction("GetEquipmentLookup", new { id = equipmentreceivedbackvm.Equipment.SerialNo }, equipmentreceivedbackvm);
        }

        // POST: api/EquipmentLookups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("PostEquipmentGoingOut")]
        public StatusResultViewModel PostEquipmentGoingOut(EquipmentGoingOutStoreInfoViewModel equipmentgoingoutvm)
        {

            StatusResultViewModel statusmessage = equipmentgoingoutvm.SaveGoingOutEquipments(this._context,true);
            return statusmessage;

            //return CreatedAtAction("GetEquipmentLookup", new { id = equipmentreceivedbackvm.Equipment.SerialNo }, equipmentreceivedbackvm);
        }


        #region GoingOUt
        // GET: api/<EquipmentCommon>
        [HttpGet("GetGoingOutStoreInfo/{TabSelected}/{FromDate}/{ToDate}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EquipmentGoingOutStoreInfoViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGoingOutStoreInfo(int TabSelected, DateTime FromDate, DateTime ToDate)
        {

            EquipmentGoingOutStoreInfoViewModel egovm = new EquipmentGoingOutStoreInfoViewModel(this._context);            
            return Ok(egovm.GetGoingOutStoreList(TabSelected,  FromDate,  ToDate));

        }


        // POST: Users/Delete/5
        [HttpGet ("DeleteAllGoingOutEquipments/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StatusResultViewModel>))]
        public  IActionResult DeleteAllGoingOutEquipments(int id)
        {
            StatusResultViewModel status = new StatusResultViewModel();
            EquipmentGoingOutStoreInfoViewModel egovm = new EquipmentGoingOutStoreInfoViewModel(this._context);
            status = egovm.DeleteAllGoingOutEquipments(id);
            return Ok(status);

        }

        #endregion




    }
}
