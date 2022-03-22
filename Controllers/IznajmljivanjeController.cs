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
    public class IznajmljivanjeController : ControllerBase
    {
        public FirmaContext Context { get; set; }

        public IznajmljivanjeController(FirmaContext context)
        {
            Context = context;
        }

        [Route("IznajmiVozilo")]
        [HttpPost]
        public async Task<ActionResult> IznajmiVozilo([FromQuery] string nazivFirme, [FromQuery] string jmbgKorisnika, [FromQuery] string nazivVozila)
        {
            if (string.IsNullOrWhiteSpace(jmbgKorisnika) || jmbgKorisnika.Length != 13)
            {
                return BadRequest(new { Poruka = "Pogrešan JMBG korisnika!"});
            }

            if (string.IsNullOrWhiteSpace(nazivFirme) ||  nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }

            if (string.IsNullOrWhiteSpace(nazivVozila) || nazivVozila.Length > 102)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv vozila!"});
            }

            try
            {
                var firmaUpit = Context.Firme.Where(p => p.Naziv == nazivFirme);
                var firma1 = firmaUpit.FirstOrDefault();
                if (firma1 is null)
                {
                    return BadRequest(new { Poruka = $"Firma sa nazivom {nazivFirme} ne postoji."});
                }

                var korisnikUpit = Context.Korisnici.Where(p =>  p.JMBG == jmbgKorisnika);
                var korisnik1 = korisnikUpit.FirstOrDefault();
                if (korisnik1 is null)
                {
                    return BadRequest(new { Poruka = $"Korisnik sa JMBG '{jmbgKorisnika}' ne postoji."});
                }

                var korisnik2 = korisnikUpit.Include(p => p.Firma).FirstOrDefault();
                if (korisnik2.Firma.Naziv != nazivFirme)
                {
                    return BadRequest(new { Poruka = $"Korisnik '{korisnik2.ZaPrikaz}' ne pripada firmi '{firma1.Naziv}', vec '{korisnik2.Firma.Naziv}'."});
                }

                var voziloUpit = Context.Vozila.Where(p => p.Marka + " " + p.Model == nazivVozila && p.Firme.Naziv == nazivFirme);
                var vozilo1 = voziloUpit.FirstOrDefault();
                if (vozilo1 is null)
                {
                    return BadRequest(new { Poruka = $"Vozilo '{nazivVozila}' ne postoji!"});
                }
                vozilo1.Iznajmnjeno=true; 
                Context.Vozila.Update(vozilo1);

                Iznajmljivanje iznajmljivanje = new Iznajmljivanje();
                iznajmljivanje.Korisnik = korisnik2;
                iznajmljivanje.Firma = firma1;
                iznajmljivanje.Vozilo = vozilo1;
                Context.Iznajmljivanja.Add(iznajmljivanje);

                await Context.SaveChangesAsync();

               return Ok
                (
                    await Context.Vozila.Where(p=>p.ID==vozilo1.ID)
                    .Select(p => new
                    {
                        p.Tablice
                    })
                    .ToListAsync()
                );
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }                                  
        }

        [Route("VratiVozilo")]
        [HttpPut]
        public async Task<ActionResult> VratiVozilo([FromQuery] string nazivFirme, [FromQuery] string jmbgKorisnika, [FromQuery] string tabliceVozila, [FromQuery] int ocenaKorisnika)
        {
            if (string.IsNullOrWhiteSpace(nazivFirme) ||  nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }
            if (string.IsNullOrWhiteSpace(jmbgKorisnika) || jmbgKorisnika.Length != 13)
            {
                return BadRequest(new { Poruka = "Pogrešan jmbg korisnika!"});
            }
            if (string.IsNullOrWhiteSpace(tabliceVozila) || tabliceVozila.Length > 7)
            {
                return BadRequest(new { Poruka = "Pogrešno vozilo!"});
            }
            if (ocenaKorisnika < 1 || ocenaKorisnika > 5)
            {
                return BadRequest(new { Poruka = "Ocena mora biti u opsegu od 1 do 5."});
            }

            try
            {
                var firmaUpit = Context.Firme.Where(p => p.Naziv == nazivFirme);
                var firma1 = firmaUpit.FirstOrDefault();
                if (firma1 is null)
                {
                    return BadRequest(new { Poruka = $"Firma sa nazivom {nazivFirme} ne postoji."});
                }

                var korisnikUpit = Context.Korisnici.Where(p =>  p.JMBG == jmbgKorisnika);
                var korisnik1 = korisnikUpit.FirstOrDefault();

                if (korisnik1 is null)
                {
                    return BadRequest(new { Poruka = $"Korisnik sa JMBG b'{jmbgKorisnika}' ne postoji."});
                }

                var korisnik2 = korisnikUpit.Include(p => p.Firma).FirstOrDefault();
                if (korisnik2.Firma.Naziv != nazivFirme)
                {
                    return BadRequest(new { Poruka = $"Korisnik '{korisnik2.ZaPrikaz}' ne pripada firmi '{firma1.Naziv}', vec '{korisnik2.Firma.Naziv}'."});
                }

                var voziloUpit = Context.Vozila.Where(p => p.Tablice  == tabliceVozila && p.Firme.Naziv == nazivFirme);
                var vozilo1 = voziloUpit.FirstOrDefault();

                if (vozilo1 is null)
                {
                    return BadRequest(new { Poruka = $"Vozilo '{tabliceVozila}' ne postoji!"});
                }

                var iznajmljivanje = Context.Iznajmljivanja.Include(p => p.Firma)
                                                            .Where(p => p.Firma == firma1)
                                                            .Include(p => p.Korisnik)
                                                            .Where(p => p.Korisnik == korisnik2)
                                                            .Include(p => p.Vozilo)
                                                            .Where(p => p.Vozilo == vozilo1 && p.Vozilo.Iznajmnjeno==true)
                                                            .FirstOrDefault();

                if (iznajmljivanje is null)
                {
                    return BadRequest(new { Poruka = $"Korisnik '{korisnik1.ZaPrikaz}' nije iznajmio vozilo '{vozilo1.ZaPrikaz}'."});
                }

                var stariBrojOcenjivanja = vozilo1.BrojOcenjivanja++;
                vozilo1.Ocena = (stariBrojOcenjivanja * vozilo1.Ocena + ocenaKorisnika) / vozilo1.BrojOcenjivanja;
                vozilo1.Iznajmnjeno=false;
                Context.Vozila.Update(vozilo1);

                await Context.SaveChangesAsync();

                 return Ok
                (
                    await Context.Vozila.Where(p=>p.ID==vozilo1.ID)
                    .Select(p => new
                    {
                        p.Tablice
                    })
                    .ToListAsync()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }      
        }
    }
}
