using System;
using System.Collections.Generic;
using Symbol = System.String;

namespace marpa_impl
{
    public class Recogniser
    {
        private readonly Grammar Grammar = null;
        private readonly List<EarleySet> Sets;

        public Recogniser(Grammar grammar)
        {
            if (grammar == null) return; // TODO: add error handle
            else if (!grammar.IsGrammarValid())
            {
                ErrorReport er = grammar.PrecomputeGrammar();
                if(!er.isSuccessfull) return; // TODO: add error handle
            }

            Grammar = grammar;
            Sets = new List<EarleySet>();
        }

        public bool CheckStringBelongsToGrammar(String input)
        {
            RecogniseString(input);
            Utils.PrintSets(Sets, true);
            return true;
        }

        internal List<EarleySet> RecogniseString(String input)
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
                Sets.Add(new EarleySet());
            }

            Grammar.GetRulesWithSpecificStartSymbol(Grammar.GetStartSymbol()).ForEach(r =>
            {
                Sets[0].AddEarleyItem(new EarleyItem(r, 0));
            });
        }

        private void RunMarpa(String input)
        {
            for (int i = 0; i <= input.Length; i++)
            {
                EarleySet earlemeSet = Sets[i];
                for (int j = 0; j < earlemeSet.GetEarlemeSetSize(); j++)
                {
                    EarleyItem current = earlemeSet.GetEarleme(j);

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

        private void LeoReducer()
        {

        }
        private void Completer(EarleyItem current, int setNumber)
        {
            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            int position = current.GetOrignPosition();
            EarleySet earlemeSet = Sets[position];

            for (int j = 0; j < earlemeSet.GetEarlemeSetSize(); j++)
            {
                EarleyItem currentEarleme = earlemeSet.GetEarleme(j);
                Symbol next = currentEarleme.GetCurrentNextSymbol();
                if (next != null && next.Equals(lhs))
                {
                    AddToSet(
                        new EarleyItem(
                        currentEarleme.GetRule(),
                        currentEarleme.GetOrignPosition(),
                        currentEarleme.GetRulePosition() + 1
                            ),
                        setNumber);

                }
            }
        }

        private void Scanner(EarleyItem current, int setNumber, Char inputSymbol)
        {
            Symbol nextSymbol = current.GetCurrentNextSymbol();
            if (nextSymbol != null && nextSymbol.Equals(inputSymbol.ToString()))
            {
                AddToSet(
                    new EarleyItem(
                        current.GetRule(),
                        current.GetOrignPosition(),
                        current.GetRulePosition() + 1
                        ),
                    setNumber + 1);
            }

        }

        private void Predictor(EarleyItem current, int setNumber)
        {
            Symbol sym = current.GetCurrentNextSymbol();
            List<Rule> filteredRules = Grammar.GetRulesWithSpecificStartSymbol(sym);
            filteredRules.ForEach((Rule r) =>
            {
                List<Symbol> symList = r.GetRightHandSideOfRule();
                if (symList.Count == 1 && Grammar.CheckIsSymbolANullStringSymbol(symList[0]))
                {
                    AddToSet(new EarleyItem(current.GetRule(), setNumber, current.GetRulePosition() + 1), setNumber);
                }
                else AddToSet(new EarleyItem(r, setNumber), setNumber);
            });

        }

        private void AddToSet(EarleyItem earleme, int setIndex)
        {
            Sets[setIndex].AddEarleyItem(earleme);
        }
    }
}
