using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoreGUI.StoreService;
using StoreGUI.WarehouseService;

namespace StoreGUI
{
    public partial class SellForm : Form
    {
        StoreService.
        StoreServiceClient storeProxy;
        WarehouseServiceClient warehouseProxy;

        public SellForm(StoreServiceClient sp)
        {
            storeProxy = sp;
            warehouseProxy = new WarehouseServiceClient();
            InitializeComponent();
        }

        private void SellForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ret = storeProxy.MakeaSell(this.client_name.Text, this.client_email.Text, this.client_address.Text, this.book_title.Text, System.Convert.ToInt32(this.quantityValue.Value));

            switch (ret)
            {
                case 1:
                    MessageBox.Show("Inventory stock updated");
                    break;
                case 2:
                    MessageBox.Show("Insufficient stock for sale. An order was created");
                    warehouseProxy.AddOrder(this.book_title.Text, System.Convert.ToInt32(this.quantityValue.Value));
                    break;
                default:
                    break;
            }
            this.Close();
        }
    }
}
