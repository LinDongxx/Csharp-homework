using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    public class Goods
    {
        public string Id { get; set; }
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
            Id = Guid.NewGuid().ToString();
        }
        public override bool Equals(object obj)
        {
            return obj is Goods goods && Id == goods.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Price:{Price}";
        }
    }
}
