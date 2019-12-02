using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    class Grammar
    {
        private ErrorCode GlobalErrorCode;
        private bool GrammarPrecomputed;
        private Symbol StartSymbol;
        private readonly List<Symbol> ExtSymList;
        private readonly List<Symbol> IntSymList;

        private readonly List<Rule> ExtRuleList;
        private readonly List<Rule> IntRuleList;
        private void SetGlobalErrorCode(ErrorCode error)
        {
            GlobalErrorCode = error;
        }

        public Grammar()
        {
            GrammarPrecomputed = false;
            GlobalErrorCode = ErrorCode.NO_ERROR;
            ExtSymList = new List<Symbol>();
            IntSymList = new List<Symbol>();
            ExtRuleList = new List<Rule>();
            IntRuleList = new List<Rule>();
        }

        public void ClearGlobalErrorCode()
        {
            GlobalErrorCode = ErrorCode.NO_ERROR;
        }
        public ErrorCode GetGlobalErrorCode()
        {
            return GlobalErrorCode;
        }
        public bool IsGrammarPrecomputed()
        {
            return GrammarPrecomputed;
        }

        // START SYMBOL
        public void SetStartSym(int startSymId)
        {
            if (!IsExtSymIdValid(startSymId))
            { 
                SetGlobalErrorCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR);
                throw new Exception(
                    ErrorHandler.getErrorMessageByCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR)
                    );
            }
            else
            {
                StartSymbol = GetExtSymById(startSymId);
            }
        }
        public Symbol GetStartSym()
        {
            return StartSymbol;
        }
        public bool CheckIsSymbolAStartSymbol(int symbolId)
        {
            return StartSymbol.GetSymbolId() == symbolId;
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
