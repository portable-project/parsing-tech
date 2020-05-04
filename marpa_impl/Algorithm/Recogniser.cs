using System;
using System.Collections.Generic;
using Symbol = System.String;

namespace marpa_impl
{
    internal class Recogniser
    {
        private readonly Grammar Grammar = null;
        private List<EarleySet> Sets;
        private ErrorHandler errorHandler;
        private String lastInputString;

        internal Recogniser(Grammar grammar, ErrorHandler _errorHandler)
        {
            errorHandler = _errorHandler;
            Grammar = grammar;
        }
        internal bool Recognise(String input)
        {
            lastInputString = input;

            InitBeforeParse(input);
            RunMarpa(input, 0);

            Utils.PrintSets(Sets, true);
            return true;
        }

        internal bool UpdateRecognise(String newInput)
        {
            if (Sets == null) return false;

            int from = GetInputsDiffPosition(newInput);
            UpdateSetsBeforeReparse(newInput, from);
            RunMarpa(newInput, from);

            Utils.PrintSets(Sets, true);
            return true;
        }

        private void InitBeforeParse(String input)
        {
            Sets = new List<EarleySet>();

            for (int i = 0; i <= input.Length; i++)
            {
                Sets.Add(new EarleySet());
            }

            Grammar.GetRulesWithSpecificStartSymbol(Grammar.GetStartSymbol()).ForEach(r =>
            {
                Sets[0].AddEarleyItem(new EarleyItem(r, 0));
            });
        }

        private int GetInputsDiffPosition(String input)
        {
            int length = input.Length > lastInputString.Length ? input.Length : lastInputString.Length;
            for(int i = 0; i < length; i++)
            {
                if(i >= input.Length || i >= lastInputString.Length) return i;
                if(!input[i].Equals(lastInputString[i])) return i;
            }
            return -1;
        }
        private void UpdateSetsBeforeReparse(String input, int position)
        {
            for (int i = position; i < lastInputString.Length; i++)
            {
                Sets[i] = new EarleySet();
            }

            int extra = input.Length - Sets.Count + 1;
            if(extra > 0)
            {
                for (int i = 0; i < extra; i++)
                {
                     Sets.Add(new EarleySet());
                }
            } else if(extra < 0)
            {
                for (int i = lastInputString.Length - extra; i < lastInputString.Length; i++)
                {
                    Sets.RemoveAt(i);
                }
            }
            
        }

        private void RunMarpa(String input, int fromPosition)
        {
            for (int i = fromPosition; i <= input.Length; i++)
            {
                EarleySet set = Sets[i];
                List<EarleyItem> items = set.GetEarleyItemList();
                for (int j = 0; j < items.Count; j++)
                {
                    EarleyItem current = items[j];

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
            List<EarleyItem> items = set.GetEarleyItemList();
            for (int j = 0; j < items.Count; j++)
            {
                EarleyItem currentEarleme = items[j];
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
            if (!Sets[setNumber].IsItemLeoEligible(earleyItem)) return;

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

        private void AddToSet(EarleyItem earleme, int setIndex)
        {
            Sets[setIndex].AddEarleyItem(earleme);
        }

        internal ParseInfoReport GetLastParseInformation(int symbolPosition)
        {
            if (Sets.Count < symbolPosition)
            {
                return new ParseInfoReport(new ErrorDescription(ErrorCode.SYMBOL_POSITION_OUT_OF_RANGE));
            }
            return new ParseInfoReport(Sets[symbolPosition]);
        }
    }
}
