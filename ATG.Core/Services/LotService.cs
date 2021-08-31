using ATG.Domain.Models;
using ATG.Infrastructure.Data.Repositories;

namespace ATG.Core.Services
{
    public class LotService : ILotService
    {
        private readonly IArchivedRepository _archivedRepository;
        private readonly IFailoverLotRepository _failoverLotRepository;
        private readonly ILotRepository _lotRepository;
        private readonly IFailoverService _failoverService;

        public LotService(IArchivedRepository archivedRepository, 
            IFailoverLotRepository failoverLotRepository, 
            ILotRepository lotRepository,
            IFailoverService failoverService)
        {
            _archivedRepository = archivedRepository;
            _failoverLotRepository = failoverLotRepository;
            _lotRepository = lotRepository;
            _failoverService = failoverService;

        }

        public Lot GetLot(int id, bool isLotArchived)
        {
            if (isLotArchived)
                return _archivedRepository.GetLot(id);
           
            Lot lot = _failoverService.ShouldProvideFailoverLots() ? _failoverLotRepository.GetLot(id) : _lotRepository.LoadCustomer();

            return lot.IsArchived ? _archivedRepository.GetLot(id) : lot;

        }


    }
}
