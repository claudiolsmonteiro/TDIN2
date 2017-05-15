using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;
using System.Data.SqlClient;
using Warehouse.StoreService;

namespace Warehouse
{
    public partial class WarehouseForm : Form, IStoreServiceCallback
    {
        ServiceHost host = null;
        StoreServiceClient storeproxy;
        bool open;
        public static string connString = ConfigurationManager.ConnectionStrings["WarehouseDB"].ToString();

        public WarehouseForm(ServiceHost h)
        {
            open = false;
            host = h;
            storeproxy = new StoreServiceClient(new InstanceContext(this));
            InitializeComponent();
            ListViewHeader();
        }

        public void PrintReceipt(string client_name, string book_title, string quantity, string price)
        {}

        public void UpdateOrder(string title, string quantity) { }

        private void WarehouseForm_Load(object sender, EventArgs e)
        {
            
        }

        private void ListViewHeader()
        {
            this.listView1.Clear();

            System.Windows.Forms.ColumnHeader id = new System.Windows.Forms.ColumnHeader();
            id.Text = "ID";
            id.Width = 50;
            id.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader book = new System.Windows.Forms.ColumnHeader();
            book.Text = "book";
            book.Width = 200;
            book.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader qt = new System.Windows.Forms.ColumnHeader();
            qt.Text = "qt";
            qt.Width = 50;
            qt.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader st = new System.Windows.Forms.ColumnHeader();
            st.Text = "state";
            st.Width = 150;
            st.TextAlign = HorizontalAlignment.Center;

            this.listView1.Columns.Add(id);
            this.listView1.Columns.Add(book);
            this.listView1.Columns.Add(qt);
            this.listView1.Columns.Add(st);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var selectedItems = this.listView1.SelectedItems;

            List<String> orderID = new List<string>();

            SqlConnection conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                foreach (ListViewItem i in selectedItems)
                {
                    string sqlcmd = "Update Orders set state='completed' where Id="+i.Text;
                    SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                    cmd.ExecuteNonQuery();
                    string[] order = GetOrderInfo(System.Convert.ToInt32(i.Text));
                    
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
            SqlConnection conn = new SqlConnection(connString);
            string[] order = new string[2];
            try
            {
                conn.Open();
                    string sqlcmd = "Select book_title, quantity from Orders where Id=" + id;
                    SqlCommand cmd = new SqlCommand(sqlcmd, conn);


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string book_title = (reader["book_title"].ToString());
                    string quantity = (reader["quantity"].ToString());

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
                this.OpenButton.Text = "Start";
            }
            else
            {
                host.Open();
                open = true;
                this.OpenButton.Text = "End";
            }
            
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlcmd = "Select Id, book_title, quantity, state from Orders";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string id = (reader["Id"].ToString());
                    string book_title = (reader["book_title"].ToString());
                    string quantity = (reader["quantity"].ToString());
                    string state = (reader["state"].ToString());

                    string[] row = { id, book_title, quantity, state };
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
