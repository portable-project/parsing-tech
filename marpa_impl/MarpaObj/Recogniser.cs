using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Recogniser
    {
        private readonly Grammar Grammar;
        private List<EarlemeSet> Sets;

        public Recogniser(Grammar grammar)
        {
            if (!grammar.IsGrammarValid())
            {
                throw new Exception(ErrorHandler.getErrorMessageByCode(ErrorCode.INCOMPLETE_GRAMMAR));
            }
            Grammar = grammar;
            Sets = new List<EarlemeSet>();
        }

        public void Parse(String input)
        {
            InitBeforeParse(input);

            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("\nSYM " + input[i] + " : ");

                EarlemeSet earlemeSet = Sets[i];
                for (int j = 0; j < earlemeSet.GetEarlemeSetSize(); j++)
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
                    else
                    {
                        Completer(current, i);
                    }

                }

                for (int k = 0; k < earlemeSet.GetEarlemeSetSize(); k++)
                {
                    Console.WriteLine(earlemeSet.GetEarleme(k).ToString());
                }
            }
        }
        private void InitBeforeParse(String input)
        {
            for (int i = 0; i <= input.Length; i++)
            {
                Sets.Add(new EarlemeSet());
            }
            Sets[0].AddEarleme(
                    new Earleme(
                        new Rule(
                            new Symbol("empty"),
                            new List<Symbol>() { Grammar.GetStartSymbol() }),
                        0
                        )
                    );
        }

        private void Completer(Earleme current, int setNumber)
        {
            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            int position = current.GetParentPosition();
            EarlemeSet earlemeSet = Sets[position];

            int j = 0;
            while (j < earlemeSet.GetEarlemeSetSize())
            {
                Earleme currentEarleme = earlemeSet.GetEarleme(j);
                Symbol next = currentEarleme.GetCurrentNextSymbol();
                if (next.Equals(lhs))
                {
                    AddToSet(
                        new Earleme(
                        currentEarleme.GetRule(),
                        currentEarleme.GetParentPosition(),
                        currentEarleme.GetRulePosition() + 1
                            ), 
                        setNumber );
                }
                j++;
            }
        }

        private void Scanner(Earleme current, int setNumber, Char inputSymbol)
        {
            if (current.GetCurrentNextSymbol().GetSymbolName().Equals(inputSymbol.ToString()))
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
                AddToSet(new Earleme(r, setNumber), setNumber);
            });

        }

        private void AddToSet(Earleme earleme, int setIndex)
        {
            Sets[setIndex].AddEarleme(earleme);
        }
    }
}
