using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class EarlemeSet
    {
        private List<Earleme> Earlemes;
        private String State;

        internal EarlemeSet()
        {
            Earlemes = new List<Earleme>();
        }
        internal void AddEarleme(Earleme earleme)
        {
            if(
                Earlemes.Find(erl => 
                    erl.GetRule().Equals(earleme.GetRule()) 
                    && erl.GetRulePosition() == earleme.GetRulePosition() 
                    ) == null
                )
            {
                Earlemes.Add(earleme);
            }
            
        }

        internal void CompleteSet()
        {
            State = "completed";
        }

        internal String GetState()
        {
            return State;
        }

        internal int GetEarlemeSetSize()
        {
            return Earlemes.Count;
        }
        internal Earleme GetEarleme(int index)
        {
            return Earlemes[index];
        }
    }
}
