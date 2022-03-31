using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    public class Goods
    {
        public string Name { get; set; }

        private float price;

        public float Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("the price must be >= 0!");
                price = value;
            }
        }

        public Goods(string name, float price)
        {
            this.Name = name;
            this.Price = price;
        }

        private Goods()
        {
            Name = null;
            Price = 0;
        }
        public override bool Equals(object obj)
        {
            var goods = obj as Goods;
            return goods != null && Name == goods.Name;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"Name:{Name}, Price:{Price}";
        }
    }
}
