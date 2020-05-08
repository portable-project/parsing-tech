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

        internal bool DoesChildExists(Symbol symbol, int left, int right)
        {
            if (_children.Count == 0) return false;

            TreeNode node = new TreeNode(symbol, left, right);
            for(int i = 0; i < _children.Count; i++)
            {
                if (_children[i].Equals(node)) return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            TreeNode node = obj as TreeNode;
            return _leftBorder == node._leftBorder && _rightBorder == node._rightBorder && _simpleNode == node._simpleNode && _complexNode.Equals(node._complexNode);
        }
    }
}
