using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementWebApi
{
    public class OrderDetail
    {
        public string ProductName { get; set; }   //商品名
        public int ProductQuantity { get; set; }   //商品数量

        Cargos cargos = new Cargos();
        public override bool Equals(object obj)
        {
            if (obj is OrderDetail o)
            {
                return ProductName == o.ProductName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ProductName.GetHashCode();
        }

        public override string ToString()
        {
            double Price = cargos.GetPrice(ProductName) * ProductQuantity;
            return $"商品：{ProductName} 数量：{ProductQuantity} 价格：{Price}";
        }
    }
}
