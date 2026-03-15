using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Obligatorisk_oppgave_1;

/// <summary>
/// AI brukt for Feilsøking under koding
/// koden er planlagt og skrevet i forhold til Oppgaveteksten.
/// 
/// Tillegg: Jeg simplifiserte koden i forhold til ID ettersom jeg prøvde å få det til å fungere på en annen måte først.
/// Forsøke bestod av å bruke kode som skulle legge til S for student før ID, (og L for lærer) dette endte opp med å ikke fungere som jeg ønsket, og jeg endte opp med å bruke int for ID, og heller legge til 1000 for lærere, og 2000 for studenter. 
/// Dette fungerte bedre, og var enklere å implementere i forhold til å finne brukere basert på ID senere i koden.
/// </summary>

public class Lexicanum
{
    public List<Kurs> AlleKurs = new List<Kurs>();
    public List<Bruker> AlleBrukere = new List<Bruker>();
    public List<Bok> AlleBøker = new List<Bok>();
    public List<Lærer> AlleLærere = new List<Lærer>();
    public List<Student> AlleStudenter = new List<Student>();

    public Lexicanum()
    {
        // Kurs - (int kode, string navn, int poeng, int maksStudenter)
        RegistrerKurs(300, "Ancient History", 5, 30);
        RegistrerKurs(400, "Abominable Intelligence History", 10, 45);

    
        // Lærer - (int id, string navn, string epost, string stilling, string avdeling)
        RegistrerLærer(1001, "Dr. Magnus Aurelius", "Aurelius4Evar@Teach.com", "Foreleser", "Foreleser");
        AlleBrukere.Add(AlleLærere.Last());
        RegistrerLærer(1002, "Professor Octavia Voss", "Voss@Teach.com", "Bibliotekar", "Bibliotek");
        AlleBrukere.Add(AlleLærere.Last());
        RegistrerLærer(1003, "Dr. Lucius Blackwood", "LuciBlack@Teach.com", "Fagansvarlig", "Administrasjon");
        AlleBrukere.Add(AlleLærere.Last());


        // Studenter - (int id, string navn, string epost)
        RegistrerStudent(2001, "Acolyte Lyra", "Lyr@Stud.Teach.com");
        AlleBrukere.Add(AlleStudenter.Last());
        RegistrerStudent(2002, "Acolyte Orion", "Orion@Stud.Teach.com");
        AlleBrukere.Add(AlleStudenter.Last());
        RegistrerStudent(2003, "Acolyte Selene", "Selen@Stud.Teach.Com");
        AlleBrukere.Add(AlleStudenter.Last());

        // Utvekslingsstudent - (int id, string navn, string epost, string hjemUni, string land, string periode)
        RegistrerUtvekslingsStudent(3001, "Acolyte Nova", "Nova@stud.teach.com", "University of Eldoria", "Eldoria", "2024-2025");
        AlleBrukere.Add(AlleStudenter.Last());


        //Bøker - (string tittel, string forfatter, string utgivelsesår, int isbn, int antallEksemplarer)
        RegistrerBok("The History of Abominable Intelligence.", "Aurelius M", "12.08.90", 129834, 1);
        RegistrerBok("Ancient Civilizations: A Comprehensive Guide.", "Dr. Magnus Aurelius", "05.03.85", 987654, 4);
        RegistrerBok("The Rise and Fall of the Roman Empire.", "Aurelius M", "22.11.88", 123456, 5);
        RegistrerBok("Medieval Europe: A Historical Overview.", "Dr. Magnus Aurelius", "15.07.92", 654321, 3);
    }

    //Registreringsfunksjoner for å legge til data i listene. Brukes i konstruktøren, og kan også brukes senere for å legge til mer data.
    public void RegistrerKurs(int kode, string navn, int poeng, int maks)
    {
        Kurs nyttKurs = new Kurs(kode, navn, poeng, maks);
        AlleKurs.Add(nyttKurs);
    }

    public void RegistrerLærer(int id, string navn, string epost, string stilling, string avdeling)
    {
        Lærer nyLærer = new Lærer(id, navn, epost, stilling, avdeling);
        AlleLærere.Add(nyLærer);
        AlleBrukere.Add(nyLærer);
    }

    public void RegistrerStudent(int id, string navn, string epost)
    {
        Student nyStudent = new Student(id, navn, epost);
        AlleStudenter.Add(nyStudent);
        AlleBrukere.Add(nyStudent);
    }

    public void RegistrerBok(string tittel, string forfatter, string utgivelsesår, int isbn, int antallEksemplarer)
    {
        Bok nyBok = new Bok(tittel, forfatter, utgivelsesår, isbn, antallEksemplarer);
        AlleBøker.Add(nyBok);
    }

    public void RegistrerUtvekslingsStudent(int id, string navn, string epost, string hjemUni, string land, string periode)
    {
        Utvekslingsstudent nyUtvekslingsstudent = new Utvekslingsstudent(id, navn, epost, hjemUni, land, periode);
        AlleStudenter.Add(nyUtvekslingsstudent);
        AlleBrukere.Add(nyUtvekslingsstudent);
    }
    //-------------------------Slutt av lister og liste-funksjoner. Funksjoner for menyen i Program.cs følger under------------------------------//

    
    //Brukes av case "2"
    public void TildelKurs(int studentId, int kursKode) //Tildeler kurs til studenter 
    {
        var student = AlleStudenter.FirstOrDefault(s => s.Id == studentId);
        var kurs = AlleKurs.FirstOrDefault(k => k.Kode == kursKode);

        if (student != null && kurs != null) 
        {
            
            int antallPåmeldte = AlleStudenter.Count(s => s.Kursliste.Contains(kurs)); 
            if (antallPåmeldte >= kurs.MaksStudenter)
            {
                Console.WriteLine($">>> ERROR: COURSE {kurs.Navn} IS FULL ({kurs.MaksStudenter}/{kurs.MaksStudenter})");
                return;
            }

            student.Kursliste.Add(kurs);
            Console.WriteLine($">>> {student.Navn} assigned to {kurs.Navn}."); //viser hvilke student er tildelt hvilket kurs
        }
    }
    // Brukes av case "3"
    public string VisKurs() // Viser alle kurs i systemet
    {
        string output = "";
        foreach (var kurs in AlleKurs) output += kurs.Navn + "\n";
        return output;
    }

    public string VisstudentListe() // Viser alle studenter i systemet
    {
        string output = "";
        foreach (var s in AlleStudenter) output += s.Navn + " (" + s.Id + ")\n";
        return output;
    }

    public void VisTildelinger() // Viser hvilke kurs hver student er tildelt
    {
        foreach (var s in AlleStudenter)
        {
            Console.WriteLine($"{s.Navn} ASSIGNED TO:"); // Viser studentens navn og "ASSIGNED TO:" for å indikere at kursene som følger er de student er tildelt
            foreach (var k in s.Kursliste) Console.WriteLine("- " + k.Navn); // Viser kursene hver student er tildelt, formatert med en bindestrek foran for å gjøre det mer lesbart
        }
    }

    // Brukes av case "4"
    public string VisKursInfo(int kode)
    {
        var kurs = AlleKurs.FirstOrDefault(k => k.Kode == kode);
        return kurs != null ? $"{kurs.Navn} ({kurs.Poeng} poeng)" : ">COULD NOT FIND COURSE<";
    }

    public string SearchBøker(string query) //ble skiftet til query for å unngå forvirring med tittel og forfatter, og for å gjøre det mer generelt for søk i både tittel og forfatter. 
    {
        // LINQ for å finne alle bøker med tittel eller forfatter 
        var treff = AlleBøker
            .Where(b => b.Tittel.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        b.Forfatter.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!treff.Any()) 
            return ">>> NO DATA FOUND."; // Hvis ingen treff, returner melding

        return string.Join("\n", treff.Select(b => $"- {b.Tittel} ({b.Forfatter}) | Copies: {b.AntallEksemplarer}")); // Returnerer en formatert liste over treff, inkludert antall eksemplarer tilgjengelig
    }
    // Brukes av case "6"
    public void LånBok(int id, string tittel)
    {
        var bruker = AlleBrukere.FirstOrDefault(b => b.Id == id); // Finner bruker basert på ID
        var bok = AlleBøker.FirstOrDefault(b => b.Tittel.Equals(tittel, StringComparison.OrdinalIgnoreCase)); // Finner bok basert på tittel.

        if (bruker == null)
        {
            Console.WriteLine(">>> ERROR: ACOLYTE NOT FOUND.");
        }

        else if (bok == null)
        {
            Console.WriteLine(">>> ERROR: DATA SLATE NOT FOUND.");
        }
        // sjekker om det er eksemplarer igjen av boken.
        else if (bok.AntallEksemplarer <= 0)
        {
            Console.WriteLine($">>> ERROR: NO COPIES OF '{bok.Tittel}' AVAILABLE IN THE ARCHIVES."); // Hvis ingen eksemplarer igjen, returner melding
        }

        else
        {
        
            bok.AntallEksemplarer--;
            
            if (bok.AntallEksemplarer == 0) bok.ErUtlånt = true; // Oppdaterer status til utlånt hvis det ikke er flere eksemplarer igjen

            Console.WriteLine($">>> SUCCESS: '{bok.Tittel}' DISTRIBUTED TO {bruker.Navn}."); // Viser at boken er lånt ut til brukeren
            Console.WriteLine($">>> COPIES REMAINING: {bok.AntallEksemplarer}"); 
        }
    }
    // Brukes av case "7"
    public void ReturnerBok(int id, string tittel) // Finner bruker og bok basert på ID og tittel, og returnerer boken hvis den er utlånt.
    {
        var bruker = AlleBrukere.FirstOrDefault(b => b.Id == id);
        var bok = AlleBøker.FirstOrDefault(b => b.Tittel.Equals(tittel, StringComparison.OrdinalIgnoreCase));

        if (bruker != null && bok != null && bok.ErUtlånt) // Sjekker at både bruker og bok finnes, og at boken er utlånt før den kan returneres
        {
            //Øk antall eksemplarer tilbake til lageret når boken returneres
            bok.AntallEksemplarer++;

            //Oppdaterer status til ikke utlånt hvis det nå er eksemplarer tilgjengelig igjen
            bok.ErUtlånt = false;

            Console.WriteLine($">>> SUCCESS: '{bok.Tittel}' returned by {bruker.Navn}.");
            Console.WriteLine($">>> COPIES NOW IN ARCHIVE: {bok.AntallEksemplarer}");
        }
        else
        {
            Console.WriteLine(">>> ERROR: Could not return book. Check ID or Title.");
        }
    }
}

