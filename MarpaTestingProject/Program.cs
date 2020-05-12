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
                MarpaParser parser = new MarpaParser(grammar);
                string str = "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))" +
                    "(()())()((()))()(())((((()))))(()())()((()))()(())((((()))))";

                var watch = System.Diagnostics.Stopwatch.StartNew();
                RecogniserReport result = parser.CheckString(str);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                var count = str.Length;

                // ParserReport result = parser.ParseString("bbb");
            }
        }
    }
}
