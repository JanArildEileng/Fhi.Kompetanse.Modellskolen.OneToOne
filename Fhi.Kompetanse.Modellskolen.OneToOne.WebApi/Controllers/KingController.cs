using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
       

            return await _context.Kings.Select(e=> new GetKingDto(e.Name,e.KingId, e.Country!=null?e.Country.Name:"")).ToListAsync();
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


            Country? country = _context.Countries.Include(e=>e.King)
                .FirstOrDefault(c => c.Name.Equals(postKingDto.CountryName));

            if (country == null)
            {
                country = new Country() { Name = postKingDto.CountryName };
                   _context.Add(country);
            } else
            {

                if ( country.King!=null && country.King.Name.Equals(postKingDto.KingnameName))
                {
                    Console.WriteLine($"Finnes allerede {postKingDto}");
                     //return UnprocessableEntity($"Finnes allerede {postKingDto}");
                      return Ok(new GetKingDto(king.Name,king.KingId, country.Name));
                }


            }
            country.King = king;

            _context.Kings.Add(king);

            var debug = _context.ChangeTracker.DebugView;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKings", new { id = king.KingId }, new GetKingDto(king.Name,king.KingId, country.Name));
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
