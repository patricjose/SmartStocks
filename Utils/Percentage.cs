namespace SmartStocksAPI.Utils
{
    public static class Percentage
    {
        public static decimal GetPercentage (decimal value, decimal total)
        {
            return (value * 100) / total;
        }
    }
}