namespace ATG.Core.Services
{
    public interface IConfig
    {
        double FailoverTimeOutPeriodInMinutes { get; }
        bool IsFailoverMode { get; }
        int MaxFailedRequest { get; }
    }
}