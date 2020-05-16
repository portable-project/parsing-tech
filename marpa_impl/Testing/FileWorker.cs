using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class FileWorker
    {
        internal Grammar GetGrammarFromFile(String filename)
        {
            return new Grammar();
        }
        internal void WriteToCSV(String input, long mstime)
        {
            // write to file
            Console.WriteLine(input + ' ' + mstime);
        }
    }
}
