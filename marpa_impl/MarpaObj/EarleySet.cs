﻿using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    internal class EarleySet
    {
        private List<EarleyItem> _earleyItemList;
        private List<LeoItem> _leoItemList;

        internal EarleySet()
        {
            _earleyItemList = new List<EarleyItem>();
            _leoItemList = new List<LeoItem>();
        }
        internal void AddEarleyItem(EarleyItem earleyItem)
        {
            if(
                _earleyItemList.Find(erl => 
                    erl.GetRule().Equals(earleyItem.GetRule()) 
                    && erl.GetRulePosition() == earleyItem.GetRulePosition()
                    && erl.GetOrignPosition() == earleyItem.GetOrignPosition()
                    ) == null
                )
            {
                _earleyItemList.Add(earleyItem);
            }
            
        }

        internal void RemoveEarleme(EarleyItem earleme)
        {
           _earleyItemList.Remove(earleme);
        }

        internal int GetEarlemeSetSize()
        {
            return _earleyItemList.Count;
        }
        internal EarleyItem GetEarleme(int index)
        {
            return _earleyItemList[index];
        }
    }
}
