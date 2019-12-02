using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    class Symbol
    {
        private int SymbolId;

        public Symbol(int id)
        {
            SymbolId = id;
        }

        public int GetSymbolId()
        {
            return SymbolId;
        }
    }
}
