namespace SIGDB1.Core.Extensions
{
    public static class FilterExtensions
    {
        public static bool IsEquals(this string obj1, string obj2) 
        {
            return obj1.Trim().TrimStart().TrimEnd().ToUpper().Equals(obj2.Trim().TrimStart().TrimEnd().ToUpper());
        }
    }
}
