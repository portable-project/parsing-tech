using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class Utils
    {
        internal static void PrintSeparator(int size)
        {
            for (int i = 0; i < size; i++) Console.WriteLine();
        }

        internal static void PrintSets(List<EarleySet> setsToPrint, bool all)
        {
            if (setsToPrint == null) return;

            for (int i = 0; i < setsToPrint.Count; i++)
            {
                Console.WriteLine("\n <=============================SET=================================> " + i);
                EarleySet set = setsToPrint[i];

                List<EarleyItem> items = set.GetEarleyItemList();
                for (int k = 0; k < items.Count; k++)
                {
                    EarleyItem e = items[k];
                    if (!all)
                    {
                        if (e.IsCompleted()) Console.WriteLine("\t\t" + e.ToString());
                    }
                    else
                    {
                        Console.WriteLine((e.IsCompleted() ? "\t\t" : "\t") + e.ToString());
                    }
                }

                Console.WriteLine("\n <--------> ");
                List<LeoItem> transitiveItems = set.GetLeoItemList();
                for (int k = 0; k < transitiveItems.Count; k++)
                {
                    LeoItem e = transitiveItems[k];
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine("\n <=====================================================================> " + i);
            }
        }

        internal static void PrintTree(TreeNode root, int space)
        {
            Console.WriteLine(root.ToString());
            root.GetTreeChildrenNodes().ForEach(node => {
                for (int i = 0; i < space-1; i++) Console.Write("\t");
                Console.WriteLine("\t|");
                
                for (int i =0; i< space; i++) Console.Write("\t");
                Console.Write("|-----");
                
                PrintTree(node, space+1);

            });
        }

        internal static void FormTreeDesctiptionForDraw(TreeNode root)
        {
            List<TreeNode> children = root.GetTreeChildrenNodes();
            for(int i = children.Count; i> 0; i--){
                TreeNode node = children[i - 1];
                Console.WriteLine('[' + root.ToString() + "]->["+node.ToString()+']');
                FormTreeDesctiptionForDraw(node);
            };
        }
    }
}
