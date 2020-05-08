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
            finalItems.ForEach(final => BuildTree(root, final, recogniserSets.Count - 1));
        }

        private void BuildTree(TreeNode parent, EarleyItem current, int setNumber)
        {
            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            List<Symbol> prev = current.GetCurrentPrevSymbolList();
            Symbol symBeforeDot = prev[prev.Count - 1];

            if (IsEmptyRule(current)) {
                
                if(!parent.DoesChildExists(lhs, setNumber, setNumber))
                {
                    parent.AddChild(new TreeNode(_grammar.GetNullStringSymbol(), setNumber, setNumber));
                }

            } 
            else if (prev.Count == 1 && IsLastSymbolBeforeDotTerminal(prev)) {
                
                if (!parent.DoesChildExists(symBeforeDot, setNumber, setNumber))
                {
                    parent.AddChild(new TreeNode(symBeforeDot, setNumber-1, setNumber));
                }

            } 
            else if (prev.Count == 1 && IsLastSymbolBeforeDotNonTerminal(prev)) {
                
                if (!parent.DoesChildExists(symBeforeDot, current.GetOrignPosition(), setNumber))
                {
                    parent.AddChild(new TreeNode(symBeforeDot, current.GetOrignPosition(), setNumber));
                    
                    // more
                }

            } 
            else if (IsLastSymbolBeforeDotTerminal(prev)) {
                
                if (!parent.DoesChildExists(symBeforeDot, setNumber - 1, setNumber))
                {
                    parent.AddChild(new TreeNode(symBeforeDot, setNumber - 1, setNumber));

                    // more
                }

            } 
            else if (IsLastSymbolBeforeDotNonTerminal(prev)) {

            }
        }

        private bool IsEmptyRule(EarleyItem current)
        {
            return current.GetRule().GetRightHandSideOfRule()[0].Equals(_grammar.GetNullStringSymbol());
        }

        private bool IsLastSymbolBeforeDotTerminal(List<Symbol> prev)
        {
            Symbol symBeforeDot = prev[prev.Count - 1];
            return _grammar.DoesBelongToTerminals(symBeforeDot);
        }

        private bool IsLastSymbolBeforeDotNonTerminal(List<Symbol> prev)
        {
            Symbol symBeforeDot = prev[prev.Count - 1];
            return !_grammar.DoesBelongToTerminals(symBeforeDot);
        }
    }
}
