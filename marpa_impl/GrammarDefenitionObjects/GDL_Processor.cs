using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace marpa_impl.GrammarDefenitionObjects
{
    public class GDL_Processor
    {
        public void TryProcessGrammarDefenition(string input)
        {
            GetOuterDefenitionStructure(input, GDL_Type.RULE_SET);
        }

        private void GetOuterDefenitionStructure(string input, GDL_Type type)
        {
            GDL_Item languageItem = GrammarDefenitionLanguage.GetLanguageItemByType(type);
            if(languageItem.GetRegex() != null)
            {
                GroupCollection x = GetTokenValue(input, languageItem.GetRegex());
                languageItem.GetTokenList().ForEach(token =>
                {
                    string tokenRep = token.ToString();
                    string inputPart = x[tokenRep].Value;
                    Console.WriteLine(tokenRep);
                    Console.WriteLine(inputPart);
                    Console.WriteLine("---------------------\n");
                    GetOuterDefenitionStructure(inputPart, token);
                });
            }
        }

        private GroupCollection GetTokenValue(string str, string pattern)
        {
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(str);
            return match.Groups;
        }
    }
}
