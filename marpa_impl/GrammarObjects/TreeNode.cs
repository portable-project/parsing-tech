using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class TreeNode
    {
        private List<TreeNode> Children;
        private Earleme Earleme;
        public TreeNode(Earleme e)
        {
            Earleme = e;
            Children = new List<TreeNode>();
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
