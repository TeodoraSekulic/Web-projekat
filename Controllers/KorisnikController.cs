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
    public class KorisnikController : ControllerBase
    {
        public FirmaContext Context { get; set; }

        public KorisnikController(FirmaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiKorisnikeIzFirme")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiKorisnikeIzFirme([FromQuery] string nazivFirme)
        {
            if (nazivFirme is null)
            {
                return BadRequest(new { Poruka = "Morate uneti naziv firme!"});
            }

            if ( nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }

            try
            {
                 var firmaUBazi = Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault();
                if (firmaUBazi is null)
                {
                    return BadRequest(new { Poruka = $"Firma sa nazivom {nazivFirme} ne postoji!"});
                }

                var korisnici = await Context.Korisnici
                                            .Include(p => p.Firma)
                                            .Where(p => p.Firma.Naziv == nazivFirme)
                                            .ToListAsync();
                            
                return Ok(
                    korisnici.Select(p => 
                    new
                    {
                        Korisnik = p.ZaPrikaz
                    }
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }  
        }

        [Route("DodajKorisnika")]
        [HttpPost]
        public async Task<ActionResult> DodajKorisnika([FromQuery] string nazivFirme, [FromQuery] string ime, [FromQuery] string prezime, [FromQuery] string JMBG,  [FromQuery] string telefon)
        {
            if (string.IsNullOrWhiteSpace(nazivFirme) || nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }

            if (string.IsNullOrWhiteSpace(JMBG) || JMBG.Length != 13)
            {
                return BadRequest(new { Poruka = "JMBG Korisnika mora sadržati 13 cifara!"});
            }
            for(int i=0; i<JMBG.Length; i++)
            {
                if(JMBG[i]<'0' || JMBG[i]>'9')
                {
                    return BadRequest(new { Poruka = "JMBG Korisnika nije validan!"});
                }
            }

            if (string.IsNullOrWhiteSpace(ime) || ime.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešno ime!"});
            }

            if (string.IsNullOrWhiteSpace(prezime) || prezime.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešno prezime!"});
            }

            if ( string.IsNullOrWhiteSpace(telefon) || telefon.Length > 11)
            {
                return BadRequest(new { Poruka = "Pogresan kontakt korisnika!"});
            }

            try
            {
                var firma = Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault();
                if (firma is null)
                {
                    return BadRequest(new { Poruka = $"Firma sa nazivom {nazivFirme} ne postoji!"});
                }


                var korisnik = Context.Korisnici.Where(p => p.JMBG == JMBG  && p.Firma.Naziv==nazivFirme).FirstOrDefault();
                if (korisnik != null)
                {
                    return BadRequest(new { Poruka = $"Korisnik sa JMBG '{JMBG}' je već registrovan u firmi '{korisnik.Firma.Naziv}'!"});
                }

                var korisnikSaKlijenta = new Korisnik();
                korisnikSaKlijenta.Ime = ime;
                korisnikSaKlijenta.Prezime = prezime;
                korisnikSaKlijenta.JMBG = JMBG;
                korisnikSaKlijenta.Firma = firma;
                korisnikSaKlijenta.Telefon = telefon;
                Context.Korisnici.Add(korisnikSaKlijenta);
                await Context.SaveChangesAsync();
                return Ok(new { Poruka = $"Korisnik '{korisnikSaKlijenta.ZaPrikaz}' je uspešno dodat u firmu '{firma.Naziv}'."});
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }
        }
    }
}