using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainModels;
using H3_PostgresRESTFulAPI.Data;

namespace H3_PostgresRESTFulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RGBDatasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public RGBDatasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/RGBDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RGBData>>> GetRGBData()
        {
            return await _context.RGBData.ToListAsync();
        }

        // GET: api/RGBDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RGBData>> GetRGBData(int id)
        {
            var rGBData = await _context.RGBData.FindAsync(id);

            if (rGBData == null)
            {
                return NotFound();
            }

            return rGBData;
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<RGBDataDTO>>> GetRGBDataByUser(int userId)
        {
            var rGBData = await _context.RGBData
                .Where(r => r.Owner.Id == userId)
                .Select(r => new RGBDataDTO
                {
                    Id = r.Id,
                    Red = r.Red,
                    Green = r.Green,
                    Blue = r.Blue,
                })
                .ToListAsync();

            if (rGBData == null || !rGBData.Any())
            {
                return NotFound();
            }

            return rGBData;
        }



        // PUT: api/RGBDatas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRGBData(int id, RGBData rGBData)
        {
            if (id != rGBData.Id)
            {
                return BadRequest();
            }

            _context.Entry(rGBData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RGBDataExists(id))
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

        // POST: api/RGBDatas
        [HttpPost]
        public async Task<ActionResult<RGBDataDTO>> PostRGBData(RGBDataDTO rGBDataDTO)
        {
            var user = await _context.Users.FindAsync(rGBDataDTO.OwnerId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Create a new RGBData instance from the DTO
            var rGBData = new RGBData
            {
                Red = rGBDataDTO.Red,
                Green = rGBDataDTO.Green,
                Blue = rGBDataDTO.Blue,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Owner = user // Set the User object as the owner of the RGBData
            };

            _context.RGBData.Add(rGBData);
            await _context.SaveChangesAsync();

            // Return the created RGBDataDTO
            return CreatedAtAction("GetRGBData", new { id = rGBData.Id }, rGBDataDTO);
        }



        // DELETE: api/RGBDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRGBData(int id)
        {
            var rGBData = await _context.RGBData.FindAsync(id);
            if (rGBData == null)
            {
                return NotFound();
            }

            _context.RGBData.Remove(rGBData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RGBDataExists(int id)
        {
            return _context.RGBData.Any(e => e.Id == id);
        }
    }
}
