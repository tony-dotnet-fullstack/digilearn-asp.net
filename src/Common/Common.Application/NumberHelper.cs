namespace Common.Application
{
    public static class NumberHelper
    {
        public static string TooMan(this int price, bool ifPriceIsZeroReturnFree = false)
        {
            if (ifPriceIsZeroReturnFree && price == 0)
                return "Free";

            return $"{price:#,0} $";
        }
        public static string TooMan(this int? price)
        {
            return $"{price:#,0} $";
        }
        public static string SplitNumber(this int price)
        {
            return $"{price:#,0}";
        }
        public static string SplitNumber(this int? price)
        {
            return $"{price:#,0}";
        }
    }
}