using ATG.Domain.Models;
using ATG.Infrastructure.Data.Repositories;
using System;
using System.Linq;

namespace ATG.Core.Services
{
    public class LotService : ILotService
    {
        private readonly IArchivedRepository _archivedRepository;
        private readonly IFailoverLotRepository _failoverLotRepository;
        private readonly ILotRepository _lotRepository;

        public LotService(IArchivedRepository archivedRepository, IFailoverLotRepository failoverLotRepository, ILotRepository lotRepository)
        {
            _archivedRepository = archivedRepository;
            _failoverLotRepository = failoverLotRepository;
            _lotRepository = lotRepository;
        }

        public Lot GetLot(int id, bool isLotArchived, bool isFailoverModeEnabled = true)
        {
            int maxFailedRequests = 50;
            Lot lot = null;

            var failoverLots = _failoverLotRepository.GetFailOverLotEntries();
            var failedRequests = failoverLots.Where(failoverLotsEntry => failoverLotsEntry.DateTime > DateTime.Now.AddMinutes(10)).Count();

            if ((failedRequests > maxFailedRequests) && isFailoverModeEnabled)
            {
                lot = _failoverLotRepository.GetLot(id);
            }

            if (lot.IsArchived && isLotArchived)
            {
                return _archivedRepository.GetLot(id);
            }
            else
            {
                return _lotRepository.LoadCustomer();
            }
        }


    }
}
