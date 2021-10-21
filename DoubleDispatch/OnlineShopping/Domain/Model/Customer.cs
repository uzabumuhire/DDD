// References :

// - Jimmy Boggard's articles on LosTechies's blog (lostechies.com)
// https://lostechies.com/jimmybogard/2010/02/24/strengthening-your-domain-aggregate-construction/
// https://lostechies.com/jimmybogard/2010/03/10/strengthening-your-domain-encapsulated-collections/
// https://lostechies.com/jimmybogard/2010/03/24/strengthening-your-domain-encapsulating-operations/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    using System.Collections.Generic;

    class Customer
    {
        private List<PurchaseOrder> _purchaseOrders = new();
        private List<Fee> _fees = new();

        public string Province { get; set; }

        public IEnumerable<PurchaseOrder> PurchaseOrders => _purchaseOrders;

        public IEnumerable<Fee> Fees => _fees;
        
        public Fee ChargeFee(decimal amount)
        {
            var fee = new Fee(amount, this);
            _fees.Add(fee);
            return fee;
        }

        internal void AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _purchaseOrders.Add(purchaseOrder);
        }
    }
}
