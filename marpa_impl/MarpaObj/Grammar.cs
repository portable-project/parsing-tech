using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    class Grammar
    {
        private readonly List<Symbol> ExtSymList;
        private readonly List<Symbol> IntSymList;

        public Grammar()
        {
            ExtSymList = new List<Symbol>();
            IntSymList = new List<Symbol>();
        }

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
    }
}
