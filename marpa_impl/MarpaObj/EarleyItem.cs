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
        private List<ItemLink> _predecessorLinkList;
        private List<ItemLink> _reducorLinkList;
        private bool isProcessed = false;
        private int _setNumber;
        internal EarleyItem(int setNumber, Rule rule, int parentPosition)
        {
            _orignPosition = parentPosition;
            _dottedRule = new DottedRule(rule, 0);
            _predecessorLinkList = new List<ItemLink>();
            _reducorLinkList = new List<ItemLink>();
            _setNumber = setNumber;
        }
        internal EarleyItem(int setNumber, DottedRule dottedRule, int parentPosition)
        {
            _dottedRule = dottedRule;
            _orignPosition = parentPosition;
            _predecessorLinkList = new List<ItemLink>();
            _reducorLinkList = new List<ItemLink>();
            _setNumber = setNumber;
        }
        internal int GetSetNumber()
        {
            return _setNumber;
        }
        internal bool DoesItemHaveLinks()
        {
            return _predecessorLinkList.Count > 0 || _reducorLinkList.Count > 0;
        }
        internal void AddPredecessorLink(EarleyItem link, int label)
        {
            _predecessorLinkList.Add(new ItemLink(link, label));
        }
        internal void AddReducerLink(EarleyItem link, int label)
        {
            _reducorLinkList.Add(new ItemLink(link, label));
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

        internal bool IsItemProcessed()
        {
            return isProcessed;
        }
        internal void SetItemIsProcessed()
        {
            isProcessed = true;
        }
        internal List<ItemLink> GetPredecessorLinks()
        {
            return _predecessorLinkList;
        }
        internal List<ItemLink> GetReducerLinks()
        {
            return _reducorLinkList;
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
            int currentRulePosition = GetRulePosition();
            return currentRulePosition == 0
                    ? new List<Symbol>()
                    : rhs.GetRange(0, currentRulePosition);
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
