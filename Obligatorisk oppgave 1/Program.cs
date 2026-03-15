// See https://aka.ms/new-console-template for more information

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
class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green; // Setter tekstfargen i konsollen til grønn for å gi en mer "terminal-lignende" følelse, og for å skille ut teksten fra standard svart-hvitt og for 
        Console.WriteLine(">>> INITIALIZING LEXICANUM DATABASE ACCESS...");
        Lexicanum system = new Lexicanum();
        bool fortsett = true;

        while (fortsett) //starter en while-løkke som fortsetter å kjøre så lenge fortsett er true, og brytes når brukeren velger å avslutte programmet ved å skrive "0"
        {
            Console.WriteLine("=== ACCESSING LEXICANUM TERMINAL ==="); //menyen er skrevet er fantasert for å gi en mer intressang og tematisk opplevelse da jeg jobbet med koden, og for å gjøre det mer engasjerende for meg selv, og forhåpentligvis også for å gjøre det litt  morsomt for andre.
            Console.WriteLine("[1] Register New Study Protocol (OpprettKurs)");
            Console.WriteLine("[2] Assign Acolyte to Protocol (Sett Student inn i Kurs)");
            Console.WriteLine("[3] Manifest: Display Protocols & Initiates (Vis deltakere og Kurs)");
            Console.WriteLine("[4] Query Protocol Database (Vis Kurs)");
            Console.WriteLine("[5] Search Data slates (Bøker)");
            Console.WriteLine("[6] Request Data Disbursement (Låne bok)");
            Console.WriteLine("[7] Return Data slate to Archives (Returner bok)");
            Console.WriteLine("[8] Catalog New Knowledge (Registrer bok)");
            Console.WriteLine("[9] Register new User. (legg til student, utvekslings student, lærer)");
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
                            }
                            break;

                        case "2":
                            Console.WriteLine(">>> ENTER ACOLYTE ID:");
                            string studentIdInput = Console.ReadLine();

                            if (int.TryParse(studentIdInput, out int studentId)) // Validering av input for å sikre at det er et gyldig tall for student ID, og deretter spør om kurskode for å tildele studenten til riktig kurs
                                {
                                Console.WriteLine(">>> ENTER COURSE CODE TO ASSIGN:");
                                string kursKodeInput = Console.ReadLine(); 

                                
                                if (int.TryParse(kursKodeInput, out int kørsKode))
                                {
                                    system.TildelKurs(studentId, kørsKode);
                                }
                                else
                                {
                                    Console.WriteLine(">>> ERROR: Course code must be a number!");
                                }
                            }
                            else
                            {
                                Console.WriteLine(">>> ERROR: ID must be a number!");
                            }
                            break;

                        case "3":
                            Console.WriteLine("=== AVAILABLE PROTOCOLS ==="); // Viser en liste over alle tilgjengelige kurs/protokoller ved å kalle VisKurs-metoden i Lexicanum-klassen, og deretter viser en liste over alle tilgjengelige acolytes/studenter ved å kalle VisstudentListe
                                string kursListe = system.VisKurs(); 
                            Console.WriteLine(kursListe);        

                            Console.WriteLine("=== AVAILABLE ACOLYTES ===");
                            string studentListe = system.VisstudentListe();
                            Console.WriteLine(studentListe);

                            Console.WriteLine("=== ASSIGNED ACOLYTES TO PROTOCOLS ===");
                            system.VisTildelinger();

                            Console.WriteLine(">>> PRESS ENTER TO RETURN TO TERMINAL");
                            Console.ReadLine();
                            break;

                        case "4":
                            Console.WriteLine("=== ALL AVAILABLE COURSES ==="); 
                              foreach (var k in system.AlleKurs) // Itererer gjennom alle kurs i AlleKurs-listen i Lexicanum-klassen og skriver ut informasjon om hvert kurs, inkludert kode, navn, poeng og maks antall studenter
                                {
                                    Console.WriteLine($"[{k.Kode}] {k.Navn} - {k.Poeng} poeng (Max: {k.MaksStudenter})");
                              }
                            Console.WriteLine(">>> PRESS ENTER TO RETURN");
                            Console.ReadLine();
                            break;

                        case "5":
                        Console.WriteLine(">>> ENTER SEARCH QUERY FOR DATA SLATES: (Press enter for all books)"); // Spør brukeren om en søkestreng for å søke etter bøker, og deretter kaller SearchBøker-metoden i Lexicanum-klassen for å få og vise søkeresultatene
                                string searchBøker = Console.ReadLine();
                        Console.WriteLine(">>> SEARCH RESULTS:");
                            
                        string searchResults = system.SearchBøker(searchBøker);
                        Console.WriteLine(searchResults);
                        Console.WriteLine(">>> END OF SEARCH.");
                        Console.ReadLine();
                        break;

                        case "6":
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

                        case "7":
                            Console.WriteLine(">>> ENTER ACOLYTE ID (NUMBER):"); // Spør bruker om ID for hvem som returnerer boken, og deretter spør om tittelen på boken som skal returneres, og kaller ReturnerBok-metoden i Lexicanum-klassen for å håndtere returprosessen
                                if (int.TryParse(Console.ReadLine(), out int returId))
                            {
                                Console.WriteLine(">>> ENTER DATA SLATE TITLE TO RETURN:");
                                string returBokTittel = Console.ReadLine();
                                system.ReturnerBok(returId, returBokTittel);
                            }
                            break;

                        case "8":
                            Console.WriteLine(">>> ENTER NEWLY FOUND DATA SLATE TITLE:"); // Spør brukeren om informasjon for å registrere en ny bok i systemet, inkludert tittel, forfatter, utgivelsesår, ISBN og antall eksemplarer, og deretter kaller RegistrerBok-metoden i Lexicanum-klassen for å legge til boken i systemet
                                string nyBokTittel = Console.ReadLine();

                            Console.WriteLine(">>> ENTER AUTHOR:");
                            string nyBokForfatter = Console.ReadLine();

                            Console.WriteLine(">>> ENTER YEAR:");
                            string nyBokÅr = Console.ReadLine();

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

                                if (rolleValg == "1") // Registrer vanlig student
                                    {
                                    system.RegistrerStudent(nyId, nyNavn, nyEpost);
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

                                    system.RegistrerUtvekslingsStudent(nyId, nyNavn, nyEpost, uni, land, periode);
                                    Console.WriteLine(">>> EXCHANGE STUDENT REGISTERED.");
                                }
                                else if (rolleValg == "3") // Registrer ansatt/lærer
                                    {
                                    Console.WriteLine(">>> ENTER POSITION (E.G. FORELESER):");
                                    string stilling = Console.ReadLine();
                                    Console.WriteLine(">>> ENTER DEPARTMENT:");
                                    string avdeling = Console.ReadLine();

                                    system.RegistrerLærer(nyId, nyNavn, nyEpost, stilling, avdeling);
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