using System;
using System.Collections.Generic;
using marpa_impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbol = System.String;

namespace MarpaTesting
{
    [TestClass]
    public class GrammarStructureTest
    {
        private Grammar grammar;
        private GrammarReport grammarReport;

        [TestInitialize]
        public void Setup()
        {
            grammar = new Grammar();
            Rule startRule1 = new Rule("S", new List<Symbol>() { "(", ")" });
            Rule startRule2 = new Rule("S", new List<Symbol>() { "(", "S", ")" });
            Rule startRule3 = new Rule("S", new List<Symbol>() { "S", "S" });
            grammar.AddRule(startRule1);
            grammar.AddRule(startRule2);
            grammar.AddRule(startRule3);
            grammar.SetStartSym("S");
            grammarReport = grammar.PrecomputeGrammar();
        }

        [TestMethod]
        public void CheckGrammarCorrect()
        {
            Assert.IsTrue(grammarReport.isSuccessfull);
        }

        [TestMethod]
        public void CheckSymbolCount()
        {
            Assert.AreEqual(3, 
                grammar.GetGrammarNonTerminals().Count + grammar.GetGrammarTerminals().Count);
        }

        [TestMethod]
        public void CheckRuleCount()
        {
            Assert.AreEqual(3, grammar.GetRules().Count);
        }
    }
}
