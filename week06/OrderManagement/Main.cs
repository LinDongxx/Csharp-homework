using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    class MainClass
    {
        public static void Main()
        {
            try
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

                OrderService orderService = new OrderService();
                orderService.AddOrder(order1);
                orderService.AddOrder(order2);
                orderService.AddOrder(order3);

                Console.WriteLine("\nGet Order By Id");
                Console.Write(orderService.QueryById(1)+"\n");
                Console.Write(orderService.QueryById(5));

                Console.WriteLine("\nGet All Orders");
                List<Order> orders = orderService.QueryAll();
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nGet Orders By Customer Name: Customer2");
                orders = orderService.QueryByCustomerName("Customer2");
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nGet Orders By Goods Name: eggs");
                orders = orderService.QueryByGoodsName("eggs");
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nGet Orders By Total Amount:1000");
                orders = orderService.QueryByTotalAmount(1000);
                orders.ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nRemove Order:Id = 2");
                orderService.RemoveOrder(2);
                orderService.QueryAll().ForEach(o => Console.WriteLine(o));

                Console.WriteLine("\nSort By Amount");
                orderService.Sort((o1, o2) => o2.TotalAmount.CompareTo(o1.TotalAmount));
                orderService.QueryAll().ForEach(o => Console.WriteLine(o));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
