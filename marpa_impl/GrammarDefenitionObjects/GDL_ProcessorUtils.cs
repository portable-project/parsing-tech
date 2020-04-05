using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace marpa_impl.GrammarDefenitionObjects
{
    class GDL_ProcessorUtils
    {
        internal static List<GDL_Node> GetInnerItems(string input, GDL_Type type)
        {
            List<GDL_Node> children = new List<GDL_Node>();

            GDL_Item languageItem = GrammarDefenitionLanguage.GetLanguageItemByType(type);
            if (languageItem.GetItemType() != GDL_Type.DEFAULT)
            {
                children.AddRange(GetListOfElements(input, languageItem));
            }
            else
            {
                languageItem = GrammarDefenitionLanguage.GetRepeatedLanguageItemByType(type);
                if (languageItem.GetItemType() != GDL_Type.DEFAULT)
                {
                    children.AddRange(GetListOfRepeatedElements(input, languageItem));
                }
                else
                {
                    Console.WriteLine("No regexp for this type: " + type);
                    children.Add(new GDL_Node(type, input, null));
                }
            }

            return children;
        }

        internal static List<GDL_Node> GetListOfRepeatedElements(string input, GDL_Item languageItem)
        {
            List<GDL_Node> children = new List<GDL_Node>();
            GDL_Type tokenType = languageItem.GetTokenList()[0];
            MatchCollection matches = GetMultipleTokens(input, languageItem.GetRegex());
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    children.Add(GDL_Processor.GetOuterDefenitionStructure(match.Value, tokenType));

            }
            return children;

        }

        internal static List<GDL_Node> GetListOfElements(string input, GDL_Item languageItem)
        {
            List<GDL_Node> children = new List<GDL_Node>();
            GroupCollection x = GetToken(input, languageItem.GetRegex());
            if (languageItem.GetTokenList() != null)
            {
                languageItem.GetTokenList().ForEach(token =>
                {
                    string tokenRep = token.ToString();
                    string inputPart = x[tokenRep].Value;
                    if (inputPart.Length > 0)
                    {
                        children.Add(GDL_Processor.GetOuterDefenitionStructure(inputPart, token));
                    }
                });
            }
            return children;
        }

        internal static GroupCollection GetToken(string str, string pattern)
        {
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(str);
            return match.Groups;
        }

        internal static MatchCollection GetMultipleTokens(string str, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Matches(str);
        }
    }
}
