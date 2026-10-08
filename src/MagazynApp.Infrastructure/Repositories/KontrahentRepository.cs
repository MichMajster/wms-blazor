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
    public class KontrahentRepository : IKontrahentRepository
    {
        private readonly AppDbContext _context;

        public KontrahentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Kontrahent> AddAsync(Kontrahent kontrahent)
        {
            _context.Kontrahenci.Add(kontrahent);
            await _context.SaveChangesAsync();
            return kontrahent;
        }

        public async Task DeleteAsync(int id)
        {
            var kontrahent = await _context.Kontrahenci.FindAsync(id);
            if (kontrahent != null)
            {
                _context.Kontrahenci.Remove(kontrahent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Kontrahent>> GetAllAsync()
        {
            return await _context.Kontrahenci
                .ToListAsync();
        }

        public async Task<Kontrahent?> GetByIdAsync(int id)
        {
            return await _context.Kontrahenci
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Kontrahent> UpdateAsync(Kontrahent kontrahent)
        {
            _context.Kontrahenci.Update(kontrahent);
            await _context.SaveChangesAsync();
            return kontrahent;
        }
    }
}

//IEnumerable pozwala iterowac petla foreach po kolekcji, ale nie pozwala na modyfikacje kolekcji w trakcie iteracji.
//List pozwala na modyfikacje kolekcji w trakcie iteracji, ale nie jest tak wydajna jak IEnumerable.