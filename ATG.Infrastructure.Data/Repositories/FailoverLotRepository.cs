using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories
{
    public class FailoverLotRepository : Repository, IFailoverLotRepository
    {
        public Lot GetLot(int id)
        {
            return new Lot();
        }
    }
}
