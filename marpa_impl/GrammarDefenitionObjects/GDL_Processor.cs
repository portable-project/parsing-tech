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
            GDL_Node node = GetOuterDefenitionStructure(files[mainFileIndex], GDL_Type.RULE_SET);
        }

        private int GetMainGrammarDefenitionDocument(List<string> files)
        {
            // TODO: implement
            return 0;
        }

        private GDL_Node GetOuterDefenitionStructure(string input, GDL_Type type)
        {
            List<GDL_Node> children = new List<GDL_Node>();

            GDL_Item languageItem = GrammarDefenitionLanguage.GetLanguageItemByType(type);
            if(languageItem.GetRegex() != null)
            {
                GroupCollection x = GetTokenValue(input, languageItem.GetRegex());
                if (languageItem.GetTokenList() != null)
                {
                    languageItem.GetTokenList().ForEach(token =>
                    {
                        string tokenRep = token.ToString();
                        string inputPart = x[tokenRep].Value;
                        if (inputPart.Length > 0)
                        {
                            Console.WriteLine(tokenRep);
                            Console.WriteLine(inputPart);
                            Console.WriteLine("---------------------\n");
                            children.Add(GetOuterDefenitionStructure(inputPart, token));
                        }
                    });
                }
            }

            return new GDL_Node(type, input, children);
        }

        private GroupCollection GetTokenValue(string str, string pattern)
        {
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(str);
            return match.Groups;
        }
    }
}
