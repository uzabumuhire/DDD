// From Steve Smith's blog (ardalis.com) :
// https://ardalis.com/double-dispatch-in-c-and-ddd/

namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    using System.Linq;
    using System.Collections.Generic;

    //  An Aggregate has an Aggregate Root and some number of children,
    //  forming a tree structure. A purchase order (PO) is defined as
    //  an aggregate with the PO as the root and individual line items
    //  as children. 
    class PurchaseOrder
    {
        public int Id { get; set; }

        // In .NET and when using EF, it's a good idea to have one-directional
        // references between entities, so the aggregate is modeled to give the
        // PurchaseOrder type a navigation property (collection) to LineItem.
        private List<LineItem> _items { get; } = new List<LineItem>();
        public IEnumerable<LineItem> Items => _items.ToList();

        public decimal SpendLimit { get; set; }

        // The root is responsible for ensuring that the total cost of all
        // items on the PO does not exceed its SpendLimit.
        public bool CheckLimit(LineItem item, decimal newValue)
        {
            var currentSum = Items.Sum(i => i.Cost);
            decimal difference = newValue - item.Cost;

            return currentSum + difference <= SpendLimit;
        }

        public bool CheckLimit(LineItem newItem)
        {
            return Items.Sum(i => i.Cost) + newItem.Cost <= SpendLimit;
        }

        public bool TryAddItem(LineItem item)
        {
            if (CheckLimit(item))
            {
                _items.Add(item);
                return true;
            }
            return false;
        }

        // Double dispatch through the domain service.
        public bool TryAddItem(LineItem item, IPurchaseOrderService poService)
        {
            if (poService.WouldAddBeUnderLimit(this, item))
            {
                _items.Add(item);
                return true;
            }
            return false;
        }
    }
}
