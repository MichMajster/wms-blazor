using MagazynApp.Application.Interfaces;
using MagazynApp.Domain.Entities;
using MagazynApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Infrastructure.Repositories
{
    public class LokalizacjaRepository : ILokalizacjaRepository
    {
        private readonly AppDbContext _context;

        public LokalizacjaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lokalizacja>> GetByMagazynIdAsync(int magazynId) 
        {
            return await _context.Lokalizacje
                .Where(l => l.MagazynId == magazynId)
                .ToListAsync();
        }

        public async Task<Lokalizacja?> GetByIdAsync(int lokalizacjaId)
        {
            return await _context.Lokalizacje
                //.Where(l => l.Id == lokalizacjaId)
                //.ToListAsync();
                .FirstOrDefaultAsync(l => l.Id == lokalizacjaId);
        }

        public async Task<int> GetIdByNameAsync(string nazwa, int idM)
        {
            return await _context.Lokalizacje
                .Where(p => p.MagazynId == idM)
                .Where(p => p.Nazwa == nazwa)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Lokalizacja>> GetAllAsync()
        {
            return await _context.Lokalizacje
                .Include(m => m.Przedmioty)
                //.Where(l => l.Id == magazynId)
                .ToListAsync();
        }

        public async Task AddAsync(Lokalizacja lokalizacja)
        {
            _context.Lokalizacje.Add(lokalizacja);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lok = await _context.Lokalizacje.FindAsync(id);
            if (lok != null)
            {
                _context.Lokalizacje.Remove(lok);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Lokalizacja lokalizacja)
        {
            _context.Lokalizacje.Update(lokalizacja);
            await _context.SaveChangesAsync();
        }
    }
}