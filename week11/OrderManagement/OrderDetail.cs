using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{

    public class OrderDetail
    {

        public string GoodsId { get; set; }
        public Goods GoodsItem { get; set; }

        public int Quantity { get; set; }

        public float Amount
        {
            get => GoodsItem.Price * Quantity;
        }

        public OrderDetail(Goods goods, int quantity)
        {
            this.GoodsItem = goods;
            this.Quantity = quantity;
        }

        private OrderDetail()
        {
            GoodsItem = null;
            this.Quantity = 0;
        }
        public override bool Equals(object obj)
        {
            return obj is OrderDetail detail && EqualityComparer<Goods>.Default.Equals(GoodsItem, detail.GoodsItem);
        }

        public override int GetHashCode()
        {
            return 785010553 + EqualityComparer<Goods>.Default.GetHashCode(GoodsItem);
        }

        public override string ToString()
        {
            return $"OrderDetail:({GoodsItem},Quantity:{Quantity})";
        }
    }
}
