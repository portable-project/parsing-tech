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

                    if (current.IsCompleted()) Completer(current, i);
                    else {
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
                }
            }
        }

        private void Completer(EarleyItem current, int setNumber)
        {
            Symbol lhs = current.GetRule().GetLeftHandSideOfRule();
            int position = current.GetOrignPosition();
            EarleySet set = Sets[position];
            LeoItem transitiveItem = set.FindLeoItemBySymbol(lhs);

            if (transitiveItem != null) LeoReducer(transitiveItem, setNumber);
            else EarleyReducer(set, lhs, setNumber);
        }

        private void LeoReducer(LeoItem leoItem, int setNumber)
        {
            AddToSet(
              new EarleyItem(
                 leoItem.GetDottedRule(),
                 leoItem.GetOrignPosition()
              ),
              setNumber
            );
        }
        private void EarleyReducer(EarleySet set, Symbol currentNonTerminal, int setNumber)
        {
            for (int j = 0; j < set.GetEarlemeSetSize(); j++)
            {
                EarleyItem currentEarleme = set.GetEarleme(j);
                Symbol next = currentEarleme.GetCurrentNextSymbol();
                if (next != null && next.Equals(currentNonTerminal))
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
                else
                {
                    EarleyItem ei = new EarleyItem(r, setNumber);
                    AddToSet(ei, setNumber);
                    LeoMemoization(ei, setNumber);
                }
            });

        }

        private void LeoMemoization(EarleyItem earleyItem, int setNumber)
        {
            if (!IsItemLeoEligible(earleyItem, setNumber)) return;

            Symbol penult = earleyItem.GetItemPenult();
            LeoItem predecessorLeoItem = FindLeoItemPredecessor(earleyItem);
            if(predecessorLeoItem != null)
            {
                Sets[predecessorLeoItem.GetOrignPosition()]
                    .AddLeoItem(new LeoItem(
                        predecessorLeoItem.GetDottedRule(),
                        predecessorLeoItem.GetOrignPosition(),
                        penult
                    ));
            }
            else {
                Sets[earleyItem.GetOrignPosition()]
                    .AddLeoItem(new LeoItem(
                        new DottedRule(earleyItem.GetRule(), earleyItem.GetRulePosition() + 1),
                        earleyItem.GetOrignPosition(),
                        penult
                    ));
            }
        }

        private LeoItem FindLeoItemPredecessor(EarleyItem earleyItem)
        {
            EarleySet predecessorSet = Sets[earleyItem.GetOrignPosition()];
            return predecessorSet.FindLeoItemBySymbol(earleyItem.GetRule().GetLeftHandSideOfRule());
        }

        private bool IsItemLeoEligible(EarleyItem earleyItem, int setNumber)
        {
            bool isRightRecursive = IsRuleRightRecursive(earleyItem.GetRule());
            bool isLeoUnique = IsItemLeoUnique(earleyItem, setNumber);
            if( isRightRecursive && isLeoUnique)
            {
                Console.WriteLine();
            }
            return isRightRecursive && isLeoUnique;
        }
        private bool IsRuleRightRecursive(Rule rule)
        {
            List<Symbol> rhs = rule.GetRightHandSideOfRule();
            return rule.GetLeftHandSideOfRule().Equals(rhs[rhs.Count - 1]);
        }
        private bool IsItemLeoUnique(EarleyItem earleyItem, int setNumber)
        {
            return earleyItem.GetItemPenult() != null && IsItemPenultUnique(earleyItem, setNumber);
        }
        private bool IsItemPenultUnique(EarleyItem selectedEarleyItem, int setNumber)
        {
            int itemsCount = Sets[setNumber].GetEarlemeSetSize();
            Symbol penult = selectedEarleyItem.GetItemPenult();
            if (penult == null) return false;

            for (int i=0; i< itemsCount; i++)
            {
                EarleyItem item = Sets[setNumber].GetEarleme(i);
                if (penult.Equals(item.GetItemPenult()) && !item.GetRule().Equals(selectedEarleyItem.GetRule())) return false;
            }
            return true;
        }

        private void AddToSet(EarleyItem earleme, int setIndex)
        {
            Sets[setIndex].AddEarleyItem(earleme);
        }
    }
}
