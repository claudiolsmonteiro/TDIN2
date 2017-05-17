﻿using System;
using System.ServiceModel;
using System.Windows.Forms;
using StorePrinter.StoreService;

namespace StorePrinter
{
    public partial class PrinterForm : Form, IStoreServiceCallback
    {
        private readonly StoreServiceClient proxy;

        public PrinterForm()
        {
            InitializeComponent();
            proxy = new StoreServiceClient(new InstanceContext(this));
            proxy.Open();
        }

        public void PrintReceipt(string client_name, string book_title, string quantity, string price)
        {
            label5.Text = client_name;
            label6.Text = book_title;
            label7.Text = quantity;
            label8.Text = price;
        }

        public void UpdateOrder(string title, string quantity)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = proxy.GetName();
            label6.Text = proxy.GetBookTitle();
            label7.Text = proxy.GetQuantity();
            label8.Text = proxy.GetPrice();
            proxy.Subscribe();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            proxy.Unsubscribe();
            proxy.Close();
        }
    }
}