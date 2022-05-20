using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementWebApi
{
    public class Order
    {
        Cargos cargos = new Cargos();
        public string Customer;
        public string ID;
        public List<OrderDetail> Details;
        public double TotalPrice = 0;

        public Order()
        {

        }

        public Order(string customer)
        {
            Customer = customer;
            ID = GetOrderID(customer);
            Details = new List<OrderDetail>();
        }

        public void AddDetail(string name, int quantity)
        {
            if (cargos.CargoExists(name))
            {
                var detail = new OrderDetail() { ProductName = name, ProductQuantity = quantity };
                if (Details.Exists(i => i.Equals(detail)))
                {
                    throw new DetailExistsException("当前添加的商品已存在。");
                }
                Details.Add(detail);
                CalculateTotalPrice();
            }
            else
            {
                throw new CargoNotExistsException("当前添加的商品不存在。");
            }

        }

        public void ModifyDetail(string name, int quantity)
        {
            var detail_new = new OrderDetail() { ProductName = name, ProductQuantity = quantity };
            bool flag = false;
            OrderDetail detail = null;
            foreach (var i in Details)
            {
                if (i.ProductName == name)
                {
                    detail = i;
                    flag = true;
                }
            }
            if (flag == false)
            {
                throw new DetailInvalidException("当前修改的商品不存在。");
            }

            if (quantity == 0)
            {
                Details.Remove(detail);
            }
            else
            {
                Details[Details.IndexOf(detail)] = detail_new;
            }
            CalculateTotalPrice();
        }

        public double CalculateTotalPrice()
        {
            TotalPrice = 0;
            foreach (var i in Details)
            {
                TotalPrice += cargos.GetPrice(i.ProductName) * i.ProductQuantity;
            }
            return TotalPrice;
        }

        public override string ToString()
        {
            string re = $"订单号：{ID}\n客户：{Customer}\n";
            foreach (var i in Details)
            {
                re = re + i.ToString() + "\n";
            }
            return re;
        }

        public bool HasCargo(String name)
        {
            foreach (var i in Details)
            {
                if (i.ProductName == name)
                    return true;
            }
            return false;
        }

        private static string GetOrderID(string customer)
        {
            string source = customer + DateTime.Now.ToString();
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, source);
            return hash;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }


        public override bool Equals(object obj)
        {
            if (obj is Order o)
            {
                return ID == o.ID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public class DetailExistsException : ApplicationException
        {
            public DetailExistsException(string message) : base(message)
            {
            }
        }

        public class DetailInvalidException : ApplicationException
        {
            public DetailInvalidException(string message) : base(message)
            {
            }
        }

    }
}
