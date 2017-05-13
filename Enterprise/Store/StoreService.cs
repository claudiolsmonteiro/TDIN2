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

        public Books GetAllBooks()
        {
            SqlConnection conn = new SqlConnection(connString);
            Books retList = new Books();
            try
            {
                conn.Open();
                string sqlcmd = "Select title, quantity,price from Books";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {   
                    Book book = new Book()
                    {
                        title = reader["title"].ToString(),
                        quantity = reader["quantity"].ToString(),
                        price = reader["price"].ToString()
                    };
                                        
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

        public Book GetBook(string title)
        {
            SqlConnection conn = new SqlConnection(connString);

            Book retList = new Book();
            try
            {
                conn.Open();
                string sqlcmd = "Select title, price, quantity from Books where title = '" + title + "'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retList.title = reader["title"].ToString();
                    retList.price = reader["price"].ToString() + " €";
                    retList.quantity = reader["quantity"].ToString();
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

        public Orders GetAllOrders()
        {
            SqlConnection conn = new SqlConnection(connString);
            Orders retList = new Orders();
            try
            {
                conn.Open();
                string sqlcmd = "Select guid, client_name, book_title, quantity, state from Orders";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order()
                    {

                        guid = reader["guid"].ToString(),
                        client_name = (reader["client_name"].ToString()),
                        book_title = (reader["book_title"].ToString()),
                        quantity = (reader["quantity"].ToString()),
                        state = (reader["state"].ToString()),
                    };
                retList.Add(order);
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

        public Orders GetOrders(string client)
        {
            SqlConnection conn = new SqlConnection(connString);
            Orders retList = new Orders();
            try
            {
                conn.Open();
                string sqlcmd = "Select guid, client_name, book_title, quantity, state from Orders where client_name ='" + client + "'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order()
                    {

                        guid = reader["guid"].ToString(),
                        client_name = (reader["client_name"].ToString()),
                        book_title = (reader["book_title"].ToString()),
                        quantity = (reader["quantity"].ToString()),
                        state = (reader["state"].ToString()),
                    };

                    retList.Add(order);
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
                    " VALUES (@bookTit, @qt, @clName, @clAddr, @clEmail, @st, NEWID())";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.Parameters.AddWithValue("@bookTit", book_title);
                cmd.Parameters.AddWithValue("@qt", quantity);
                cmd.Parameters.AddWithValue("@clName", client_name);
                cmd.Parameters.AddWithValue("@clAddr", client_addr);
                cmd.Parameters.AddWithValue("@clEmail", client_email);
                cmd.Parameters.AddWithValue("@st", "waiting expedition");
/*
                string guidstring = book_title + client_name + quantity;
                i = new Guid(guidstring);
                cmd.Parameters.AddWithValue("@id", i);
                */
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

        public int ConfirmSell(string client_name, string book_title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            int rows;
            try
            {
                conn.Open();
                string sqlcmd = "Update Books set quantity=quantity-"+quantity+" where title='"+book_title+"'";
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
            return 1;
        }

        public int MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity)
        {
            int stock = GetStock(book_title);
            if (stock < quantity)
            {
                CreateStoreOrder(client_name, client_email, client_addr, book_title, quantity);
                return 2;
            }
            else
               return ConfirmSell(client_name, book_title, quantity);
        }
        
    }
}
