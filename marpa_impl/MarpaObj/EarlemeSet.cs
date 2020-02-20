using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class EarlemeSet
    {
        private List<Earleme> Earlemes;
        private String State;

        internal void AddEarleme(Earleme earleme)
        {
            Earlemes.Add(earleme);
        }

        internal void CompleteSet()
        {
            State = "completed";
        }

        internal String GetState()
        {
            return State;
        }
    }
}
