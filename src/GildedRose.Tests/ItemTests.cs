using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class ItemTests
    {
        [Fact]
        public void NormalItemDefaultsAreSetCorrectly()
        {
            const int defaultNormalDecreaseBy = 1;
            const int defaultOverdueDecreaseBy = 2;
            const int defaultMinimumQualityAllowed = 0;
            const int defaultMaximumQualityAllowed = 50;

            const int sellIn = 10;
            const int quality = 20;

            NormalItem normalItem = new NormalItem {
                Name = ItemNames.DEXTERITY_VEST,
                SellIn = sellIn,
                Quality = quality
            };

            Assert.Equal(ItemNames.DEXTERITY_VEST, normalItem.Name);
            Assert.Equal(sellIn, normalItem.SellIn);
            Assert.Equal(quality, normalItem.Quality);

            Assert.Equal(defaultNormalDecreaseBy, normalItem.NormalDecreaseBy);
            Assert.Equal(defaultOverdueDecreaseBy, normalItem.OverdueDecreaseBy);
            Assert.Equal(defaultMinimumQualityAllowed, normalItem.MinimumQualityAllowed);
            Assert.Equal(defaultMaximumQualityAllowed, normalItem.MaximumQualityAllowed);
        }

        [Fact]
        public void DecreaseQualityByOneIfNotOverdue()
        {
            const int sellIn = 10;
            const int quality = 20;

            Item item = new NormalItem {
                Name = ItemNames.DEXTERITY_VEST,
                SellIn = sellIn,
                Quality = quality
            };
            item.UpdateQuality();

            Assert.Equal(sellIn - 1, item.SellIn);
            Assert.Equal(quality - 1, item.Quality);
        }

        [Fact]
        public void DecreaseQualityByTwoIfOverdue()
        {
            const int sellIn = 10;
            const int quality = 20;

            Item item = new NormalItem {
                Name = ItemNames.DEXTERITY_VEST,
                SellIn = sellIn,
                Quality = quality
            };

            // Normal decrements for the first "sellIn" days.
            for (int i = 0; i < sellIn; i++)
            {
                item.UpdateQuality();
            }

            // Double decrement for the first overdue day.
            item.UpdateQuality();

            Assert.Equal(0, item.SellIn);
            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void AgedBrieQualityAlwaysIncreases()
        {
            const int sellIn = 2;
            const int quality = 0;

            Item item = new NormalItem {
                Name = ItemNames.AGED_BRIE,
                SellIn = sellIn,
                Quality = quality,
                NormalDecreaseBy = -1,
                OverdueDecreaseBy = -1
            };
            item.UpdateQuality();

            Assert.Equal(sellIn - 1, item.SellIn);
            Assert.Equal(quality + 1, item.Quality);
        }

        [Fact]
        public void AgedBrieQualityNeverExceedsFifty()
        {
            const int sellIn = 2;
            const int quality = 50;

            Item item = new NormalItem {
                Name = ItemNames.AGED_BRIE,
                SellIn = sellIn,
                Quality = quality,
                NormalDecreaseBy = -1,
                OverdueDecreaseBy = -1
            };
            item.UpdateQuality();
            item.UpdateQuality();
            item.UpdateQuality();

            // SellIn days went down to zero but quality never changed as maximum quality allowed is 50.
            Assert.Equal(0, item.SellIn);
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void SulfurasQualityNeverChanges()
        {
            const int sellIn = 1;
            const int quality = 80;

            Item item = new NormalItem {
                Name = ItemNames.SULFURAS,
                SellIn = sellIn,
                Quality = quality,
                NormalDecreaseBy = 0,
                OverdueDecreaseBy = 0,
                MinimumQualityAllowed = 80,
                MaximumQualityAllowed = 80
            };
            item.UpdateQuality();

            Assert.Equal(sellIn - 1, item.SellIn);
            Assert.Equal(quality, item.Quality);
        }

        [Fact]
        public void BackstagePassIsSpecial()
        {
            const int sellIn = 12;
            const int quality = 20;

            Item item = new SpecialItem {
                Name = ItemNames.BACKSTAGE_PASS,
                SellIn = sellIn,
                Quality = quality
            };
            item.UpdateQuality(); // Quality should increase by 1.
            item.UpdateQuality(); // Quality should increase by 2.

            Assert.Equal(sellIn - 2, item.SellIn);
            Assert.Equal(quality + 3, item.Quality);
        }
    }
}