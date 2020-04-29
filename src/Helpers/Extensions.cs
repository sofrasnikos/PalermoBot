using System;
using System.Collections.Generic;
using System.Text;

namespace Palermo.Helpers
{
    public static class Extensions
    {
        private static Random rng = new Random();

        /// <summary>
        /// Shuffles the given list using the Fisher-Yates shuffle
        /// </summary>
        /// Taken from https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
