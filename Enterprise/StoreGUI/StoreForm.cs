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
        public StoreForm()
        {
            storeProxy = new StoreServiceClient();
            InitializeComponent();
            BookView();
            
        }

        private void StoreForm_Load(object sender, EventArgs e)
        {

        }

        private void BookView()
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

            
            
        }

        private void OrderView()
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
        }

        private void BooksButton_Click(object sender, EventArgs e)
        {
            BookView();
        }

        private void OrdersButton_Click(object sender, EventArgs e)
        {
            OrderView();
        }

        private void SellButton_Click(object sender, EventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            //int txt = storeProxy.GetStock("Lusiadas");

            //int stock = storeProxy.GetStock(txt);

            this.BooksButton.Text = storeProxy.GetAllBooks().First()[1];
        }
    }
}
