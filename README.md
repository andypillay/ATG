# ATG

## Approach 

I followed a clean architecture approach; 
Reorganised the solution so it is split into something like the image below. The idea of Clean Architecture is, the center is the business logic and the layers around it take dependencies on it, but it doesn't take dependencies on anything else. https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture 

Once I refactored  and seperated `LogService` into `LogService` and `FailOverService`.
Created a `Settings.settings` to allow for changing of application variables without making changes to code e.g `IsFailoverMode, FailoverTimeOutPeriodInMinutes, MaxFailedRequest`.
This allowed me to simplify the `LogService` further.

Wrapped and extracted interfaces out of all services and repositories. Wrote unit tests for all updated/created services using Nunit. I used Moq to return back values I expected to get from the Moq'd repositories (or services). 
