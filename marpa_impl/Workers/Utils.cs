using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class Utils
    {
        internal static void PrintSeparator(int size)
        {
            for (int i = 0; i < size; i++) Console.WriteLine();
        }
        internal static void PrintSets(List<EarlemeSet> setsToPrint, bool all)
        {
            for (int i = 0; i < setsToPrint.Count; i++)
            {
                Console.WriteLine("\n\tSet num " + i);
                EarlemeSet earlemeSet = setsToPrint[i];
                for (int k = 0; k < earlemeSet.GetEarlemeSetSize(); k++)
                {
                    Earleme e = earlemeSet.GetEarleme(k);
                    if (!all)
                    {
                        if (e.IsCompleted()) Console.WriteLine("\t" + e.ToString());
                    }
                    else
                    {
                        Console.WriteLine((e.IsCompleted() ? "\t" : "") + e.ToString());
                    }
                }
            }
        }
    }
}
