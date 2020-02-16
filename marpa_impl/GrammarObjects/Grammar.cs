using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Grammar
    {
        private ErrorCode GlobalErrorCode;
        private bool GrammarPrecomputed;
        private Symbol StartSymbol;
        private readonly List<Symbol> ExtSymList;
        private readonly List<Rule> ExtRuleList;

        private void SetGlobalErrorCode(ErrorCode error)
        {
            GlobalErrorCode = error;
            throw new Exception( ErrorHandler.getErrorMessageByCode(error) );
        }
        private Boolean DoesBelongToGrammarSymbolsList(Symbol Symbol)
        {
            return ExtSymList.Contains(Symbol);
        }
        private void CheckRuleSymbols(Rule Rule)
        {
            Symbol lhs = Rule.GetLeftHandSideOfRule();
            if (!DoesBelongToGrammarSymbolsList(lhs))
            {
                SetGlobalErrorCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR);
            }

            List<Symbol> rhs = Rule.GetRightHandSideOfRule();
            for(int i=0;i<rhs.Count; i++)
            {
                if (!DoesBelongToGrammarSymbolsList(rhs[i]))
                {
                    SetGlobalErrorCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR);
                }
            }
        }

        public Grammar()
        {
            GrammarPrecomputed = false;
            GlobalErrorCode = ErrorCode.NO_ERROR;
            ExtSymList = new List<Symbol>();
            ExtRuleList = new List<Rule>();
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
        public void AddExtSym(List<Symbol> ExtSym)
        {
            ExtSymList.AddRange(ExtSym);
        }
        public bool IsExtSymIdValid(int ExtSymId)
        {
            return ExtSymId >= 0 && ExtSymId < ExtSymList.Count;
        }


        // RULES
        public void AddRule(Rule ExtRule)
        {
            ExtRuleList.Add(ExtRule);
        }
        public void AddRule(Symbol lhs, List<Symbol> rhs)
        {
            ExtRuleList.Add(new Rule(lhs, rhs));
        }

        public Rule GetExtRuleById(int ExtRuleId)
        {
            if (IsExtRuleIdValid(ExtRuleId))
                return ExtRuleList[ExtRuleId];
            else return null;
        }
      
        public bool IsExtRuleIdValid(int ExtRuleId)
        {
            return ExtRuleId >= 0 && ExtRuleId < ExtRuleList.Count;
        }

    }
}
