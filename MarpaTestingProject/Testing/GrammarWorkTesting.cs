using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Symbol = System.String;
using marpa_impl;

namespace MarpaTestingProject
{
    public static class GrammarWorkTesting
    {
        private static string _dirPrefix = "E://parsing-tech/marpa_impl/Testing/examples/";
        private static string _grammarPrefix = "G";
        private static string _inputsPrefix = "I";
        private static string _filePostfix = ".txt";
        private static string filePath = "E://parsing-tech/marpa_impl/Testing/results.csv";
        
        public static void RunGrammarTests(int numberOfGrammars)
        {
            FileWorker.CleanFile(filePath);

            for (int i = 1; i<=numberOfGrammars; i++)
                RunRecongniserForGrammar(i);
        }
        internal static void RunRecongniserForGrammar(int grammarNumber) {
            string grammarFilePath = _dirPrefix + _grammarPrefix + grammarNumber.ToString() + _filePostfix;
            string inputsFilePath = _dirPrefix + _inputsPrefix + grammarNumber.ToString() + _filePostfix;
            Grammar grammar = GetGrammarFromFile(grammarFilePath);
            List<string> inputs = GetListOfInputs(inputsFilePath);

            GrammarReport er = grammar.PrecomputeGrammar();
            if (er.isSuccessfull)
            {
                CalculateTimeOfRecognise(filePath, grammar, inputs);
            }
        }

        internal static Grammar GetGrammarFromFile(string filePath)
        {
            Grammar grammar = new Grammar();
            List<string> fileLines = FileWorker.GetLinesFromXMLFile(filePath);
            fileLines.ForEach(line =>
            {
                if (line.Contains("->")) grammar.AddRule(GetRuleFromTextLine(line));
                if (line.Contains("start:")) grammar.SetStartSym(line.Replace("start:", "").Trim());
                if (line.Contains("null:")) grammar.SetNullStringSymbol(line.Replace("null:", "").Trim());
            });
            return grammar;
        }

        internal static List<String> GetListOfInputs(string filePath)
        {
           return FileWorker.GetLinesFromXMLFile(filePath);
        }

        private static Rule GetRuleFromTextLine(string line)
        {
            string[] parts = line.Replace("->", ">").Split('>');
            List<Symbol> rhs = new List<Symbol>() { };
            char[] rhsSymbols = parts[1].Trim().ToCharArray();
            for(int i = 0; i < rhsSymbols.Length; i++) rhs.Add(rhsSymbols[i].ToString());
            return new Rule(parts[0].Trim(), rhs);
        }
        internal static void CalculateTimeOfRecognise(string filePath, Grammar grammar, List<String> inputList)
        {
            MarpaParser recogniser = new MarpaParser(grammar);

            inputList.ForEach(input =>
            {
                Stopwatch timePerParse = Stopwatch.StartNew();
                RecogniserReport result = recogniser.CheckString(input);
                timePerParse.Stop();
                long mstime = timePerParse.ElapsedMilliseconds;

                if (result.isSuccessfull && result.isRecognised) FileWorker.WriteToCSVFile(filePath, input, mstime);
            });
        }
        private static void DisplayTimerProperties()
        {
            // Display the timer frequency and resolution.
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Operations timed using the system's high-resolution performance counter.");
            }
            else
            {
                Console.WriteLine("Operations timed using the DateTime class.");
            }

            long frequency = Stopwatch.Frequency;
            Console.WriteLine("  Timer frequency in ticks per second = {0}",
                frequency);
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            Console.WriteLine("  Timer is accurate within {0} nanoseconds",
                nanosecPerTick);
        }
    }
}
