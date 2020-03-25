using System.Collections.Generic;
using System.Linq;

namespace FManager.Units
{
    public static class Extensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }
    }
}
