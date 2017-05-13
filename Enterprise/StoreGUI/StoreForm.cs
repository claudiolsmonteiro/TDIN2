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
using StoreGUI.StoreService;

namespace StoreGUI
{
    public partial class StoreForm : Form
    {
        StoreServiceClient storeProxy;
        bool active;
        public StoreForm()
        {
            storeProxy = new StoreServiceClient();
            InitializeComponent();
            BooksView();
            active = true;
        }

        private void StoreForm_Load(object sender, EventArgs e)
        {

        }

        private void BooksView()
        {
            this.listView1.Clear();
            System.Windows.Forms.ColumnHeader title = new System.Windows.Forms.ColumnHeader();
            title.Text = "title";
            title.Width = 400;
            title.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader price = new System.Windows.Forms.ColumnHeader();
            price.Text = "price";
            price.Width = 125;
            price.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader stock = new System.Windows.Forms.ColumnHeader();
            stock.Text = "stock";
            stock.Width = 50;
            stock.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(title);
            this.listView1.Columns.Add(price);
            this.listView1.Columns.Add(stock);

            String[][] books = storeProxy.GetAllBooks();
            for(int i = 0; i < books.Length; i++)
            {
                string[] row = { books[i][0], books[i][2]+" €", books[i][1] };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }
        
        private void BookView(string book_title)
        {
            this.listView1.Clear();
            System.Windows.Forms.ColumnHeader title = new System.Windows.Forms.ColumnHeader();
            title.Text = "title";
            title.Width = 400;
            title.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader price = new System.Windows.Forms.ColumnHeader();
            price.Text = "price";
            price.Width = 125;
            price.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader stock = new System.Windows.Forms.ColumnHeader();
            stock.Text = "stock";
            stock.Width = 50;
            stock.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(title);
            this.listView1.Columns.Add(price);
            this.listView1.Columns.Add(stock);

            string[] book = storeProxy.GetBook(book_title);
            var listViewItem = new ListViewItem(book);
            listView1.Items.Add(listViewItem);
        }

        private void OrdersView()
        {
            this.listView1.Clear();
            System.Windows.Forms.ColumnHeader id = new System.Windows.Forms.ColumnHeader();
            id.Text = "id";
            id.Width = 120;
            id.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader client = new System.Windows.Forms.ColumnHeader();
            client.Text = "client name";
            client.Width = 150;
            client.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader book = new System.Windows.Forms.ColumnHeader();
            book.Text = "book";
            book.Width = 150;
            book.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader qt = new System.Windows.Forms.ColumnHeader();
            qt.Text = "qt";
            qt.Width = 40;
            qt.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader status = new System.Windows.Forms.ColumnHeader();
            status.Text = "status";
            status.Width = 125;
            status.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(id);
            this.listView1.Columns.Add(client);
            this.listView1.Columns.Add(book);
            this.listView1.Columns.Add(qt);
            this.listView1.Columns.Add(status);

            String[][] orders = storeProxy.GetAllOrders();
            for (int i = 0; i < orders.Length; i++)
            {
                string[] row = { orders[i][0], orders[i][1], orders[i][2], orders[i][3], orders[i][4] };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }

        private void OrderView(string client_name)
        {
            this.listView1.Clear();
            System.Windows.Forms.ColumnHeader id = new System.Windows.Forms.ColumnHeader();
            id.Text = "id";
            id.Width = 120;
            id.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader client = new System.Windows.Forms.ColumnHeader();
            client.Text = "client name";
            client.Width = 150;
            client.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader book = new System.Windows.Forms.ColumnHeader();
            book.Text = "book";
            book.Width = 150;
            book.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader qt = new System.Windows.Forms.ColumnHeader();
            qt.Text = "qt";
            qt.Width = 40;
            qt.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader status = new System.Windows.Forms.ColumnHeader();
            status.Text = "status";
            status.Width = 125;
            status.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(id);
            this.listView1.Columns.Add(client);
            this.listView1.Columns.Add(book);
            this.listView1.Columns.Add(qt);
            this.listView1.Columns.Add(status);
            
            String[][] orders = storeProxy.GetOrders(client_name);
            for (int i = 0; i < orders.Length; i++)
            {
                string[] row = { orders[i][0], orders[i][1], orders[i][2], orders[i][3], orders[i][4] };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
            
        }

        private void BooksButton_Click(object sender, EventArgs e)
        {
            BooksView();
            active = true;
        }

        private void OrdersButton_Click(object sender, EventArgs e)
        {
            OrdersView();
            active = false;
        }

        private void SellButton_Click(object sender, EventArgs e)
        {

        }
        
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (active)
                BookView(SearchBox.Text);
            else
                OrderView(SearchBox.Text);
        }
    }
}
