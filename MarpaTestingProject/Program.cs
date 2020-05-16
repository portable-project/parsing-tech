using System.Collections.Generic;
using marpa_impl;
using Symbol = System.String;
using System;

namespace MarpaTestingProject
{
    class Program
    { 
        static void Main(string[] args)
        {
            string grammarFilePath = "E://parsing-tech/marpa_impl/Testing/grammars/1.txt";
            string filePath = "E://parsing-tech/marpa_impl/Testing/results.csv";
            GrammarWorkTesting grammarWorkTesting = new GrammarWorkTesting();
            Grammar grammar = grammarWorkTesting.GetGrammarFromFile(grammarFilePath);

            GrammarReport er = grammar.PrecomputeGrammar();
            if (er.isSuccessfull)
            {
                grammarWorkTesting.CalculateTimeOfRecognise(filePath, grammar, new List<string>() { "(()(()))", "(()(()))" , "()()((()()))((()())())"} );
            }
        }
    }
}
