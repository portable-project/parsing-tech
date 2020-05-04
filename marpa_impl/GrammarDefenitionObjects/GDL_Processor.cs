using System;
using System.Collections.Generic;

namespace marpa_impl.GrammarDefenitionObjects
{
    internal class GDL_Processor
    {
        public void TryProcessGrammarDefenition(List<string> files)
        {
            int mainFileIndex = GetMainGrammarDefenitionDocument(files);
            GDL_Node node = GetOuterDefenitionStructure(files[mainFileIndex], GDL_Type.RULE_SET);
        }

        private int GetMainGrammarDefenitionDocument(List<string> files)
        {
            // TODO: implement
            return 0;
        }

        internal static GDL_Node GetOuterDefenitionStructure(string input, GDL_Type type)
        {
            List<GDL_Node> children = new List<GDL_Node>();
            switch (type)
            {
                case GDL_Type.RULES: {
                        children.AddRange(GDL_ComplexProcessor.GetRulesItems(input));
                        break; 
                    }
                case GDL_Type.ATTRIBUTE: {
                        children.AddRange(GDL_ComplexProcessor.GetAttributeItems(input));
                        break; 
                    }
                case GDL_Type.USAGE_ARG_LIST:
                    {
                        children.AddRange(GDL_ComplexProcessor.GetUsageArgsListItems(input));
                        break;
                    }
                default: {
                        children = GDL_ProcessorUtils.GetInnerItems(input, type);
                        break;
                    }
            }

            return new GDL_Node(type, input, children);
        }

    }
}
