using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Parser
    {
        private List<EarleySet> _recogniserSets;
        private List<EarleySet> _sets;
        private Grammar _grammar;
        private Recogniser recogniser;
        private String _input;

        public Parser(Grammar grammar)
        {
            recogniser = new Recogniser(grammar);
            _grammar = grammar;
            _sets = new List<EarleySet>();
        }

        public void Parse(String input)
        {
            _input = input;
            Console.WriteLine(input);
            Utils.PrintSeparator(3);

            _recogniserSets = recogniser.RecogniseString(input);
            Utils.PrintSets(_recogniserSets, true);
            // Utils.PrintSeparator(4);

            // Reorganize();
            // Utils.PrintSets(_sets, false);
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
                        _sets[e.GetOrignPosition()].AddEarleyItem(ne);
                    }
                }
            }
        }
    }
}
