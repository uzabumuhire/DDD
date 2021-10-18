// From Steve Smith's blog (ardalis.com) :
// https://ardalis.com/double-dispatch-in-c-and-ddd/

namespace DoubleDispatch.OnlineShopping.Tests
{
    using Xunit;

    using DoubleDispatch.OnlineShopping.Domain.Model;
    using DoubleDispatch.OnlineShopping.Domain.Repositories;
    using DoubleDispatch.OnlineShopping.Domain.Services;

    public class PurchaseOrderAggregateTests
    {
        [Fact]
        public void UpdateItemAboveLimitReturnsFalse()
        {
            var po = new PurchaseOrder() { SpendLimit = 100 };
            po.TryAddItem(new LineItem(50));
            var item = new LineItem(25);
            po.TryAddItem(item);

            Assert.False(item.TryUpdateCost(51, po)); // it's possible to use the wrong PO
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

        [Fact]
        public void UpdateItemAboveLimitReturnsFalseWithService()
        {
            var purchaseOrderRepo = new InMemoryPurchaseOrderRepository();
            var purchaseOrderService = new PurchaseOrderService(purchaseOrderRepo);
            var po = new PurchaseOrder() { SpendLimit = 100 };
            purchaseOrderRepo.Add(po);
            po.TryAddItem(new LineItem(50), purchaseOrderService);
            var item = new LineItem(25);
            po.TryAddItem(item, purchaseOrderService);

            Assert.False(item.TryUpdateCost(51, purchaseOrderService));
        }

        [Fact]
        public void AddItemAboveLimitReturnsFalse()
        {
            var po = new PurchaseOrder() { SpendLimit = 100 };
            po.TryAddItem(new LineItem(50));
            var item = new LineItem(51);
            Assert.False(po.TryAddItem(item));
        }

        [Fact]
        public void AddItemAboveLimitReturnsFalseWithService()
        {
            var purchaseOrderRepo = new InMemoryPurchaseOrderRepository();
            var purchaseOrderService = new PurchaseOrderService(purchaseOrderRepo);
            var po = new PurchaseOrder() { SpendLimit = 100 };
            purchaseOrderRepo.Add(po);
            po.TryAddItem(new LineItem(50), purchaseOrderService);
            var item = new LineItem(51);

            Assert.False(po.TryAddItem(item, purchaseOrderService));
        }
    }
}
