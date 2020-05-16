using System;
using System.IO;
using System.Collections.Generic;

namespace marpa_impl
{
    internal static class FileWorker
    {
        internal static List<string> GetLinesFromXMLFile(String filename)
        {
            List<string> lines = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) lines.Add(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return lines;
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
