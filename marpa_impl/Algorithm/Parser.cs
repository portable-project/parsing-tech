using System;
using System.Collections.Generic;
using Symbol = System.String;

namespace marpa_impl
{
    internal class Parser
    {
        private Grammar _grammar;
        private ErrorHandler _errorHandler;
        internal Parser(Grammar grammar, ErrorHandler errorHandler)
        {
            _grammar = grammar;
            _errorHandler = errorHandler;
        }
        internal void Parse(List<EarleySet> recogniserSets)
        {
            TreeNode root = new TreeNode();
        }
    }
}
