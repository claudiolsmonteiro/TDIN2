using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Warehouse
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WarehouseService" in both code and config file together.
    public class WarehouseService : IWarehouseService
    {
        public static string connString = ConfigurationManager.ConnectionStrings["WarehouseDB"].ToString();

        
        [OperationBehavior(TransactionScopeRequired = true)]
        public void AddOrder(string title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                string sqlcmd = "Insert into Orders (book_title, quantity, state) VALUES (@bookTit, @qt, @st)";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.Parameters.Add("@bookTit", SqlDbType.VarChar, 50).Value = title;
                cmd.Parameters.Add("@qt", SqlDbType.Int).Value = quantity;
                cmd.Parameters.Add("@st", SqlDbType.VarChar, 50).Value = "Pending";

                cmd.ExecuteNonQuery();
                
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }

        
    }
}
