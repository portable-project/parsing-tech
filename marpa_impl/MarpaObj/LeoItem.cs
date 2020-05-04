using Symbol = System.String;

namespace marpa_impl
{
    class LeoItem
    {
        private Rule _rule;
        private Symbol _symbol;
        private int _orignPosition;

        public LeoItem(EarleyItem ei, Symbol symbol)
        {
            _symbol = symbol;
            _rule = ei.GetRule();
            _orignPosition = ei.GetOrignPosition();
        }

        public LeoItem(Rule rule, int position, Symbol symbol)
        {
            _symbol = symbol;
            _rule = rule;
            _orignPosition = position;
        }

        public Rule GetRule()
        {
            return _rule;
        }

        public Symbol GetSymbol()
        {
            return _symbol;
        }

        public int GetOrignPosition()
        {
            return _orignPosition;
        }
    }
}
