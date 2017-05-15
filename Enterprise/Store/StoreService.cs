using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Store
{
    public delegate void ChangeDataDel();

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class StoreService : IStoreService
    {
        public static string cName = "", bTitle ="", q= "0", p = "0.0", bookTitle = "", qty = "0";

        public static string connString = ConfigurationManager.ConnectionStrings["StoreDB"].ToString();
        public static List<IPrintReceipt> subscribers = new List<IPrintReceipt>();
        public static ChangeDataDel changeData = null;

        public void Subscribe()
        {
            IPrintReceipt callback = OperationContext.Current.GetCallbackChannel<IPrintReceipt>();
            if (!subscribers.Contains(callback))
            {
                subscribers.Add(callback);
                if (changeData != null)
                    changeData();
            }
        }

        public string GetName()
        {
            return cName;
        }

        public string GetBookTitle()
        {
            return bTitle;
        }

        public string GetQuantity()
        {
            return q;
        }

        public string GetPrice()
        {
            return p;
        }

        public void Unsubscribe()
        {
            IPrintReceipt callback = OperationContext.Current.GetCallbackChannel<IPrintReceipt>();
            subscribers.Remove(callback);
            if (changeData != null)
                changeData();
        }

        public static void Notify()
        {
            subscribers.ForEach(delegate (IPrintReceipt callback) {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                    callback.PrintReceipt(cName,bTitle,q,p);
                else
                    subscribers.Remove(callback);
            });
        }

        public static void NotifyOrder()
        {
            subscribers.ForEach(delegate (IPrintReceipt callback) {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                    callback.UpdateOrder(bookTitle, qty);
                else
                    subscribers.Remove(callback);
            });
        }

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

        public void CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlcmd = "Insert into Orders (book_title, quantity, client_name, client_address, client_email, state) VALUES (@bookTit, @qt, @clName, @clAddr, @clEmail, @st)";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.Parameters.Add("@bookTit", SqlDbType.VarChar,50).Value= book_title;
                cmd.Parameters.Add("@qt", SqlDbType.Int).Value = quantity;
                cmd.Parameters.Add("@clName", SqlDbType.VarChar, 50).Value = client_name;
                cmd.Parameters.Add("@clAddr", SqlDbType.VarChar, 50).Value = client_name;
                cmd.Parameters.Add("@clEmail", SqlDbType.VarChar, 50).Value = client_addr;
                cmd.Parameters.Add("@st", SqlDbType.VarChar, 50).Value = "waiting expedition";

                cmd.ExecuteNonQuery();
                
                //TODO
                //Send remote call to warehouse
                SendEmail(client_name, client_email, client_addr, book_title, quantity);
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }
        }

        public void SendEmail(string client_name , string client_email , string client_addr , string book_title , int quantity ) 
        {
            //string orderprice = GetPrice(book_title, quantity);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("tdinproj2@gmail.com", "tdinproj2");
            string id = getGuid(client_name, book_title);
            MailMessage mm = new MailMessage("tdinproj2@gmail.com", client_email, "Order with ID:" + id +" placed", "Hi "+client_name+".\n The order you placed for "+ quantity+ " "+ book_title+" is being worked on.");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        string getGuid(string client_name, string book_title)
        {
            SqlConnection conn = new SqlConnection(connString);

            string id = "";
            try
            {
                conn.Open();
                string sqlcmd = "Select guid from Orders where title = '" + book_title + "' and client_name= '"+ client_name+"'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                id = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        public int ConfirmSell(string client_name, string book_title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            int rows;
            try
            {
                conn.Open();
                string sqlcmd = "Update Books set quantity=quantity-" + quantity + " where title='" + book_title + "'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                rows = cmd.ExecuteNonQuery();

                if (rows == 1)
                {
                    cName = client_name;
                    bTitle = book_title;
                    q = quantity.ToString();
                    p = GetPrice(book_title, quantity);
                    Notify();
                }

            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return 1;
        }

        public string GetPrice(string title, int quantity)
        {
            double singleprice;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlcmd = "Select price from Books where title = '" + title + "'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                singleprice = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch
            {
                return "error";
            }
            finally
            {
                conn.Close();
            }
            return (singleprice*quantity).ToString();
        }

        public int MakeaSell(string client_name, string client_email, string client_addr, string book_title, int quantity)
        {
            int stock = GetStock(book_title);
            if (stock < quantity)
            {
                CreateStoreOrder(client_name, client_email, client_addr, book_title, (quantity+10));
                return 2;
            }
            else
               return ConfirmSell(client_name, book_title, quantity);
        }
        
        public void ReceiveOrder(string[] order)
        {
            bookTitle = order[0];
            qty = order[1];
            NotifyOrder();
        }
    }
}
