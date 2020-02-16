using System.Collections.Generic;
using System;

namespace marpa_impl
{
    public class Rule
    {
        private int RuleId;
        private RuleState State;
        private Symbol LHS = null;
        private List<Symbol> RHS = null;

        public Rule(Symbol lhs)
        {
            LHS = lhs;
            RHS = new List<Symbol>();
        }

        public Rule(Symbol lhs, List<Symbol> rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }
        internal Rule(Symbol lhs, List<Symbol> rhs, int ruleId)
        {
            LHS = lhs;
            RHS = rhs;
            RuleId = ruleId;
        }

        public Symbol GetLeftHandSideOfRule()
        {
            return LHS;
        }

        public List<Symbol> GetRightHandSideOfRule()
        {
            return RHS;
        }

        public void AddToRightHandSideOfRule(Symbol symbol)
        {
            if(RHS != null) RHS.Add(symbol);
            else throw new Exception(ErrorHandler.getErrorMessageByCode(ErrorCode.INCORRECT_RULE));
        }
        public void AddToRightHandSideOfRule(List<Symbol> symbols)
        {
            if (RHS != null) RHS.AddRange(symbols);
            else throw new Exception(ErrorHandler.getErrorMessageByCode(ErrorCode.INCORRECT_RULE));
        }

        internal void SetRuleId(int Id)
        {
            RuleId = Id;
        }

        internal int GetRuleId()
        {
            return RuleId;
        }
        internal void SetRuleState(RuleState state)
        {
            State = state;
        }
        internal RuleState GetRuleState()
        {
            return State;
        }
        internal bool IsRuleAccurate()
        {
            return LHS != null && RHS != null && RHS.Count > 0;
        }
    }
}