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
            }

        }

        private void Predictor(Earleme current, int parentPosition)
        {
            Symbol sym = current.GetCurrentNextSymbol();
            List<Rule> filteredRules = Grammar.GetRulesWithSpecificStartSymbol(sym);
            filteredRules.ForEach((Rule r) =>
            {
                AddToSet(new Earleme(parentPosition, r), parentPosition);
            });

        }

        private void AddToSet(Earleme earleme, int setIndex)
        {
            Sets[setIndex].AddEarleme(earleme);
        }
    }
}
