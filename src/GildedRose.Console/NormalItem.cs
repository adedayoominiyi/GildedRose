using System.Collections.Generic;

namespace GildedRose.Console
{
    public class NormalItem : Item
    {
        public NormalItem()
        {
            NormalDecreaseBy = 1; // Use negative numbers to increment.
            // Once the sell by date has passed, Quality degrades twice as fast
            OverdueDecreaseBy = 2; // Use negative numbers to increment.
            MinimumQualityAllowed = 0; // The Quality of an item is never negative
            MaximumQualityAllowed = 50; // The Quality of an item is never more than 50
        }

        public int NormalDecreaseBy { get; set; } // Use negative numbers to increment.
        public int OverdueDecreaseBy { get; set; } // Use negative numbers to increment.
        public int MinimumQualityAllowed { get; set; }
        public int MaximumQualityAllowed { get; set; }


        public virtual void UpdateQuality()
        {
            int newSellIn = SellIn - 1;
            if (newSellIn >= 0)
            {
                // Still within sellIn day.
                SellIn = newSellIn;
                
                int newQuality = Quality - NormalDecreaseBy;

                // We are not allowed to go below the minimum nor above the maximum.
                if (IsValidQuality(newQuality))
                {

                    Quality = newQuality;
                }
            }
            else if (newSellIn < 0)
            {
                // No point reducing sellIn below zero as a negative already tells us it is passed sellIn days.
                int newQuality = Quality - OverdueDecreaseBy;

                // We are not allowed to go below the minimum nor above the maximum.
                if (IsValidQuality(newQuality))
                {
                    
                    Quality = newQuality;
                }
            }
        }

        public bool IsValidQuality(int quality)
        {
            return quality >= MinimumQualityAllowed && quality <= MaximumQualityAllowed;
        }
    }
}
