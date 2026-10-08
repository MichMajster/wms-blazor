using MagazynApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Application.Interfaces
{
    public interface IMagazynRepository
    {
        Task<List<Magazyn>> GetAllAsync();
        Task<Magazyn?> GetByIdAsync(int id);
        Task AddAsync(Magazyn magazyn);
        Task UpdateAsync(Magazyn magazyn);
        Task DeleteAsync(int id);

        List<Lokalizacja> GenerujLokalizacje(int magazynId, int pojemnoscMag, int pojemnoscSek, int ilePODLOGAwSEK);

    }
}
