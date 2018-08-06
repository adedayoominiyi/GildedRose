using System.Collections.Generic;

namespace GildedRose.Console
{
    public static class ItemExtensions
    {
        public static void UpdateQuality(this Item item)
        {
            switch (item.GetType().Name)
            {
                case "NormalItem": //case nameof(NormalItem)
                    NormalItem normalItem = item as NormalItem;
                    normalItem.UpdateQuality();
                    break;

                case "SpecialItem": //case nameof(SpecialItem):
                    SpecialItem specialItem = item as SpecialItem;
                    specialItem.UpdateQuality();
                    break;
            }
        }
    }
}
