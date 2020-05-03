using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public class Grammar
    {
        private Symbol StartSymbol;
        private Symbol NullingSymbol;
        private readonly List<Symbol> TerminalsList;
        private readonly List<Symbol> NonTerminalsList;
        private readonly List<Rule> RuleList;

        public Grammar()
        {
            RuleList = new List<Rule>();
            TerminalsList = new List<Symbol>();
            NonTerminalsList = new List<Symbol>();
        }
        public Grammar(List<Rule> ruleList)
        {
            RuleList = ruleList;
            TerminalsList = new List<Symbol>();
            NonTerminalsList = new List<Symbol>();
        }
        public bool IsGrammarValid()
        {
            bool result = true;
            if(NullingSymbol != null) result &= !DoesBelongToGrammarSymbolsList(NullingSymbol);
            if (!result)
            {
                ErrorHandler.PrintErrorCode(ErrorCode.NULLING_SYMBOL_BELONGS_TO_GRAMMAR, NullingSymbol);
            }

            result &= DoesBelongToGrammarSymbolsList(StartSymbol);
            if (!result)
            {
                ErrorHandler.PrintErrorCode(ErrorCode.NO_SUCH_SYMBOL_IN_GRAMMAR, StartSymbol);
            }

            RuleList.ForEach(r =>
            {
                result &= IsRuleCorrect(r);
                if (!result)
                {
                    ErrorHandler.PrintErrorCode(ErrorCode.INCORRECT_RULE_SYMBOLS, r);
                }
            });
            // FIX
            // result &= SymbolList.Count > 0 && GetStartSymbol() != null && RuleList.Count > 0;

            return result;
        }

        private bool DoesBelongToGrammarSymbolsList(Symbol Symbol)
        {
            return SymbolList.Contains(Symbol);
        }
        private bool IsRuleCorrect(Rule Rule)
        {
            Symbol lhs = Rule.GetLeftHandSideOfRule();
            if (!DoesBelongToGrammarSymbolsList(lhs))
            {
                return false;
            }

            List<Symbol> rhs = Rule.GetRightHandSideOfRule();
            if (rhs.Count == 1 && CheckIsSymbolANullingSymbol(rhs[0]))
            {
                return true;
            }

            for (int i = 0; i < rhs.Count; i++)
            {
                if (!DoesBelongToGrammarSymbolsList(rhs[i]))
                {
                    return false;
                }
            }

            return true;
        }

        internal bool DoesBelongToTerminals(Symbol Symbol)
        {
            List<Symbol> symbols = new List<Symbol>();
            RuleList.ForEach(rule => symbols.Add(rule.GetLeftHandSideOfRule()));
            return !symbols.Contains(Symbol);
        }

        // NULLING SYMBOL
        public void SetNullingSymbol(Symbol NullingSym)
        {
            NullingSymbol = NullingSym;
        }
        public Symbol GetNullingSymbol()
        {
            return NullingSymbol;
        }
        internal bool CheckIsSymbolANullingSymbol(Symbol Symbol)
        {
            return NullingSymbol != null && NullingSymbol.GetSymbolName() == Symbol.GetSymbolName();
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
            return StartSymbol.GetSymbolName() == Symbol.GetSymbolName();
        }

        // SYMBOLS
        public void AddSymbol(Symbol Symbol)
        {
            SymbolList.Add(Symbol);
        }
        public void AddSymbol(List<Symbol> Symbols)
        {
            Symbols.ForEach(AddSymbol);
        }
        internal bool IsExtSymIdValid(int ExtSymId)
        {
            return ExtSymId >= 0 && ExtSymId < SymbolList.Count;
        }
        internal Symbol GetSymbolByName(Char symName)
        {
            return SymbolList.Find(sym => sym.GetSymbolName() == symName.ToString());
        }


        // RULES
        public int GetRulesListSize()
        {
            return RuleList.Count;
        }
        public void AddRule(Rule Rule)
        {
            RuleList.Add(Rule);
        }
        public void AddRule(Symbol lhs, List<Symbol> rhs)
        {
            Rule newRule = new Rule(lhs, rhs, GetRulesListSize());
            RuleList.Add(newRule);
        }

        internal List<Rule> GetRulesWithSpecificStartSymbol(Symbol symbol)
        {
            return symbol != null ? RuleList.FindAll((Rule r) =>
            {
                return r.GetLeftHandSideOfRule().GetSymbolName() == symbol.GetSymbolName();
            }) : new List<Rule>();
        }

        internal Rule GetExtRuleById(int ExtRuleId)
        {
            if (IsExtRuleIdValid(ExtRuleId))
                return RuleList[ExtRuleId];
            else return null;
        }
      
        internal bool IsExtRuleIdValid(int ExtRuleId)
        {
            return ExtRuleId >= 0 && ExtRuleId < RuleList.Count;
        }

    }
}
