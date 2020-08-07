using System.Linq;

namespace SIGDB1.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            else if (string.IsNullOrWhiteSpace(str))
                return true;

            return false;
        }

        public static string OnlyLetters(this string str)
        {
            if (!str.IsEmpty())
                return new string(str.Where(char.IsLetter).ToArray());

            return string.Empty;
        }

        public static string OnlyNumbers(this string str)
        {
            if (!str.IsEmpty())
                return new string(str.Where(char.IsDigit).ToArray());

            return string.Empty;
        }
    }
}
