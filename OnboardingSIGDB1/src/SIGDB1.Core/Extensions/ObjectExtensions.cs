namespace SIGDB1.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
