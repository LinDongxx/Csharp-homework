using System;
using System.Collections;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            
            InitializeComponent();
            InitializeDataSource();
        }
        public void InitializeDataSource()
        {
            ds = new OrderDataSet();
            bindingSourceOrders.DataSource = ds.Set;
            bindingSourceOrders.DataMember = "Orders";

            bindingSourceDetails.DataSource = bindingSourceOrders;
            bindingSourceDetails.DataMember = "Order_Details";

            dataGridViewOrders.DataSource = bindingSourceOrders;
            dataGridViewDetails.DataSource = bindingSourceDetails;

            dataGridViewDetails.Columns[0].Visible = false;

        }
        private OrderDataSet ds;
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            buttonClear_Click(this, new EventArgs());
            UpdateDialog form = new UpdateDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.order != null)    
                    ds.AddOrder(form.order);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            var rows = dataGridViewOrders.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("未选中所要删除的订单或者选中的多于一个");
                return;
            }
            int index = dataGridViewOrders.CurrentCell.RowIndex;
            ds.DeleteOrder(index);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var rows = dataGridViewOrders.SelectedRows;
            if(rows.Count != 1)
            {
                MessageBox.Show("未选中所要修改的订单或者选中的多于一个");
                return;
            }
            int index = dataGridViewOrders.CurrentCell.RowIndex;
            DataRow row = ds.Set.Tables["Orders"].Rows[index];
            Order order = new Order((int)row[0],new Customer((int)row[1],(string)row[2]),(DateTime)row[3]);
            foreach (DataRow r in ds.Set.Tables["Details"].Rows)
                if ((int)r[0] == order.Id) 
                    order.AddDetails(new OrderDetail(new Goods((int)r[1], (string)r[2], (float)r[3]),(int)r[4]));
            UpdateDialog form = new UpdateDialog(order);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.order != null)
                    ds.EditOrder(form.order);
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                if (!string.IsNullOrEmpty(textBoxOrderId.Text))
                {
                    filter += $"订单编号 = {textBoxOrderId.Text}";
                }
                if (!string.IsNullOrEmpty(textBoxGoodsName.Text))
                {
                    if (filter != "") filter += " AND ";
                    string specialFilter = "";
                    var rows = ds.Set.Tables["Details"].Select($"商品名称 = {textBoxGoodsName.Text}");
                    if (rows != null && rows.Length != 0) 
                    {
                        rows.ToList().ForEach(row => { if (specialFilter != "") specialFilter += " OR "; specialFilter += $"订单编号 = {row[0]}"; });
                        filter += "(" + specialFilter + ")";
                    }
                }
                if (!string.IsNullOrEmpty(textBoxCustomerName.Text))
                {
                    if (filter != "") filter += " AND ";
                    filter += $"客户名 = {textBoxCustomerName.Text}";
                }
                if (!string.IsNullOrEmpty(textBoxMinAmount.Text))
                {
                    if (filter != "") filter += " AND ";
                    filter += $"总金额 >= {textBoxMinAmount.Text}";
                }
                if (!string.IsNullOrEmpty(textBoxMaxAmount.Text))
                {
                    if (filter != "") filter += " AND ";
                    filter += $"总金额 <= {textBoxMaxAmount.Text}";
                }
                bindingSourceOrders.Filter = filter;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxOrderId.Text = "";
            textBoxGoodsName.Text = "";
            textBoxCustomerName.Text = "";
            textBoxMinAmount.Text = "";
            textBoxMaxAmount.Text = "";
            bindingSourceOrders.Filter = null;
        }

        private void bindingSourceDetails_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingSourceOrders_CurrentChanged(object sender, EventArgs e)
        {

        }
    }

    public class OrderDataSet
    {
        public DataSet Set;
        private DataTable OrderTable;
        private DataTable DetailTable;
        public OrderDataSet()
        {
            Set = new DataSet("OrderManagement");
            OrderTable = Set.Tables.Add("Orders");
            DetailTable = Set.Tables.Add("Details");
            OrderTable.Columns.Add("订单编号",typeof(int));
            OrderTable.Columns.Add("客户编号", typeof(int));
            OrderTable.Columns.Add("客户名",typeof(string));
            OrderTable.Columns.Add("创建时间", typeof(DateTime));
            OrderTable.Columns.Add("总金额", typeof(float));
            OrderTable.PrimaryKey = new DataColumn[]{OrderTable.Columns[0]};

            DetailTable.Columns.Add("订单编号", typeof(int));
            DetailTable.Columns.Add("商品编号", typeof(int));
            DetailTable.Columns.Add("商品名称", typeof(string));
            DetailTable.Columns.Add("商品价格", typeof(float));
            DetailTable.Columns.Add("交易数量", typeof(int));
            DetailTable.PrimaryKey = new DataColumn[] { DetailTable.Columns[0],DetailTable.Columns[1] };

            DataColumn parentCol = OrderTable.Columns[0];
            DataColumn childCol = DetailTable.Columns[0];
            DataRelation relation = new DataRelation("Order_Details",parentCol,childCol);  
            Set.Relations.Add(relation);

        }

        public void AddOrder(Order order)
        {
            OrderTable.Rows.Add(order.Id,order.Customer.Id,order.Customer.Name,order.CreateTime,order.TotalAmount);
            order.Details.ForEach(d => DetailTable.Rows.Add(order.Id, d.Goods.Id, d.Goods.Name, d.Goods.Price, d.Quantity));

        }

        public void EditOrder(Order order)
        {
            var row = OrderTable.Rows.Find(order.Id);
            row[1] = order.Customer.Id; row[2] = order.Customer.Name; row[3] = order.CreateTime; row[4] = order.TotalAmount;
            for (int i = DetailTable.Rows.Count - 1; i >= 0; i--)
            {
                if ((int)DetailTable.Rows[i][0]==order.Id)
                {
                    DetailTable.Rows[i].Delete();
                }
            }
            DetailTable.AcceptChanges();
            order.Details.ForEach(d => DetailTable.Rows.Add(order.Id, d.Goods.Id, d.Goods.Name, d.Goods.Price, d.Quantity));

        }

        public void DeleteOrder(int index)
        {
            int id = (int)OrderTable.Rows[index][0];
            OrderTable.Rows.RemoveAt(index);
            for (int i = DetailTable.Rows.Count - 1; i >= 0; i--)
            {
                if ((int)DetailTable.Rows[i][0] == id)
                {
                    DetailTable.Rows[i].Delete();
                }
            }
            DetailTable.AcceptChanges();
        }
    }
}
