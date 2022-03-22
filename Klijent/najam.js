import { Firma } from "./Firma.js";
import { Korisnik } from "./Korisnik.js";
import { Vozilo } from "./Vozilo.js";

var listaNazivaFirmi=[];
fetch("https://localhost:5001/Firma/PreuzmiSveFirme",
{
    method: "GET"
})
.then(p=>{
    p.json().then(korisnici=>{
        korisnici.forEach(k => {
            listaNazivaFirmi.push(k);
        })
const firma1 = new Firma(listaNazivaFirmi);
firma1.crtajIznajmiVozilo(document.body);
    })
})