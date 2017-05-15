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

namespace Store
{
    public partial class HostForm : Form
    {
        private ServiceHost SService = null;
        public HostForm()
        {
            InitializeComponent();
            this.label1.Text = "service closed";
            StoreService.changeData = new ChangeDataDel(OnChange);
        }
        public void OnChange()
        {
            //label1.Text = StoreService.subscribers.Count.ToString();
        }
        private void HostForm_Load(object sender, EventArgs e)
        {
            SService = new ServiceHost(typeof(StoreService));
            this.label1.Text = "Store Service Open";
            SService.Open();
        }
        
    }
}
