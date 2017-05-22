using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using StoreGUI.StoreService;

namespace StoreGUI
{
    public partial class StoreForm : Form, IStoreServiceCallback
    {
        private readonly List<KeyValuePair<string, string>> incoming;
        private readonly StoreServiceClient storeProxy;
        private string active;

        public StoreForm()
        {
            InitializeComponent();
            storeProxy = new StoreServiceClient(new InstanceContext(this));

            BooksView();
            active = "books";
            incoming = new List<KeyValuePair<string, string>>();
            //storeProxy.Open();
        }

        public void PrintReceipt(string client_name, string book_title, string quantity, string price)
        {
        }

        public void UpdateOrder(string title, string quantity)
        {
            incoming.Insert(0, new KeyValuePair<string, string>(title, quantity));
            MessageBox.Show(quantity + " copies of " + title + " are going to be shipped. Dispatch should occur at " +
                            DateTime.Today.AddDays(2).ToString("d"));
        }

        private void StoreForm_Load(object sender, EventArgs e)
        {
            storeProxy.Subscribe();
        }

        private void refreshList()
        {
            if (active.Equals("books"))
                BooksView();
            else if (active.Equals("pending"))
                PendingView();
            else
                OrdersView();
        }

        private void BooksView()
        {
            listView1.Clear();
            var title = new ColumnHeader();
            title.Text = "Title";
            title.Width = 375;
            title.TextAlign = HorizontalAlignment.Center;
            var price = new ColumnHeader();
            price.Text = "Price";
            price.Width = 125;
            price.TextAlign = HorizontalAlignment.Center;
            var stock = new ColumnHeader();
            stock.Text = "Stock";
            stock.Width = 50;
            stock.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(title);
            listView1.Columns.Add(price);
            listView1.Columns.Add(stock);

            foreach (var b in storeProxy.GetAllBooks())
            {
                string[] row = {b.title, b.price + " €", b.quantity};
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }

        private void BookView(string book_title)
        {
            listView1.Clear();
            var title = new ColumnHeader();
            title.Text = "Title";
            title.Width = 375;
            title.TextAlign = HorizontalAlignment.Center;
            var price = new ColumnHeader();
            price.Text = "Price";
            price.Width = 125;
            price.TextAlign = HorizontalAlignment.Center;
            var stock = new ColumnHeader();
            stock.Text = "Stock";
            stock.Width = 50;
            stock.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(title);
            listView1.Columns.Add(price);
            listView1.Columns.Add(stock);

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
            listView1.Clear();
            var id = new ColumnHeader();
            id.Text = "Id";
            id.Width = 120;
            id.TextAlign = HorizontalAlignment.Center;
            var client = new ColumnHeader();
            client.Text = "Client name";
            client.Width = 125;
            client.TextAlign = HorizontalAlignment.Center;
            var book = new ColumnHeader();
            book.Text = "Book";
            book.Width = 125;
            book.TextAlign = HorizontalAlignment.Center;
            var qt = new ColumnHeader();
            qt.Text = "Quantity";
            qt.Width = 40;
            qt.TextAlign = HorizontalAlignment.Center;
            var status = new ColumnHeader();
            status.Text = "Status";
            status.Width = 125;
            status.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(id);
            listView1.Columns.Add(client);
            listView1.Columns.Add(book);
            listView1.Columns.Add(qt);
            listView1.Columns.Add(status);

            foreach (var o in storeProxy.GetAllOrders())
            {
                string[] row = {o.guid, o.client_name, o.book_title, o.quantity, o.state};
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }

        private void OrderView(string client_name)
        {
            listView1.Clear();
            var id = new ColumnHeader();
            id.Text = "Id";
            id.Width = 120;
            id.TextAlign = HorizontalAlignment.Center;
            var client = new ColumnHeader();
            client.Text = "Client name";
            client.Width = 150;
            client.TextAlign = HorizontalAlignment.Center;
            var book = new ColumnHeader();
            book.Text = "Book";
            book.Width = 150;
            book.TextAlign = HorizontalAlignment.Center;
            var qt = new ColumnHeader();
            qt.Text = "Quantity";
            qt.Width = 40;
            qt.TextAlign = HorizontalAlignment.Center;
            var status = new ColumnHeader();
            status.Text = "status";
            status.Width = 125;
            status.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(id);
            listView1.Columns.Add(client);
            listView1.Columns.Add(book);
            listView1.Columns.Add(qt);
            listView1.Columns.Add(status);

            foreach (var o in storeProxy.GetOrders(client_name))
            {
                string[] row = {o.guid, o.client_name, o.book_title, o.quantity, o.state};
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
            listView1.Clear();
            var title = new ColumnHeader();
            title.Text = "Title";
            title.Width = 400;
            title.TextAlign = HorizontalAlignment.Center;
            var quantity = new ColumnHeader();
            quantity.Text = "Quantity";
            quantity.Width = 125;
            quantity.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(title);
            listView1.Columns.Add(quantity);
            listView1.FullRowSelect = true;
            foreach (var i in incoming)
            {
                string[] row = {i.Key, i.Value};
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
                var result = MessageBox.Show("Accept Order", "Book", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var title = "";
                    var x = 0;
                    var quantity = 0;
                    foreach (ListViewItem Item in listView1.SelectedItems)
                    {
                        x = Item.Index;
                        title = listView1.Items[x].SubItems[0].Text;
                        quantity = Convert.ToInt32(listView1.Items[x].SubItems[1].Text);
                    }
                    storeProxy.UpdateStock(title, quantity);
                    incoming.Remove(new KeyValuePair<string, string>(listView1.Items[x].SubItems[0].Text,
                        listView1.Items[x].SubItems[1].Text));
                }
                else if (result == DialogResult.No)
                {
                }
            }
            refreshList();
        }
    }
}