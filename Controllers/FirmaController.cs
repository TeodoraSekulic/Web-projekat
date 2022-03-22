using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace rent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirmaController : ControllerBase
    {
        public FirmaContext Context { get; set; }

        public FirmaController(FirmaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiFirme")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiFirme([FromQuery] string nazivFirme)
        {
            try
            {
                var firme = await Context.Firme.Where(p => p.Naziv==nazivFirme).ToListAsync();
                return Ok(
                    firme.Select(p => 
                        new
                        {
                            Adresa = p.Adresa,
                            Telefon = p.Telefon
                        })
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiSveFirme")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiSveFirme()
        {
             try
            {
                return Ok
                (
                    await Context.Firme
                    .Select(p => p.Naziv)
                    .Distinct()
                    .ToListAsync()
                );
            }
            catch (Exception e)
            {
                return BadRequest("Doslo je do greske: " + e.Message);
            }
        }

        [Route("BrojKorisnika")]
        [HttpGet]
        public int BrojKorisnika([FromQuery] string nazivFirme)
        {
            var broj = Context.Korisnici.Where(p=> p.Firma.Naziv == nazivFirme).Count();  
            return broj;
        }

        [Route("BrojVozila")]
        [HttpGet]
        public int BrojVozila([FromQuery] string nazivFirme)
        {
            var broj = Context.Vozila.Where(p=> p.Firme.Naziv == nazivFirme).Count();  
            return broj;
        }

        [Route("BrojIznjamljivanja")]
        [HttpGet]
        public int BrojIznjamljivanja([FromQuery] string nazivFirme)
        {
            var broj = Context.Iznajmljivanja.Where(p=> p.Firma.Naziv == nazivFirme).Count();  
            return broj;
        }

        
    }
}