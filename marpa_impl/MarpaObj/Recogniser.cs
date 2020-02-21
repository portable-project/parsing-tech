using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Recogniser
    {
        private readonly Grammar Grammar;
        private List<EarlemeSet> Sets;
        private int CurrentEarlemeSet;

        public Recogniser(Grammar grammar)
        {
            if (!grammar.IsGrammarValid())
            {
                throw new Exception(ErrorHandler.getErrorMessageByCode(ErrorCode.INCOMPLETE_GRAMMAR));
            }
            Grammar = grammar;
            Sets = new List<EarlemeSet>();
            CurrentEarlemeSet = 0;
        }

        public void Parse(String input)
        {
            for(int i=0; i<input.Length; i++)
            {
                Sets.Add(new EarlemeSet());
                Sets[0].AddEarleme(
                    new Earleme(0, 
                        new Rule(
                            new Symbol("empty"), 
                            new List<Symbol>() { Grammar.GetStartSymbol()})
                        )
                    );
            }

            for (int i = 0; i < input.Length; i++)
            {
                EarlemeSet earlemeSet = Sets[i];
                for (int j = 0; j < earlemeSet.GetEarlemeSetSize(); j++)
                {
                    Earleme current = earlemeSet.GetEarleme(j);
                    if (!current.IsCompleted()) {
                        Predictor(current, j);
                    }
                }
            }
        }

        private void Predictor(Earleme current, int setNumber)
        {
            Symbol sym = current.GetCurrentNextSymbol();
            List<Rule> filteredRules = Grammar.GetRulesWithSpecificStartSymbol(sym);
            filteredRules.ForEach((Rule r) =>
            {
                AddToSet(new Earleme(setNumber, r), setNumber);
            });

        }

        private void AddToSet(Earleme earleme, int setIndex)
        {
            Sets[setIndex].AddEarleme(earleme);
        }
    }
}
