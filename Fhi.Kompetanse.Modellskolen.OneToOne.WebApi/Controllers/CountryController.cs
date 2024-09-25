
namespace Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly KompetanseContext _context;

        public CountryController(KompetanseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCountryDto>>> GetCountry()
        {
            //TODO add King.Name if exists
            return await _context.Countries.Select(e => new GetCountryDto(e.Name,e.CountryId, "<KingName>")).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<GetCountryDto>> PostCountry(PostCountryDto countryDto)
        {
            if (String.IsNullOrEmpty(countryDto.CountryName))
                return NotFound("CountryName missing");

            //TODO Find country if exsists , else create
            Country country = null; 

            //If KingnameName , add to country (if combination not already exists...)
          
            var debug = _context.ChangeTracker.DebugView;
            await _context.SaveChangesAsync();

            //TODO Set in King.Name
            return CreatedAtAction("GetCountry", new { id = country.CountryId }, new GetCountryDto(country.Name, country.CountryId,"<King.Name>"));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                Console.WriteLine($"DeleteCountry Not Found  id={id}");
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }
    }
}
