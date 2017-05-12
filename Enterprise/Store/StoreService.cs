using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Store
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class StoreService : IStoreService
    {
        public static string connString = ConfigurationManager.ConnectionStrings["StoreDB"].ToString();

        public List<List<String>> GetAllBooks()
        {
            SqlConnection conn = new SqlConnection(connString);
            List<List<String>> retList = new List<List<string>>();
            try
            {
                conn.Open();
                string sqlcmd = "Select title, quantity,price from Books";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {    //Every new row will create a new dictionary that holds the columns
                    List<String> book = new List<string>();

                    book.Add(reader["title"].ToString());
                    book.Add(reader["quantity"].ToString());
                    book.Add(reader["price"].ToString());
                    
                    retList.Add(book);
                }
                reader.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
            return retList;
        }





        public int GetStock(string book_title)
        {
            SqlConnection conn = new SqlConnection(connString);

            int stock=0;
            try
            {
                conn.Open();
                string sqlcmd = "Select quantity from Books where title = '" + book_title+"'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                stock = (int)cmd.ExecuteScalar();
               
            }
            catch
            {
                return stock;
            }
            finally
            {
                conn.Close();
            }
            return stock;
        }

        public Guid CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            Guid i = new Guid("...");
            try
            {
                conn.Open();
                string sqlcmd = "Insert into Orders (book_title, quantity, client_name, client_address, client_email, state, guid)" +
                    " VALUES (@bookTit, @qt, @clName, @clAddr, @clEmail, @st, @id)";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.Parameters.AddWithValue("@bookTit", book_title);
                cmd.Parameters.AddWithValue("@qt", quantity);
                cmd.Parameters.AddWithValue("@clName", client_name);
                cmd.Parameters.AddWithValue("@clAddr", client_addr);
                cmd.Parameters.AddWithValue("@clEmail", client_email);
                cmd.Parameters.AddWithValue("@st", "waiting expedition");

                string guidstring = book_title + client_name + quantity;
                i = new Guid(guidstring);
                cmd.Parameters.AddWithValue("@id", i);

                cmd.ExecuteNonQuery();

                //TODO
                //Send remote call to warehouse
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }
            return i;
        }

        public void ConfirmSell(string client_name, string book_title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            int rows;
            try
            {
                conn.Open();
                string sqlcmd = "Update Books set quantity=quantity-"+quantity+" where title="+book_title;
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                rows = cmd.ExecuteNonQuery();

                if(rows == 1)
                {
                    //print receipt
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public void MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity)
        {
            int stock = GetStock(book_title);
            if (stock < quantity)
                CreateStoreOrder(client_name, client_email, client_addr, book_title, quantity);
            else
                ConfirmSell(client_name, book_title, quantity);
        }
        
    }
}
