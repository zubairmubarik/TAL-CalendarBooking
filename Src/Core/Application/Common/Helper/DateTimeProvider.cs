using Application.Common.Interfaces;

namespace Application.Common.Helper
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate() => DateTime.UtcNow;
    }
}
