namespace DoubleDispatch.OnlineShopping.Domain.Repositories
{
    using System.Collections.Generic;

    using DoubleDispatch.OnlineShopping.Domain.Model;

    class InMemoryPurchaseOrderRepository : IPurchaseOrderRepository
    {
        private Dictionary<int, PurchaseOrder> _collection = new();

        public void Add(PurchaseOrder purchaseOrder)
        {
            if (!_collection.ContainsKey(purchaseOrder.Id))
            {
                _collection.Add(purchaseOrder.Id, purchaseOrder);
            }
        }

        public PurchaseOrder GetById(int id)
        {
            if (!_collection.ContainsKey(id)) return null;
            return _collection[id];
        }
    }
}
