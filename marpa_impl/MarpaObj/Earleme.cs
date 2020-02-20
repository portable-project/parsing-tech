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

        internal Earleme(int origin, Rule rule)
        {
            Rule = rule;
            ParentPosition = origin;
            RulePosition = 0;
            state = RuleState.PREDICTED;
        }

        internal Earleme(int origin, Rule rule, RuleState ruleState)
        {
            Rule = rule;
            ParentPosition = origin;
            RulePosition = 0;
            state = ruleState;
        }

        internal Symbol GetCurrentNextSymbol()
        {
            return Rule.GetRightHandSideOfRule(RulePosition);
        }
    }
}
