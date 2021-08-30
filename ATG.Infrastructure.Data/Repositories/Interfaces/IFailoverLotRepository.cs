using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories
{
    public interface IFailoverLotRepository
    {
        Lot GetLot(int id);
    }
}