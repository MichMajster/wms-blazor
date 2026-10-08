using MagazynApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Application.Interfaces
{
    public interface ILokalizacjaRepository
    {
        Task<List<Lokalizacja>> GetByMagazynIdAsync(int magazynId);
        Task<Lokalizacja?> GetByIdAsync (int lokalizacjaId);
        Task<List<Lokalizacja>> GetAllAsync();
        Task AddAsync(Lokalizacja lokalizacja);
        Task UpdateAsync(Lokalizacja lokalizacja);
        Task DeleteAsync(int id);
        Task<int> GetIdByNameAsync(string nazwa, int idM);
    }
}
