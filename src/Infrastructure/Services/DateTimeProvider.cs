using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Infrastructure.Services;

public class DateTimeProvider: IDateTimeProvider
{
    private static Func<DateTime> getDateTimeNow = () => DateTime.UtcNow;
    public DateTime UtcNow => getDateTimeNow();
    public void SetDateTimeNow(DateTime dateTime)
    {
        getDateTimeNow = () => dateTime;
    }
   
    public static void ResetDateTime()
    {
        getDateTimeNow = () => DateTime.UtcNow;
    }
}
