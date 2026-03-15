using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Kurs //Kurs-klassen har kode, navn, poeng og maks antall studenter. Koden er unik for hvert kurs, og brukes for å identifisere kurset i systemet.
{
    public int Kode { get; set; }
    public string Navn { get; set; }
    public int Poeng { get; set; }
    public int MaksStudenter { get; set; }
    public Kurs(int kode, string navn, int poeng, int maks)
    {
        Kode = kode;
        Navn = navn;
        Poeng = poeng;
        MaksStudenter = maks;
    }
}
