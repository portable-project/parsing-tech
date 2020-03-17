﻿using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class Earleme
    {
        private Rule Rule;
        private int RulePosition;
        private int ParentPosition;

        internal Earleme(Rule rule, int parentPosition)
        {
            Rule = rule;
            ParentPosition = parentPosition;
            RulePosition = 0;
        }

        internal Earleme(Rule rule, int parentPosition, int rulePosition)
        {
            Rule = rule;
            ParentPosition = parentPosition;
            RulePosition = rulePosition;
        }

        internal Symbol GetCurrentNextSymbol()
        {
            return IsCompleted() ? null : Rule.GetRightHandSideOfRule(RulePosition);
        }
        internal int GetRulePosition()
        {
            return RulePosition;
        }
        internal int GetParentPosition()
        {
            return ParentPosition;
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
            return Rule.ToString() + " - in " + RulePosition + " - from " + ParentPosition;
        }
    }
}
