using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;

namespace rent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoziloController : ControllerBase
    {
        public FirmaContext Context { get; set; }

        public VoziloController(FirmaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiVoziloIzFirme")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiVoziloIzFirme([FromQuery] string nazivFirme)
        {
            if (nazivFirme is null)
            {
                return BadRequest(new { Poruka = "Morate uneti ime firme!"});
            }

            if (nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešno ime firme!"});
            }

            try
            {
                var firma1=Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault(); 

                if (firma1 is null)
                {
                    return BadRequest(new { Poruka = $"Frima sa nazivom {nazivFirme} ne postoji!"});
                }

                var voziloUpit = Context.Vozila.Include(p => p.Firme).Where(p => p.Firme == firma1); 
                                                
                var vozila = await voziloUpit.ToListAsync();
                return Ok(
                    vozila.Select(p =>
                        new
                        {   
                            Marka = p.Marka,
                            Model = p.Model,
                            Tablice=p.Tablice,
                            Cena=p.Cena,
                            Ocena = p.Ocena >= 1 ? Math.Round(p.Ocena, 2).ToString() : "UNK",
                            Iznajmnjeno=p.Iznajmnjeno
                        }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }
        }

         [Route("PreuzmiVozilaKorisnika")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiVozilaKorisnika([FromQuery] string nazivFirme, [FromQuery] string jmbg)
        {
            if (jmbg is null)
            {
                return BadRequest(new { Poruka = "Morate uneti JMBG!"});
            }
            if (nazivFirme is null)
            {
                return BadRequest(new { Poruka = "Morate uneti ime firme!"});
            }

            if (nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešno ime firme!"});
            }

             if (jmbg.Length > 13 || jmbg.Length < 13)
            {
                return BadRequest(new { Poruka = "Pogrešan jmbg korisnika!"});
            }

            try
            {
                var firma1=Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault();
                if (firma1 is null)
                {
                    return BadRequest(new { Poruka = $"Frima sa nazivom {nazivFirme} ne postoji!"});
                }

                var korisnik = await Context.Korisnici.Where(p=> p.JMBG == jmbg).FirstOrDefaultAsync();
                if (korisnik is null)
                {
                    return BadRequest(new { Poruka = $"Korisnik sa JMBG {jmbg} ne postoji!"});
                }
                var korisnikFirma=await Context.Korisnici.Where(p=> p.JMBG == jmbg && p.Firma.Naziv==nazivFirme).FirstOrDefaultAsync();
                if (korisnikFirma is null) // ukoliko ne pripada odgovarajucoj firmi
                {
                    return BadRequest(new { Poruka = $"Korisnik sa JMBG {jmbg} ne pripada firmi {nazivFirme}!"});
                }

                var iznajmljivanje= Context.Iznajmljivanja.Where(p=> p.Firma.ID==firma1.ID && p.Korisnik.ID==korisnik.ID);

                var vozila =iznajmljivanje.Select(p => p.Vozilo).Distinct();  
                return Ok(
                    vozila.Select(p =>
                        new
                        {   
                            Marka = p.Marka,
                            Model = p.Model,
                            Tablice=p.Tablice,
                            Cena=p.Cena,
                            Ocena = p.Ocena >= 1 ? Math.Round(p.Ocena, 2).ToString() : "UNK",
                        }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }
        }


        [Route("PreuzmiVozilaMM")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiVozilaMM([FromQuery] string nazivFirme)
        {
            if (nazivFirme is null)
            {
                return BadRequest(new { Poruka = "Morate uneti ime firme!"});
            }

            if (nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešno ime firme!"});
            }

            try
            {
                var firma1=Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault();

                if (firma1 is null)
                {
                    return BadRequest(new { Poruka = $"Frima sa nazivom {nazivFirme} ne postoji!"});
                }

                var voziloUpit = Context.Vozila.Include(p => p.Firme).Where(p => p.Firme == firma1); 
                                                
                var vozila = await voziloUpit.ToListAsync();
                return Ok(
                    vozila.Select(p =>
                        new
                        {   
                            MarkaModel = p.Marka + " " + p.Model,
                        }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }
        }

        [Route("DodajVoziloUFirmu")]
        [HttpPost]
        public async Task<ActionResult> DodajVoziloUFirmu([FromQuery] string nazivFirme, [FromQuery] string markaVozila, [FromQuery] string modelVozila, [FromQuery] string tabliceVozila, [FromQuery] int cenaVozila)
        {
            if (string.IsNullOrWhiteSpace(nazivFirme)  || nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }

            if (string.IsNullOrWhiteSpace(markaVozila) || markaVozila.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešna marka vozila!"});
            }

            if (string.IsNullOrWhiteSpace(modelVozila) || modelVozila.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan model vozila!"});
            }

             if (string.IsNullOrWhiteSpace(tabliceVozila) || tabliceVozila.Length > 7)
            {
                return BadRequest(new { Poruka = "Pogrešne tablice vozila!"});
            }

            try
            {
                var firma1 = Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault(); 
                if (firma1 is null)
                {
                    return BadRequest(new { Poruka = $"Firma sa nazivom {nazivFirme} ne postoji!"});
                }

                var vozilo1 = Context.Vozila.Where(p => p.Tablice == tabliceVozila).Include(p => p.Firme).FirstOrDefault();
                Vozilo voziloZaUnos = null;

                if (vozilo1 != null) // ispitujemo da li vozilo vec postoji u firmi
                {
                        return BadRequest(new { Poruka = $"Vozilo sa tablicama '{vozilo1.Tablice}' vec postoji u firmi '{vozilo1.Firme.Naziv}'!"});
                    
                }
                else // vozilo ne postoji, kreira se
                {
                    voziloZaUnos = new Vozilo();
                    voziloZaUnos.Marka = markaVozila;
                    voziloZaUnos.Model = modelVozila;
                    voziloZaUnos.Tablice = tabliceVozila;
                    voziloZaUnos.Cena = cenaVozila;
                    voziloZaUnos.Ocena = 0;
                    voziloZaUnos.BrojOcenjivanja = 0;
                    voziloZaUnos.Iznajmnjeno=false;
                    voziloZaUnos.Firme=firma1;
                    
                    Context.Vozila.Add(voziloZaUnos);

                }
                await Context.SaveChangesAsync();

                return Ok(new { 
                    Ocena = voziloZaUnos.Ocena >= 1 ? Math.Round(voziloZaUnos.Ocena, 2).ToString() : "UNK",
                    Poruka = $"Vozilo '{voziloZaUnos.ZaPrikaz}' je uspešno dodato u katalog firme '{firma1.Naziv}'."
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniVoziloUFirmi")]
        [HttpPut]
        public async Task<ActionResult> IzmeniVoziloUFirmi([FromQuery] string nazivFirme, [FromQuery] string markaVozila, [FromQuery] string modelVozila, [FromQuery] string tabliceVozila, [FromQuery] int cenaVozila)
        {
            if (string.IsNullOrWhiteSpace(nazivFirme)  || nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }

            if (string.IsNullOrWhiteSpace(markaVozila) || markaVozila.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešna marka vozila!"});
            }

            if (string.IsNullOrWhiteSpace(modelVozila) || modelVozila.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan model vozila!"});
            }

             if (string.IsNullOrWhiteSpace(tabliceVozila) || tabliceVozila.Length > 7)
            {
                return BadRequest(new { Poruka = "Pogrešne tablice vozila!"});
            }
            try
            {
                var vozilo = Context.Vozila.Where(p=> p.Marka==markaVozila && p.Model==modelVozila  && p.Firme.Naziv==nazivFirme).FirstOrDefault();
                
                if(vozilo != null) 
                {
                    vozilo.Tablice=tabliceVozila;
                    vozilo.Cena=cenaVozila;

                    await Context.SaveChangesAsync();
                    return Ok(await Context.Vozila.Where(p=>p.Marka==markaVozila && p.Model==modelVozila).Select(p=> 
                    new {
                        Tablice=p.Tablice,
                        Cena=p.Cena
                    }).ToListAsync());
                }
                else{
                    return BadRequest("Nije pronadjeno vozilo u bazi!");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }
        }

        [Route("UkloniVoziloIzFirme")]
        [HttpDelete]
        public async Task<ActionResult> UkloniVoziloIzFirme([FromQuery] string nazivFirme, [FromQuery] string tabliceVozila)
        {
            if (string.IsNullOrWhiteSpace(nazivFirme)  || nazivFirme.Length > 50)
            {
                return BadRequest(new { Poruka = "Pogrešan naziv firme!"});
            }

            if (string.IsNullOrWhiteSpace(tabliceVozila) || tabliceVozila.Length > 7)
            {
                return BadRequest(new { Poruka = "Pogrešne tablice vozila!"});
            }

           try
            {
                var firma = Context.Firme.Where(p => p.Naziv == nazivFirme).FirstOrDefault();
                if (firma is null)
                {
                    return BadRequest(new { Poruka = $"Firma sa nazivom {nazivFirme} ne postoji!"});
                }

                var vozilo = Context.Vozila.Where(p => p.Tablice == tabliceVozila).FirstOrDefault();
                if (vozilo == null) 
                {
                    return BadRequest(new { Poruka = $"Vozilo '{tabliceVozila}' ne postoji."});
                }

                var voziloFirma = Context.Vozila.Where(p => p.Firme == firma && p.Tablice == tabliceVozila).FirstOrDefault();
                if (voziloFirma.Iznajmnjeno == true)
                {
                    return BadRequest(new { Poruka = $"Vozilo '{vozilo.ZaPrikaz}' se ne može obrisati jer je trenutno iznajmljeno!"});
                }

                if (voziloFirma == null)
                {
                    return BadRequest(new { Poruka = $"Vozilo '{vozilo.ZaPrikaz}' ne postoji u firmi '{firma.Naziv}'."});
                }

                Context.Vozila.Remove(voziloFirma);
                await Context.SaveChangesAsync();

                return Ok(new { Poruka = $"Vozilo uspešno uklonjena iz firme '{firma.Naziv}'." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Poruka = e.Message});
            }
        }
    }
}