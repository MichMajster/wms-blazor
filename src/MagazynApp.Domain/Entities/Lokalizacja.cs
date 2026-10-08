using MagazynApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Domain.Entities
{
    public class Lokalizacja
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public TypLokalizacja Typ { get; set; }
        public bool CzyZaladunek { get; set; }
        public bool CzyRozladunek { get; set; }

        public int MagazynId { get; set; }
        public Magazyn Magazyn { get; set; } = null!;

        // Kolekcja przedmiotów przypisanych do tej lokalizacji (Relacja 1:N)
        public ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();
    }
}
