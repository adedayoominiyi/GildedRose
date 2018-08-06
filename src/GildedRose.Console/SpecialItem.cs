using System.Collections.Generic;

namespace GildedRose.Console
{
    public class SpecialItem : NormalItem
    {
        public override void UpdateQuality()
        {
            if (Name == ItemNames.BACKSTAGE_PASS)
            {
                int newSellIn = SellIn - 1;
                if (newSellIn >= 0)
                {
                    SellIn = newSellIn;

                    // Quality drops to 0 after the concert.
                    int newQuality = 0;
                    if (newSellIn > 10)
                    {
                        // increases in Quality as it's SellIn value approaches
                        newQuality = Quality + 1;
                    }
                    else if (newSellIn > 5 && newSellIn <= 10)
                    {
                        // Quality increases by 2 when there are 10 days or less
                        newQuality = Quality + 2;
                    }
                    else if (newSellIn > 0 && newSellIn <= 5)
                    {
                        // Quality increases by 3 when there are 5 days or less
                        newQuality = Quality + 3;
                    }

                    // We are not allowed to go below the minimum nor above the maximum.
                    if (IsValidQuality(newQuality))
                    {
                        Quality = newQuality;
                    }
                }
                else if (newSellIn < 0)
                {
                    Quality = 0;
                    SellIn = 0;
                }
            }
        }
    }
}
