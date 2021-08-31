using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories
{
    public class LotRepository : ILotRepository
    {
        public Lot LoadCustomer()
        {
            return new Lot();
        }
    }
}
