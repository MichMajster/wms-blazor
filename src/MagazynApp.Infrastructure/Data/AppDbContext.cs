using Microsoft.EntityFrameworkCore;
using MagazynApp.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Magazyn> Magazyny { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Kontrahent> Kontrahenci { get; set; }
        public DbSet<Lokalizacja> Lokalizacje { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Konfiguracja relacji 1 do wielu między Magazyn a Lokalizacja
            //modelBuilder.Entity<Magazyn>()
            //    .HasMany(m => m.Lokalizacje)
            //    .WithOne(l => l.Magazyn)
            //    .HasForeignKey(l => l.MagazynId)
            //    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Lokalizacja>()
                .HasOne(l => l.Magazyn)          // Lokalizacja ma JEDNEN Magazyn
                .WithMany(m => m.Lokalizacje)    // ...który ma WIELE Lokalizacji
                .HasForeignKey(l => l.MagazynId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Przedmiot>()
                .HasOne(p => p.Lokalizacja)          // Przedmiot ma JEDNĄ Lokalizację
                .WithMany(l => l.Przedmioty)         // ...która ma WIELE Przedmiotów
                .HasForeignKey(p => p.LokalizacjaId) // Klucz obcy w tabeli Przedmioty
                .OnDelete(DeleteBehavior.Cascade);   // Usunięcie lokalizacji usuwa jej przedmioty

            modelBuilder.Entity<Przedmiot>()
                .HasOne(p => p.Dostawca)
                .WithMany()
                .HasForeignKey(p => p.DostawcaId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Przedmiot>()
                .HasOne(p => p.Odbiorca)
                .WithMany()
                .HasForeignKey(p => p.OdbiorcaId)
                .OnDelete(DeleteBehavior.Restrict);

            //DOBRA PRAKTYKA JEST ZACZĄĆ OD STRONY "WIELE" RELACJI, CZYLI OD LOKALIZACJI,
            //BO WTEDY MOŻEMY USTALIĆ CO MA SIĘ STAĆ Z LOKALIZACJAMI GDY USUNIEMY MAGAZYN
            //+ TO LOKALIZACJA MA KLUCZ OBCY, A NIE MAGAZYN, WIĘC LOGICZNIE LEPIEJ ZACZĄĆ OD LOKALIZACJI


        }
    }
}


//Add-Migration NAZWA -Project MagazynApp.Infrastructure -StartupProject MagazynApp.Blazor
//Update-Database