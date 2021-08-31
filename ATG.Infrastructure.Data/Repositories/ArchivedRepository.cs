using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories
{
    public class ArchivedRepository : IArchivedRepository
    {
        public Lot GetLot(int id)
        {
            return new Lot() { IsArchived = true };
        }
    }
}
