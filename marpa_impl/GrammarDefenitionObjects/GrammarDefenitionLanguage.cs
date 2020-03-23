using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    internal static class GrammarDefenitionLanguage
    {
        internal static List<GDL_Item> GetSimpleExpressionList()
        {
            return new List<GDL_Item>
            {
                new GDL_Item(GDL_Type.STRING, "\"[^\"\\]*(?:\\.[^\"\\]*)*\""),
                new GDL_Item(GDL_Type.CHARS, "'[^']*'"),
                new GDL_Item(GDL_Type.ANY_CHAR, "'.'"),
                new GDL_Item(GDL_Type.CHAR_CODE, "<hex>", new List<GDL_Type>(){ GDL_Type.HEX }),
                new GDL_Item(GDL_Type.GROUP, "'('<expr>')'", new List<GDL_Type>(){ GDL_Type.EXPRESSION }),
                new GDL_Item(GDL_Type.CHECK, "'&'<simple>", new List<GDL_Type>(){ GDL_Type.SIMPLE }),
                new GDL_Item(GDL_Type.CHECK_NOT, "'!'<simple>", new List<GDL_Type>(){ GDL_Type.SIMPLE }),
            }; ;
        }

        internal static GDL_Item GetRuleSet()
        {
            return
                new GDL_Item(
                    GDL_Type.RULE_SET,
                    @"(?<ruleset_head>[^{]*){(?<ruleset_body>(\w|\W)+)}$", 
                    new List<GDL_Type>() { GDL_Type.RULE_SET_HEAD, GDL_Type.RULE_SET_BODY }
                );
        }
    }
}
