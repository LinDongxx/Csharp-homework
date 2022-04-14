using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrderManagement
{

    public class OrderService
    {
        public List<Order> orderList = new List<Order>();

        public OrderService()
        {
        }

        public void Export(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, orderList);
            }

        }

        public void Import(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                orderList = (List<Order>)xmlSerializer.Deserialize(fileStream);
            }
        }

        public void AddOrder(Order order)
        {
            if (order == null || !order.IsValid())
            {
                throw new ApplicationException($"Invalid order!");
            }
            if (orderList.Contains(order))
            {
                throw new ApplicationException($"the order {order.Id} already exists!");
            }
            orderList.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            if (order == null || !order.IsValid())
            {
                throw new ApplicationException($"Invalid order!");
            }
            Order orderInList = QueryById(order.Id);
            if (orderInList == null)
            {
                throw new ApplicationException($"the order {order.Id} does not exist!");
            }
            orderList.Remove(orderInList);
            orderList.Add(order);
        }

        public void RemoveOrder(int orderId)
        {
            orderList.RemoveAll(o => o.Id == orderId);
        }

        public Order QueryById(int orderId)
        {
            return orderList.Where(o => o.Id == orderId).FirstOrDefault();
        }

        public List<Order> QueryAll()
        {
            return orderList.OrderBy(o => o.TotalAmount).ToList<Order>();
        }

        public List<Order> QueryByGoodsName(string goodsName)
        {
            var query = orderList
              .Where(o => o.Details.Any(d => d.Goods.Name == goodsName))
              .OrderBy(o => o.TotalAmount);
            return query.ToList();
        }

        public List<Order> QueryByTotalAmount(float totalAmount)
        {
            var query = orderList
              .Where(o => o.TotalAmount >= totalAmount)
              .OrderBy(o => o.TotalAmount);
            return query.ToList();
        }

        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = orderList
                .Where(o => o.Customer.Name == customerName)
                .OrderBy(o => o.TotalAmount);
            return query.ToList();
        }

        public IEnumerable<Order> Query(Predicate<Order> condition)
        {
            return orderList
              .Where(o => condition(o))
              .OrderBy(o => o.TotalAmount);
        }

        public void Sort(Comparison<Order> comparison = null)
        {
            comparison = comparison ?? ((Order o1, Order o2) => o1.Id.CompareTo(o2.Id));
            orderList.Sort(comparison);
        }

    }
}
