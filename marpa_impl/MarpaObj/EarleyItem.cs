using System;
using System.Collections.Generic;
using Symbol = System.String;

namespace marpa_impl
{
    internal class EarleyItem
    {
        private DottedRule _dottedRule;
        private int _orignPosition;

        internal EarleyItem(Rule rule, int parentPosition)
        {
            _orignPosition = parentPosition;
            _dottedRule = new DottedRule(rule, 0);
        }
        internal EarleyItem(Rule rule, int parentPosition, int rulePosition)
        {
            _orignPosition = parentPosition;
            _dottedRule = new DottedRule(rule, rulePosition);
        }
        internal EarleyItem(DottedRule dottedRule, int parentPosition)
        {
            _dottedRule = dottedRule;
            _orignPosition = parentPosition;
        }
        internal int GetRulePosition()
        {
            return _dottedRule.GetPosition();
        }
        internal int GetOrignPosition()
        {
            return _orignPosition;
        }
        internal DottedRule GetDottedRule()
        {
            return _dottedRule;
        }
        internal Rule GetRule()
        {
            return _dottedRule.GetRule();
        }


        internal Symbol GetCurrentNextSymbol()
        {
            return IsCompleted() ? null : GetRule().GetRightHandSideOfRule(GetRulePosition());
        }
        internal List<Symbol> GetCurrentPostSymbolList()
        {
            List<Symbol> rhs = GetRule().GetRightHandSideOfRule();
            return IsCompleted() 
                ? null 
                : (rhs.Count - GetRulePosition()) == 1 
                    ? new List<Symbol>() 
                    : rhs.GetRange(GetRulePosition() + 1, rhs.Count - GetRulePosition() - 2);
        }
        
        internal bool IsCompleted()
        {
            return GetRulePosition() == GetRule().GetRightHandSideOfRule().Count;
        }
        
        internal Symbol GetItemPenult()
        {
            List<Symbol> rhs = GetCurrentPostSymbolList();
            Symbol nextSymbol = GetCurrentNextSymbol();

            if (nextSymbol != null && rhs != null && rhs.Count == 0)
                return nextSymbol;
            else return null;
        }

        public override bool Equals(object obj)
        {
            EarleyItem earleyItem = obj as EarleyItem;
            return GetDottedRule().Equals(earleyItem.GetDottedRule()) && GetOrignPosition() == earleyItem.GetOrignPosition();
        }
        public override String ToString()
        {
            return GetDottedRule().ToString() + " PP: " + _orignPosition;
        }
    }
}
