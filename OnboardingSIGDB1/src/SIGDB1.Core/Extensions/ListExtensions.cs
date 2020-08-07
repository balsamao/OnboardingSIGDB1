using System.Collections.Generic;
using System.Linq;

namespace SIGDB1.Core.Extensions
{
    public static class ListExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> list) 
        {
            if (list == null)
                return true;
            else if (list.Count() == 0)
                return true;

            return false;
        }
    }
}
