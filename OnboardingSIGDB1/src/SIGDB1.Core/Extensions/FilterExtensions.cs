namespace SIGDB1.Core.Extensions
{
    public static class FilterExtensions
    {
        public static bool IsEquals(this string obj1, string obj2) 
        {
            return obj1.Trim().TrimStart().TrimEnd().ToUpper().Equals(obj2.Trim().TrimStart().TrimEnd().ToUpper());
        }

        public static bool Like(this string obj1, string obj2)
        {
            return obj1.Trim().TrimStart().TrimEnd().ToUpper().Contains(obj2.Trim().TrimStart().TrimEnd().ToUpper());
        }
    }
}
