using MagazynApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Application.Interfaces
{
    public interface IKontrahentRepository
    {
        Task<List<Kontrahent>> GetAllAsync();
        Task<Kontrahent?> GetByIdAsync(int id);
        Task<Kontrahent> AddAsync(Kontrahent kontrahent);
        Task<Kontrahent> UpdateAsync(Kontrahent kontrahent);
        Task DeleteAsync(int id);
    }
}
