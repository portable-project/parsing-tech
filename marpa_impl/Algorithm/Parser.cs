using System;
using System.Collections.Generic;

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
            _sets = new List<EarleySet>();
        }
        internal void Parse(List<EarleySet> recogniserSets)
        {

        }
    }
}
