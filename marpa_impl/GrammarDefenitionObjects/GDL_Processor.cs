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
            switch (type)
            {
                case GDL_Type.RULES: {
                        List<string> allRules = GetRules(input);
                        break; 
                    }
                case GDL_Type.ATTRIBUTE: { 
                        break; 
                    }
                default: {
                        children = GetInnerItems(input, type);
                        break;
                    }
            }

            return new GDL_Node(type, input, children);
        }

        private List<string> GetRules(string input)
        {
            int bracesCount = 0;
            char[] inputAsArray = input.Trim(' ').ToCharArray();
            List<int> splitIndexes = new List<int>();
            splitIndexes.Add(0);
            for (int i = 0; i< inputAsArray.Length; i++)
            {
                char el = inputAsArray[i];
                if (i > 0 && !inputAsArray[i-1].Equals('\'')) {
                    if (el.Equals('{')) bracesCount++;
                    if (el.Equals('}'))
                    {
                        bracesCount--;
                    }
                }

                if (bracesCount == 0 && el.Equals(';'))
                {
                    splitIndexes.Add(i+1);
                }
            }
            splitIndexes.Add(inputAsArray.Length);


            List<string> allRules = new List<string>();
            for (int j=0; j< splitIndexes.Count - 1; j++)
            {
                allRules.Add(input.Substring(splitIndexes[j], splitIndexes[j + 1] - splitIndexes[j]).Trim(' '));
            }
            allRules.RemoveAt(allRules.Count - 1);

            return allRules;
        }

        private List<GDL_Node> GetInnerItems(string input, GDL_Type type)
        {
            List<GDL_Node> children = new List<GDL_Node>();

            GDL_Item languageItem = GrammarDefenitionLanguage.GetLanguageItemByType(type);
            if (languageItem.GetRegex() != null)
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
                            // Console.WriteLine(tokenRep);
                            // Console.WriteLine(inputPart);
                            // Console.WriteLine("---------------------\n");
                            children.Add(GetOuterDefenitionStructure(inputPart, token));
                        }
                    });
                }
            }

            return children;
        }

        private GroupCollection GetTokenValue(string str, string pattern)
        {
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(str);
            return match.Groups;
        }
    }
}
