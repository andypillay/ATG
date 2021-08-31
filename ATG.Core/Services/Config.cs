namespace ATG.Core.Services
{
    public class Config : IConfig
    {
        public double FailoverTimeOutPeriodInMinutes => Settings.Default.failoverTimePeriod;
        public int MaxFailedRequest => Settings.Default.maxFailedRequests;
        public bool IsFailoverMode => Settings.Default.isFailoverModeEnabled;
    }
}
