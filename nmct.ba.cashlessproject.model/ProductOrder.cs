using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class ProductOrder
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public ProductOrder() { }

        public ProductOrder(Product product)
        {
            Product = product;
            Quantity = 0;
        }

        public static ObservableCollection<ProductOrder> ConstructsFromProducts(IEnumerable<Product> list)
        {
            ObservableCollection<ProductOrder> result = new ObservableCollection<ProductOrder>();

            foreach (Product prod in list)
                if(prod != null ) result.Add(new ProductOrder(prod));

            return result;
        }

        public override string ToString()
        {
            if (Product == null) return "Leeg product";
            return Product.Name + " " + Quantity;
        }
    }
}
