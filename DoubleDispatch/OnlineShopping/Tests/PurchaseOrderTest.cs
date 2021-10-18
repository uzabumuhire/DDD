namespace DoubleDispatch.OnlineShopping
{
    using DoubleDispatch.OnlineShopping.Domain.Model;

    using Xunit;

    public class PurchaseOrderTest
    {
        [Fact]
        public void AddItemAboveLimitReturnsFalse()
        {
            var po = new PurchaseOrder() { SpendLimit = 100 };
            po.TryAddItem(new LineItem(50));
            var item = new LineItem(51);
            Assert.False(po.TryAddItem(item));
        }  
    }
}
