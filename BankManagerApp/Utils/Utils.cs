namespace BankManagerApp.Utils
{
    public static class Utils
    {
        public const string BankBranchIDFormat = "TECH\\d{3}";
        public const string CustomerIDFormat = "CUS\\d{3}";
        public const string AccountNumberFormat = "ACC\\d{3}";
        public const string TransactionNumberFormat = "TRANS\\d{3}";

        private static Random random = new Random();

        /// <summary>
        /// Generates a string consists of 3 random digits
        /// </summary>
        /// <returns>A string</returns>
        public static string GenerateRandomNumString()
        {
            const string numChars = "0123456789";
            return new string(Enumerable.Repeat(numChars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
