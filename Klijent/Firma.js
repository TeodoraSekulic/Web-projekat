import { Korisnik } from "./Korisnik.js";
import { Vozilo } from "./Vozilo.js";

export class Firma
{
    constructor(listaFirmi)
    {
        this.kontejner = null;
        this.listaFirmi=listaFirmi;
    }

    crtajInformacije(host)
    {
        let divForma = document.createElement("div");
        divForma.className = "divForma";  
        this.kontejner=host;
        host.appendChild(divForma);
        
        var elLabela = document.createElement("h2");
        elLabela.className="naslov";
        elLabela.innerHTML="Izaberite firmu cije vas informacije interesuju";
        divForma.appendChild(elLabela);

        let divPrvi = document.createElement("div");
        divPrvi.className = "divPrvi";  
        divForma.appendChild(divPrvi);

        let sel= document.createElement("select");
        sel.className="select naziv"
        let l = document.createElement("label");
        l.innerHTML="Naziv firme"
        l.className="labela2"; 
        this.listaFirmi.forEach(p=>{
            console.log("Naziv");
            console.log(p);
        })
        let op;
        console.log(this.listaFirmi);
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op=document.createElement("option");
            op.innerHTML=p;
            sel.appendChild(op);
        });
        divPrvi.appendChild(l);
        divPrvi.appendChild(sel);

        let divStat = document.createElement("div");
        divStat.className = "divStat";  
        divForma.appendChild(divStat);

        let infoDivv = document.createElement("div");
        infoDivv.className = "infoKlasa";
        divStat.appendChild(infoDivv);

        let infoDiv = document.createElement("div");
        infoDiv.className = "infoDiv info"; 
        infoDivv.appendChild(infoDiv);

        let infoDivZaj = document.createElement("div");
        infoDivZaj.className = "infoDivZaj"; 
        divStat.appendChild(infoDivZaj);

        let infoDiv2 = document.createElement("div");
        infoDiv2.className = "infoDiv3 info2"; 
        infoDivZaj.appendChild(infoDiv2);

        let infoDiv3 = document.createElement("div");
        infoDiv3.className = "infoDiv3 info3"; 
        infoDivZaj.appendChild(infoDiv3);

        let infoDiv4 = document.createElement("div");
        infoDiv4.className = "infoDiv3 info4"; 
        infoDivZaj.appendChild(infoDiv4);
        this.statistika();

        sel.onclick=()=>this.statistika();
    }

    statistika()
    {
            var form=this.kontejner.querySelector('.info');
            var roditelj=form.parentElement;
            roditelj.removeChild(form);
            let infoDiv = document.createElement("div");
            infoDiv.className = "infoDiv info"; 
            roditelj.appendChild(infoDiv);

            form=this.kontejner.querySelector('.info2');
            roditelj=form.parentElement;
            roditelj.removeChild(form);
            let infoDiv2 = document.createElement("div");
            infoDiv2.className = "infoDiv3 info2"; 
            roditelj.appendChild(infoDiv2);

            form=this.kontejner.querySelector('.info3');
            roditelj.removeChild(form);
            let infoDiv3 = document.createElement("div");
            infoDiv3.className = "infoDiv3 info3"; 
            roditelj.appendChild(infoDiv3);

            form=this.kontejner.querySelector('.info4');
            roditelj.removeChild(form);
            let infoDiv4 = document.createElement("div");
            infoDiv4.className = "infoDiv3 info4"; 
            roditelj.appendChild(infoDiv4);

            //var naziv = document.querySelector('.naziv').value;
            var naziv = this.kontejner.querySelector('.naziv').value;
            console.log(naziv);
            fetch("https://localhost:5001/Firma/PreuzmiFirme?nazivFirme="+ naziv,
            {
                method:"GET"
            }).then(p=>{
                p.json().then(vozila=>{
                    vozila.forEach(p => {

                        let elLabela = document.createElement("label");
                        elLabela.innerHTML="Adresa: " + p.adresa;
                        elLabela.className="labela2"
                        infoDiv.appendChild(elLabela);

                        elLabela = document.createElement("label");
                        elLabela.innerHTML="Telefon: " + p.telefon;
                        elLabela.className="labela2"
                        infoDiv.appendChild(elLabela);

                        fetch("https://localhost:5001/Firma/BrojKorisnika?nazivFirme="+ naziv,
                        {
                            method:"GET"
                        }).then(p=>{
                            if(p.ok){
            
                                p.json().then(data=>{
                                console.log(data);

                                let elLabela = document.createElement("label");
                                elLabela.innerHTML="Trenutni broj korisnika:";
                                elLabela.className="labela3";
                                infoDiv2.appendChild(elLabela);

                                let l = document.createElement("label");
                                l.className="br";
                                l.innerHTML=data;
                                infoDiv2.appendChild(l);
                    
                                    })
                                }
                        })

                        fetch("https://localhost:5001/Firma/BrojVozila?nazivFirme="+ naziv,
                        {
                            method:"GET"
                        }).then(p=>{
                            if(p.ok){
            
                                p.json().then(data=>{
                                console.log(data);

                                let elLabela = document.createElement("label");
                                elLabela.innerHTML="Trenutni broj vozila:";
                                elLabela.className="labela3";
                                infoDiv3.appendChild(elLabela);

                                let l = document.createElement("label");
                                l.className="br";
                                l.innerHTML=data;
                                infoDiv3.appendChild(l);
                    
                                    })
                                }
                        })

                        fetch("https://localhost:5001/Firma/BrojIznjamljivanja?nazivFirme="+ naziv,
                        {
                            method:"GET"
                        }).then(p=>{
                            if(p.ok){
            
                                p.json().then(data=>{
                                console.log(data);

                                let elLabela = document.createElement("label");
                                elLabela.innerHTML="Trenutni broj iznajmljivanja:";
                                elLabela.className="labela3";
                                infoDiv4.appendChild(elLabela);

                                let l = document.createElement("label");
                                l.className="br";
                                l.innerHTML=data;
                                infoDiv4.appendChild(l);
                                    })
                                }
                        })
                    })
                })
            });
    }

    crtajIstorijuKorisnika(host)
    {
        let istorija = document.createElement("div");
        istorija.className = "istorija";
        this.kontejner=host;
        host.appendChild(istorija);

        let divUnos = document.createElement("div");
        divUnos.className = "divUnos";  
        istorija.appendChild(divUnos);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Unesite podatke ";
        divUnos.appendChild(elLabela);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*JMBG korisnika";
        elLabela.className="labela1"; 
        divUnos.appendChild(elLabela);

        let tb = document.createElement("input");
        tb.className="jmbg";
        divUnos.appendChild(tb);

        let sel= document.createElement("select");
        sel.className="select naziv"
        let l = document.createElement("label");
        l.innerHTML="Naziv firme"
        l.className="labela1"; 
        this.listaFirmi.forEach(p=>{
            console.log("Naziv");
            console.log(p);
        })
        let op;
        console.log(this.listaFirmi);
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op=document.createElement("option");
            op.innerHTML=p;
            sel.appendChild(op);
        });
        divUnos.appendChild(l);
        divUnos.appendChild(sel);
        sel.value="";

        // za prikaz vozila
        let divIspis = document.createElement("div");
        divIspis.className = "divIspis ispis";  
        istorija.appendChild(divIspis);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Vozila koje je korisnik do sada iznajmljivao:";
        divIspis.appendChild(elLabela);

        const dugme = document.createElement("button");
        dugme.className="dugmePrikazi";
        dugme.innerHTML="Prikazi iznajmljivanja";
        divUnos.appendChild(dugme);
        dugme.onclick=()=>
        {
            var form=this.kontejner.querySelector('.ispis');
            var roditelj=form.parentElement;
            roditelj.removeChild(form);
            let divIspis = document.createElement("div");
            divIspis.className = "divIspis ispis"; 
            roditelj.appendChild(divIspis);

            var elLabela = document.createElement("h3");
            elLabela.innerHTML="Vozila koje je korisnik do sada iznajmljivao:";
            divIspis.appendChild(elLabela);

            let jmbg = this.kontejner.querySelector('.jmbg').value;
            console.log(jmbg);
            let naziv = this.kontejner.querySelector('.naziv').value;
            fetch("https://localhost:5001/Vozilo/PreuzmiVozilaKorisnika?nazivFirme="+naziv+"&jmbg="+jmbg ,
            {
                method: "GET"
            })
            .then(p => {
                if (p.ok)
                {
                    p.json().then(msg =>
                    {
                        if (msg == 0)
                        {
                            alert("Korisnik do sada nije iznajmljivao vozila!");
                            return;
                        }
                        msg.forEach(t=>{ 

                            let podaciVozila = document.createElement("div");
                            podaciVozila.className = "podaciVozila";  
                            divIspis.appendChild(podaciVozila);

                            let elLabela = document.createElement("label");
                            elLabela.innerHTML="Marka: " + t.marka;
                            elLabela.className="labelaPodaciVozila"
                            podaciVozila.appendChild(elLabela);

                            elLabela = document.createElement("label");
                            elLabela.innerHTML="Model: " + t.model;
                            elLabela.className="labelaPodaciVozila"
                            podaciVozila.appendChild(elLabela);

                            elLabela = document.createElement("label");
                            elLabela.innerHTML="Tablice: " + t.tablice;
                            elLabela.className="labelaPodaciVozila"
                            podaciVozila.appendChild(elLabela);

                            elLabela = document.createElement("label");
                            elLabela.innerHTML="Cena: " + t.cena;
                            elLabela.className="labelaPodaciVozila"
                            podaciVozila.appendChild(elLabela);

                            elLabela = document.createElement("label");
                            elLabela.innerHTML="Ocena: " + t.ocena;
                            elLabela.className="labelaPodaciVozila"
                            podaciVozila.appendChild(elLabela);
                        })
                    })
                }
                else
                {
                    p.json().then(msg => alert(msg.poruka))
                }  
            })
        }
    }

    crtajIznajmiVozilo(host)  
    {
        let osnovniDiv = document.createElement("div");
        osnovniDiv.className = "osnovni"; 
        host.appendChild(osnovniDiv);

        let podaciDiv = document.createElement("div");
        podaciDiv.className = "podaciDiv";  
        osnovniDiv.appendChild(podaciDiv);

        let iznajmiDiv = document.createElement("div");
        iznajmiDiv.className = "divZaIznajmljivanje";  
        podaciDiv.appendChild(iznajmiDiv);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Iznajmi vozilo ";
        iznajmiDiv.appendChild(elLabela);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*JMBG korisnika";
        elLabela.className="labela1"; 
        iznajmiDiv.appendChild(elLabela);

        let tb = document.createElement("input");
        tb.className="jmbg";
        iznajmiDiv.appendChild(tb);
        
        let sel= document.createElement("select");
        sel.className="select naziv"
        let l = document.createElement("label");
        l.innerHTML="Naziv firme"
        l.className="labela1"; 
        this.listaFirmi.forEach(p=>{
            console.log("naziv");
            console.log(p);
        })
        let op;
        console.log(this.listaFirmi);
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op=document.createElement("option");
            op.innerHTML=p;
            sel.appendChild(op);
        });
        iznajmiDiv.appendChild(l);
        iznajmiDiv.appendChild(sel);
        let naziv = document.querySelector('.naziv').value;
        console.log(naziv);

        let sel1= document.createElement("select");
        sel1.className="select makraModel"
        let l1 = document.createElement("label");
        l1.innerHTML="Izaberi marku i model vozila"
        l1.className="labela1"; 
        sel1.value="";
        
        sel.onclick=()=>{
            sel1.innerHTML="";
            naziv = document.querySelector('.naziv').value;
            let op1;
            fetch("https://localhost:5001/Vozilo/PreuzmiVozilaMM?nazivFirme="+ naziv,
            {
                method:"GET"
            }).then(p=>{
                p.json().then(vozila=>{
                    vozila.forEach(p => {
                            op1=document.createElement("option");
                            op1.innerHTML=p.markaModel;
                            op1.value=p.markaModel;
                            sel1.appendChild(op1);
                        });
                    })
              })
            
        }
        iznajmiDiv.appendChild(l1);
        iznajmiDiv.appendChild(sel1);
        
        const dugme = document.createElement("button");
        dugme.className="dugmeIznajmi";
        dugme.innerHTML="Iznajmi vozilo";
        iznajmiDiv.appendChild(dugme);
        dugme.onclick=()=>
        {
            let jmbg = document.querySelector('.jmbg').value;
            console.log(jmbg);
            let makraModel = document.querySelector('.makraModel').value;
            this.iznajmiVozilo(document.querySelector('.naziv').value, jmbg, makraModel);
        }

        //za vracanje
        let voziloDiv = document.createElement("div");
        voziloDiv.className = "divZaVozilo";  
        podaciDiv.appendChild(voziloDiv);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Vrati vozilo ";
        voziloDiv.appendChild(elLabela);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*JMBG korisnika";
        elLabela.className="labela1"; 
        voziloDiv.appendChild(elLabela);

        var tb1 = document.createElement("input");
        tb1.className="jmbg jmbg2";
        voziloDiv.appendChild(tb1);
        
        var sel2= document.createElement("select");
        sel2.className="select naziv2";
        var l2 = document.createElement("label");
        l2.innerHTML="Naziv firme";
        l2.className="labela1";
        var op2; 
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op2=document.createElement("option");
            op2.innerHTML=p;
            sel2.appendChild(op2);
        });
        voziloDiv.appendChild(l2);
        voziloDiv.appendChild(sel2);
        let naziv2 = document.querySelector('.naziv2').value;

        let sel3= document.createElement("select");
        sel3.className="select vozilox"
        let l3 = document.createElement("label");
        l3.innerHTML="Tablice"
        l3.className="labela1"; 
        sel3.value="";
        sel2.onclick=()=>{
            sel3.innerHTML="";
            naziv2 = document.querySelector('.naziv2 option:checked').value;
            let op3;
            fetch("https://localhost:5001/Vozilo/PreuzmiVoziloIzFirme?nazivFirme=" + naziv2,
            {
                method: "GET"
            })
            .then(p=>{
                p.json().then(vozila=>{
                    vozila.forEach(k => {
                        op3=document.createElement("option");
                        op3.innerHTML=k.tablice;
                        op3.value=k.tablice;
                        sel3.appendChild(op3);
                    })
                })
            })

        }
        voziloDiv.appendChild(l3);
        voziloDiv.appendChild(sel3);

        let lab = document.createElement("label");
        lab.innerHTML = "Molimo Vas ocenite uslugu!";
        lab.className="labela1"; 
        voziloDiv.appendChild(lab);

        let redOcena = document.createElement("div");  
        redOcena.className = "redOcena";
        voziloDiv.appendChild(redOcena);
        let radio;

        let labela = document.createElement("label");
        labela.innerHTML = "Ocena: ";
        labela.className = "ocena";
        redOcena.appendChild(labela);

        for (let i = 1; i <= 5; i++)
        {
            radio = document.createElement("input");
            radio.type = "radio";
            radio.name = "name";
            radio.value = i;

            redOcena.appendChild(radio);
            let labela1 = document.createElement("label");
            labela1.innerHTML = i;
            labela1.className="broj";
            redOcena.appendChild(labela1);
        }

        const dugme1 = document.createElement("button");
        dugme1.className="dugmeVrati";
        dugme1.innerHTML="Vrati vozilo";
        voziloDiv.appendChild(dugme1);
        dugme1.onclick=()=>
        {
            let jmbg2 = document.querySelector('.jmbg2').value;
            console.log(jmbg2);
            if (jmbg2 === "")
            {
                alert("Morate uneti JMBG korisnika!");
                return;
            }
            naziv2 = document.querySelector('.naziv2').value;
            if (naziv2 === "")
            {
                alert("Morate izabrati firmu!");
                return;
            }
            let tablice=document.querySelector(".vozilox").value;
            console.log(tablice);
            if (tablice === "")
            {
                alert("Morate uneti tablice vozila!");
                return;
            }

            let ocena = document.querySelector("input[type='radio']:checked");
            if (ocena == null || ocena==undefined)
            {
                alert("Morate uneti ocenu!");
                return;
            }
            
            this.vratiIznajmljenoVozilo(naziv2, jmbg2, tablice , ocena.value);
        }

        // za parking
        let ObeSlikeDiv = document.createElement("div");
        ObeSlikeDiv.className = "ObeSlikeDiv";  
        osnovniDiv.appendChild(ObeSlikeDiv);

        this.listaFirmi.forEach(p=>{

            //debugger;
            let slikaDiv = document.createElement("div");
            slikaDiv.className = "slikaDiv " + p;
            ObeSlikeDiv.appendChild(slikaDiv);

            let divPrviRed  = document.createElement("div");
            divPrviRed.className = "Red prviRed" + p;
            slikaDiv.appendChild(divPrviRed);

            let divPut  = document.createElement("div");
            divPut.className = "Put put" + p;
            slikaDiv.appendChild(divPut);

            var labelaFirma = document.createElement("labela");
            labelaFirma.innerHTML="Parking firme "+ p ;
            labelaFirma.className="labelaFirma";
            divPut.appendChild(labelaFirma);

            let divDrugiRed  = document.createElement("div");
            divDrugiRed.className = "Red drugiRed" + p;
            slikaDiv.appendChild(divDrugiRed);
            
            console.log(p); 

            this.crtajParking(p);
            this.crtajVozilo(p);
        })

        //za dodaj i obrisi
        let podaciDiv2 = document.createElement("div");
        podaciDiv2.className = "podaciDiv2";  
        osnovniDiv.appendChild(podaciDiv2);
        this.crtajDodajVozilo(podaciDiv2);
        this.crtajObrisi(podaciDiv2);
    }

    crtajSliku()
    {
        this.listaFirmi.forEach(p=>{

            //debugger;
            var form=document.querySelector(".prviRed"+p);
            var roditelj=form.parentElement;
            roditelj.removeChild(form);
            console.log(roditelj);
            let divPrviRed = document.createElement("div");
            divPrviRed.className = "Red prviRed" + p;
            roditelj.appendChild(divPrviRed);

            var form=document.querySelector(".put"+p);
            var roditelj=form.parentElement;
            roditelj.removeChild(form);
            let divPut  = document.createElement("div");
            divPut.className = "Put put" + p;
            roditelj.appendChild(divPut);

            var labelaFirma = document.createElement("labela");
            labelaFirma.innerHTML="Parking firme "+ p;
            labelaFirma.className="labelaFirma";
            divPut.appendChild(labelaFirma);

            var form=document.querySelector(".drugiRed"+p);
            var roditelj=form.parentElement;
            roditelj.removeChild(form);
            console.log(roditelj);
            let divDrugiRed = document.createElement("div");
            divDrugiRed.className = "Red drugiRed" + p;
            roditelj.appendChild(divDrugiRed);
            
            console.log(p); 

            this.crtajParking(p);
            this.crtajVozilo(p);

        })
    }

    crtajParking(p)
    {
        var host1=document.querySelector(".prviRed"+p);
        var host2=document.querySelector(".drugiRed"+p);
        for(var i=0; i<7; i++)
        {
            let parking1 = document.createElement("div");
            parking1.className = "parking gore" + i + p;  
            host1.appendChild(parking1);
        }
        for(var i=0; i<7; i++)
        {
            let parking2 = document.createElement("div");
            parking2.className = "parking dole" + i + p;  
            host2.appendChild(parking2);
        }
    }

    crtajVozilo(naziv)
    {
        console.log(this.kontejner);
        var i=0;
        var j=0;
        let str="https://localhost:5001/Vozilo/PreuzmiVoziloIzFirme?nazivFirme="+naziv;
            fetch(str,
            {
                method:"GET"
            }).then(p=>{
                p.json().then(vozila=>{
                    console.log(vozila);
                    vozila.forEach(p => {

                       let vozilo = document.createElement("div");
                       //debugger;
                       if(p.iznajmnjeno==true)
                       {
                           vozilo.className = "zauzeto vozilo " + p.tablice; 
                       }
                       else 
                            vozilo.className = "slobodno vozilo " + p.tablice;
                       
                        var elLabela = document.createElement("label");
                        elLabela.innerHTML="Marka: "+ p.marka;
                        vozilo.appendChild(elLabela);
                        elLabela = document.createElement("label");
                        elLabela.innerHTML="Model: "+ p.model;
                        vozilo.appendChild(elLabela);
                        elLabela = document.createElement("label");
                        elLabela.innerHTML="Tablice: "+ p.tablice;
                        vozilo.appendChild(elLabela);
                        elLabela = document.createElement("label");
                        elLabela.innerHTML="Cena: "+ p.cena;
                        vozilo.appendChild(elLabela);
                        elLabela = document.createElement("label");
                        elLabela.innerHTML="Ocena: "+ p.ocena;
                        vozilo.appendChild(elLabela);
                        if(i<7)
                        {
                            //debugger;
                            let gorei = document.querySelector(".gore"+ i + naziv);
                            gorei.appendChild(vozilo);
                            i++;                       
                        }
                        else if(j<7)
                        {
                            let dolej = document.querySelector('.dole' + j + naziv);
                            //debugger;
                            dolej.appendChild(vozilo);
                            j++;
                        }
                        });
                    })
              })
    }

    crtajDodajVozilo(host)
    {
        let dodajDiv = document.createElement("div");
        dodajDiv.className = "dodajDiv";  
        this.kontejner=host;
        host.appendChild(dodajDiv);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Dodaj vozilo ";
        dodajDiv.appendChild(elLabela);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Marka:";
        elLabela.className="labela1"; 
        dodajDiv.appendChild(elLabela);
        let tb = document.createElement("input");
        tb.className="marka";
        dodajDiv.appendChild(tb);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Model:";
        elLabela.className="labela1"; 
        dodajDiv.appendChild(elLabela);
        tb = document.createElement("input");
        tb.className="model";
        dodajDiv.appendChild(tb);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Tablice:";
        elLabela.className="labela1"; 
        dodajDiv.appendChild(elLabela);
        tb = document.createElement("input");
        tb.className="tablice tabl";
        dodajDiv.appendChild(tb);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Cena:";
        elLabela.className="labela1"; 
        dodajDiv.appendChild(elLabela);
        tb = document.createElement("input");
        tb.className="cena";
        dodajDiv.appendChild(tb);
        
        let sel= document.createElement("select");
        sel.className="select naziv1"
        let l = document.createElement("label");
        l.innerHTML="Naziv firme"
        l.className="labela1"; 
        let op;
        console.log(this.listaFirmi);
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op=document.createElement("option");
            op.innerHTML=p;
            sel.appendChild(op);
        });
        dodajDiv.appendChild(l);
        dodajDiv.appendChild(sel);
        let naziv1 = this.kontejner.querySelector('.naziv1').value;
        console.log(naziv1);

        const dugme = document.createElement("button");
        dugme.className="dugmeDodaj";
        dugme.innerHTML="Dodaj vozilo";
        dodajDiv.appendChild(dugme);
        dugme.onclick=()=>
        {
            
            let marka = this.kontejner.querySelector('.marka').value;
            console.log(marka);
            let model = this.kontejner.querySelector('.model').value;
            console.log(model);
            let tablice = this.kontejner.querySelector('.tabl').value;
            console.log(tablice);
            let cena = this.kontejner.querySelector('.cena').value;
            console.log(cena);
            console.log(this.kontejner.querySelector('.naziv').value);
            this.dodajVozilo(this.kontejner.querySelector('.naziv1').value, marka, model, tablice, cena);
        }
    }

    crtajObrisi(host)
    {
        let obrisiDiv = document.createElement("div");
        obrisiDiv.className = "obrisiDiv";  
        this.kontejner=host;
        host.appendChild(obrisiDiv);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Izbrisi vozilo ";
        obrisiDiv.appendChild(elLabela);

        let sel4= document.createElement("select");
        sel4.className="select naziv4"
        let l4 = document.createElement("label");
        l4.innerHTML="Naziv firme"
        l4.className="labela1"; 
        let op4;
        console.log(this.listaFirmi);
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op4=document.createElement("option");
            op4.innerHTML=p;
            sel4.appendChild(op4);
        });
        obrisiDiv.appendChild(l4);
        obrisiDiv.appendChild(sel4);

        let sel5= document.createElement("select");
        sel5.className="select voz"
        let l5 = document.createElement("label");
        l5.innerHTML="Izaberite vozilo"
        l5.className="labela1"; 
        sel5.value="";
        sel4.onclick=()=>{
            sel5.innerHTML="";
            let naziv4 = this.kontejner.querySelector('.naziv4 option:checked').value;
            let op5;
            fetch("https://localhost:5001/Vozilo/PreuzmiVoziloIzFirme?nazivFirme=" + naziv4,
            {
                method: "GET"
            })
            .then(p=>{
                p.json().then(vozila=>{
                    vozila.forEach(k => {
                        op5=document.createElement("option");
                        op5.innerHTML=k.tablice;
                        op5.value=k.tablice;
                        sel5.appendChild(op5);
                    })
                })
            })

        }
        obrisiDiv.appendChild(l5);
        obrisiDiv.appendChild(sel5);

        const dugme = document.createElement("button");
        dugme.className="dugmeObrisi";
        dugme.innerHTML="Obrisi vozilo";
        obrisiDiv.appendChild(dugme);
        dugme.onclick=()=>
        {
            let tablice = this.kontejner.querySelector('.voz option:checked').value;
            let naziv4 = this.kontejner.querySelector('.naziv4 option:checked').value;
            console.log(tablice);
            console.log(naziv4);
            this.obrisiVozilo(naziv4, tablice);
        }
        
    }
    crtajRegistracijuKorisnika(host)  
    {
        const osnova = document.createElement("div");
        osnova.className="osnovniDiv";
        this.kontejner=host;
        host.appendChild(osnova);

        const kontForma = document.createElement("div");
        kontForma.className="kontRegKorisnika";
        osnova.appendChild(kontForma);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML="Registracija korisnika ";
        kontForma.appendChild(elLabela);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Ime korisnika";
        elLabela.className="labela1";
        kontForma.appendChild(elLabela);

        let tb= document.createElement("input");
        tb.className="ime"; 
        kontForma.appendChild(tb);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Prezime korisnika";
        elLabela.className="labela1";
        kontForma.appendChild(elLabela);

        tb= document.createElement("input");
        tb.className="prezime"; 
        kontForma.appendChild(tb);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*JMBG";
        elLabela.className="labela1";
        kontForma.appendChild(elLabela);

        tb = document.createElement("input");
        tb.className="jmbg";
        kontForma.appendChild(tb);

        elLabela = document.createElement("label");
        elLabela.innerHTML="*Telefon";
        elLabela.className="labela1";
        kontForma.appendChild(elLabela);

        tb = document.createElement("input");
        tb.className="telefon";
        kontForma.appendChild(tb);

        let sel= document.createElement("select");
        sel.className="select nazivFirme"
        let l = document.createElement("label");
        l.innerHTML="Naziv firme";
        l.className="labela1";
        

        let op;
        console.log(this.listaFirmi);
        this.listaFirmi.forEach((p) => {
            console.log("Naziv");
            console.log(p);
            op=document.createElement("option");
            op.innerHTML=p;
            sel.appendChild(op);
        });
        kontForma.appendChild(l);
        kontForma.appendChild(sel);
        sel.value="";

        const dugme = document.createElement("button");
        dugme.className="dugme";
        dugme.innerHTML="Registruj novog korisnika";
        kontForma.appendChild(dugme);
        dugme.onclick=()=>
        {
            let ime = this.kontejner.querySelector('.ime').value;
            console.log(ime);
            let prezime=this.kontejner.querySelector(".prezime").value;
            console.log(prezime);
            let jmbg=this.kontejner.querySelector(".jmbg").value;
            console.log(jmbg);
            let telefon=this.kontejner.querySelector(".telefon").value;
            console.log(telefon);
            let nazivFirme = this.kontejner.querySelector('.nazivFirme').value;

            this.dodajKorisnika(nazivFirme, ime,prezime,jmbg,telefon);
        }
    }

    dodajVozilo(nazivFirme, marka, model, tablice, cena)
    {
        if (marka === "")
        {
            alert("Morate uneti marku!");
            return;
        }
        if (model === "")
        {
            alert("Morate uneti model!");
            return;
        }
        if (tablice === "")
        {
            alert("Morate uneti tablice!");
            return;
        }
        if (tablice.length > 7)
        {
            alert("Broj tablice nije validan!");
            return;
        }
        if (cena === "")
        {
            alert("Morate uneti cenu!");
            return;
        }

        fetch( "https://localhost:5001/Vozilo/DodajVoziloUFirmu?nazivFirme="+ nazivFirme + "&markaVozila=" + marka + "&modelVozila=" + model+ "&tabliceVozila=" + tablice + "&cenaVozila=" + cena,
        {
            method: "POST"
        })
        .then(p => {
            if (p.ok)
            {
                p.json().then(msg =>
                {
                    this.crtajSliku();
                })
            }
            else
            {
                p.json().then(msg => alert(msg.poruka))
            }  
        })
    }

    obrisiVozilo(nazivFirme, tablice)
    {

        if (nazivFirme === "")
        {
            alert("Morate izabrati firmu!");
            return;
        }
        if (tablice === "")
        {
            alert("Morate izabrati vozilo!");
            return;
        }

        fetch( "https://localhost:5001/Vozilo/UkloniVoziloIzFirme?nazivFirme="+ nazivFirme +  "&tabliceVozila=" + tablice,
        {
            method: "DELETE"
        })
        .then(p => {
            if (p.ok)
            {
                p.json().then(msg =>
                {
                    this.crtajSliku();
                })
            }
            else
            {
                p.json().then(msg => alert(msg.poruka))
            }  
        })
    }
    dodajKorisnika(nazivFirme,ime, prezime, JMBG, telefon)
    {

            if (ime === "")
            {
                alert("Morate uneti ime!");
                return;
            }
            if (prezime === "")
            {
                alert("Morate uneti prezime!");
                return;
            }
            if (JMBG === "")
            {
                alert("Morate uneti JMBG!");
                return;
            }
            if (JMBG.length != 13)
            {
                alert("JMBG mora imati 13 cifara!");
                return;
            }
            if (telefon === "")
            {
                alert("Morate uneti telefon!");
                return;
            }
            if (telefon.length > 11)
            {
                alert("Telefon korisnika ne moze imati vise od 11 cifara!");
                return;
            }

            fetch( "https://localhost:5001/Korisnik/DodajKorisnika?nazivFirme="+nazivFirme + "&ime=" + ime + "&prezime=" + prezime+ "&JMBG=" + JMBG + "&telefon=" + telefon,
            {
                method: "POST"
            })
            .then(p => {
                if (p.ok)
                {
                    p.json().then(msg =>
                    {
                        alert(msg.poruka);
                    })
                }
                else
                {
                    p.json().then(msg => alert(msg.poruka))
                }  
            })
        
    } 

    iznajmiVozilo(naziv, korisnik, vozilo)  // vozilo su zapravo tablice, korisnik je jmbg
    {
        if (korisnik === "")
        {
            alert("Morate uneti JMBG korisnika");
            return;
        }

        if (vozilo === "")
        {
            alert("Morate izabrati vozilo!");
            return;
        }

        fetch("https://localhost:5001/Iznajmljivanje/IznajmiVozilo?nazivFirme=" + naziv + "&jmbgKorisnika=" + korisnik + "&nazivVozila=" + vozilo,
        {
            method: "POST"
        }).then(p =>{
                if (p.ok)
                {
                    p.json().then(msg =>
                    {
                        let tablice = document.querySelector("."+ msg[0].tablice);
                        console.log(msg[0].tablice);
                        console.log(tablice);
                        tablice.className="zauzeto vozilo " + msg[0].tablice;
                        alert("Uspesno ste iznajmili vozilo!");
                    })
                }
                else
                {
                    p.json().then(msg => alert(msg.poruka))
                }
            })
    }

    vratiIznajmljenoVozilo(naziv, korisnik, vozilo, ocena)  // vozilo su tablice
    {
        fetch("https://localhost:5001/Iznajmljivanje/VratiVozilo?nazivFirme=" + naziv + "&jmbgKorisnika=" + korisnik + "&tabliceVozila=" + vozilo + "&ocenaKorisnika=" + ocena,
        {
            method: "PUT"
        }).then(p =>{
            if (p.ok)
            {
                p.json().then(k => {

                    let tablice = document.querySelector("."+ k[0].tablice);
                    console.log(tablice);
                    tablice.className="slobodno vozilo " + k[0].tablice;
                    alert("Uspesno ste vratili vozilo!");
                    this.crtajSliku();
                    
                })
            }
            else
            {
                p.json().then(k => alert(k.poruka))
            }
        })
    }

}  
