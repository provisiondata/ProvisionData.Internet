namespace ProvisionData.Internet.UnitTests
{
    using NodaTime;
    using System;

    public interface ITestableClock : IClock
    {
        void DateIs(Int32 year, Int32 monthOfYear, Int32 dayOfMonth);
        void DateIs(Int32 year, Int32 monthOfYear, Int32 dayOfMonth, Int32 hourOfDay, Int32 minuteOfHour);
        void DateIs(Int32 year, Int32 monthOfYear, Int32 dayOfMonth, Int32 hourOfDay, Int32 minuteOfHour, Int32 secondOfMinute);
        void Reset();
        void DateIs(Instant instant);
    }
}
