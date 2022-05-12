using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    public class Order : IComparable<Order>
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime CreateTime { get; set; }

        public float TotalAmount
        {
            get => Details.Sum(d => d.Amount);
        }

        public List<OrderDetail> Details { get; } = new List<OrderDetail>();


        public Order(string orderId, Customer customer, DateTime createTime)
        {
            OrderId = orderId;
            Customer = customer;
            CreateTime = createTime;
        }

       
        public Order(Order another)
        {
            OrderId = another.OrderId;
            Customer = another.Customer;
            CreateTime = another.CreateTime;
            another.Details.ForEach(d => Details.Add(d));
        }

        public void AddDetails(OrderDetail orderDetail)
        {
            if (this.Details.Contains(orderDetail))
            {
                throw new ApplicationException($"The goods ({orderDetail.GoodsItem.Name}) exist in order {OrderId}");
            }
            Details.Add(orderDetail);
        }

        public int CompareTo(Order other)
        {
            if (other == null) return 1;
            return this.OrderId.CompareTo(other.OrderId);
        }

        public override bool Equals(object obj)
        {
            return obj is Order order && OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            return 2108858624 + OrderId.GetHashCode();
        }

        public void RemoveDetails(int num)
        {
            Details.RemoveAt(num);
        }

        public override string ToString()
        {
            String result = $"\torderId:{OrderId}, customer:({Customer}), totalAmount:{TotalAmount}";
            Details.ForEach(detail => result += "\n\t\t" + detail);
            return result+"\n";
        }

        //public bool IsValid()
        //{
        //    return this.OrderId != 0 && this.Details != null && this.Details.Count > 0;
        //}

    }
}
