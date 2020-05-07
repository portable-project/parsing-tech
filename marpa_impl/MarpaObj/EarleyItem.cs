using System;
using System.Collections.Generic;
using Symbol = System.String;

namespace marpa_impl
{
    internal struct ItemLink
    {
        internal EarleyItem _link;
        internal int _label;

        internal ItemLink(EarleyItem link, int label)
        {
            _label = label;
            _link = link;
        }
    }
    internal class EarleyItem
    {
        private DottedRule _dottedRule;
        private int _orignPosition;
        private ItemLink? _predecessorLink = null;
        private ItemLink? _reducorLink = null;

        internal EarleyItem(Rule rule, int parentPosition)
        {
            _orignPosition = parentPosition;
            _dottedRule = new DottedRule(rule, 0);
        }
        internal EarleyItem(DottedRule dottedRule, int parentPosition)
        {
            _dottedRule = dottedRule;
            _orignPosition = parentPosition;
        }
        internal bool DoesItemHaveLinks()
        {
            return _predecessorLink != null || _reducorLink != null;
        }
        internal void SetPredecessorLink(EarleyItem link, int label)
        {
            _predecessorLink = new ItemLink(link, label);
        }
        internal void SetReducerLink(EarleyItem link, int label)
        {
            _reducorLink = new ItemLink(link, label);
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
        internal List<Symbol> GetCurrentPrevSymbolList()
        {
            List<Symbol> rhs = GetRule().GetRightHandSideOfRule();
            return GetRulePosition() == 0
                    ? new List<Symbol>()
                    : rhs.GetRange(0, GetRulePosition() - 1);
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
        public override Symbol ToString()
        {
            return GetDottedRule().ToString() + " PP: " + _orignPosition;
        }
    }
}
