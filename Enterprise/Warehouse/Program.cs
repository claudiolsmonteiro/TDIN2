using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace Warehouse
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var host = new ServiceHost(typeof(WarehouseService));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WarehouseForm(host));
        }
    }
}