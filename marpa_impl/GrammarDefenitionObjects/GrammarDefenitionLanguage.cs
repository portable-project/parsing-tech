using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    internal static class GrammarDefenitionLanguage
    {
        // part of regexp for rules = (rule|ruleSet)*
        // @"(?<RULE>[a-zA-Z]*:(\w|\W)*;)(\s)(?<RULES>(\w|\W)*)", new List<GDL_Type>() { GDL_Type.RULE, GDL_Type.RULES }),

        // ATTRIBUTE, RULES - нельзя регуляркой разобрать

        private static List<GDL_Item> _gdlTokenListOfRepearedTokens = new List<GDL_Item>()
        {
            new GDL_Item(GDL_Type.COMPLEX_NAME, @"(?<NAME>[^.]+)", new List<GDL_Type>() { GDL_Type.NAME }),
            new GDL_Item(
                GDL_Type.ATTRIBUTES,
                @"\[(?<ATTRIBUTE>[^\[|^\]]*)\]",
                new List<GDL_Type>() { GDL_Type.ATTRIBUTE }
            ),
            new GDL_Item(
                GDL_Type.IMPORTS,
                @"(\s)*"+@"(?<RULE_SET_IMPORT>([^\;])+)"+@";",
                new List<GDL_Type>() { GDL_Type.RULE_SET_IMPORT }
            ),
        };

        private static List<GDL_Item> _languageTokenList = new List<GDL_Item>()
        {
            new GDL_Item(GDL_Type.STRING, "\"[^\"\\]*(?:\\.[^\"\\]*)*\""),
            new GDL_Item(GDL_Type.CHARS, "'[^']*'"), 
            new GDL_Item(GDL_Type.ANY_CHAR, "'.'"), 
            new GDL_Item(GDL_Type.CHAR_CODE, "<hex>", new List<GDL_Type>() { GDL_Type.HEX }),
            new GDL_Item(GDL_Type.GROUP, "'('<expr>')'", new List<GDL_Type>() { GDL_Type.EXPRESSION }),
            new GDL_Item(GDL_Type.NAME, "[a-zA-Z_][a-zA-Z_0-9]*", null),
            new GDL_Item(GDL_Type.ALIAS, @"(?<NAME>(\w)*)(\s)*=",  new List<GDL_Type>() { GDL_Type.NAME }),
            new GDL_Item(GDL_Type.USAGE, @"(?<COMPLEX_NAME>(\w|\.)+)(\(?<USAGE_ARG_LIST>(.)*\)$)",  new List<GDL_Type>() { GDL_Type.COMPLEX_NAME, GDL_Type.USAGE_ARG_LIST }),
   
            new GDL_Item(
                GDL_Type.RULE_SET_IMPORT,
                @"(?<ATTRIBUTES>^\[[^{]*(\]))?"+@"(\s)*"+@"(?<ALIAS>(\w)*(\s)*=)?"+@"(\s)*"+@"(?<COMPLEX_NAME>[^{]+)", 
                new List<GDL_Type>() { GDL_Type.ATTRIBUTES, GDL_Type.ALIAS, GDL_Type.COMPLEX_NAME }
            ),
            new GDL_Item(
                        GDL_Type.RULE_SET,
                        @"(?<ATTRIBUTES>^\[[^{]*(\]))?"+@"(\s)*"+@"(?<COMPLEX_NAME>[^{|^\s]+)"+@"(\s)*"+@"{(\s)*"+@"(?<IMPORTS>[^:]+;)?"+@"(\s)*"+@"(?<RULES>(\w|\W)+)?}$",
                        new List<GDL_Type>() { GDL_Type.ATTRIBUTES, GDL_Type.COMPLEX_NAME, GDL_Type.IMPORTS, GDL_Type.RULES }
                        )
        };
        

        internal static GDL_Item GetLanguageItemByType(GDL_Type type)
        {
            return _languageTokenList.Find(item => item.GetItemType() == type);
        }

        internal static GDL_Item GetRepeatedLanguageItemByType(GDL_Type type)
        {
            return _gdlTokenListOfRepearedTokens.Find(item => item.GetItemType() == type);
        }
    }
}
