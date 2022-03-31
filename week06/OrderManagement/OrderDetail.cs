using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    public class OrderDetail
    {

        public Goods Goods { get; set; }

        public int Quantity { get; set; }

        public float Amount
        {
            get => Goods.Price * Quantity;
        }

        public OrderDetail(Goods goods, int quantity)
        {
            this.Goods = goods;
            this.Quantity = quantity;
        }

        private OrderDetail()
        {
            Goods = null;
            this.Quantity = 0;
        }
        public override bool Equals(object obj)
        {
            var detail = obj as OrderDetail;
            return detail != null && EqualityComparer<Goods>.Default.Equals(Goods, detail.Goods);
        }

        public override int GetHashCode()
        {
            return 785010553 + EqualityComparer<Goods>.Default.GetHashCode(Goods);
        }

        public override string ToString()
        {
            return $"OrderDetail:({Goods},Quantity:{Quantity})";
        }
    }
}
