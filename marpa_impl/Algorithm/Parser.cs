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
        internal List<TreeNode> Parse(List<EarleySet> recogniserSets)
        {
            Symbol startSymbol = _grammar.GetStartSymbol();
            List<TreeNode> forest = new List<TreeNode>();

            recogniserSets[recogniserSets.Count - 1].GetEarleyItemList().ForEach(item => {
                if (item.GetRule().GetLeftHandSideOfRule().Equals(startSymbol) && item.IsCompleted() && item.GetOrignPosition() == 0)
                {
                    TreeNode root = new TreeNode(startSymbol, 0, recogniserSets.Count - 1);
                    BuildTree(root, item);
                    forest.Add(root);
                }
            });

            return forest;
        }

        private void BuildTree(TreeNode parent, EarleyItem current)
        {
            current.SetItemIsProcessed();

            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            List<Symbol> prev = current.GetCurrentPrevSymbolList();
            int setNumber = current.GetSetNumber();

            if (IsEmptyRule(current))
            {

                if (!parent.DoesChildExists(new TreeNode(lhs, setNumber, setNumber)))
                {
                    parent.AddChild(new TreeNode(_grammar.GetNullStringSymbol(), setNumber, setNumber));
                }

            }
            else if (prev.Count == 1 && IsLastSymbolBeforeDotTerminal(prev))
            {
                Symbol symBeforeDot = prev[prev.Count - 1];

                if (!parent.DoesChildExists(new TreeNode(symBeforeDot, setNumber, setNumber)))
                {
                    parent.AddChild(new TreeNode(symBeforeDot, setNumber - 1, setNumber));
                }

            }
            else if (prev.Count == 1 && IsLastSymbolBeforeDotNonTerminal(prev))
            {
                Symbol symBeforeDot = prev[prev.Count - 1];

                TreeNode newNode = new TreeNode(symBeforeDot, current.GetOrignPosition(), setNumber);
                if (!parent.DoesChildExists(newNode)) parent.AddChild(newNode);

                current.GetReducerLinks().ForEach(el =>
                {
                    if (el._label == current.GetOrignPosition() && !el._link.IsItemProcessed())
                    {
                        BuildTree(newNode, el._link);
                    }
                });

            }
            else if (IsLastSymbolBeforeDotTerminal(prev))
            {
                Symbol symBeforeDot = prev[prev.Count - 1];

                TreeNode newNode = new TreeNode(symBeforeDot, setNumber - 1, setNumber);
                if (!parent.DoesChildExists(newNode)) parent.AddChild(newNode);

                TreeNode newComplexNode = new TreeNode(
                    new DottedRule(current.GetRule(), current.GetRulePosition() - 1),
                    current.GetOrignPosition(),
                    setNumber - 1
                );
                if (!parent.DoesChildExists(newComplexNode)) parent.AddChild(newComplexNode);

                current.GetPredecessorLinks().ForEach(link =>
                {
                    if (link._label == setNumber - 1 && !link._link.IsItemProcessed())
                    {
                        BuildTree(newComplexNode, link._link);
                    }
                });
            }
            else if (IsLastSymbolBeforeDotNonTerminal(prev))
            {
                Symbol symBeforeDot = prev[prev.Count - 1];

                current.GetReducerLinks().ForEach(item =>
                {
                    TreeNode newNode = new TreeNode(symBeforeDot, item._label, setNumber);
                    if (!parent.DoesChildExists(newNode)) parent.AddChild(newNode);
                    if (!item._link.IsItemProcessed()) BuildTree(newNode, item._link);

                    TreeNode newComplexNode = new TreeNode(
                        new DottedRule(current.GetRule(), current.GetRulePosition() - 1), 
                        current.GetOrignPosition(),
                        item._label
                    );
                    if (!parent.DoesChildExists(newComplexNode)) parent.AddChild(newComplexNode);
                    current.GetPredecessorLinks().ForEach(predItem =>
                    {
                        if (!predItem._link.IsItemProcessed()) BuildTree(newComplexNode, predItem._link);
                    });
                });
            };
        }

        private bool IsEmptyRule(EarleyItem current)
        {
            return current.GetRule().GetRightHandSideOfRule()[0].Equals(_grammar.GetNullStringSymbol());
        }

        private bool IsLastSymbolBeforeDotTerminal(List<Symbol> prev)
        {
            if (prev.Count == 0) return false;
            Symbol symBeforeDot = prev[prev.Count - 1];
            return _grammar.DoesBelongToTerminals(symBeforeDot);
        }

        private bool IsLastSymbolBeforeDotNonTerminal(List<Symbol> prev)
        {
            if (prev.Count == 0) return false;
            Symbol symBeforeDot = prev[prev.Count - 1];
            return !_grammar.DoesBelongToTerminals(symBeforeDot);
        }
    }
}
