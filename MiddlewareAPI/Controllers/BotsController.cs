using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiddlewareAPI.Models;

namespace MiddlewareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotsController : ControllerBase
    {
        private readonly RPAdatabaseContext _context;

        public BotsController(RPAdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Bots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BotsTable>>> GetBotTables()
        {
            return await _context.BotsTables.ToListAsync();
        }

        // GET: api/Bots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BotsTable>> GetBotTable(int id)
        {
            var botTable = await _context.BotsTables.FindAsync(id);

            if (botTable == null)
            {
                return NotFound();
            }

            return botTable;
        }

        // PUT: api/Bots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBotTable(int id, BotsTable botTable)
        {
            if (id != botTable.BotId)
            {
                return BadRequest();
            }

            _context.Entry(botTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotTableExists(id))
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

        // POST: api/Bots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BotsTable>> PostBotTable(BotsTable botTable)
        {
            _context.BotsTables.Add(botTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBotTable", new { id = botTable.BotId }, botTable);
        }

        // DELETE: api/Bots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBotTable(int id)
        {
            var botTable = await _context.BotsTables.FindAsync(id);
            if (botTable == null)
            {
                return NotFound();
            }

            _context.BotsTables.Remove(botTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BotTableExists(int id)
        {
            return _context.BotsTables.Any(e => e.BotId == id);
        }

    }
}
