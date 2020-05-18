using System.Collections.Generic;

namespace marpa_impl
{
    public struct AnalyseReport
    {
        public List<TreeNode> tree;
        public bool isSuccessful;
        internal AnalyseReport(List<TreeNode> Tree, bool IsSuccessful)
        {
            tree = Tree;
            isSuccessful = IsSuccessful;
        }
    }
}
