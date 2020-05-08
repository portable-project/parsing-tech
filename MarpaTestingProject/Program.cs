using System.Collections.Generic;
using marpa_impl;
using Symbol = System.String;

namespace MarpaTestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Grammar grammar = new Grammar();

            Rule startRule1 = new Rule("S", new List<Symbol>() { "S", "S" });
            Rule startRule2 = new Rule("S", new List<Symbol>() { "b" });
            grammar.AddRule(startRule1);
            grammar.AddRule(startRule2);
            grammar.SetStartSym("S");
            
            GrammarReport er = grammar.PrecomputeGrammar();
            if (er.isSuccessfull)
            {
                MarpaParser parser = new MarpaParser(grammar);
                ParserReport result = parser.ParseString("bbb");
            }
        }
    }
}
