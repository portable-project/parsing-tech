using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace marpa_impl.GrammarDefenitionObjects
{
    public class GDL_Processor
    {
        public void TryProcessGrammarDefenition(List<string> files)
        {
            int mainFileIndex = GetMainGrammarDefenitionDocument(files);
            GetOuterDefenitionStructure(files[mainFileIndex], GDL_Type.RULE_SET);
        }

        private int GetMainGrammarDefenitionDocument(List<string> files)
        {
            // TODO: implement
            return 0;
        }

        private void GetOuterDefenitionStructure(string input, GDL_Type type)
        {
            GDL_Item languageItem = GrammarDefenitionLanguage.GetLanguageItemByType(type);
            if(languageItem.GetRegex() != null)
            {
                GroupCollection x = GetTokenValue(input, languageItem.GetRegex());
                if(languageItem.GetTokenList() == null)
                {
                    // TODO: construct some tree of lexems
                    return;
                }
                languageItem.GetTokenList().ForEach(token =>
                {
                    string tokenRep = token.ToString();
                    string inputPart = x[tokenRep].Value;
                    if (inputPart.Length > 0)
                    {
                        Console.WriteLine(tokenRep);
                        Console.WriteLine(inputPart);
                        Console.WriteLine("---------------------\n");
                        GetOuterDefenitionStructure(inputPart, token);
                    }
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
