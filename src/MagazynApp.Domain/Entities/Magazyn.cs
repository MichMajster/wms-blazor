using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Domain.Entities
{
    public class Magazyn
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;

        public int MaksLokalizacji { get; set; }
        //Relacja 1 do wielu - Magazyn może mieć wiele lokalizacji
        public ICollection<Lokalizacja> Lokalizacje { get; set; } = new List<Lokalizacja>();
    }
}
