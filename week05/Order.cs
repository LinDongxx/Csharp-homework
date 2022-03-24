using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace management
{
    public class Order : IComparable  //单个订单项
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public double TotalAmount
        {
            get
            {
                double amount = 0;
                foreach (var orderDetail in orderDetails)
                {
                    amount += orderDetail.TotalPrice;
                }
                return amount;
            }
        }
        public string Date { get; set; }

        public List<OrderDetail> orderDetails = new List<OrderDetail>();

        public int CompareTo(object obj)
        {
            Order other = obj as Order;
            if (other == null) throw new ArgumentException();
            return this.Id.CompareTo(other.Id);
        }
        public override bool Equals(object obj)
        {
            Order other = obj as Order;
            if (other.Id != this.Id) return false;
            if (other.Customer != this.Customer) return false;
            if (other.TotalAmount != this.TotalAmount) return false;
            if (other.Date != this.Date) return false;
            if (other.orderDetails.Count != this.orderDetails.Count) return false;
            return this.orderDetails.Except(other.orderDetails).Count() == 0;
        }
        public override string ToString()
        {
            string info;
            info = "id: " + Id + ", customer: " + Customer + ", total amount: " + TotalAmount + ", date: " + Date;
            return info;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public Order(int id, string customer, string date)
        {
            this.Id = id;
            this.Customer = customer;
            this.Date = date;
        }

        public void showOrderDetails()
        {
            Console.WriteLine("序号 商品名称 交易数量 单价");
            for (int i = 0; i < orderDetails.Count; i++)
            {
                OrderDetail orderDetail = orderDetails[i];
                Console.WriteLine("{0} {1} {2} {3}", i + 1, orderDetail.ProductName, orderDetail.Count, orderDetail.UnitPrice);
            }
        }

        public void addOrderDetail(string name, int count, double unitPrice)   //添加订单项
        {
            OrderDetail newDetail = new OrderDetail(name, count, unitPrice);
            if (orderDetails.Contains(newDetail))
            {
                Console.WriteLine("订单明细重复");
                return;
            }
            this.orderDetails.Add(newDetail);
        }

        public void removeOrderDetail(int index)
        {
            this.orderDetails.RemoveAt(index - 1);
        }

        public void editOrderDerail(int index, string name, int count, double unitPrice)
        {
            this.orderDetails[index - 1].ProductName = name;
            this.orderDetails[index - 1].Count = count;
            this.orderDetails[index - 1].UnitPrice = unitPrice;
        }

    }
}
