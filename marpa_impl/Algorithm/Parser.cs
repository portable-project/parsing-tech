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
            current.SetItemIsProcessed();

            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            List<Symbol> prev = current.GetCurrentPrevSymbolList();
            Symbol symBeforeDot = prev[prev.Count - 1];
            int setNumber = current.GetSetNumber();

            if (IsEmptyRule(current)) {
                
                if(!parent.DoesChildExists(new TreeNode (lhs, setNumber, setNumber)))
                {
                    parent.AddChild(new TreeNode(_grammar.GetNullStringSymbol(), setNumber, setNumber));
                }

            } 
            else if (prev.Count == 1 && IsLastSymbolBeforeDotTerminal(prev)) {
                
                if (!parent.DoesChildExists(new TreeNode(symBeforeDot, setNumber, setNumber)))
                {
                    parent.AddChild(new TreeNode(symBeforeDot, setNumber-1, setNumber));
                }

            } 
            else if (prev.Count == 1 && IsLastSymbolBeforeDotNonTerminal(prev)) {

                TreeNode newNode = new TreeNode(symBeforeDot, current.GetOrignPosition(), setNumber);
                if (!parent.DoesChildExists(newNode))
                {
                    parent.AddChild(newNode);
                    current.GetReducerLinks().ForEach(el =>
                    {
                        if(el._label == current.GetOrignPosition() && !el._link.IsItemProcessed())
                        {
                            BuildTree(newNode, el._link);
                        }
                    });
                }

            } 
            else if (IsLastSymbolBeforeDotTerminal(prev)) {

                TreeNode newNode = new TreeNode(symBeforeDot, setNumber - 1, setNumber);
                if (!parent.DoesChildExists(newNode))
                {
                    parent.AddChild(newNode);

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
