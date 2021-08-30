using ATG.Domain.Models;
using System.Collections.Generic;

namespace ATG.Core.Services
{
    public interface ILotService
    {
        Lot GetLot(int id, bool isLotArchived, bool isFailoverModeEnabled = true);
    }
}