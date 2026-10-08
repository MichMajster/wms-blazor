using MagazynApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Application.Interfaces
{
    public interface IPrzedmiotRepository
    {
        Task<List<Przedmiot>> GetAllAsync();
        Task<Przedmiot?> GetByIdAsync(int id);
        Task AddAsync(Przedmiot przedmiot);
        Task UpdateAsync(Przedmiot przedmiot);
        Task DeleteAsync(int id);
        Task<Przedmiot?> GetByNameAsync(string nazwa);
        Task<List<Przedmiot>> GetByMagazynIdAsync(int magazynId);
        Task<List<Przedmiot>> GetByLokalizacjaIdAsync(int lokId);
    }
}
