using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    class GDL_Node
    {
        private GDL_Type _type;
        private string _value;
        private List<GDL_Node> _children;

        public GDL_Node(GDL_Type type, string value, List<GDL_Node> children)
        {
            _type = type;
            _value = value;
            _children = children;
        }
    }
}
