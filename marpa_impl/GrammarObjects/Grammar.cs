using System.Collections.Generic;
using Symbol = System.String;

namespace marpa_impl
{
    public class Grammar
    {
        private Symbol StartSymbol;
        private Symbol NullString;
        private readonly List<Symbol> TerminalsList;
        private readonly List<Symbol> NonTerminalsList;
        private readonly List<Rule> RuleList;
        private bool _IsGrammarValid = false;

        public Grammar()
        {
            RuleList = new List<Rule>();
            TerminalsList = new List<Symbol>();
            NonTerminalsList = new List<Symbol>();
        }
        public Grammar(List<Rule> ruleList)
        {
            RuleList = ruleList == null ? ruleList : new List<Rule>();
            TerminalsList = new List<Symbol>();
            NonTerminalsList = new List<Symbol>();
        }

        public void SetNullStringSymbol(Symbol _nullString)
        {
            NullString = _nullString;
        }
        public Symbol GetNullStringSymbol()
        {
            return NullString;
        }

        public void SetStartSym(Symbol _startSym)
        {
            StartSymbol = _startSym;
        }
        public Symbol GetStartSymbol()
        {
            return StartSymbol;
        }

        public void AddRule(Rule _rule)
        {
            RuleList.Add(_rule);
        }
        public void AddRule(List<Rule> _rules)
        {
            RuleList.AddRange(_rules);
        }
        public void AddRule(Symbol _lhs, List<Symbol> _rhs)
        {
            RuleList.Add(new Rule(_lhs, _rhs));
        }


        public GrammarReport PrecomputeGrammar()
        {
            ErrorHandler eh = new ErrorHandler();
            if (StartSymbol == null) eh.AddNewError(ErrorCode.NO_START_SYMBOL_DETECTED, this);

            for (int i=0; i<RuleList.Count; i++) { 
                Symbol lhs = RuleList[i].GetLeftHandSideOfRule();
                if (CheckIsSymbolANullStringSymbol(lhs)) eh.AddNewError(ErrorCode.NULL_STRING_SYMBOL_ON_LHS, RuleList[i]);
                if (!NonTerminalsList.Contains(lhs)) NonTerminalsList.Add(lhs);
            };

            if (!NonTerminalsList.Contains(StartSymbol)) eh.AddNewError(ErrorCode.NO_START_SYMBOL_IN_LHS_OF_RULES, this);

            for (int i = 0; i < RuleList.Count; i++)
            {
                List<Symbol> rhs = RuleList[i].GetRightHandSideOfRule();
                if(rhs.Count < 1) eh.AddNewError(ErrorCode.RHS_HAS_NO_SYMBOLS, RuleList[i]);

                for (int j = 0; j < rhs.Count; j++)
                {
                    Symbol symbol = rhs[j];
                    if (CheckIsSymbolANullStringSymbol(symbol) && rhs.Count>1) eh.AddNewError(ErrorCode.NULL_STRING_SYMBOL_NOT_ALONE_ON_RHS, RuleList[i]);
                    if (!NonTerminalsList.Contains(symbol) && !TerminalsList.Contains(symbol)) TerminalsList.Add(symbol);
                }
            };

            GrammarReport report = new GrammarReport(eh.GetErrorDescriptionList());
            _IsGrammarValid = report.isSuccessfull;
            return report;
        }

        internal bool IsGrammarValid()
        {
            return _IsGrammarValid;
        }

        internal bool DoesBelongToTerminals(Symbol Symbol)
        {
            return TerminalsList.Contains(Symbol);
        }

        internal bool CheckIsSymbolANullStringSymbol(Symbol Symbol)
        {
            return NullString != null && NullString == Symbol;
        }
                
        internal bool CheckIsSymbolAStartSymbol(Symbol Symbol)
        {
            return StartSymbol == Symbol;
        }


        internal List<Rule> GetRulesWithSpecificStartSymbol(Symbol symbol)
        {
            return symbol != null ? RuleList.FindAll((Rule r) =>
            {
                return r.GetLeftHandSideOfRule() == symbol;
            }) : new List<Rule>();
        }
    }
}
