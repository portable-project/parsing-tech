using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    internal static class GrammarDefenitionLanguage
    {
        private static List<GDL_Item> _languageTokenList = new List<GDL_Item>()
        {
            new GDL_Item(GDL_Type.STRING, "\"[^\"\\]*(?:\\.[^\"\\]*)*\""),
            new GDL_Item(GDL_Type.CHARS, "'[^']*'"), 
            new GDL_Item(GDL_Type.ANY_CHAR, "'.'"), 
            new GDL_Item(GDL_Type.CHAR_CODE, "<hex>", new List<GDL_Type>() { GDL_Type.HEX }),
            new GDL_Item(GDL_Type.GROUP, "'('<expr>')'", new List<GDL_Type>() { GDL_Type.EXPRESSION }),
            new GDL_Item(GDL_Type.NAME, "[a-zA-Z_][a-zA-Z_0-9]*", null),
            new GDL_Item(GDL_Type.COMPLEX_NAME, @"(?<"+GDL_Type.NAME+@">(\w)+)(?<"+GDL_Type.COMPLEX_NAME+@">(.|(\w))*)", new List<GDL_Type>() { GDL_Type.NAME, GDL_Type.COMPLEX_NAME }),
            new GDL_Item(
                        GDL_Type.RULE_SET,
                        @"(?<"+GDL_Type.ATTRIBUTES+@">^\[[^{]*(\]))" + @"(?<"+GDL_Type.COMPLEX_NAME+@">[^{]+){(?<"+GDL_Type.RULE_SET_BODY +@">(\w|\W)+)}$",
                        // @"(?<"+GDL_Type.RULE_SET_HEAD+@">[^{]*){(?<"+GDL_Type.RULE_SET_BODY +@">(\w|\W)+)}$",
                        new List<GDL_Type>() { GDL_Type.ATTRIBUTES, GDL_Type.COMPLEX_NAME, GDL_Type.RULE_SET_BODY }
                        )
        };
        

        internal static GDL_Item GetLanguageItemByType(GDL_Type type)
        {
            return _languageTokenList.Find(item => item.GetItemType() == type);
        }
    }
}
