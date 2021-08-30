using ATG.Domain.Models;
using System.Collections.Generic;

namespace ATG.Core.Services
{
    public interface ILotService
    {
        List<FailoverLots> GetFailOverLotEntries();
        Lot GetLot(int id, bool isLotArchived);
    }
}