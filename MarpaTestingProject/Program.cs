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
            string filePath = "E://parsing-tech/marpa_impl/Testing/results.csv";
            Grammar grammar = new Grammar();

            Rule startRule1 = new Rule("S", new List<Symbol>() { "(", ")" });
            Rule startRule2 = new Rule("S", new List<Symbol>() { "(","S", ")" });
            Rule startRule3 = new Rule("S", new List<Symbol>() { "S","S" });
            grammar.AddRule(startRule1);
            grammar.AddRule(startRule2);
            grammar.AddRule(startRule3);
            grammar.SetStartSym("S");
            
            GrammarReport er = grammar.PrecomputeGrammar();
            if (er.isSuccessfull)
            {
                GrammarWorkTesting grammarWorkTesting = new GrammarWorkTesting();
                grammarWorkTesting.CalculateTimeOfRecognise(filePath, grammar, new List<string>() { "(()(()))", "(()(()))" , "()()((()()))((()())())"} );
            }
        }
    }
}
