using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Bok //Bok-klassen har tittel, forfatter, utgivelsesår, ISBN og antall eksemplarer. Den har også en boolsk variabel som indikerer om boken er utlånt eller ikke.
{
    public string Tittel { get; set; }
    public string Forfatter { get; set; }
    public int Utgivelsesår { get; set; }
    public int ISBN { get; set; }
    public int AntallEksemplarer { get; set; }

    public bool ErUtlånt { get; set; } = false;

    public Bok(string tittel, string forfatter, int utgivelsesår, int isbn, int antallEksemplarer)
    {
        Tittel = tittel;
        Forfatter = forfatter;
        Utgivelsesår = utgivelsesår;
        ISBN = isbn;
        AntallEksemplarer = antallEksemplarer;
    }
}
