using System;
using Symbol = System.String;

namespace marpa_impl
{
    internal class EarleyItem
    {
        private Rule _rule;
        private int _rulePosition; 
        private int _orignPosition;

        internal EarleyItem(Rule rule, int parentPosition)
        {
            _rule = rule;
            _orignPosition = parentPosition;
            _rulePosition = 0;
        }

        internal EarleyItem(Rule rule, int parentPosition, int rulePosition)
        {
            _rule = rule;
            _orignPosition = parentPosition;
            _rulePosition = rulePosition;
        }

        internal Symbol GetCurrentNextSymbol()
        {
            return IsCompleted() ? null : _rule.GetRightHandSideOfRule(_rulePosition);
        }
        internal int GetRulePosition()
        {
            return _rulePosition;
        }
        internal int GetOrignPosition()
        {
            return _orignPosition;
        }
        internal bool IsCompleted()
        {
            return GetRulePosition() == _rule.GetRightHandSideOfRule().Count;
        }
        internal Rule GetRule()
        {
           return _rule;
        }
        public override String ToString()
        {
            return _rule.ToString() + "\t RP: " + _rulePosition + "\t PP: " + _orignPosition;
        }
    }
}
