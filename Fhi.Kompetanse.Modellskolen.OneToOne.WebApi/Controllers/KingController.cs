
namespace Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KingController : ControllerBase
    {
        private readonly KompetanseContext _context;

        public KingController(KompetanseContext context)
        {
            _context = context;
        }

        // GET: api/King
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetKingDto>>> GetKings()
        {
       
            //TODO add Country.Name if exists
            return await _context.Kings.Select(e=> new GetKingDto(e.Name,e.KingId,"<CountryName here>")).ToListAsync();
        }


        // POST: api/King
        [HttpPost]
        public async Task<ActionResult<GetKingDto>> PostKing([FromBody] PostKingDto postKingDto)
        {
            if (String.IsNullOrEmpty(postKingDto.CountryName))
                return NotFound("CountryName missing");
            if (String.IsNullOrEmpty(postKingDto.KingnameName))
                return NotFound("KingnameName missing");
           


            King king = new King() { Name = postKingDto.KingnameName };


            //TODO 
            //Find country , if not found create and add, else update with King (if changed)

              
            //Add King


            var debug = _context.ChangeTracker.DebugView;
            await _context.SaveChangesAsync();

            //TODO Set in country.Name
            return CreatedAtAction("GetKings", new { id = king.KingId }, new GetKingDto(king.Name,king.KingId,"<country.Name>"));
        }

        // DELETE: api/King/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKing(int id)
        {
            var king = await _context.Kings.FindAsync(id);
            if (king == null)
            {
               Console.WriteLine($"DeleteKing Not Found  id={id}");
                return NotFound();
            }

            _context.Kings.Remove(king);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KingExists(int id)
        {
            return _context.Kings.Any(e => e.KingId == id);
        }



    }
}
