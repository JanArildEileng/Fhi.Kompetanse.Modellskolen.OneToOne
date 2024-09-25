using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Context;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Entities;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Model;
using Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;

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
            return await _context.Countries.Select(e => new GetCountryDto(e.Name, e.CountryId, e.King!=null?e.King.Name:"")).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<GetCountryDto>> PostCountry(PostCountryDto countryDto)
        {
            if (String.IsNullOrEmpty(countryDto.CountryName))
                return NotFound("CountryName missing");

            Country? country = _context.Countries.Where(e => e.Name.Equals(countryDto.CountryName)).Include(e => e.King).FirstOrDefault();

            if (country == null)
            {
                country = new Country() { Name = countryDto.CountryName };
                _context.Countries.Add(country);
            }

            if (!String.IsNullOrEmpty(countryDto.KingnameName))
            {
                if (country.King == null || !country.King.Name.Equals(countryDto.KingnameName))
                {
                    country.King = new King() { Name = countryDto.KingnameName };
                }
                else
                {
                    //  return UnprocessableEntity($"Finnes allerede {countryDto}");
                    return Ok(new GetCountryDto(country.Name, country.CountryId, country.King != null ? country.King.Name : ""));
                }
            }

            var debug = _context.ChangeTracker.DebugView;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCountry", new { id = country.CountryId }, new GetCountryDto(country.Name, country.CountryId, country.King != null ? country.King.Name : ""));
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
