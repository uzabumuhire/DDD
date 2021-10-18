namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    using System;

    class LineItem
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; } // avoid having circular reference between aggregate and children
        public LineItem(decimal cost)
        {
            Cost = cost;
        }
        public decimal Cost { get; private set; }

        public bool TryUpdateCost(decimal cost, PurchaseOrder parent)
        {
            if (parent.Id != PurchaseOrderId) throw new Exception("Incorrect parent PO.");
            // check if new cost would exceed PO
            if (parent.CheckLimit(this, cost))
            {
                Cost = cost;
                return true;
            }
            return false;
        }

        // alternate implementation
        public bool TryUpdateCost(decimal cost, IPurchaseOrderRepository purchaseOrderRepository)
        {
            var parent = purchaseOrderRepository.GetById(PurchaseOrderId);
            // check if new cost would exceed PO
            if (parent.CheckLimit(this, cost))
            {
                Cost = cost;
                return true;
            }
            return false;
        }
    }
}
