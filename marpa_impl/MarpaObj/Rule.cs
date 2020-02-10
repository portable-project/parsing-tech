using System.Collections.Generic;

namespace marpa_impl
{
    internal class Rule
    {
        private int RuleId;
        private Symbol LHS = null;
        private List<Symbol> RHS = null;

        public Rule(int id)
        {
            RuleId = id;
        }

        public int GetRuleId()
        {
            return RuleId;
        }

        public void SetLeftHandSideOfRule(Symbol lhs)
        {
            LHS = lhs;
        }

        public void SetRightHandSideOfRule(List<Symbol> rhs)
        {
            RHS = rhs;
        }
        public void AddToRightHandSideOfRule(Symbol symbol)
        {
            RHS.Add(symbol);
        }

        public bool IsRuleAccurate()
        {
            return LHS != null && RHS != null && RHS.Count > 0;
        }

    }
}