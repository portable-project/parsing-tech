using Symbol = System.String;
using System;

namespace marpa_impl
{
    internal class LeoItem
    {
        private DottedRule _dotteRule;
        private Symbol _symbol;
        private int _orignPosition;

        public LeoItem(EarleyItem ei, Symbol symbol)
        {
            _symbol = symbol;
            _dotteRule = ei.GetDottedRule();
            _orignPosition = ei.GetOrignPosition();
        }

        public LeoItem(DottedRule dottedRule, int position, Symbol symbol)
        {
            _symbol = symbol;
            _dotteRule = dottedRule;
            _orignPosition = position;
        }

        public DottedRule GetDottedRule()
        {
            return _dotteRule;
        }

        public Symbol GetSymbol()
        {
            return _symbol;
        }

        public int GetOrignPosition()
        {
            return _orignPosition;
        }
        public override String ToString()
        {
            return "LI | " + GetDottedRule().GetRule().ToString() + "\t RP: " + GetDottedRule().GetPosition() + "\t PP: " + _orignPosition;
        }
    }
}
