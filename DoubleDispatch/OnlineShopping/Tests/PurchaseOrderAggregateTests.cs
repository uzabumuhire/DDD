// References :

// - Steve Smith's article on his blog (ardalis.com)
// https://ardalis.com/double-dispatch-in-c-and-ddd/

// - Jimmy Boggard's articles on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/02/24/strengthening-your-domain-aggregate-construction/
// https://lostechies.com/jimmybogard/2010/03/10/strengthening-your-domain-encapsulated-collections/

namespace DoubleDispatch.OnlineShopping.Tests
{
    using Xunit;

    using DoubleDispatch.OnlineShopping.Domain.Model;
    using DoubleDispatch.OnlineShopping.Domain.Repositories;
    using DoubleDispatch.OnlineShopping.Domain.Services;

    public class PurchaseOrderAggregateTests
    {
        [Fact]
        public void Update_item_above_limit_is_invalid()
        {
            var po = new PurchaseOrder() { SpendLimit = 100 };
            po.TryAddItem(new LineItem(50));
            var item = new LineItem(25);
            po.TryAddItem(item);

            Assert.False(item.TryUpdateCost(51, po)); // it's possible to use the wrong PO
        }

        [Fact]
        public void Update_item_above_limit_is_invalid_using_repository()
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
        public void Update_item_above_limit_is_invalid_using_service()
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
        public void Add_item_above_limit_is_not_valid()
        {
            var po = new PurchaseOrder() { SpendLimit = 100 };
            po.TryAddItem(new LineItem(50));
            var item = new LineItem(51);
            Assert.False(po.TryAddItem(item));
        }

        [Fact]
        public void Add_item_above_limit_is_not_valid_using_service()
        {
            var purchaseOrderRepo = new InMemoryPurchaseOrderRepository();
            var purchaseOrderService = new PurchaseOrderService(purchaseOrderRepo);
            var po = new PurchaseOrder() { SpendLimit = 100 };
            purchaseOrderRepo.Add(po);
            po.TryAddItem(new LineItem(50), purchaseOrderService);
            var item = new LineItem(51);

            Assert.False(po.TryAddItem(item, purchaseOrderService));
        }

        [Fact]
        public void Order_is_local_when_customer_province_and_order_billing_province_are_the_same()
        {
            var customer = new Customer { Province = "Kalundborg" };
            var po = new PurchaseOrder
            {
                BillingProvince = "Kalundborg",
                Customer = customer
            };

            Assert.True(po.IsLocal());
        }

        [Fact]
        public void Add_the_order_to_the_customers_order_lists_when_an_order_is_created()
        {
            var customer = new Customer();
            var purchaseOrder = new PurchaseOrder(customer);

            Assert.Contains(customer.PurchaseOrders, po => po.Id == po.Id);
        }
    }
}
