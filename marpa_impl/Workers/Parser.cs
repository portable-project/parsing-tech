using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Parser
    {
        private List<EarlemeSet> _recogniserSets;
        private List<EarlemeSet> _sets;
        private Grammar _grammar;
        private Recogniser recogniser;
        private String _input;

        public Parser(Grammar grammar)
        {
            recogniser = new Recogniser(grammar);
            _grammar = grammar;
            _sets = new List<EarlemeSet>();
        }

        public void Parse(String input)
        {
            _input = input;
            Console.WriteLine(input);
            Utils.PrintSeparator(3);

            _recogniserSets = recogniser.Recognise(input);
            Utils.PrintSets(_recogniserSets, true);
            // Utils.PrintSeparator(4);

            // Reorganize();
            // Utils.PrintSets(_sets, false);
        }
        private void Reorganize()
        {
            for (int i = 0; i < _recogniserSets.Count; i++)
            {
                _sets.Add(new EarlemeSet());
            }

            for (int i = 0; i < _recogniserSets.Count; i++)
            {
                EarlemeSet earlemeSet = _recogniserSets[i];
                for (int k = 0; k < earlemeSet.GetEarlemeSetSize(); k++)
                {
                    Earleme e = earlemeSet.GetEarleme(k);
                    if (e.IsCompleted())
                    {
                        Earleme ne = new Earleme(e.GetRule(), i, e.GetRulePosition());
                        _sets[e.GetParentPosition()].AddEarleme(ne);
                    }
                }
            }
        }
    }
}
