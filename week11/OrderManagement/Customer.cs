using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    public class Customer
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public Customer(string name) : this()
        {
            Name = name;
        }
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }
      

       

        public override string ToString()
        {
            return $"customerId:{Id}, CustomerName:{Name}";
        }

        public override bool Equals(object obj)
        {
            return obj is Customer customer && Id == customer.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
