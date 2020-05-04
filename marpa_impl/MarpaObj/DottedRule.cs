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
    }
}
