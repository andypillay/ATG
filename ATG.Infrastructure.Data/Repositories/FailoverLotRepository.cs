using ATG.Domain.Models;
using System.Collections.Generic;

namespace ATG.Infrastructure.Data.Repositories
{
    public class FailoverLotRepository : IFailoverLotRepository
    {
        public Lot GetLot(int id) => new Lot();

        public List<FailoverLots> GetFailOverLotEntries() =>           
            new List<FailoverLots>();
    }
}
