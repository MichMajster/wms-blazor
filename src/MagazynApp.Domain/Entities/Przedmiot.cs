using MagazynApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Domain.Entities
{
    public class Przedmiot
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;

        // klucz obcy (FK) do lokalizacji
        public int LokalizacjaId { get; set; }
        // właściwość nawigacyjna
        public Lokalizacja? Lokalizacja { get; set; }

        public int? DostawcaId { get; set; }
        public Kontrahent? Dostawca { get; set; }

        public int? OdbiorcaId { get; set; }
        public Kontrahent? Odbiorca { get; set; }


    }
}
