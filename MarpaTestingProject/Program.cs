using System;
using System.Collections.Generic;
using System.Linq;
using marpa_impl;
using marpa_impl.GrammarDefenitionObjects;

namespace MarpaTestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            GDL_Processor gDL_Processor = new GDL_Processor();
            gDL_Processor.TryProcessGrammarDefenition(
                "[dsssd]" +
                "[dsssd]" +
                "{ dc rule: {fv}cdd }");

            /*
            Grammar grammar = new Grammar();
            Symbol s = new Symbol("S");
            Symbol m = new Symbol("M");
            Symbol t = new Symbol("T");
            Symbol d1 = new Symbol("1");
            Symbol d2 = new Symbol("2");
            Symbol d3 = new Symbol("3");
            Symbol d4 = new Symbol("4");
            Symbol o1 = new Symbol("+");
            Symbol o2 = new Symbol("*");
            Symbol nulling = new Symbol("e");

            grammar.AddSymbol(new List<Symbol>() { s, m, t, d1, d2, d3, d4, o1, o2 });
            grammar.AddRule(s, new List<Symbol>() { s, o1, m });
            grammar.AddRule(s, new List<Symbol>() { m });
            grammar.AddRule(m, new List<Symbol>() { t, o2, m });
            grammar.AddRule(m, new List<Symbol>() { t });
            grammar.AddRule(m, new List<Symbol>() { nulling });
            grammar.AddRule(t, new List<Symbol>() { d1 });
            grammar.AddRule(t, new List<Symbol>() { d2 });
            grammar.AddRule(t, new List<Symbol>() { d3 });
            grammar.AddRule(t, new List<Symbol>() { d4 });
            grammar.SetStartSym(s);
            grammar.SetNullingSymbol(nulling);

            Recogniser recogniser = new Recogniser(grammar);
            recogniser.Parse("1+4*3+3");
            */
        }
    }
}
