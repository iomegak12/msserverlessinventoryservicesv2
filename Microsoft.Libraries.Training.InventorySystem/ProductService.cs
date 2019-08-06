using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Libraries.Training.InventorySystem
{
    public class ProductService : IProductService
    {
        private string connectionString = default(string);
        public ProductService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            this.connectionString = connectionString;
        }

        public Product GetProductDetails(int productId)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProductsContext>();

            dbContextOptionsBuilder.UseSqlServer(this.connectionString);

            var filteredProduct = default(Product);

            using (var context = new ProductsContext(dbContextOptionsBuilder.Options))
            {
                filteredProduct = context.Products.
                    Where(product => product.ProductId.Equals(productId)).
                    FirstOrDefault();
            }

            return filteredProduct;
        }
    }
}
