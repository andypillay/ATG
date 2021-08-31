using ATG.Infrastructure.Data.Repositories;
using System;
using System.Linq;

namespace ATG.Core.Services
{
    public class FailoverService : IFailoverService
    {

        private readonly IFailoverLotRepository _failoverLotRepository;
        private readonly IConfig _config;

        public FailoverService(IFailoverLotRepository failoverLotRepository, IConfig config)
        {
            _failoverLotRepository = failoverLotRepository;
            _config = config;
        }

        public bool ShouldProvideFailoverLots()
        {
            if (!_config.IsFailoverMode)
            {
                return false;
            }
                

            var failoverTimeLimit = DateTime.Now.AddMinutes(_config.FailoverTimeOutPeriodInMinutes);

            var failedRequests = _failoverLotRepository.GetFailOverLotEntries()
                .Where(entries => entries.DateTime > failoverTimeLimit).Count();

            return failedRequests > _config.MaxFailedRequest;
        }
    }
}