using MagazynApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Domain.Entities
{
    public class Kontrahent
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        //public string Adres { get; set; }
        //public string NIP { get; set; }
        //public string Telefon { get; set; }
        //public string Email { get; set; }
        public string Opis { get; set; } = string.Empty;
        //public string Uwagi { get; set; }
        public TypKontrahent Typ { get; set; }
    }
}

//Add-Migration NAZWA -Project MagazynApp.Infrastructure -StartupProject MagazynApp.Blazor
//Update-Database