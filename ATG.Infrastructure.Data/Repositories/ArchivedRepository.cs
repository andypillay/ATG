using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories
{
    public class ArchivedRepository : Repository, IArchivedRepository
    {
        public Lot GetLot(int id)
        {
            return new Lot();
        }
    }
}
