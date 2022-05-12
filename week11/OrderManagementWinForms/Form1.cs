using OrderManagement;
using System.Linq;
using System.Windows.Forms;

namespace OrderForm
{
    public partial class FormDetailEdit : Form
    {
        public OrderDetail Detail { get; set; }

        public FormDetailEdit()
        {
            InitializeComponent();
        }

        public FormDetailEdit(OrderDetail detail) : this()
        {
            this.Detail = detail;
            this.bdsDetail.DataSource = detail;
            using (OrderContext ctx = new OrderContext())
            {
                bdsGoods.DataSource = ctx.Goods.ToList();
            }
        }

    }
}
