using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {
        IList<Item> Items;
        
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                                          {
                                              new NormalItem {Name = ItemNames.DEXTERITY_VEST, SellIn = 10, Quality = 20},
                                              // "Aged Brie" actually increases in Quality the older it gets
                                              new NormalItem {Name = ItemNames.AGED_BRIE, SellIn = 2, Quality = 0, NormalDecreaseBy = -1, OverdueDecreaseBy = -1},
                                              new NormalItem {Name = ItemNames.MONGOOSE_ELIXIR, SellIn = 5, Quality = 7},
                                              // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
                                              new NormalItem {Name = ItemNames.SULFURAS, SellIn = 0, Quality = 80, NormalDecreaseBy = 0, OverdueDecreaseBy = 0, MinimumQualityAllowed = 80, MaximumQualityAllowed = 80},
                                              new SpecialItem {Name = ItemNames.BACKSTAGE_PASS, SellIn = 15, Quality = 20},
                                              // "Conjured" items degrade in Quality twice as fast as normal items
                                              new NormalItem {Name = ItemNames.CONJURED, SellIn = 3, Quality = 6, NormalDecreaseBy = 2, OverdueDecreaseBy = 4}
                                          }

                          };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            int itemsCount = Items.Count;

            for (var i = 0; i < itemsCount; i++)
            {
                Items[i].UpdateQuality();
            }
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
