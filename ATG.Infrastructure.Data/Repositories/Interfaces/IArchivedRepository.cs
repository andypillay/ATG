using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories.Interfaces
{
    public interface IArchivedRepository
    {
        Lot GetLot(int id);
    }
}