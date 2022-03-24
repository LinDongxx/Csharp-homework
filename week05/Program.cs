using System;

namespace management
{
    public class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            while (true)
            {
                Console.WriteLine("输入数字选择操作 1：增加订单，2：删除订单，3：修改订单，4：查询订单，5：退出");
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1": orderService.addOrder(); break;
                    case "2": orderService.removeOrder(); break;
                    case "3": orderService.EditOrder(); break;
                    case "4": orderService.searchOrder(); break;
                    case "5": return;
                    default: Console.WriteLine("输入错误，请重新输入"); break;
                }
            }
        }
    }
}
