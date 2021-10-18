namespace DoubleDispatch.OnlineShopping.Domain.Model
{
    using System.Linq;
    using System.Collections.Generic;

    class PurchaseOrder
    {
        public int Id { get; set; }
        private List<LineItem> _items { get; } = new List<LineItem>();
        public IEnumerable<LineItem> Items => _items.ToList();

        public decimal SpendLimit { get; set; }

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
    }
}
