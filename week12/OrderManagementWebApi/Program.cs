// See https://aka.ms/new-console-template for more information

using static OrderManagementWebApi.Order;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderManagementWebApi
{
    class Program
    {
        static HttpListener httpobj;
        static OrderService orderService;
        static Order current;

        static void Main(string[] args)
        {
            orderService = new OrderService();
            var cargos = new Cargos();
            current = new Order("you");
            orderService.AddOrder(current);
            cargos.printCargos();
            try
            {
                orderService.Import();
                Console.WriteLine("成功读取了存档数据: ");
                Console.WriteLine(JsonConvert.SerializeObject(orderService.OrderList));
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("未找到存档数据。");
            }
            
            httpobj = new HttpListener();
            httpobj.Prefixes.Add("http://localhost:8081/");
            httpobj.Start();

            httpobj.BeginGetContext(handleRequest, null);
            Console.WriteLine("服务端初始化完毕");
            Console.ReadKey();
        }

        static void handleRequest(IAsyncResult ar)
        {
            httpobj.BeginGetContext(handleRequest, null);

            var context = httpobj.EndGetContext(ar);
            var request = context.Request;
            var response = context.Response;
            response.AddHeader("Conten-Type", "application/json");
            response.ContentEncoding = Encoding.UTF8;

            string returnS = "done";
            string requestBody = null;

            if (!request.HasEntityBody)
            {
                returnS = "request Body Wrong";
            }
            using (Stream bodys = request.InputStream) // here we have data
            {
                using (StreamReader reader = new System.IO.StreamReader(bodys, request.ContentEncoding))
                {
                    requestBody = reader.ReadToEnd();
                }
            }

            JObject body = JsonConvert.DeserializeObject<JObject>(requestBody)!;

            var method = body["method"].ToString();
            var data = body["data"].ToString();

            if (method == "Add")
            {
                returnS = handleAdd(data);
            }else if (method == "Update")
            {
                returnS = handleUpdate(data);
            }else if (method == "Query")
            {
                returnS = handleQuery(data);
            }else if (method == "Delete")
            {
                returnS = handleDelete(data);
            }else
            {
                returnS = "Something Wrong With You Request";
            }

            var returnByteArr = Encoding.UTF8.GetBytes(returnS);
            try
            {
                using (var stream = response.OutputStream)
                {
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"网络崩了：{ex.ToString()}");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"处理完成");
        }

        static string handleAdd(string data)
        {
            JObject da = JsonConvert.DeserializeObject<JObject>(data)!;
            try
            {
                current.AddDetail(da["name"].ToString(), int.Parse(da["number"].ToString()));
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return JsonConvert.SerializeObject(current.Details);
        }
        static string handleUpdate(string data)
        {
            JObject da = JsonConvert.DeserializeObject<JObject>(data)!;

            try
            {
                orderService.ModifyOrder(current.ID, da["name"].ToString(), int.Parse(da["number"].ToString()));
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return JsonConvert.SerializeObject(current.Details); 
        }

        static string handleQuery(string data)
        {
            JObject da = JsonConvert.DeserializeObject<JObject>(data)!;
            try
            {
                return orderService.Query(da["operate"].ToString(), da["source"].ToString());
            }catch (Exception e)
            {
                return e.Message;
            }
            
        }

        static string handleDelete(string data)
        {
            JObject da = JsonConvert.DeserializeObject<JObject>(data)!;
            List<OrderDetail> list = current.Details;
            OrderDetail temp = null;
            foreach (var i in list)
            {
                if (i.ProductName == da["name"].ToString())
                {
                    temp = i;
                };
            }
            current.Details.Remove(temp);
            return JsonConvert.SerializeObject(current.Details);
        }

    }
}