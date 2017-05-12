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
        }

        private void StoreForm_Load(object sender, EventArgs e)
        {
            
        }
        
        private void OpenButton_Click(object sender, EventArgs e)
        {
            if(SService == null)
            {
                SService = new ServiceHost(typeof(StoreService));
                SService.Open();
                this.label1.Text = "Service open";
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (SService != null)
            {
                SService.Close();
                SService = null;
                this.label1.Text = "Service closed";
            }
        }

    }
}
