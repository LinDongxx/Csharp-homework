using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace management
{
    public class OrderDetail
    {
        public string ProductName { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }

        public double TotalPrice { get { return UnitPrice * Count; } }
        public OrderDetail(string name, int count, double unitPrice)
        {
            this.ProductName = name;
            this.Count = count;
            this.UnitPrice = unitPrice;
        }

        public override string ToString()
        {
            return ProductName + " " + Count + " " + UnitPrice;
        }

        public override bool Equals(object obj)
        {
            OrderDetail other = obj as OrderDetail;
            return other != null && other.ProductName == this.ProductName && other.Count == this.Count && other.UnitPrice == this.UnitPrice;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}

