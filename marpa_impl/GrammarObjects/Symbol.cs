using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Symbol
    {
        private int SymbolId;
        private String SymbolName;

        public Symbol(String name)
        {
            SymbolName = name;
        }
        public String GetSymbolName()
        {
            return SymbolName;
        }
        internal int GetSymbolId()
        {
            return SymbolId;
        }
        internal void SetSymbolId(int Id)
        {
            SymbolId = Id;
        }
    }
}
