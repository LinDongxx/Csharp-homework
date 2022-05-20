using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementWebApi
{
    class Cargo
    {
        public string name { get; set; }
        public double price { get; set; }
    }

    class Cargos
    {
        private List<Cargo> CargosList = new List<Cargo>();

        public Cargos()
        {
            InitializeCargos();
        }

        public void InitializeCargos()
        {
            CargosList.Add(new Cargo() { name = "葡萄", price = 20 });
            CargosList.Add(new Cargo() { name = "香蕉", price = 12 });
            CargosList.Add(new Cargo() { name = "菠萝", price = 16 });
            CargosList.Add(new Cargo() { name = "苹果", price = 10 });
            CargosList.Add(new Cargo() { name = "哈密瓜", price = 25 });
        }

        public bool CargoExists(string goodname)
        {
            foreach (var g in CargosList)
            {
                if (g.name == goodname)
                {
                    return true;
                }
            }
            return false;
        }

        public double GetPrice(string cargoname)
        {
            foreach (var c in CargosList)
            {
                if (c.name == cargoname)
                {
                    return c.price;
                }
            }
            return 0;
        }

        public void printCargos()
        {
            Console.WriteLine("所有商品: ");
            foreach (var i in CargosList)
            {
                Console.WriteLine("name : {0}, price : {1}",i.name,i.price);
            }
        }
    }

    public class CargoNotExistsException : ApplicationException
    {
        public CargoNotExistsException(string message) : base(message)
        {
        }
    }
}
