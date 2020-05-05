using System;
using System.Collections.Generic;

namespace marpa_impl
{
    internal class Parser
    {
        private List<EarleySet> _recogniserSets;
        private List<EarleySet> _sets;
        private Grammar _grammar;
        private ErrorHandler _errorHandler;
        internal Parser(Grammar grammar, ErrorHandler errorHandler)
        {
            _grammar = grammar;
            _errorHandler = errorHandler;
            _sets = new List<EarleySet>();
        }
        internal void Parse(List<EarleySet> recogniserSets)
        {

        }
        private void Reorganize()
        {
            for (int i = 0; i < _recogniserSets.Count; i++)
            {
                _sets.Add(new EarleySet());
            }

            for (int i = 0; i < _recogniserSets.Count; i++)
            {
                EarleySet set = _recogniserSets[i];
                List<EarleyItem> items = set.GetEarleyItemList();
                for (int k = 0; k < items.Count; k++)
                {
                    EarleyItem e = items[k];
                    if (e.IsCompleted())
                    {
                        EarleyItem ne = new EarleyItem(e.GetRule(), i, e.GetRulePosition());
                        _sets[e.GetOrignPosition()].AddEarleyItem(ne, "Reorganize");
                    }
                }
            }
        }
    }
}
