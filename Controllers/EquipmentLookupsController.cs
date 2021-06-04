using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquipmentAPI.Models;

namespace EquipmentAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentLookupsController : ControllerBase
    {
        private readonly GOT_EquipmentContext _context;

        public EquipmentLookupsController(GOT_EquipmentContext context)
        {
            _context = context;
        }

        //// GET: api/EquipmentLookups
        //[HttpGet]
        //public  EquipmentLookup GetEquipmentDetails(string serialNumber)
        //{
        //    var equipmentLookup =  _context.EquipmentLookup.Where(e => e.SerialNo == serialNumber).FirstOrDefault();

        //    //if (equipmentLookup == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return equipmentLookup;

        //}


        // GET: api/EquipmentLookups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentLookup>>> GetEquipmentLookup()
        {
            return await _context.EquipmentLookup.ToListAsync();
        }

        // GET: api/EquipmentLookups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentLookup>> GetEquipmentLookup(int id)
        {
            var equipmentLookup = await _context.EquipmentLookup.FindAsync(id);

            if (equipmentLookup == null)
            {
                return NotFound();
            }

            return equipmentLookup;
        }

        // PUT: api/EquipmentLookups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentLookup(int id, EquipmentLookup equipmentLookup)
        {
            if (id != equipmentLookup.Uid)
            {
                return BadRequest();
            }

            _context.Entry(equipmentLookup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentLookupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EquipmentLookups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //public async Task<ActionResult<EquipmentLookup>> PostEquipmentLookup(string body)
        public async Task<ActionResult<EquipmentLookup>> PostEquipmentLookup(EquipmentLookup equipmentLookup)
        {
           // EquipmentLookup equipmentLookup = new EquipmentLookup();
            _context.EquipmentLookup.Add(equipmentLookup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipmentLookupExists(equipmentLookup.Uid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return equipmentLookup;
          //  return CreatedAtAction("GetEquipmentLookup", new { id = equipmentLookup.Uid }, equipmentLookup);
        }

        // DELETE: api/EquipmentLookups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EquipmentLookup>> DeleteEquipmentLookup(int id)
        {
            var equipmentLookup = await _context.EquipmentLookup.FindAsync(id);
            if (equipmentLookup == null)
            {
                return NotFound();
            }

            _context.EquipmentLookup.Remove(equipmentLookup);
            await _context.SaveChangesAsync();

            return equipmentLookup;
        }

        private bool EquipmentLookupExists(int id)
        {
            return _context.EquipmentLookup.Any(e => e.Uid == id);
        }
    }
}
