using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public class Recogniser
    {
        private readonly Grammar Grammar = null;
        private readonly List<EarlemeSet> Sets;

        public Recogniser(Grammar grammar)
        {
            if (grammar == null || !grammar.IsGrammarValid())
            {
                ErrorHandler.PrintErrorCode(ErrorCode.INCOMPLETE_GRAMMAR);
                return;
            }
            Grammar = grammar;
            Sets = new List<EarlemeSet>();
        }

        internal List<EarlemeSet> Recognise(String input)
        {
            if (Grammar == null) return null;

            InitBeforeParse(input);
            RunMarpa(input);

            return Sets;
        }

        private void InitBeforeParse(String input)
        {
            for (int i = 0; i <= input.Length; i++)
            {
                Sets.Add(new EarlemeSet());
            }

            Grammar.GetRulesWithSpecificStartSymbol(Grammar.GetStartSymbol()).ForEach(r =>
            {
                Sets[0].AddEarleme(new Earleme(r, 0));
            });
        }

        private void RunMarpa(String input)
        {
            for (int i = 0; i <= input.Length; i++)
            {
                EarlemeSet earlemeSet = Sets[i];
                for (int j = 0; j < earlemeSet.GetEarlemeSetSize(); j++)
                {
                    Earleme current = earlemeSet.GetEarleme(j);

                    if (!current.IsCompleted())
                    {
                        bool condition = Grammar.DoesBelongToTerminals(current.GetCurrentNextSymbol());
                        if (!condition)
                        {
                            Predictor(current, i);
                        }
                        else if (input.Length > i)
                        {
                            Scanner(current, i, input[i]);
                        }
                    }
                    else
                    {
                        Completer(current, i);
                    }
                }
            }
        }

        private void Completer(Earleme current, int setNumber)
        {
            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            int position = current.GetParentPosition();
            EarlemeSet earlemeSet = Sets[position];

            for (int j = 0; j < earlemeSet.GetEarlemeSetSize(); j++)
            {
                Earleme currentEarleme = earlemeSet.GetEarleme(j);
                Symbol next = currentEarleme.GetCurrentNextSymbol();
                if (next != null && next.Equals(lhs))
                {
                    AddToSet(
                        new Earleme(
                        currentEarleme.GetRule(),
                        currentEarleme.GetParentPosition(),
                        currentEarleme.GetRulePosition() + 1
                            ),
                        setNumber);

                }
            }
        }

        private void Scanner(Earleme current, int setNumber, Char inputSymbol)
        {
            Symbol nextSymbol = current.GetCurrentNextSymbol();
            if (nextSymbol != null && nextSymbol.GetSymbolName().Equals(inputSymbol.ToString()))
            {
                AddToSet(
                    new Earleme(
                        current.GetRule(),
                        current.GetParentPosition(),
                        current.GetRulePosition() + 1
                        ),
                    setNumber + 1);
            }

        }

        private void Predictor(Earleme current, int setNumber)
        {
            Symbol sym = current.GetCurrentNextSymbol();
            List<Rule> filteredRules = Grammar.GetRulesWithSpecificStartSymbol(sym);
            filteredRules.ForEach((Rule r) =>
            {
                List<Symbol> symList = r.GetRightHandSideOfRule();
                if (symList.Count == 1 && Grammar.CheckIsSymbolANullingSymbol(symList[0]))
                {
                    AddToSet(new Earleme(current.GetRule(), setNumber, current.GetRulePosition() + 1), setNumber);
                }
                else AddToSet(new Earleme(r, setNumber), setNumber);
            });

        }

        private void AddToSet(Earleme earleme, int setIndex)
        {
            Sets[setIndex].AddEarleme(earleme);
        }
    }
}
