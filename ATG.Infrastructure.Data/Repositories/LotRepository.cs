using ATG.Domain.Models;
using ATG.Infrastructure.Data.Repositories.Interfaces;

namespace ATG.Infrastructure.Data.Repositories
{
    public class LotRepository : Repository, ILotRepository
    {
        public Lot LoadCustomer()
        {
            return new Lot();
        }
    }
}
