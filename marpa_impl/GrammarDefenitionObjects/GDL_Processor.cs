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
            GroupCollection x = GetTokenValue(input, GrammarDefenitionLanguage.GetRuleSet().GetRegex());
            Console.WriteLine(x["ruleset_head"].Value);
            Console.WriteLine(x["ruleset_body"].Value);
        }

        private GroupCollection GetTokenValue(string str, string pattern)
        {
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(str);
            return match.Groups;
        }
    }
}
