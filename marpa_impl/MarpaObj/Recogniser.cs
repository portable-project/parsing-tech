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
            /* Sets init */
            for(int i=0; i<input.Length; i++)
            {
                Sets.Add(new EarlemeSet());
            }
            Sets[0].AddEarleme(
                    new Earleme(0,
                        new Rule(
                            new Symbol("empty"),
                            new List<Symbol>() { Grammar.GetStartSymbol() })
                        )
                    );
            /* -------- */

            for (int i = 0; i < input.Length; i++)
            {
                EarlemeSet earlemeSet = Sets[i];
                Console.WriteLine(i + " : ");
                int j = 0;
                
                while (j < earlemeSet.GetEarlemeSetSize())
                {
                    Earleme current = earlemeSet.GetEarleme(j);
                    
                    if (!current.IsCompleted()) {
                        bool condition = Grammar.DoesBelongToTerminals(current.GetCurrentNextSymbol());
                        if (!condition)
                        {
                            Predictor(current, i);
                        }
                        else
                        {
                            Scanner(current, i, input[i]);
                        }
                    }

                    j++;
                }

                for (int k = 0; k < earlemeSet.GetEarlemeSetSize(); k++)
                {
                    Console.WriteLine(earlemeSet.GetEarleme(k).ToString());
                }
            }
        }

        private void Scanner(Earleme current, int setNumber, Char inputSymbol)
        {
            if (current.GetCurrentNextSymbol().GetSymbolName().Equals(inputSymbol.ToString()))
            {
                AddToSet(
                    new Earleme(
                        current.GetRulePosition() + 1, 
                        current.GetRule()
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
                AddToSet(new Earleme(setNumber, r), setNumber);
            });

        }

        private void AddToSet(Earleme earleme, int setIndex)
        {
            Sets[setIndex].AddEarleme(earleme);
        }
    }
}
