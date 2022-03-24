using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace management
{
    public class OrderService
    {
        public List<Order> OrderList = new List<Order>();
        public void addOrder()          //增加订单
        {
            try
            {
                Console.WriteLine("请输入订单编号：");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入客户名称：");
                string customer = Console.ReadLine();
                Console.WriteLine("请输入时间：");
                string date = Console.ReadLine();
                Order newOrder = new Order(id, customer, date);
                Console.WriteLine("输入订单项：");
                bool checkAddDetail = true;

                while (checkAddDetail)
                {
                    Console.WriteLine("请输入物品名称：");
                    string name = Console.ReadLine();
                    Console.WriteLine("请输入购买数量：");
                    int count = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("请输入单价：");
                    double unitPrice = Convert.ToDouble(Console.ReadLine());
                    newOrder.addOrderDetail(name, count, unitPrice);
                    Console.WriteLine("是否继续添加订单项,1：是，其他：否");
                    string x = Console.ReadLine();
                    if (x != "1") checkAddDetail = false;
                }
                if (OrderList.Contains(newOrder))
                {
                    Console.WriteLine("订单重复");
                    return;
                }
                this.OrderList.Add(newOrder);
                Console.WriteLine("建立成功");
                Console.WriteLine("-------------------------");
            }
            catch
            {
                Console.WriteLine("输入错误");
            }

        }
        public void removeOrder()           //删除订单
        {
            Console.WriteLine("以下是所有订单\n请输入要删除的订单编号");
            ShowOrders(OrderList);
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                int index = -1;
                for (int i = 0; i < OrderList.Count; i++)
                {
                    if (OrderList[i].Id == id) index = i;
                }
                if (index == -1)
                {
                    Console.WriteLine("订单不存在");
                    return;
                }
                OrderList.RemoveAt(index);

            }
            catch
            {
                Console.WriteLine("输入错误");
            }

        }

        public void EditOrder()
        {

            Console.WriteLine("以下是所有订单\n请输入要修改的订单编号");
            ShowOrders(OrderList);
            try
            {
                int orderId = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < OrderList.Count; i++)
                {
                    if (OrderList[i].Id == orderId)
                    {
                        Console.WriteLine("请输入新的客户名称：");
                        string customer = Console.ReadLine();
                        Console.WriteLine("请输入新的时间：");
                        string date = Console.ReadLine();
                        Order newOrder = new Order(orderId, customer, date);
                        Console.WriteLine("输入新的订单项：");
                        bool checkAddDetail = true;

                        while (checkAddDetail)
                        {
                            Console.WriteLine("请输入物品名称：");
                            string name = Console.ReadLine();
                            Console.WriteLine("请输入购买数量：");
                            int count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("请输入单价：");
                            double unitPrice = Convert.ToDouble(Console.ReadLine());
                            newOrder.addOrderDetail(name, count, unitPrice);
                            Console.WriteLine("是否继续添加订单项,1：是，其他：否");
                            string x = Console.ReadLine();
                            if (x != "1") checkAddDetail = false;
                        }
                        if (OrderList.Contains(newOrder))
                        {
                            Console.WriteLine("订单重复");
                            return;
                        }
                        this.OrderList[i] = newOrder;
                    }
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }


        }
        public void searchOrder()
        {
            try
            {
                Console.WriteLine("输入数字选择查询方式，1：订单号，2：商品名称，3：客户名，4或其他：订单金额");
                string choose = Console.ReadLine();
                IOrderedEnumerable<Order> queryOrders;
                switch (choose)
                {
                    case "1":
                        Console.WriteLine("输入订单号");
                        int id = Convert.ToInt32(Console.ReadLine());
                        queryOrders = from s in OrderList
                                      where s.Id == id
                                      orderby s.TotalAmount
                                      select s;
                        break;
                    case "2":
                        Console.WriteLine("输入商品名称");
                        string productName = Console.ReadLine();
                        queryOrders = OrderList.Where(s =>
                        {
                            foreach (OrderDetail detail in s.orderDetails)
                            {
                                if (detail.ProductName == productName)
                                    return true;
                            }
                            return false;
                        }).OrderBy(s => s.TotalAmount);
                        break;
                    case "3":

                        Console.WriteLine("输入客户名称：");
                        string name = Console.ReadLine();
                        queryOrders = from s in OrderList
                                      where s.Customer == name
                                      orderby s.TotalAmount
                                      select s;
                        break;
                    default:
                        int minNum, maxNum;
                        Console.WriteLine("输入要查询的最小金额：");
                        minNum = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("输入要查询的最大金额：");
                        maxNum = Convert.ToInt32(Console.ReadLine());

                        var query1 = from s in OrderList
                                     where maxNum >= s.TotalAmount
                                     orderby s.TotalAmount
                                     select s;
                        queryOrders = from s in query1
                                      where s.TotalAmount >= minNum
                                      orderby s.TotalAmount
                                      select s;
                        break;
                }
                List<Order> queryList = queryOrders.ToList();
                ShowOrders(queryList);
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }

        public void ShowOrders(List<Order> orders)
        {
            Console.WriteLine("订单号 客户名 日期 总金额");
            foreach (Order order in orders)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("{0} {1} {2} {3}", order.Id, order.Customer, order.Date, order.TotalAmount);
                Console.WriteLine("订单明细项：");
                order.showOrderDetails();
                Console.WriteLine("\n");
            }
            Console.WriteLine("---------------------------");
        }

    }
}
