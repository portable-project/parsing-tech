using System;

namespace marpa_impl
{
    public class Symbol
    {
        private String SymbolName;
        public Symbol(String name)
        {
            SymbolName = name;
        }
        public String GetSymbolName()
        {
            return SymbolName;
        }
        public override string ToString()
        {
            return SymbolName;
        }
        public override bool Equals(object obj)
        {
            Symbol compare = obj as Symbol;
            return SymbolName.Equals(compare.GetSymbolName());
        }
    }
}
