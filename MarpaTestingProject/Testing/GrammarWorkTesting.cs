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
        private static string _dirPrefix = "E://parsing-tech/MarpaTestingProject/Testing/examples/";
        private static string _grammarPrefix = "G";
        private static string _inputsPrefix = "I";
        private static string _filePostfix = ".txt";
        private static string filePath = "E://parsing-tech/MarpaTestingProject/Testing/results.csv";
        
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

            GrammarValidationReport er = grammar.PrecomputeGrammar();
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
            List<Result> results = new List<Result>();
            MarpaParser recogniser = new MarpaParser(grammar);

            inputList.ForEach(input =>
            {
                Stopwatch timePerParse = Stopwatch.StartNew();
                RecogniseReport result = recogniser.CheckString(input);
                timePerParse.Stop();
                long mstime = timePerParse.ElapsedTicks;

                if (result.isSuccessfull && result.isRecognised)
                {
                    results.Add(new Result(input, mstime));
                }
            });

            FileWorker.WriteMultipleDataToCSVFile(filePath, results);
        }
    }
}
