using ATG.Domain.Models;
using System.Collections.Generic;

namespace ATG.Infrastructure.Data.Repositories
{
    public interface IFailoverLotRepository
    {
        Lot GetLot(int id);
        IEnumerable<FailoverLots> GetFailOverLotEntries();
    }
}