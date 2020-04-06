using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    class GDL_ComplexProcessor
    {
        internal static List<GDL_Node> GetUsageArgsListItems(string input)
        {
            List<GDL_Node> children = new List<GDL_Node>();

            int quoteCount = 0;
            int lastSplitPosition = 0;
            int bracesCount = 0;
            int bracketsCount = 0;
            char[] inputAsArray = input.Trim(' ').ToCharArray();
            for (int i = 0; i < inputAsArray.Length; i++)
            {
                char prev = i > 0 ? inputAsArray[i - 1] : ' ';
                char next = i+1 < inputAsArray.Length ? inputAsArray[i + 1] : ' ';
                char el = inputAsArray[i];
                

                if (el.Equals('\''))
                {
                    quoteCount++;
                    continue;
                } 
                else if (el.Equals('{')) bracesCount++;
                else if (el.Equals('}')) bracesCount--;
                else if (el.Equals('<')) bracketsCount++;
                else if (el.Equals('>')) bracketsCount--;

                
                if ((next.Equals(',') || i + 1 == inputAsArray.Length) && bracketsCount == 0 && bracesCount == 0)
                {
                    if ((next.Equals('\'') || prev.Equals('\'')) && quoteCount % 2 != 0) continue;

                    string part = input.Substring(lastSplitPosition + 1, i - lastSplitPosition);
                    children.Add(GDL_Processor.GetOuterDefenitionStructure(part, GDL_Type.EXPRESSION));
                    lastSplitPosition = i + 1;
                }
            }

            return children;
        }

        internal static List<GDL_Node> GetAttributeItems(string input)
        {
            List<GDL_Node> children = new List<GDL_Node>();

            int lastSplitPosition = 0;
            int bracesCount = 0;
            char[] inputAsArray = input.Trim(' ').ToCharArray();
            for (int i = 0; i < inputAsArray.Length; i++)
            {
                char prev = i > 0 ? inputAsArray[i - 1] : ' ';
                char next = i + 1 < inputAsArray.Length ? inputAsArray[i + 1] : ' ';
                char el = inputAsArray[i];
                if (!prev.Equals('\'') && (!next.Equals('\'')))
                {
                    if (el.Equals('(')) bracesCount++;
                    if (el.Equals(')'))
                    {
                        bracesCount--;
                        
                    }
                    
                    if(bracesCount == 0 && (i + 1 == inputAsArray.Length || i + 1 < inputAsArray.Length && inputAsArray[i + 1] == ',') )
                    {
                        string part = input.Substring(lastSplitPosition + 1, i - lastSplitPosition);
                        children.Add(GDL_Processor.GetOuterDefenitionStructure(part, GDL_Type.USAGE));

                        if (i + 1 < inputAsArray.Length && inputAsArray[i + 1] == ',') lastSplitPosition = i + 2;
                    }
                }
            }

            return children;
        }

        internal static GDL_Node GetRuleOrRulesetStructure(string input)
        {
            string[] parts = input.Split(':');
            return null;
        }
        internal static List<string> GetRulesItems(string input)
        {
            int bracesCount = 0;
            char[] inputAsArray = input.Trim(' ').ToCharArray();
            List<int> splitIndexes = new List<int>();

            splitIndexes.Add(0);
            for (int i = 0; i < inputAsArray.Length; i++)
            {
                char prev = i > 0 ? inputAsArray[i - 1] : ' ';
                char el = inputAsArray[i];
                if (!prev.Equals('\''))
                {
                    if (el.Equals('{')) bracesCount++;
                    if (el.Equals('}'))
                    {
                        bracesCount--;
                        if (bracesCount == 0) // && !prev.Equals('\'') && el.Equals(';'))
                        {
                            splitIndexes.Add(i + 1);
                        }
                    }
                }
            }
            splitIndexes.Add(inputAsArray.Length);


            List<string> allRules = new List<string>();
            for (int j = 0; j < splitIndexes.Count - 1; j++)
            {
                allRules.Add(input.Substring(splitIndexes[j], splitIndexes[j + 1] - splitIndexes[j]).Trim(' '));
            }
            allRules.RemoveAt(allRules.Count - 1);

            return allRules;
        }
    }
}
