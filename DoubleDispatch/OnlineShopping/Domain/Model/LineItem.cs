namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    using System;

    class LineItem
    {
        public int Id { get; set; }

        //  LineItem does not have a navigation property to PurchaseOrder
        //  (this also avoids serialization issues due to circular references).
        //  Instead, a LineItem has a PurchaseOrderId property which can be
        //  used to get an instance of a PO from a repository any time one
        //  is needed.
        public int PurchaseOrderId { get; set; } 

        public LineItem(decimal cost)
        {
            Cost = cost;
        }
        public decimal Cost { get; private set; }

        // A LineItem instance whose cost is being updated doesn't have enough
        // information to determine whether the new cost will break the PO's
        // invariant of its spending limit. Thus, we can use double dispatch
        // to pass in the parent PO and have the LineItem instance pass itself
        // to the parent PO so that it can perform the check. This requires
        // that we pass in the parent PO instance to the TryUpdateCost method,
        // which is problematic because there's nothing in the code that requires
        // us to pass any particular PO instance. We're expecting the LineItem
        // parent, but the code will allow any instance. Thus, we must perform
        // runtime checks to ensure the correct instance has been passed.
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

        // Another approach is to use a repository as the second parameter,
        // which is then used to fetch the appropriate parent PO by using
        // the LineItem's PurchaseOrderId property. This is somewhat better
        // since it ensures we always get the proper parent PO, but does
        // require the calling code to get a repository instance for us to use.
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
