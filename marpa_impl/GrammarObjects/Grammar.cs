using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public class Grammar
    {
        private Symbol StartSymbol;
        private readonly List<Symbol> ExtSymList;
        private readonly List<Rule> ExtRuleList;

        private bool DoesBelongToGrammarSymbolsList(Symbol Symbol)
        {
            return ExtSymList.Contains(Symbol);
        }
        private bool IsRuleCorrect(Rule Rule)
        {
            Symbol lhs = Rule.GetLeftHandSideOfRule();
            if (!DoesBelongToGrammarSymbolsList(lhs))
            {
                return false;
            }

            List<Symbol> rhs = Rule.GetRightHandSideOfRule();
            for(int i=0;i<rhs.Count; i++)
            {
                if (!DoesBelongToGrammarSymbolsList(rhs[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public Grammar()
        {
            ExtRuleList = new List<Rule>();
            ExtSymList = new List<Symbol>();
        }
        public bool IsGrammarValid()
        {
            bool result = true;
            result &= DoesBelongToGrammarSymbolsList(StartSymbol);
            if (!result)
            {
                ErrorHandler.PrintErrorCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR, StartSymbol);
            }

            ExtRuleList.ForEach(r =>
            {
                result &= IsRuleCorrect(r);
                if (!result)
                {
                    ErrorHandler.PrintErrorCode(ErrorCode.INCORRECT_RULE_SYMBOLS, r);
                }
            });

            result &= ExtSymList.Count > 0 && GetStartSymbol() != null && ExtRuleList.Count > 0;

            return result;
        }

        internal bool DoesBelongToTerminals(Symbol Symbol)
        {
            List<Symbol> symbols = new List<Symbol>();
            ExtRuleList.ForEach(rule => symbols.Add(rule.GetLeftHandSideOfRule()));
            return !symbols.Contains(Symbol);
        }

        // START SYMBOL
        public void SetStartSym(Symbol StartSym)
        {
            StartSymbol = StartSym;
        }
        public Symbol GetStartSymbol()
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
        internal Symbol GetSymbolByName(Char symName)
        {
            return ExtSymList.Find(sym => sym.GetSymbolName() == symName.ToString());
        }


        // RULES
        public int GetRulesListSize()
        {
            return ExtRuleList.Count;
        }
        public void AddRule(Rule Rule)
        {
            Rule.SetRuleId(GetRulesListSize());
            ExtRuleList.Add(Rule);
        }
        public void AddRule(Symbol lhs, List<Symbol> rhs)
        {
            Rule newRule = new Rule(lhs, rhs, GetRulesListSize());
            ExtRuleList.Add(newRule);
        }

        internal List<Rule> GetRulesWithSpecificStartSymbol(Symbol symbol)
        {
            return ExtRuleList.FindAll((Rule r) =>
            {
                return r.GetLeftHandSideOfRule().GetSymbolName() == symbol.GetSymbolName();
            });
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
