using System;
using System.Collections.Generic;
using marpa_impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbol = System.String;

namespace MarpaTesting
{
    [TestClass]
    class RecogniserTest
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
               "E://parsing-tech/MarpaTestingProject/GrammarOneData.xml",
               "Row",
                DataAccessMethod.Sequential)]
        public void AlgorithmSpeedCheck()
        {
            int x = Convert.ToInt32(TestContext.DataRow["Count"]);
        }
    }
}
