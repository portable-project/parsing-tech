using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class DottedRule
    {
        private Rule _rule;
        private int _position;
        public DottedRule(Rule rule, int position)
        {
            _rule = rule;
            _position = position;
        }

        public Rule GetRule()
        {
            return _rule;
        }
        public int GetPosition()
        {
            return _position;
        }

        public override bool Equals(object obj)
        {
            DottedRule dottedRule = obj as DottedRule;
            return GetPosition() == dottedRule.GetPosition() && GetRule().Equals(dottedRule.GetRule());
        }

        public override string ToString()
        {
            return GetRule().ToString() + " RP: " + GetPosition();
        }
    }
}
