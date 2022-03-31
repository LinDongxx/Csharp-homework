using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderManagement;
namespace MergeOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Multiselect = true;
            ofd.Title = "选择要合并的两个xml文件";
            ofd.Filter = "xml文件(*.xml)|*.xml";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if(ofd.FileNames.Count() != 2)
                {
                    MessageBox.Show("请选取两个文件");
                    return;
                }
                OrderService orderService = new OrderService();
                OrderService tmp = new OrderService();
                foreach(string file in ofd.FileNames)
                {
                    tmp.Import(file);
                    tmp.QueryAll().ForEach(o => orderService.AddOrder(o));
                }
                string newFileDir = Application.StartupPath + "\\Data";
                if(!Directory.Exists(newFileDir))
                    Directory.CreateDirectory(newFileDir);
                orderService.Export(newFileDir+"\\order.xml");
                MessageBox.Show("合并完成");
            }
        }
    }
}
