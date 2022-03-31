using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderManagement;
namespace MergeOrder
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitOrder();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static void InitOrder()
        {
            Customer customer1 = new Customer(1, "Customer1");
            Customer customer2 = new Customer(2, "Customer2");

            Goods milk = new Goods("milk", 69.9f);
            Goods eggs = new Goods("eggs", 4.99f);
            Goods apple = new Goods("apple", 5.59f);

            Order order1 = new Order(1, customer1);
            order1.AddDetails(new OrderDetail(apple, 8));
            order1.AddDetails(new OrderDetail(eggs, 10));

            Order order2 = new Order(2, customer2);
            order2.AddDetails(new OrderDetail(eggs, 10));
            order2.AddDetails(new OrderDetail(milk, 10));

            Order order3 = new Order(3, customer2);
            order3.AddDetails(new OrderDetail(milk, 100));

            OrderService orderService1 = new OrderService();
            orderService1.AddOrder(order1);
            orderService1.AddOrder(order2);
            orderService1.AddOrder(order3);


            Customer customer3 = new Customer(3, "Customer3");
            Customer customer4 = new Customer(4, "Customer4");

            Goods bread = new Goods("bread", 3.7f);
            Goods fish = new Goods("fish", 120f);
            Goods potato = new Goods("potato", 4.5f);

            Order order4 = new Order(4, customer3);
            order4.AddDetails(new OrderDetail(bread, 10));
            order4.AddDetails(new OrderDetail(fish, 2));

            Order order5 = new Order(5, customer4);
            order5.AddDetails(new OrderDetail(fish, 1));
            order5.AddDetails(new OrderDetail(potato, 4));

            Order order6 = new Order(6, customer3);
            order6.AddDetails(new OrderDetail(potato, 10));

            OrderService orderService2 = new OrderService();
            orderService2.AddOrder(order4);
            orderService2.AddOrder(order5);
            orderService2.AddOrder(order6);

            orderService1.Export("order1.xml");
            orderService2.Export("order2.xml");
        }
    }
}
