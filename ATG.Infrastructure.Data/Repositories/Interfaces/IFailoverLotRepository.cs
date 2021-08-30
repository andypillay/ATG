using ATG.Domain.Models;

namespace ATG.Infrastructure.Data.Repositories.Interfaces
{
    public interface IFailoverLotRepository
    {
        Lot GetLot(int id);
    }
}