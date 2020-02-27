using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class Earleme
    {
        private Rule Rule;
        private int RulePosition;
        private int ParentPosition;
        private RuleState state;

        internal Earleme(int parentPosition, Rule rule)
        {
            Rule = rule;
            ParentPosition = parentPosition;
            RulePosition = 0;
            state = RuleState.PREDICTED;
        }

        internal Symbol GetCurrentNextSymbol()
        {
            return Rule.GetRightHandSideOfRule(RulePosition);
        }
        internal int GetRulePosition()
        {
            return RulePosition;
        }
        internal int GetParentPosition()
        {
            return ParentPosition;
        }
        internal RuleState GetEarlemePosition()
        {
            return state;
        }
        internal bool IsCompleted()
        {
            return GetRulePosition() == Rule.GetRightHandSideOfRule().Count;
        }
        internal Rule GetRule()
        {
           return Rule;
        }
        public override String ToString()
        {
            return Rule.ToString() + " - at " + RulePosition;
        }
    }
}
