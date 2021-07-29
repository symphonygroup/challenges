using System;

namespace congestion.calculator.TollFee
{
    public interface ITollFeeProvider
    {
        int GetTollFee(DateTime date);
    }

    public class TollFeeProvider : ITollFeeProvider
    {
        /// <summary>
        ///     This could be set through configuration.
        /// </summary>
        public int GetTollFee(DateTime date)
        {
            var hour = date.Hour;
            var minute = date.Minute;

            if (hour == 6 && minute <= 29) return 8;
            if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            if (hour == 7 && minute <= 59) return 18;
            if (hour == 8 && minute <= 29) return 13;
            if (hour == 8 && minute >= 30 || hour >= 9 && hour <= 14) return 8; 
            if (hour == 15 && minute <= 29) return 13;
            if (hour == 15 && minute >= 30 || hour == 16) return 18;
            if (hour == 17) return 13;
            if (hour == 18 && minute <= 29) return 8;
            return 0;
        }
    }
}