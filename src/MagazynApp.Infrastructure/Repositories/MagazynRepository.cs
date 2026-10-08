using MagazynApp.Application.Interfaces;
using MagazynApp.Domain.Entities;
using MagazynApp.Domain.Enums;
using MagazynApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Infrastructure.Repositories
{
    public class MagazynRepository : IMagazynRepository
    {
        private readonly AppDbContext _context;

        public MagazynRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Lokalizacja> GenerujLokalizacje(int magazynId, int pojemnoscMag, int pojemnoscSek, int ilePODLOGAwSEK)
        {
            var lista = new List<Lokalizacja>();

            char ABCSektor = 'A';
            int NUMSektor = 1;
            int k = 0;
            int i = 0;
            while (i < pojemnoscMag)
            {
                for (int j = 0; j < pojemnoscSek; j++)
                {
                    if (i>= pojemnoscMag)
                    {
                        break;
                    }

                    MagazynApp.Domain.Enums.TypLokalizacja typLokalizacji = (k < ilePODLOGAwSEK) ?
                        TypLokalizacja.Podloga :
                        TypLokalizacja.Polka;

                    lista.Add(new Lokalizacja
                    {
                        MagazynId = magazynId,
                        Nazwa = $"{ABCSektor}{NUMSektor:D3}",   // by generowala sie ladna nazwa sektoru np. A001
                        Opis = "Wygenerowano automatycznie.",
                        Typ = typLokalizacji

                    });
                    NUMSektor++;
                    k++;
                    i++;

                }

                ABCSektor++;
                NUMSektor = 1;
                k = 0;
            }
            return lista;
        }

        public async Task AddAsync(Magazyn magazyn)
        {
            _context.Magazyny.Add(magazyn);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var magazyn = await _context.Magazyny.FindAsync(id);
            if (magazyn != null)
            {
                _context.Magazyny.Remove(magazyn);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Magazyn>> GetAllAsync()
        {
            return await _context.Magazyny
            .Include(m => m.Lokalizacje) // Ładuje też powiązane lokalizacje (inaczej bylyby puste) - LEFT JOIN
            .ToListAsync();
        }

        public async Task<Magazyn?> GetByIdAsync(int id)
        {
            return await _context.Magazyny
                .Include(m => m.Lokalizacje)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Magazyn magazyn)
        {
            _context.Magazyny.Update(magazyn);
            await _context.SaveChangesAsync();
        }
    }
}

//IEnumerable pozwala iterowac petla foreach po kolekcji, ale nie pozwala na modyfikacje kolekcji w trakcie iteracji.
//List pozwala na modyfikacje kolekcji w trakcie iteracji, ale nie jest tak wydajna jak IEnumerable.