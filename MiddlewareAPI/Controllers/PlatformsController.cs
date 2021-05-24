using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiddlewareAPI.DataContext;
using MiddlewareAPI.Enums;
using MiddlewareAPI.Models;
using MiddlewareAPI.Services.Interfaces;

namespace MiddlewareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly RPAdatabaseContext _context;
        public PlatformNamesEnums platformNamesEnums;
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IIdentityService _identityService;

        public PlatformsController(RPAdatabaseContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        // GET: api/Platforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformTable>>> GetPlatformGroupTables()
        {
            return await _context.PlatformTables.ToListAsync();
        }

        // GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformTable>> GetPlatformGroupTable(int id)
        {
            var platformGroupTable = await _context.PlatformTables.FindAsync(id);

            if (platformGroupTable == null)
            {
                return NotFound();
            }
            
            return platformGroupTable;
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatformGroupTable(int id, PlatformTable platformGroupTable)
        {
            if (id != platformGroupTable.PlatformId)
            {
                return BadRequest();
            }

            _context.Entry(platformGroupTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformGroupTableExists(id))
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

        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlatformTable>> PostPlatformDetails(PlatformTable platformGroupTable)
        {
            _context.PlatformTables.Add(platformGroupTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlatformGroupTableExists(platformGroupTable.PlatformId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var platformUser = new UserPlatformTable
            {
                PlatformId = platformGroupTable.PlatformId,
           

            };
            await _context.UserPlatformTables.AddAsync(platformUser);
            await _context.SaveChangesAsync();
            //Content to send to next api
            var content = new PlatformTable()
            {
                PlatformId = platformGroupTable.PlatformId,
                PlatformEmail = platformGroupTable.PlatformEmail,
                PlatformPassword = platformGroupTable.PlatformPassword
            };
            string[] plats = { "AutomationAnywhere","UiPath","MicrosoftRPA"};
            var results = Array.Find(plats, s => s.Equals(platformUser.PlatformTable.PlatformName));
            PlatformNamesEnums platform = (PlatformNamesEnums)Enum.Parse(typeof(PlatformNamesEnums), results);

            if( platform == PlatformNamesEnums.AutomationAnywhere)
            { 
                    var urlAA = "";
                    var platAA = JsonSerializer.Serialize(content);
                    var requestAA = new HttpRequestMessage(HttpMethod.Post, urlAA);
                    requestAA.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestAA.Content = new StringContent(platAA, Encoding.UTF8);
                    requestAA.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var responseAA = await _httpClient.SendAsync(requestAA);
                    responseAA.EnsureSuccessStatusCode();
            }
            if (platform == PlatformNamesEnums.MicrosoftRPA)
            {

                var url = "";
                var company = JsonSerializer.Serialize(content);
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(company, Encoding.UTF8);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

            }
            if (platform == PlatformNamesEnums.UiPath)
            {
                var urlUI = "";
                var platUI = JsonSerializer.Serialize(content);
                var requestUI = new HttpRequestMessage(HttpMethod.Post, urlUI);
                requestUI.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                requestUI.Content = new StringContent(platUI, Encoding.UTF8);
                requestUI.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseUI = await _httpClient.SendAsync(requestUI);
                responseUI.EnsureSuccessStatusCode();
            }
        

            return CreatedAtAction("GetPlatformGroupTable", new { id = platformGroupTable.PlatformId }, platformGroupTable);
        }

        // DELETE: api/Platforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatformGroupTable(int id)
        {
            var platformGroupTable = await _context.PlatformTables.FindAsync(id);
            if (platformGroupTable == null)
            {
                return NotFound();
            }

            _context.PlatformTables.Remove(platformGroupTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatformGroupTableExists(int id)
        {
            return _context.PlatformTables.Any(e => e.PlatformId == id);
        }
    }
}
