using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.Training.InventorySystem
{
    public interface IProductService
    {
        Product GetProductDetails(int productId);
    }
}
