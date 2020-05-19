using marpa_impl;
using Symbol = System.String;
using System.Collections.Generic;

namespace MarpaTestingProject
{
    class Program
    { 
        static void Main(string[] args)
        {
            GrammarWorkTesting.RunGrammarTests(3);
            Grammar grammar = new Grammar();
            Rule r1 = new Rule("S", new List<Symbol>() { "S", "a", "S", "b" });
            Rule r2 = new Rule("S", new List<Symbol>() { "e" });
            grammar.AddRule(r1);
            grammar.AddRule(r2);
            grammar.SetStartSym("S");

            GrammarValidationReport report = grammar.PrecomputeGrammar();
            if (report.isSuccessfull)
            {
                MarpaParser mp = new MarpaParser(grammar);
                // RecogniseReport rr = mp.CheckString("eaeaeaeaeaeaebbbbbb");
            }
        }
    }
}
