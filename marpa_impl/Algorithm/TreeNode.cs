using System;
using System.Collections.Generic;
using System.Text;
using Symbol = System.String;

namespace marpa_impl
{
    public class TreeNode
    {
        private int _leftBorder;
        private int _rightBorder;
        private Symbol _simpleNode;
        private DottedRule _complexNode;

        private List<TreeNode> _children;
        internal TreeNode(Symbol nonTerminal, int leftBorder, int rightBorder)
        {
            _leftBorder = leftBorder;
            _rightBorder = rightBorder;
            _children = new List<TreeNode>();
            _simpleNode = nonTerminal;
        }
        internal TreeNode(DottedRule rule, int leftBorder, int rightBorder)
        {
            _leftBorder = leftBorder;
            _rightBorder = rightBorder;
            _children = new List<TreeNode>();
            _complexNode = rule;
        }
        
        internal void AddChild(TreeNode node)
        {
            _children.Add(node);
        }
    }
}
