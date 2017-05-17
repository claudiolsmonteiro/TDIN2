using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Windows.Forms;
using Warehouse.StoreService;

namespace Warehouse
{
    public partial class WarehouseForm : Form, IStoreServiceCallback
    {
        public static string connString = ConfigurationManager.ConnectionStrings["WarehouseDB"].ToString();
        private readonly ServiceHost host;
        private readonly StoreServiceClient storeproxy;
        private bool open;

        public WarehouseForm(ServiceHost h)
        {
            open = false;
            host = h;
            storeproxy = new StoreServiceClient(new InstanceContext(this));
            InitializeComponent();
            ListViewHeader();
        }

        public void PrintReceipt(string client_name, string book_title, string quantity, string price)
        {
        }

        public void UpdateOrder(string title, string quantity)
        {
        }

        private void WarehouseForm_Load(object sender, EventArgs e)
        {
        }

        private void ListViewHeader()
        {
            listView1.Clear();

            var id = new ColumnHeader();
            id.Text = "ID";
            id.Width = 50;
            id.TextAlign = HorizontalAlignment.Center;
            var book = new ColumnHeader();
            book.Text = "book";
            book.Width = 200;
            book.TextAlign = HorizontalAlignment.Center;
            var qt = new ColumnHeader();
            qt.Text = "qt";
            qt.Width = 50;
            qt.TextAlign = HorizontalAlignment.Center;
            var st = new ColumnHeader();
            st.Text = "state";
            st.Width = 150;
            st.TextAlign = HorizontalAlignment.Center;

            listView1.Columns.Add(id);
            listView1.Columns.Add(book);
            listView1.Columns.Add(qt);
            listView1.Columns.Add(st);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var selectedItems = listView1.SelectedItems;

            var orderID = new List<string>();

            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                foreach (ListViewItem i in selectedItems)
                {
                    var sqlcmd = "Update Orders set state='completed' where Id=" + i.Text;
                    var cmd = new SqlCommand(sqlcmd, conn);

                    cmd.ExecuteNonQuery();
                    var order = GetOrderInfo(Convert.ToInt32(i.Text));

                    storeproxy.ReceiveOrder(order);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private string[] GetOrderInfo(int id)
        {
            var conn = new SqlConnection(connString);
            var order = new string[2];
            try
            {
                conn.Open();
                var sqlcmd = "Select book_title, quantity from Orders where Id=" + id;
                var cmd = new SqlCommand(sqlcmd, conn);


                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var book_title = reader["book_title"].ToString();
                    var quantity = reader["quantity"].ToString();

                    order[0] = book_title;
                    order[1] = quantity;
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return order;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (open)
            {
                host.Close();
                open = false;
                OpenButton.Text = "Start";
            }
            else
            {
                host.Open();
                open = true;
                OpenButton.Text = "End";
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                var sqlcmd = "Select Id, book_title, quantity, state from Orders";
                var cmd = new SqlCommand(sqlcmd, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var id = reader["Id"].ToString();
                    var book_title = reader["book_title"].ToString();
                    var quantity = reader["quantity"].ToString();
                    var state = reader["state"].ToString();

                    string[] row = {id, book_title, quantity, state};
                    var listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                }
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
        }
    }
}