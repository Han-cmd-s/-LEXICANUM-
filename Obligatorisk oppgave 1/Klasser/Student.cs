using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Student : Bruker //arver fra Bruker-klassen, som har id, navn og epost. Student-klassen legger til en liste over kurs som studenten er påmeldt.
{
    public List<Kurs> Kursliste { get; set; } = new List<Kurs>();

    public Student(int id, string navn, string epost)
          : base(id, navn, epost)
    {
    
    }

}