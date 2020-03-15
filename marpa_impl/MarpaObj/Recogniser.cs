using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Recogniser
    {
        private Symbol EmptySymbol = new Symbol("empty");
        private readonly Grammar Grammar;
        private List<EarlemeSet> Sets;

        public Recogniser(Grammar grammar)
        {
            if (!grammar.IsGrammarValid())
            {
                ErrorHandler.PrintErrorCode(ErrorCode.INCOMPLETE_GRAMMAR);
                return;
            }
            Grammar = grammar;
            Sets = new List<EarlemeSet>();
        }

        public void Parse(String input)
        {
            if (Grammar == null) return;
            InitBeforeParse(input);
            RunMarpa(input);
            PrintSets();
            Console.WriteLine();
            Console.WriteLine("____________________");
            Console.WriteLine();
            // ConstructParseTree(input.Length);
        }

        private void ConstructParseTree(int inputLength)
        {
            Earleme completedStartSymbolEarleme = GetCompletedEarlemeWithSecialLHS(inputLength, Grammar.GetStartSymbol());
            if (completedStartSymbolEarleme == null)
            {
                Console.WriteLine("This input string is not recognised by the grammar");
                return;
            }

            FindAllDerivedRules(completedStartSymbolEarleme);
            
        }

        private void FindAllDerivedRules(Earleme earleme)
        {
            List<Symbol> rhs = earleme.GetRule().GetRightHandSideOfRule();
            Console.WriteLine("RULE: " + earleme.GetRule().ToString());
            for (int i = rhs.Count-1; i >=0 ; i--)
            {
                Symbol s = rhs[i];
                if (!Grammar.DoesBelongToTerminals(s))
                {
                    Console.WriteLine(s.GetSymbolName());
                    Earleme e = GetCompletedEarlemeWithSecialLHS(earleme.GetParentPosition(), s);
                    if(e != null) Console.WriteLine(e.ToString());
                }
            }
        }

        private Earleme GetCompletedEarlemeWithSecialLHS(int setNumber, Symbol lhs)
        {
            EarlemeSet lastSet = Sets[setNumber];
            Earleme completedEarleme = null;
            for (int i = 0; i < lastSet.GetEarlemeSetSize(); i++)
            {
                Earleme e = lastSet.GetEarleme(i);
                if (e.IsCompleted()
                    && e.GetRule().GetLeftHandSideOfRule().Equals(lhs))
                {
                    completedEarleme = e;
                    break;
                }
            }
            return completedEarleme;
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

        private void PrintSets()
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                Console.WriteLine("\nSet size " + i);
                EarlemeSet earlemeSet = Sets[i];
                for (int k = 0; k < earlemeSet.GetEarlemeSetSize(); k++)
                {
                    Earleme e = earlemeSet.GetEarleme(k);
                    if (e.IsCompleted()) {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }
        private void InitBeforeParse(String input)
        {
            for (int i = 0; i <= input.Length; i++)
            {
                Sets.Add(new EarlemeSet());
            }

            Grammar.GetRulesWithSpecificStartSymbol(Grammar.GetStartSymbol()).ForEach(r =>
            {
                Sets[0].AddEarleme(new Earleme(r,0));
            });
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
