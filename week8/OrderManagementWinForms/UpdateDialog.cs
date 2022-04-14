using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderManagement;
namespace OrderManagementWinForms
{
    public partial class UpdateDialog : Form
    {
        public UpdateDialog()
        {
            InitializeComponent();
            labelTime.Text = CreateTime.ToString();
            bindingSourceDetailRows.DataSource = details;
        }
        public UpdateDialog(Order order)
        {
            InitializeComponent();
            labelTime.Text = CreateTime.ToString();
            bindingSourceDetailRows.DataSource = details;
            textBoxOrderId.Text = order.Id.ToString();
            textBoxCustomerId.Text= order.Customer.Id.ToString();
            textBoxCustomerName.Text = order.Customer.Name;
            labelTime.Text = order.CreateTime.ToString();
            order.Details.ForEach(d => details.Add(new DetailRow(d.Goods.Id,d.Goods.Name,d.Goods.Price,d.Quantity)));
        }
        private BindingList<DetailRow> details = new BindingList<DetailRow>();
        public int OrderId;
        public int CustomerId;
        public string CustomerName;
        public DateTime CreateTime = DateTime.Now;
        public Order order = null;
        public float TotalAmount { 
            get {
                float total = details.Sum(d => d.Amount); 
                labelAmount.Text = total.ToString();    
                return total;
            } 
        }

        public class DetailRow
        {
            public int GoodsId { get; set; }
            public string GoodsName { get; set; }
            public float GoodsPrice { get; set; }
            public int Quantity { get; set; }
            public float Amount
            {
                get { return GoodsPrice * Quantity; }
            }
            public DetailRow()
            {
                GoodsId = 0;
                GoodsName = null;
                Quantity = 0;
                GoodsPrice = 0; 
            }
            public DetailRow(int gid,string gn,float gp,int q)
            {
                GoodsId = gid;
                GoodsName = gn;
                GoodsPrice = gp;
                Quantity = q;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TotalAmount.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            order = null;
            try
            {
                if (details.Count == 0) return;
                OrderId = Convert.ToInt32(textBoxOrderId.Text);
                CustomerId = Convert.ToInt32(textBoxCustomerId.Text);
                CustomerName = textBoxCustomerName.Text;
                if (string.IsNullOrEmpty(CustomerName)) return;
                order = new Order(OrderId, new Customer(CustomerId, CustomerName),CreateTime);
                foreach (var item in details)
                    order.AddDetails(new OrderDetail(new Goods(item.GoodsId, item.GoodsName, item.GoodsPrice), item.Quantity));

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
    }
    
}
