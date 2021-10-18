namespace DoubleDispatch.OnlineShopping.Tests
{
    using Xunit;

    using DoubleDispatch.OnlineShopping.Domain.Model;
    using DoubleDispatch.OnlineShopping.Domain.Repositories;

    public class LineItemTests
    {
        [Fact]
        public void UpdateItemAboveLimitReturnsFalseWithoutRepository()
        {
            var po = new PurchaseOrder() { SpendLimit = 100 };
            po.TryAddItem(new LineItem(50));
            var item = new LineItem(25);
            po.TryAddItem(item);

            Assert.False(item.TryUpdateCost(51, po));
        }

        [Fact]
        public void UpdateItemAboveLimitReturnsFalseWithRepository()
        {
            var repo = new InMemoryPurchaseOrderRepository();

            var po = new PurchaseOrder() { SpendLimit = 100 };
            repo.Add(po);

            po.TryAddItem(new LineItem(50));
            var item = new LineItem(25);
            po.TryAddItem(item);

            Assert.False(item.TryUpdateCost(51, repo)); // no longer possible to use wrong PO
        }
    }
}
