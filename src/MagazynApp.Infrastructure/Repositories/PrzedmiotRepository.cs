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
    public class PrzedmiotRepository : IPrzedmiotRepository
    {
        private readonly AppDbContext _context;

        public PrzedmiotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Przedmiot przedmiot)
        {
            _context.Przedmioty.Add(przedmiot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var przedmiot = await _context.Przedmioty.FindAsync(id);
            if (przedmiot != null)
            {
                _context.Przedmioty.Remove(przedmiot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Przedmiot>> GetAllAsync()
        {
            return await _context.Przedmioty
                .Include(p => p.Lokalizacja)
                .ThenInclude(p => p.Magazyn)
                .Include(p => p.Dostawca)
                .Include(p => p.Odbiorca)
                .ToListAsync();
        }

        public async Task<Przedmiot?> GetByIdAsync(int id)
        {
            return await _context.Przedmioty
                .Include(p => p.Lokalizacja)
                    .ThenInclude(l => l.Magazyn)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //wsm niepotrebne
        public async Task<Przedmiot?> GetByNameAsync(string nazwa)
        {
            return await _context.Przedmioty
                .Include(p => p.Lokalizacja)
                    .ThenInclude(l => l.Magazyn)
                .FirstOrDefaultAsync(p => p.Nazwa == nazwa);
        }

        public async Task UpdateAsync(Przedmiot przedmiot)
        {
            _context.Przedmioty.Update(przedmiot);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Przedmiot>> GetByLokalizacjaIdAsync(int lokalId)
        {
            return await _context.Przedmioty
                .Where(l => l.LokalizacjaId == lokalId)
                .ToListAsync();
        }

        public async Task<List<Przedmiot>> GetByMagazynIdAsync(int magazynId)
        {
            return await _context.Przedmioty
                .Where(l => l.Lokalizacja.MagazynId == magazynId)
                .ToListAsync();
        }
    }
}

//IEnumerable pozwala iterowac petla foreach po kolekcji, ale nie pozwala na modyfikacje kolekcji w trakcie iteracji.
//List pozwala na modyfikacje kolekcji w trakcie iteracji, ale nie jest tak wydajna jak IEnumerable.