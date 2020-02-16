using System;
using marpa_impl;

namespace MarpaTestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Grammar grammar = new Grammar();
            Symbol first = new Symbol("first");
            Symbol second = new Symbol("second");
            Rule rule = new Rule(first, new System.Collections.Generic.List<Symbol>() { second });
            grammar.AddExtSym(first);
            grammar.AddRule(rule);
        }
    }
}
