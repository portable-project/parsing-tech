using System.Collections.Generic;
using System;

namespace marpa_impl
{
    public class Rule
    {
        private Symbol LHS = null;
        private List<Symbol> RHS = null;

        public Rule(Symbol lhs, List<Symbol> rhs)
        {
            LHS = lhs;
            RHS = rhs;
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