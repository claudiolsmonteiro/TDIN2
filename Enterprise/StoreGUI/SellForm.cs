using System;
using System.Windows.Forms;
using StoreGUI.StoreService;
using StoreGUI.WarehouseService;

namespace StoreGUI
{
    public partial class SellForm : Form
    {
        private readonly StoreServiceClient storeProxy;

        private readonly WarehouseServiceClient warehouseProxy;

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
            var ret = storeProxy.MakeaSell(client_name.Text, client_email.Text, client_address.Text, book_title.Text,
                Convert.ToInt32(quantityValue.Value));

            switch (ret)
            {
                case 1:
                    MessageBox.Show("Inventory stock updated");
                    break;
                case 2:
                    MessageBox.Show("Insufficient stock for sale. An order was created");
                    warehouseProxy.AddOrder(book_title.Text, Convert.ToInt32(quantityValue.Value) + 10);
                    break;
                default:
                    break;
            }
            Close();
        }
    }
}