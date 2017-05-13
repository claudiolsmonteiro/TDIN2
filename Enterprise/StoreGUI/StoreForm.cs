using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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

        private void refreshList()
        {
            if (active)
                BooksView();
            else
                OrdersView();
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

            storeProxy.GetAllBooks();
            foreach(book b in storeProxy.GetAllBooks())
            {
                string[] row = { b.title, b.price+" €", b.quantity };
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

            string[] book =
            {
                storeProxy.GetBook(book_title).title, storeProxy.GetBook(book_title).price,
                storeProxy.GetBook(book_title).quantity
            };
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

            foreach (order o in storeProxy.GetAllOrders())
            {
                string[] row = { o.guid, o.client_name, o.book_title, o.quantity, o.state };
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
            
            foreach (order o in storeProxy.GetOrders(client_name))
            {
                string[] row = { o.guid, o.client_name, o.book_title, o.quantity, o.state };
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
            SellForm s = new SellForm(storeProxy);
            s.ShowDialog();
            refreshList();
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
