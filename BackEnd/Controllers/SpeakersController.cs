using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Infrastructure;
using Speaker = BackEnd.Data.Speaker;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpeakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Speakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConferenceDTO.SpeakerResponse>>> GetSpeakers()
        {
            return await _context.Speakers.AsNoTracking()
                .Include(s=>s.SessionSpeakers)
                .ThenInclude(ss=>ss.Session)
                .Select(s=>s.MapSpeakerResponse())
                .ToListAsync();
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDTO.SpeakerResponse>> GetSpeaker(int id)
        {
            var speaker = await _context.Speakers
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .SingleOrDefaultAsync(s => s.Id == id);

            if (speaker is null)
            {
                return NotFound();
            }

            return speaker.MapSpeakerResponse();
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speakers.Any(e => e.Id == id);
        }
    }
}
