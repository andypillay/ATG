using ATG.Domain.Models;
using ATG.Infrastructure.Data.Repositories.Interfaces;

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
