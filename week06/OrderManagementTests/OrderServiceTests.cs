using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void ImportTest()
        {
            OrderService service = InitTestService();
            service.Export(".\\order.xml");

            OrderService serviceIn = new OrderService();
            serviceIn.Import(".\\order.xml");
            Assert.IsTrue(ListEqual(service.QueryAll(),serviceIn.QueryAll()));
        }

        [TestMethod()]
        public void AddOrderTest()
        {
            OrderService service = InitTestService();
            Order order4 = new Order(4, new Customer(3, "Customer3"));
            order4.AddDetails(new OrderDetail(eggs, 10));
            service.AddOrder(order4);
            Assert.IsTrue(ListEqual(service.QueryAll(), new List<Order> { order1, order2, order3, order4 }));
        }

        [TestMethod()]
        public void UpdateOrderTest()
        {
            OrderService service = InitTestService();
            Order order4 = new Order(order3);
            order4.AddDetails(new OrderDetail(eggs, 20));
            service.UpdateOrder(order4);
            Assert.IsTrue(ListEqual(service.QueryAll(), new List<Order> { order1, order2, order4 }));
        }

        [TestMethod()]
        public void RemoveOrderTest()
        {
            OrderService service = InitTestService();
            service.RemoveOrder(1);
            Assert.IsTrue(ListEqual(service.QueryAll(),new List<Order> { order2, order3 }));
        }

        [TestMethod()]
        public void QueryByIdTest()
        {
            OrderService service = InitTestService();
            Assert.AreEqual(service.QueryById(1), order1);
        }

        [TestMethod()]
        public void QueryAllTest()
        {
            OrderService service = InitTestService();

            List<Order> list = new List<Order> { order1, order2, order3 };

            Assert.IsTrue(ListEqual(service.QueryAll(), list));
        }

        [TestMethod()]
        public void QueryByGoodsNameTest()
        {
            OrderService service = InitTestService();

            List<Order> list = new List<Order> { order1, order2 };

            Assert.IsTrue(ListEqual(service.QueryByGoodsName("eggs"),list));
        }

        [TestMethod()]
        public void QueryByTotalAmountTest()
        {
            OrderService service = InitTestService();

            List<Order> list = new List<Order> { order3 };

            Assert.IsTrue(ListEqual(service.QueryByTotalAmount(1000), list));
        }

        [TestMethod()]
        public void QueryByCustomerNameTest()
        {
            OrderService service = InitTestService();

            List<Order> list = new List<Order> { order2, order3 };

            Assert.IsTrue(ListEqual(service.QueryByCustomerName("Customer2"), list));
        }

        private static readonly Customer customer1 = new Customer(1, "Customer1");
        private static readonly Customer customer2 = new Customer(2, "Customer2");

        private static readonly Goods milk = new Goods("milk", 69.9f);
        private static readonly Goods eggs = new Goods("eggs", 4.99f);
        private static readonly Goods apple = new Goods("apple", 5.59f);

        private static readonly Order order1;
        private static readonly Order order2;
        private static readonly Order order3;

        static OrderServiceTests()
        {
            order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(apple, 8));
            order1.AddDetails(new OrderDetail(eggs, 10));

            order2 = new Order(2, customer2);
            order2.AddDetails(new OrderDetail(eggs, 10));
            order2.AddDetails(new OrderDetail(milk, 10));

            order3 = new Order(3, customer2);
            order3.AddDetails(new OrderDetail(milk, 100));
        }

        private static OrderService InitTestService()
        {
            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);

            return orderService;
        }

        private static bool ListEqual<T>(List<T> l1, List<T> l2)
        {
            if (l1.Count != l2.Count) return false;
            return l1.Except(l2).Count() == 0;
        }
    }
}