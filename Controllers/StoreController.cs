using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentAPI.Models;

namespace EquipmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly GOT_EquipmentContext _context;

        public StoreController(GOT_EquipmentContext context)
        {
            _context = context;
        }

        [HttpGet("GetLookup")]
        public IActionResult GetLookup() {
            /*
            var allchecklistmappings = from emap in _context.EquipmentChecklistMapping
                                       join et in _context.EquipmentType on emap.EquipmentTypeId equals et.Id
                                       join chk in _context.EquipmentCheckList on emap.CheckListId equals chk.Id
                                       where emap.TransactionScreen == ""
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
            */

            var result = _context.StoreInfo.Take(10);
            /*
            result = _context.StoreInfo.Take(10).Select(row => new
            {
                StoreID = row.StoreId,
                StoreName = row.StoreName
            });
            */

            return Ok(result);
        }

    }
}
