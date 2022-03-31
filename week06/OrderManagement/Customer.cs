using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    public class Customer
    {

        public uint Id { get; set; }

        public string Name { get; set; }

        public Customer(uint id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        private Customer()
        {
            Id = 0;
            Name = null;
        }
        public override string ToString()
        {
            return $"customerId:{Id}, CustomerName:{Name}";
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null && Id == customer.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
