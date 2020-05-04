using System.Collections.Generic;
using System;
using Symbol = System.String;

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
        internal void AddLeoItem(LeoItem leoItem)
        {
            _leoItemList.Add(leoItem);
        }
        internal LeoItem FindLeoItemBySymbol(Symbol symbol)
        {
            return null;
        }
        internal List<EarleyItem> GetEarleyItemList()
        {
            return _earleyItemList;
        }
        internal List<LeoItem> GetLeoItemList()
        {
            return _leoItemList;
        }
        internal bool IsItemPenultUnique(EarleyItem selectedEarleyItem)
        {
            List<EarleyItem> items = GetEarleyItemList();
            if (items.Find(el => el.Equals(selectedEarleyItem)) == null) return false;

            Symbol penult = selectedEarleyItem.GetItemPenult();
            if (penult == null) return false;

            for (int i = 0; i < items.Count; i++)
            {
                EarleyItem item = items[i];
                if (penult.Equals(item.GetItemPenult()) && !item.GetRule().Equals(selectedEarleyItem.GetRule())) return false;
            }
            return true;
        }

        internal bool IsItemLeoUnique(EarleyItem earleyItem)
        {
            return earleyItem.GetItemPenult() != null && IsItemPenultUnique(earleyItem);
        }

        internal bool IsItemLeoEligible(EarleyItem earleyItem)
        {
            bool isRightRecursive = earleyItem.GetRule().IsRuleRightRecursive();
            bool isLeoUnique = IsItemLeoUnique(earleyItem);
            if (isRightRecursive && isLeoUnique)
            {
                Console.WriteLine();
            }
            return isRightRecursive && isLeoUnique;
        }
    }
}
