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
using StoreGUI.WarehouseService;
using System.Threading;

namespace StoreGUI
{
    public partial class StoreForm : Form, IStoreServiceCallback
    {
        StoreServiceClient storeProxy;
        private List<KeyValuePair<string, string>> incoming;
        string active;
        public StoreForm()
        {
            InitializeComponent();
            storeProxy = new StoreServiceClient(new InstanceContext(this));
            
            BooksView();
            active = "books";
            incoming = new List<KeyValuePair<string, string>>();
            //storeProxy.Open();
        }

        private void StoreForm_Load(object sender, EventArgs e)
        {
            storeProxy.Subscribe();
        }

        public void PrintReceipt(string client_name, string book_title, string quantity, string price) { }

        public void UpdateOrder(string title, string quantity)
        {
            incoming.Insert(0,new KeyValuePair<string, string>(title, quantity));
            MessageBox.Show(quantity + " copies of " + title + " are going to be shipped. Dispatch should occur at "+ System.DateTime.Today.AddDays(2).ToString("d"));
        }

        private void refreshList()
        {
            if (active.Equals("books"))
                BooksView();
            else if(active.Equals("pending"))
                PendingView();
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
            active = "books";
        }

        private void OrdersButton_Click(object sender, EventArgs e)
        {
            OrdersView();
            active = "orders";
        }

        private void SellButton_Click(object sender, EventArgs e)
        {
            new Thread(() => new SellForm(storeProxy).ShowDialog()).Start();
            refreshList();
        }
        
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (active.Equals("books"))
                BookView(SearchBox.Text);
            else
                OrderView(SearchBox.Text);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            active = "pending";
            PendingView();

            //storeProxy.UpdateStock();
        }

        private void PendingView()
        {
            this.listView1.Clear();
            System.Windows.Forms.ColumnHeader title = new System.Windows.Forms.ColumnHeader();
            title.Text = "Title";
            title.Width = 400;
            title.TextAlign = HorizontalAlignment.Center;
            System.Windows.Forms.ColumnHeader quantity = new System.Windows.Forms.ColumnHeader();
            quantity.Text = "Quantity";
            quantity.Width = 125;
            quantity.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(title);
            this.listView1.Columns.Add(quantity);
            this.listView1.FullRowSelect = true;
            foreach (KeyValuePair<string, string> i in incoming)
            {
                string[] row = { i.Key, i.Value };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (active.Equals("books"))
            {
                
            }
            else if (active.Equals("pending"))
            {
                DialogResult result = MessageBox.Show("Accept Order", "Book", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string title="";
                    int x = 0;
                    int quantity= 0;
                    foreach (ListViewItem Item in listView1.SelectedItems)
                    {
                        x = Item.Index;
                        title = listView1.Items[x].SubItems[0].Text;
                        quantity = System.Convert.ToInt32(listView1.Items[x].SubItems[1].Text);
                    }
                    storeProxy.UpdateStock(title,quantity);
                    incoming.Remove(new KeyValuePair<string, string>(listView1.Items[x].SubItems[0].Text, listView1.Items[x].SubItems[1].Text));
                }
                else if (result == DialogResult.No)
                {
                    
                }
            }
            refreshList();
        }
    }
}
