using System;
using System.Collections.Generic;

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
        public bool IsGrammarPrecomputed()
        {
            return GrammarPrecomputed;
        }

        internal void ClearGlobalErrorCode()
        {
            GlobalErrorCode = ErrorCode.NO_ERROR;
        }
        internal ErrorCode GetGlobalErrorCode()
        {
            return GlobalErrorCode;
        }

        // START SYMBOL
        public void SetStartSym(Symbol StartSym)
        {
            if (!DoesBelongToGrammarSymbolsList(StartSym))
            { 
                SetGlobalErrorCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR);
            }
            else
            {
                StartSymbol = StartSym;
            }
        }
        public Symbol GetStartSym()
        {
            return StartSymbol;
        }
        internal bool CheckIsSymbolAStartSymbol(Symbol Symbol)
        {
            return StartSymbol.GetSymbolId() == Symbol.GetSymbolId();
        }

        // SYMBOLS
        public int GetSymbolsListSize()
        {
            return ExtSymList.Count;
        }
        public void AddSymbol(Symbol Symbol)
        {
            Symbol.SetSymbolId(GetSymbolsListSize());
            ExtSymList.Add(Symbol);
        }
        public void AddSymbol(List<Symbol> Symbols)
        {
            Symbols.ForEach(AddSymbol);
        }
        internal bool IsExtSymIdValid(int ExtSymId)
        {
            return ExtSymId >= 0 && ExtSymId < ExtSymList.Count;
        }


        // RULES
        public int GetRulesListSize()
        {
            return ExtRuleList.Count;
        }
        public void AddRule(Rule Rule)
        {
            CheckRuleSymbols(Rule);
            Rule.SetRuleId(GetRulesListSize());
            ExtRuleList.Add(Rule);
        }
        public void AddRule(Symbol lhs, List<Symbol> rhs)
        {
            Rule newRule = new Rule(lhs, rhs, GetRulesListSize());
            CheckRuleSymbols(newRule);
            ExtRuleList.Add(newRule);
        }

        internal Rule GetExtRuleById(int ExtRuleId)
        {
            if (IsExtRuleIdValid(ExtRuleId))
                return ExtRuleList[ExtRuleId];
            else return null;
        }
      
        internal bool IsExtRuleIdValid(int ExtRuleId)
        {
            return ExtRuleId >= 0 && ExtRuleId < ExtRuleList.Count;
        }

    }
}
