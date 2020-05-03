using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class TreeNode
    {
        private List<TreeNode> Children;
        private Rule rule;
        private int startPosition;
        private int endPosition;
        public TreeNode(EarleyItem e, int setNumber)
        {
            rule = e.GetRule();
            startPosition = setNumber;
            endPosition = e.GetOrignPosition();
            Children = new List<TreeNode>();
        }

        public int GetEndPosition()
        {
            return endPosition;
        }
        public void AddChildren(TreeNode node)
        {
            Children.Add(node);
        }
        public void AddChildren(List<TreeNode> nodes)
        {
            Children.AddRange(nodes);
        }
    }
}
