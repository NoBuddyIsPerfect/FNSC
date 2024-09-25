using System;
using System.Collections.Generic;

namespace FNSC.Helpers
{
    

    public static class ListExtensions
    {
        private static Random rand = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
