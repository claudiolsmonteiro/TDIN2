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

        public void SatisfyOrders(string book, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlcmd = "Select top (1) order_id, guid, client_name, client_email, book_title, quantity from Orders where book_title ='" + book + "' and quantity="+ (quantity-10) + " and state ='waiting expedition' order by order_id asc";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                string id = "", uid = "", clName="", clEmail="", bTitle="", qt="";
                while (reader.Read())
                {
                    id = reader["order_id"].ToString();
                    uid = reader["guid"].ToString();
                    clName = (reader["client_name"].ToString());
                    clEmail = (reader["client_email"].ToString());
                    bTitle = (reader["book_title"].ToString());
                    qt = (reader["quantity"].ToString());
                }
                reader.Close();

                SatisfyOrder(System.Convert.ToInt32(id), clName, clEmail, bTitle, System.Convert.ToInt32(qt), uid);

                //SatisfyPossibleOrders(book_title);
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
        }

        public void SatisfyOrder(int orderID, string client, string email, string book, int quant, string guid)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlcmd = "Update Orders set state= 'dispatched at " + DateTime.Today.ToString("d") + "' where order_id=" + orderID;
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.ExecuteNonQuery();

                string subject = "Order with ID:" + guid + " placed";
                string msg = "Hello " + client + ", your order " + guid + " for " + quant + " " + book + " has been dispatched.";
                SendEmail(email, subject, msg);

                string sqlcmd2 = "Update Books set quantity=quantity-" + quant + " where title='" + book + "'";
                SqlCommand cmd2 = new SqlCommand(sqlcmd, conn);
                cmd2.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
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
                cmd.Parameters.Add("@clAddr", SqlDbType.VarChar, 50).Value = client_addr;
                cmd.Parameters.Add("@clEmail", SqlDbType.VarChar, 50).Value = client_email;
                cmd.Parameters.Add("@st", SqlDbType.VarChar, 50).Value = "waiting expedition";

                cmd.ExecuteNonQuery();

                //TODO
                //Send remote call to warehouse
                string subject = "New order";
                string msg = "Hello " + client_name + ", your order for " + quantity + " " + book_title + " has been registered. We will dispatch it as soon as possible.";
                SendEmail(client_email, subject, msg);
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateStock(string book_title, int quantity)
        {
            SqlConnection conn = new SqlConnection(connString);
            int rows;
            try
            {
                conn.Open();
                string sqlcmd = "Update Books set quantity=quantity+" + quantity + " where title='" + book_title + "'";
                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                rows = cmd.ExecuteNonQuery();
                SatisfyOrders(book_title, quantity);
            }
            catch
            {
                return;
            }
            finally
            {
                conn.Close();
            }
        }

        public void SendEmail(string client_email , string subject , string message ) 
        {
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("tdinproj2@gmail.com", "tdinproj2");
            MailMessage mm = new MailMessage("tdinproj2@gmail.com", client_email, subject, message);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
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
