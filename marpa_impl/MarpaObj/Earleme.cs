using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    class Earleme
    {
        private Rule Rule;
        private int RulePosition;
        private int ParentPosition;
        private RuleState state;

        public Earleme(int origin, Rule rule)
        {
            Rule = rule;
            ParentPosition = origin;
            RulePosition = 0;
            state = RuleState.PREDICTED;
        }

        public Earleme(int origin, Rule rule, RuleState ruleState)
        {
            Rule = rule;
            ParentPosition = origin;
            RulePosition = 0;
            state = ruleState;
        }
    }
}
