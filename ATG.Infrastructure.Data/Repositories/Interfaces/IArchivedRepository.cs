using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories
{
    public interface IArchivedRepository
    {
        Lot GetLot(int id);
    }
}