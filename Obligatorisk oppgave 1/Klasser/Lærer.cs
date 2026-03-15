using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Lærer : Bruker
{
    public string Stilling { get; set; }
    public string Avdeling { get; set; }
    public List<Kurs> Kursliste { get; set; } = new List<Kurs>();

    public Lærer(int id, string navn, string epost, string stilling, string avdeling)
        : base(id, navn, epost)
    {
        Stilling = stilling;
        Avdeling = avdeling;
    }

}

