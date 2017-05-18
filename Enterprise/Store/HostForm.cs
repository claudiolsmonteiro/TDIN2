using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace Store
{
    public partial class HostForm : Form
    {
        private ServiceHost SService;
        private ServiceHost WebService;

        public HostForm()
        {
            InitializeComponent();
            label1.Text = "service closed";
            StoreService.changeData = OnChange;
        }

        public void OnChange()
        {
            //label1.Text = StoreService.subscribers.Count.ToString();
        }

        private void HostForm_Load(object sender, EventArgs e)
        {
            SService = new ServiceHost(typeof(StoreService));
            WebService = new ServiceHost(typeof(StoreWebService));
            label1.Text = "Store Service Open";
            WebService.Open();
            SService.Open();
            
            
        }
    }
}