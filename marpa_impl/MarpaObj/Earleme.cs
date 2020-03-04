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
        private int Id;
        private (int, int) PreviousEarleme;

        internal Earleme(Rule rule, int parentPosition, (int, int) prevoiusEarleme)
        {
            Rule = rule;
            ParentPosition = parentPosition;
            RulePosition = 0;
            PreviousEarleme = prevoiusEarleme;
        }

        internal Earleme(Rule rule, int parentPosition, (int, int) prevoiusEarleme, int rulePosition)
        {
            Rule = rule;
            ParentPosition = parentPosition;
            RulePosition = rulePosition;
            PreviousEarleme = prevoiusEarleme;
        }

        internal (int, int) GetPreviousEarleme()
        {
            return PreviousEarleme;
        }
        internal void SetId(int id)
        {
            Id = id;
        }
        internal int GetId()
        {
            return Id;
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
            return "ID "+ GetId()  + " FROM (" + GetPreviousEarleme().Item1 + " , " +  GetPreviousEarleme().Item2 + ")  " 
                + Rule.ToString() + " - at " + RulePosition + (IsCompleted() ? " COMPL " : "");
        }
    }
}
