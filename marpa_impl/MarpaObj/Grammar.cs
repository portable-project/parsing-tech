using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    class Grammar
    {
        private readonly List<Symbol> ExtSymList;
        private readonly List<Symbol> IntSymList;

        private readonly List<Rule> ExtRuleList;
        private readonly List<Rule> IntRuleList;

        public Grammar()
        {
            ExtSymList = new List<Symbol>();
            IntSymList = new List<Symbol>();
            ExtRuleList = new List<Rule>();
            IntRuleList = new List<Rule>();
        }

        // SYMBOLS
        public int GetExtSymListSize()
        {
            return ExtSymList.Count;
        }
        public Symbol GetExtSymById(int ExtSymId)
        {
            return ExtSymList[ExtSymId];
        }
        public void AddExtSym(Symbol ExtSym)
        {
            ExtSymList.Add(ExtSym);
        }
        public bool IsExtSymIdValid(int ExtSymId)
        {
            return ExtSymId >= 0 && ExtSymId < ExtSymList.Count;
        }

        public int GetIntSymListSize()
        {
            return IntSymList.Count;
        }
        public Symbol GetIntSymById(int IntSymId)
        {
            return IntSymList[IntSymId];
        }
        public void AddIntSym(Symbol IntSym)
        {
            IntSymList.Add(IntSym);
        }
        public bool IsIntSymIdValid(int IntSymId)
        {
            return IntSymId >= 0 && IntSymId < IntSymList.Count;
        }


        // RULES
        public Rule GetExtRuleById(int ExtRuleId)
        {
            return ExtRuleList[ExtRuleId];
        }
        public void AddExtRule(Rule ExtRule)
        {
            ExtRuleList.Add(ExtRule);
        }
        public bool IsExtRuleIdValid(int ExtRuleId)
        {
            return ExtRuleId >= 0 && ExtRuleId < ExtRuleList.Count;
        }

        public Rule GetIntRuleById(int IntRuleId)
        {
            return IntRuleList[IntRuleId];
        }
        public void AddIntRule(Rule IntRule)
        {
            IntRuleList.Add(IntRule);
        }
        public bool IsIntRuleIdValid(int IntRuleId)
        {
            return IntRuleId >= 0 && IntRuleId < IntRuleList.Count;
        }
    }
}
