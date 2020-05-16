using System;
using System.IO;

namespace marpa_impl
{
    internal static class FileWorker
    {
        internal static Grammar GetGrammarFromXMLFile(String filename)
        {
            return new Grammar();
        }
        
        internal static void CleanFile(String filePath)
        {
            using (var w = new StreamWriter(filePath))
            {
                w.WriteLine();
                w.Flush();
            }
        }
        internal static void WriteToCSVFile(String filePath, String input, long mstime)
        {
            using (var w = new StreamWriter(filePath, true))
            {
                var line = string.Format("{0};{1};{2}", input, input.Length, mstime);
                w.WriteLine(line);
                w.Flush();
            }
        }
    }
}
