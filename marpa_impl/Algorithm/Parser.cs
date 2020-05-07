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
        internal void Parse(List<EarleySet> recogniserSets, List<EarleyItem> finalItems)
        {
            TreeNode root = new TreeNode("alpha", 0, recogniserSets.Count - 1);
            finalItems.ForEach(final => BuildTree(root, final));
        }

        private void BuildTree(TreeNode parent, EarleyItem current)
        {
            List<Symbol> prev = current.GetCurrentPrevSymbolList();
            List<Symbol> post = current.GetCurrentPostSymbolList();

            if (current.GetRule().GetRightHandSideOfRule()[0].Equals(_grammar.GetStartSymbol())) {

            } else if (prev.Count == 1 && _grammar.DoesBelongToTerminals(prev[0])) {

            } else if (prev.Count == 1 && !_grammar.DoesBelongToTerminals(prev[0])) {

            } else if (_grammar.DoesBelongToTerminals(prev[prev.Count - 1])) {

            } else if (!_grammar.DoesBelongToTerminals(prev[prev.Count - 1])) {

            }
        }
    }
}
