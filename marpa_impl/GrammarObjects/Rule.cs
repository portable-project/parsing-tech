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
        public Symbol GetRightHandSideOfRule(int position)
        {
            return RHS[position];
        }

        public void AddToRightHandSideOfRule(Symbol symbol)
        {
            if(RHS != null) RHS.Add(symbol);
        }
        public void AddToRightHandSideOfRule(List<Symbol> symbols)
        {
            if (RHS != null) RHS.AddRange(symbols);
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

        public override string ToString()
        {
            String rhs = "";
            RHS.ForEach((Symbol s) => {
                rhs += s.GetSymbolName();
            });
            return LHS.GetSymbolName() + " -> " + rhs;
        }

        public override bool Equals(object obj)
        {
            Rule compare = obj as Rule;
            
            List<Symbol> compareList = compare.GetRightHandSideOfRule();
            List <Symbol> thisList = this.GetRightHandSideOfRule();

            bool isRHSEqual = compareList.Count == thisList.Count;
            if (!isRHSEqual) return false;

            bool isLHSEqual = compare.GetLeftHandSideOfRule().GetSymbolName() 
                == this.GetLeftHandSideOfRule().GetSymbolName();
            if (!isLHSEqual) return false;

            for (int i =0; i< thisList.Count; i++)
            {
                isRHSEqual &= thisList[i].GetSymbolName() == compareList[i].GetSymbolName();
            }

            return isLHSEqual && isRHSEqual;
        }
    }
}