using System;

namespace Microsoft.Libraries.Training.InventorySystem
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Units { get; set; }
        public int UnitPrice { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}, {3}, {4}",
                this.ProductId, this.Title, this.Description, this.Units, this.UnitPrice);
        }
    }
}
