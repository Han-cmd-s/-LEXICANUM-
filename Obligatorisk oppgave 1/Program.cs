// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Obligatorisk_oppgave_1;

/// <summary>
/// v0.1
/// AI brukt for Feilsøking under koding samt anbefallinger og informasjons søking om anbefalinger.
/// koden er planlagt og skrevet i forhold til Oppgaveteksten.
/// 
/// Tillegg: Jeg simplifiserte koden i forhold til ID ettersom jeg prøvde å få det til å fungere på en annen måte først.
/// Forsøke bestod av å bruke kode som skulle legge til S for student før ID, (og L for lærer) dette endte opp med å ikke fungere som jeg ønsket, og jeg endte opp med å bruke int for ID, og heller legge til 1000 for lærere, og 2000 for studenter. 
/// Dette fungerte bedre, og var enklere å implementere i forhold til å finne brukere basert på ID senere i koden.
/// 
/// v0.2
/// Implementert innloggings system
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green; // Setter tekstfargen i konsollen til grønn for å gi en mer "terminal-lignende" følelse, og for å skille ut teksten fra standard svart-hvitt og for 
        Console.WriteLine(">>> INITIALIZING LEXICANUM DATABASE ACCESS...");
        Lexicanum system = new Lexicanum();
        Bruker innloggetbruker = null; // Variabel for å holde styr på den innloggede brukeren, og for å implementere en enkel form for tilgangskontroll basert på brukerroller

        Console.WriteLine(">>>INITIALIZING LOGIN REQUIERED<<<");

        while (innloggetbruker == null)
        {
            Console.Clear();
            Console.WriteLine("[1] LOGIN<");
            Console.WriteLine("[2] REGISTER INTO THE SYSTEM<");
            string loginValg = Console.ReadLine();

            if (loginValg == "1")
            {
                Console.WriteLine(">>> ENTER IDENTITY (Name):");
                string navn = Console.ReadLine();
                Console.WriteLine(">>> ENTER ACCESS KEY (Passord):");
                string passord = Console.ReadLine();

                innloggetbruker = system.AlleBrukere.FirstOrDefault(b => b.Navn.Equals(navn, StringComparison.OrdinalIgnoreCase) && b.Passord == passord); // Sjekker om det finnes en bruker i systemet som matcher det oppgitte navnet og passordet, og hvis det gjør det, settes innloggetbruker til den brukeren
                if (innloggetbruker != null)
                {
                    Console.WriteLine($">>> WELCOME, {innloggetbruker.Navn}! ACCESS GRANTED.");
                    Console.WriteLine($">>> YOUR ROLE: {innloggetbruker.Rolle.ToUpper()}");
                    Console.WriteLine(">>> PRESS ENTER TO PROCEED TO TERMINAL...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(">>> LOGIN FAILED. INVALID NAME OR PASSWORD. PLEASE TRY AGAIN.");
                    Console.ReadKey();
                }
            }
            else if (loginValg == "2")
            {
                string typeValg = ""; 

                while (typeValg != "1" && typeValg != "2")
                {
                    Console.WriteLine(">>> SELECT ROLE TO REGISTER: [1] Acolyte (Student) [2] Adept (Ansatt)");
                    typeValg = Console.ReadLine(); 
                }

                Console.WriteLine(">>> ENTER NAME:");
                string nyttNavn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nyttNavn))
                {
                    Console.WriteLine(">>> ERROR: Name cannot be empty. Please try again.");
                    continue;
                }
                Console.WriteLine(">>> ENTER COMM-LINK (E-mail):");
                string nyEpost = Console.ReadLine();
                Console.WriteLine(">>> ENTER ACCESS KEY");
                string nyttPassord = Console.ReadLine();

                if (typeValg == "2")
                {
                    Console.WriteLine(">>> ENTER DESIGNATION (stilling):"); //kan per nå skrive hva som helst default "faglærer"

                    string stilling = Console.ReadLine();
                    Console.WriteLine(">>> ENTER DEPARTMENT (Avdeling):");

                    string avdeling = Console.ReadLine();
                    
                    int nyID = system.AlleLærere.Max(l => l.Id) + 1; // Genererer en ny ID for den ansatte ved å finne den høyeste eksisterende ID i listen over lærere og legge til 1
                    system.RegistrerAnsatt(nyID, nyttNavn, nyEpost, nyttPassord, stilling, avdeling); // Kaller RegistrerAnsatt-metoden i Lexicanum-klassen for å legge til den nye ansatte i systemet
                }
                else if (typeValg == "1")
                {
                    int nyID = system.AlleStudenter.Max(s => s.Id) + 1; // Genererer en ny ID for studenten ved å finne den høyeste eksisterende ID i listen over studenter og legge til 1
                    system.RegistrerStudent(nyID, nyttNavn, nyEpost, nyttPassord); // Kaller RegistrerStudent-metoden i Lexicanum-klassen for å legge til den nye studenten i systemet
                }
                else
                {
                    Console.WriteLine(">>> INVALID ROLE SELECTED. PLEASE SELECT [1] OR [2].");
                    continue;
                }
                Console.WriteLine(">>> REGISTRATION SUCCESSFUL. YOU CAN NOW LOGIN WITH YOUR NEW CREDENTIALS.");
                Console.WriteLine(">>> PRESS ENTER TO RETURN TO LOGIN SCREEN...");
                Console.ReadKey();
                
            }
            else
            {
                Console.WriteLine(">>> INVALID OPTION. PLEASE SELECT [1] OR [2].");
            }
        }

        bool fortsett = true;

        while (fortsett) //starter en while-løkke som fortsetter å kjøre så lenge fortsett er true, og brytes når brukeren velger å avslutte programmet ved å skrive "0"
        {
            Console.WriteLine("=== ACCESSING LEXICANUM TERMINAL ==="); //menyen er skrevet er fantasert for å gi en mer intressang og tematisk opplevelse da jeg jobbet med koden, og for å gjøre det mer engasjerende for meg selv, og forhåpentligvis også for å gjøre det litt  morsomt for andre.
            Console.WriteLine("[1] Study Protocols [ONLY ACCESIBLE FOR ADEPTS]  (OpprettKurs, legg til pensum)"); //lærer only
            Console.WriteLine("[2] Acolytes to and from Protocols (Sett Student inn i Kurs eller meld dem ut)"); //student only (husk å legg til kun innlogget student kan melde seg av kurs, og ingen andre kan melde av andre studenter)
            Console.WriteLine("[3] Manifest: Display Protocols & Initiates || Query Protocol Database (Vis deltakere og Kurs)");
            Console.WriteLine("[4] Search Data slates (Bøker)");
            Console.WriteLine("[5] Request Data Disbursement (Låne bok)");
            Console.WriteLine("[6] Return Data slate to Archives (Returner bok)");
            Console.WriteLine("[7] REVIEW DATA SLATE LOGS [ONLY ACCESIBLE FOR BIBLIOTHECARIUS] (Vis Historikk)");
            Console.WriteLine("[8] Catalog New Knowledge [ONLY ACCESIBLE FOR BIBLIOTHECARIUS] (Registrer bok)"); //Bibliotekar only
            Console.WriteLine("[9] Register new User. (legg til student, utvekslings student, lærer)"); //lærer only
            Console.WriteLine("[0] Terminate Session (avslutt)");
            Console.WriteLine(">>> ENTER COMMAND:");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int valg)) // Validering av input for å sikre at det er et gyldig tall som vist i siste forelesning
                try
                {
                    if (valg < 0 || valg > 9)
                    {
                        Console.WriteLine(">>> INVALID COMMAND. PLEASE ENTER A NUMBER BETWEEN 0 AND 9."); // Validering av input for å sikre at det er et gyldig tall mellom 0 og 9, og gir en feilmelding hvis det ikke er det
                    }
                    else
                    {


                        switch (input) // Switch-case struktur for å håndtere de forskjellige kommandoene basert på brukerens input, og kaller de respektive metodene i Lexicanum-klassen
                        {
                            case "1":
                                if (innloggetbruker.Rolle != "FagLærer")
                                {
                                    Console.WriteLine(">>> ACCESS DENIED. INNADEQUATE CLEARANCE LEVEL.");
                                    break;
                                }
                                Console.WriteLine("[1] Register New Study Protocol | [2] Add Data Slate to existing Protocol");
                                string kursValg = Console.ReadLine();
                                
                                if (kursValg == "1")
                                {
                                    Console.WriteLine($"ACCESS GRANTED. WELCOME {innloggetbruker.Navn} ");
                                    Console.WriteLine(">>> ENTER COURSE CODE (NUMBER):");
                                    if (int.TryParse(Console.ReadLine(), out int kode)) // Validering av input for å sikre at det er et gyldig tall for kurskode, og deretter spør om resten av informasjonen for å registrere et nytt kurs
                                    {
                                        Console.WriteLine(">>> ENTER COURSE NAME:");
                                        string navn = Console.ReadLine();
                                        Console.WriteLine(">>> ENTER COURSE POINTS:");
                                        int poeng = int.Parse(Console.ReadLine());
                                        Console.WriteLine(">>> ENTER MAX ENROLLMENT:");
                                        int maks = int.Parse(Console.ReadLine());
                                        system.RegistrerKurs(kode, navn, poeng, maks);
                                        Console.WriteLine(">>> COURSE SUCCESSFULLY REGISTERED.");
                                    }
                                }
                                else if (kursValg == "2")
                                {
                                    Console.WriteLine(">>> ENTER COURSE CODE TO ADD DATA SLATE TO:");
                                    int pkurs = int.Parse(Console.ReadLine());
                                    Console.WriteLine(">>> ENTER DATA SLATE TITLE TO ADD TO COURSE:");
                                    string pbok = Console.ReadLine();
                                    system.LeggTilPensum(pkurs, pbok);
                                    Console.WriteLine(">>> DATA SLATE SUCCESSFULLY ADDED TO COURSE.");
                                }

                                break;

                            case "2":
                                Console.WriteLine(">>> [1] Assign Acolyte | [2] Remove Acolyte | [3] Record Evaluation [ADEPTS]");
                                string valgHandling = Console.ReadLine();

                                Console.WriteLine(">>> ENTER ACOLYTE ID:");
                                if (int.TryParse(Console.ReadLine(), out int studentId))
                                {
                                    Console.WriteLine(">>> ENTER COURSE CODE:");
                                    if (int.TryParse(Console.ReadLine(), out int kursKode))
                                    {
                                        if (valgHandling == "1") 
                                        {
                                            system.TildelKurs(studentId, kursKode);
                                        }
                                        else if (valgHandling == "2") 
                                        {
                                            // Sjekker student ID mot innlogget bruker for å sikre at en student kun kan melde seg av kurs de selv er tildelt, og ikke andre studenter, og gir en feilmelding hvis de prøver å melde av en annen student
                                            if (innloggetbruker.Rolle == "Student" && innloggetbruker.Id != studentId)
                                            {
                                                Console.WriteLine(">>> ACCESS DENIED: You can only remove yourself from protocols.");
                                            }
                                            else
                                            {
                                                system.MeldAvKurs(studentId, kursKode);
                                            }
                                        }
                                        else if (valgHandling == "3") // Karakter gradering er kun tilgjengelig for lærere, og sjekker derfor om den innloggede bruker har rollen "FagLærer" før de får lov til å registrere en karakter, og gir en feilmelding hvis de ikke har riktig rolle
                                        {
                                            if (innloggetbruker.Rolle == "FagLærer")
                                            {
                                                Console.Write(">>> ENTER GRADE (A-F): ");
                                                string grad = Console.ReadLine();
                                                system.SettKarakter(studentId, kursKode, grad);
                                            }
                                            else
                                            {
                                                Console.WriteLine(">>> ACCESS DENIED: Only Adepts can record evaluations.");
                                            }
                                        }
                                    }
                                }
                                break;

                            case "3":
                                Console.WriteLine(">>> [1] ALL PROTOCOLS | [2] RETURN | [3] MY PERSONAL RECORD | OR ENTER SEARCH TERM:");
                                string kursSøk = Console.ReadLine();

                                if (kursSøk == "2")
                                {
                                    Console.WriteLine(">>> RETURNING TO TERMINAL...");
                                    break;
                                }
                                // NYTT VALG: Studentens personlige oversikt
                                else if (kursSøk == "3")
                                {
                                    if (innloggetbruker.Rolle == "Student")
                                    {
                                        system.VisMinStatus(innloggetbruker.Id);
                                    }
                                    else
                                    {
                                        Console.WriteLine(">>> ERROR: Personal records only available for Acolytes.");
                                    }
                                }
                                else if (kursSøk == "1")
                                {
                                    Console.WriteLine(">>> DISPLAYING ALL COURSES...");
                                    Console.WriteLine("=== AVAILABLE PROTOCOLS ===");
                                    Console.WriteLine(system.VisKurs());

                                    // Sikkerhet: Kanskje bare lærere skal se hele listen over alle studenter og alle tildelinger?
                                    if (innloggetbruker.Rolle == "FagLærer" || innloggetbruker.Rolle == "Bibliotekar")
                                    {
                                        Console.WriteLine("=== AVAILABLE ACOLYTES ===");
                                        Console.WriteLine(system.VisstudentListe());

                                        Console.WriteLine("=== ASSIGNED ACOLYTES TO PROTOCOLS ===");
                                        system.VisTildelinger();
                                    }
                                }
                                else
                                {
                                    // ... din eksisterende LINQ-søk-logikk her ...
                                    Console.WriteLine("<<COURSE RETREIVAL PROTOCOL INITIATED>>");
                                    var kursTreff = system.AlleKurs.Where(k =>
                                        string.IsNullOrEmpty(kursSøk) ||
                                        k.Navn.Contains(kursSøk, StringComparison.OrdinalIgnoreCase) ||
                                        k.Kode.ToString() == kursSøk).ToList();

                                    if (!kursTreff.Any())
                                    {
                                        Console.WriteLine(">>> ERROR: NO COURSES MATCHING YOUR CRITERIA.");
                                    }
                                    else
                                    {
                                        foreach (var k in kursTreff)
                                        {
                                            Console.WriteLine($"[{k.Kode}] {k.Navn} - {k.Poeng} poeng (Max: {k.MaksStudenter})");

                                            // Bonus: Vis pensum i søkeresultatet hvis det finnes!
                                            if (k.Pensumliste.Any())
                                            {
                                                Console.WriteLine("   Pensum: " + string.Join(", ", k.Pensumliste.Select(b => b.Tittel)));
                                            }
                                        }
                                    }
                                }

                                Console.WriteLine("\n>>> SEARCH COMPLETE. PRESS ENTER TO RETURN.");
                                Console.ReadLine();
                                break;

                            case "4":
                                Console.WriteLine(">>> [1] SEARCH FOR DATA SLATES | [2] VIE LOAN LOGS."); //valg av hva som ønskes
                                string arkivValg = Console.ReadLine();

                                if (arkivValg == "2") //kunn 2 valg derfor kan dette gjøres simpelt med if/else
                                {
                                    // Viser historikken av bøkene (utlån)
                                    system.VisHistorikk();
                                    Console.WriteLine("\n>>> END OF LOGS. PRESS ENTER TO RETURN.");
                                }
                                else
                                {
                                    // søk etter bøker
                                    Console.WriteLine(">>> ENTER SEARCH FOR DATA SLATE (Press enter for all books):");
                                    string searchBøker = Console.ReadLine();
                                    Console.WriteLine(">>> SEARCH RESULTS:");
                                    string searchResults = system.SearchBøker(searchBøker);
                                    Console.WriteLine(searchResults);
                                    Console.WriteLine(">>> END OF SEARCH.");
                                }

                                Console.WriteLine("\n>>> PRESS ENTER TO RETURN TO TERMINAL");
                                Console.ReadLine();
                                break;

                            case "5":
                                Console.WriteLine(">>> ENTER ACOLYTE ID (NUMBER):"); // Spør brukeren om ID for Studenten som ønsker å låne en bok, og deretter spør om tittelen på boken de ønsker å låne, og kaller LånBok-metoden i Lexicanum-klassen for å håndtere låneprosessen
                                string inputId = Console.ReadLine();

                                if (int.TryParse(inputId, out int id))
                                {
                                    Console.WriteLine(">>> ENTER DATA SLATE TITLE:");
                                    string bokTittel = Console.ReadLine();

                                    system.LånBok(id, bokTittel);
                                }
                                else
                                {
                                    Console.WriteLine(">>> ERROR: ID must be a number!");
                                }
                                break;

                            case "6":
                                Console.WriteLine(">>> ENTER ACOLYTE ID (NUMBER):"); // Spør bruker om ID for hvem som returnerer boken, og deretter spør om tittelen på boken som skal returneres, og kaller ReturnerBok-metoden i Lexicanum-klassen for å håndtere returprosessen
                                if (int.TryParse(Console.ReadLine(), out int returId))
                                {
                                    Console.WriteLine(">>> ENTER DATA SLATE TITLE TO RETURN:");
                                    string returBokTittel = Console.ReadLine();
                                    system.ReturnerBok(returId, returBokTittel);
                                    Console.WriteLine(">>> DATA SLATE SUCCESSFULLY RETURNED TO ARCHIVES.");
                                }
                                break;

                            case "7":
                                if (innloggetbruker.Rolle != "Bibliotekar") // Sjekker om den innloggede brukeren har rollen "Bibliotekar" før de får lov til å se utlånshistorikken, og gir en feilmelding hvis de ikke har riktig rolle
                                {
                                    Console.WriteLine(">>> ACCESS DENIED. ONLY BIBLIOTHECARIANS CAN REVIEW DATA SLATE LOGS.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"ACCESS GRANTED. WELCOME {innloggetbruker.Navn} ");
                                    system.VisHistorikk(); // Kaller VisHistorikk-metoden i Lexicanum-klassen for å vise historikken av bøkene (utlån)
                                    Console.WriteLine("\n>>> END OF LOGS. PRESS ENTER TO RETURN.");
                                    Console.ReadLine();
                                    break;
                                }

                            case "8":
                                if (innloggetbruker.Rolle != "Bibliotekar") // Sjekker om den innloggede brukeren har rollen "Bibliotekar" før de får lov til å registrere en ny bok, og gir en feilmelding hvis de ikke har riktig rolle
                                {
                                    Console.WriteLine(">>> ACCESS DENIED. ONLY BIBLIOTHECARIANS CAN CATALOG NEW KNOWLEDGE.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"ACCESS GRANTED. WELCOME {innloggetbruker.Navn} ");
                                    Console.WriteLine(">>> ENTER NEWLY FOUND DATA SLATE TITLE:"); // Spør brukeren om informasjon for å registrere en ny bok i systemet, inkludert tittel, forfatter, utgivelsesår, ISBN og antall eksemplarer, og deretter kaller RegistrerBok-metoden i Lexicanum-klassen for å legge til boken i systemet
                                    string nyBokTittel = Console.ReadLine();

                                    Console.WriteLine(">>> ENTER AUTHOR:");
                                    string nyBokForfatter = Console.ReadLine();
                                    

                                    Console.WriteLine(">>> ENTER YEAR:");
                                    int nyBokÅr = int.Parse(Console.ReadLine());

                                    Console.WriteLine(">>> ENTER ISBN (NUMBER):");
                                    if (int.TryParse(Console.ReadLine(), out int isbn)) // Validering av input for å sikre at ISBN er et gyldig tall, og deretter spør om antall eksemplarer
                                    {
                                        // Spør om antall eksemplarer
                                        Console.WriteLine(">>> ENTER NUMBER OF COPIES FOUND:");
                                        if (int.TryParse(Console.ReadLine(), out int antall))
                                        {
                                            system.RegistrerBok(nyBokTittel, nyBokForfatter, nyBokÅr, isbn, antall);
                                            Console.WriteLine(">>> DATA SLATE SUCCESSFULLY CATALOGED.");
                                        }
                                        else
                                        {
                                            Console.WriteLine(">>> ERROR: Amount must be a number!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(">>> ERROR: ISBN must be a number!");
                                    }
                                    break;
                                }

                            case "9":
                                Console.WriteLine(">>> SELECT ROLE TO INITIATE:"); // Spør brukeren om hvilken type bruker de ønsker å registrere, og deretter spør om relevant informasjon basert på valget, og kaller de respektive registreringsmetodene i Lexicanum-klassen for å legge til den nye brukeren i systemet
                                Console.WriteLine("[1] Acolyte (Student)");
                                Console.WriteLine("[2] External Acolyte (Utvekslingsstudent)");
                                Console.WriteLine("[3] Adept (Ansatt/Lærer)");

                                string rolleValg = Console.ReadLine(); // for å definere hvilken type bruker som skal registreres

                                Console.WriteLine(">>> ENTER ID (NUMBER):");
                                if (int.TryParse(Console.ReadLine(), out int nyId))
                                {
                                    Console.WriteLine(">>> ENTER NAME:");
                                    string nyNavn = Console.ReadLine();
                                    Console.WriteLine(">>> ENTER E-MAIL:");
                                    string nyEpost = Console.ReadLine();
                                    Console.WriteLine(">>> ENTER ACCESS KEY");
                                    string nyPassord = Console.ReadLine();

                                    if (rolleValg == "1") // Registrer vanlig student
                                    {
                                        system.RegistrerStudent(nyId, nyNavn, nyPassord, nyEpost);
                                        Console.WriteLine(">>> STUDENT REGISTERED.");
                                    }
                                    else if (rolleValg == "2") // Registrer utvekslingsstudent
                                    {
                                        Console.WriteLine(">>> ENTER HOME UNIVERSITY:");
                                        string uni = Console.ReadLine();
                                        Console.WriteLine(">>> ENTER COUNTRY:");
                                        string land = Console.ReadLine();
                                        Console.WriteLine(">>> ENTER PERIOD (FROM-TO):");
                                        string periode = Console.ReadLine();

                                        system.RegistrerUtvekslingsStudent(nyId, nyNavn, nyEpost, nyPassord, uni, land, periode);
                                        Console.WriteLine(">>> EXCHANGE STUDENT REGISTERED.");
                                    }
                                    else if (rolleValg == "3") // Registrer ansatt/lærer
                                    {
                                        Console.WriteLine(">>> ENTER POSITION (E.G. FORELESER):");
                                        string stilling = Console.ReadLine();
                                        Console.WriteLine(">>> ENTER DEPARTMENT:");
                                        string avdeling = Console.ReadLine();

                                        system.RegistrerAnsatt(nyId, nyNavn, nyEpost, nyPassord, stilling, avdeling);
                                        Console.WriteLine(">>> EMPLOYEE REGISTERED.");
                                    }
                                }
                                break;


                            case "0": // Avslutter programmet som går gjennom alle case og valideringer, og setter fortsett til false for å bryte while-løkken
                                Console.WriteLine(">>> TERMINATING SESSION...");
                                fortsett = false;
                                break;
                        }

                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(">>> INVALID INPUT. PLEASE ENTER A NUMBER BETWEEN 0 AND 9.");
                }

        }
    }
}