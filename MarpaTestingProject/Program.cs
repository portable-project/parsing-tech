﻿using System.Collections.Generic;
using marpa_impl;
using Symbol = System.String;

namespace MarpaTestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Grammar grammar = new Grammar();

            Rule startRule1 = new Rule("S", new List<Symbol>() { "S", "+", "M" });
            Rule startRule2 = new Rule("S", new List<Symbol>() { "M" });
            grammar.AddRule(startRule1);
            grammar.AddRule(startRule2);

            grammar.AddRule("M", new List<Symbol>() { "T", "*", "M" });
            grammar.AddRule("M", new List<Symbol>() { "T" });
            grammar.AddRule("M", new List<Symbol>() { "e" });
            grammar.AddRule("T", new List<Symbol>() { "1" });
            grammar.AddRule("T", new List<Symbol>() { "2" });
            grammar.AddRule("T", new List<Symbol>() { "3" });
            grammar.AddRule("T", new List<Symbol>() { "4" });
            grammar.SetStartSym("S");
            grammar.SetNullStringSymbol("e");

            GrammarReport er = grammar.PrecomputeGrammar();
            if (er.isSuccessfull)
            {
                MarpaParser parser = new MarpaParser(grammar);
                ParserReport result = parser.ParseString("1+1*4*4");
            }
        }
    }
}
