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

        public Parser(Grammar grammar)
        {
            recogniser = new Recogniser(grammar);
            _grammar = grammar;
            _sets = new List<EarlemeSet>();
        }

        public void Parse(String input)
        {
            _recogniserSets = recogniser.Recognise(input);

            // Reorganize();
            // TreeNode tree = BuildParseTree(0, _grammar.GetStartSymbol());
        }
        private TreeNode BuildParseTree(int setNum, Symbol s)
        {
            if (setNum >= _sets.Count) return null;

            EarlemeSet set = _sets[setNum];
            Earleme choosen = null;

            for (int i = 0; i < set.GetEarlemeSetSize(); i++)
            {
                Earleme e = set.GetEarleme(i);
                if (
                    (choosen == null || choosen.GetParentPosition() < e.GetParentPosition())
                    && e.GetRule().GetLeftHandSideOfRule().Equals(s)
                )
                {
                    choosen = e;
                }
            }

            if (choosen == null) return null;
            set.RemoveEarleme(choosen);


            List<TreeNode> children = new List<TreeNode>();
            List<Symbol> rhs = choosen.GetRule().GetRightHandSideOfRule();
            int currentPosition = setNum;
            rhs.ForEach(symbol =>
            {
                TreeNode node = BuildParseTree(currentPosition, symbol);
                if (node != null)
                {
                    children.Add(node);
                    currentPosition = node.GetEndPosition() + 1;
                }
            });

            /*
            // this level side one
            // choosen
            TreeNode firstChild = new TreeNode(choosen, setNum);
            firstChild.AddChildren(BuildParseTree(setNum));
            children.Add(firstChild);


            // this level side two
            List<TreeNode> siblings = BuildParseTree(choosen.GetParentPosition() + 1);
            if(siblings != null) children.AddRange(siblings);
            */

            TreeNode tn = new TreeNode(choosen, setNum);
            tn.AddChildren(children);
            return tn;
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
