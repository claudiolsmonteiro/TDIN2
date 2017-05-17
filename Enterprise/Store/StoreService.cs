using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Store
{
    public delegate void ChangeDataDel();

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class StoreService : IStoreService
    {
        public static string cName = "", bTitle = "", q = "0", p = "0.0", bookTitle = "", qty = "0";

        public static string connString = ConfigurationManager.ConnectionStrings["StoreDB"].ToString();
        public static List<IPrintReceipt> subscribers = new List<IPrintReceipt>();
        public static ChangeDataDel changeData = null;

        public void Subscribe()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IPrintReceipt>();
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
            var callback = OperationContext.Current.GetCallbackChannel<IPrintReceipt>();
            subscribers.Remove(callback);
            if (changeData != null)
                changeData();
        }

        public Books GetAllBooks()
        {
            var conn = new SqlConnection(connString);
            var retList = new Books();
            try
            {
                conn.Open();
                var sqlcmd = "Select title, quantity,price from Books";
                var cmd = new SqlCommand(sqlcmd, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var book = new Book
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
            var conn = new SqlConnection(connString);

            var retList = new Book();
            try
            {
                conn.Open();
                var sqlcmd = "Select title, price, quantity from Books where title = '" + title + "'";
                var cmd = new SqlCommand(sqlcmd, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retList.title = reader["title"].ToString();
                    retList.price = reader["price"] + " €";
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
            var conn = new SqlConnection(connString);
            var retList = new Orders();
            try
            {
                conn.Open();
                var sqlcmd = "Select guid, client_name, book_title, quantity, state from Orders";
                var cmd = new SqlCommand(sqlcmd, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var order = new Order
                    {
                        guid = reader["guid"].ToString(),
                        client_name = reader["client_name"].ToString(),
                        book_title = reader["book_title"].ToString(),
                        quantity = reader["quantity"].ToString(),
                        state = reader["state"].ToString()
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
            var conn = new SqlConnection(connString);
            var retList = new Orders();
            try
            {
                conn.Open();
                var sqlcmd = "Select guid, client_name, book_title, quantity, state from Orders where client_name ='" +
                             client + "'";
                var cmd = new SqlCommand(sqlcmd, conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var order = new Order
                    {
                        guid = reader["guid"].ToString(),
                        client_name = reader["client_name"].ToString(),
                        book_title = reader["book_title"].ToString(),
                        quantity = reader["quantity"].ToString(),
                        state = reader["state"].ToString()
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

        public void SatisfyOrders(string book, int quantity, bool first)
        {
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                var sqlcmd= "";
                if(first == true)
                    sqlcmd =
                    "Select TOP(1) order_id, guid, client_name, client_email, book_title, quantity from Orders where book_title ='" +
                    book + "' and quantity=" + quantity + " and state LIKE '%occur%' order by order_id asc";
                else
                    sqlcmd =
                        "Select TOP(1) order_id, guid, client_name, client_email, book_title, quantity from Orders where book_title ='" +
                        book + "' and quantity<=" + quantity + " and state LIKE '%waiting%' order by order_id asc";
                var cmd = new SqlCommand(sqlcmd, conn);

                var reader = cmd.ExecuteReader();
                string id = "", uid = "", cl_Name = "", cl_Email = "", b_Title = "", qt = "";

                while (reader.Read())
                {
                    id = reader["order_id"].ToString();
                    uid = reader["guid"].ToString();
                    cl_Name = reader["client_name"].ToString();
                    cl_Email = reader["client_email"].ToString();
                    b_Title = reader["book_title"].ToString();
                    qt = reader["quantity"].ToString();
                }
                reader.Close();
                if(!id.Equals(""))
                    SatisfyOrder(Convert.ToInt32(id), cl_Name, cl_Email, b_Title, Convert.ToInt32(qt), uid);

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
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();

                var sqlcmd = "Update Orders set state= 'dispatched at " + DateTime.Today.ToString("d") +
                             "' where order_id=" + orderID;
                var cmd = new SqlCommand(sqlcmd, conn);
                cmd.ExecuteNonQuery();

                var subject = "Order with ID:" + guid + " dispatched";
                var msg = "Hello " + client + ", your order consisting of " + quant + " " + book +
                          " has been dispatched.";
                SendEmail(email, subject, msg);

                var sqlcmd2 = "Update Books set quantity=quantity-" + quant + " where title='" + book + "'";
                var cmd2 = new SqlCommand(sqlcmd2, conn);
                cmd2.ExecuteNonQuery();

                SatisfyOrders(book,System.Convert.ToInt32(GetBook(book).quantity), false);
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
            var conn = new SqlConnection(connString);

            var stock = 0;
            try
            {
                conn.Open();
                var sqlcmd = "Select quantity from Books where title = '" + book_title + "'";
                var cmd = new SqlCommand(sqlcmd, conn);
                stock = (int) cmd.ExecuteScalar();
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

        public void CreateStoreOrder(string client_name, string client_email, string client_addr, string book_title,
            int quantity)
        {
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                var sqlcmd =
                    "Insert into Orders (book_title, quantity, client_name, client_address, client_email, state) VALUES (@bookTit, @qt, @clName, @clAddr, @clEmail, @st)";
                var cmd = new SqlCommand(sqlcmd, conn);
                cmd.Parameters.Add("@bookTit", SqlDbType.VarChar, 50).Value = book_title;
                cmd.Parameters.Add("@qt", SqlDbType.Int).Value = quantity - 10;
                cmd.Parameters.Add("@clName", SqlDbType.VarChar, 50).Value = client_name;
                cmd.Parameters.Add("@clAddr", SqlDbType.VarChar, 50).Value = client_addr;
                cmd.Parameters.Add("@clEmail", SqlDbType.VarChar, 50).Value = client_email;
                cmd.Parameters.Add("@st", SqlDbType.VarChar, 50).Value = "waiting expedition";

                cmd.ExecuteNonQuery();
                Book book;
                book = GetBook(book_title);

                //Send remote call to warehouse
                var subject = "New order";
                var msg = "Hello " + client_name + ".\n " +
                          "Your order for " + (quantity - 10) +" " + book_title+  
                          " has been placed. We will dispatch it as soon as possible.\n\n"+
                          "Single Price = " +book.price+"\n"+
                          "Total Price = " +((quantity-10)*(float.Parse(Regex.Replace(book.price, "[^0-9,]", ""))))+" €";
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
            var conn = new SqlConnection(connString);
            int rows;
            try
            {
                conn.Open();
                var sqlcmd = "Update Books set quantity=quantity+" + quantity + " where title='" + book_title + "'";
                var cmd = new SqlCommand(sqlcmd, conn);
                rows = cmd.ExecuteNonQuery();
                SatisfyOrders(book_title, quantity - 10,true);
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
        }

        public int ConfirmSell(string client_name, string book_title, int quantity)
        {
            var conn = new SqlConnection(connString);
            int rows;
            try
            {
                conn.Open();
                var sqlcmd = "Update Books set quantity=quantity-" + quantity + " where title='" + book_title + "'";
                var cmd = new SqlCommand(sqlcmd, conn);
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

        public int MakeaSell(string client_name, string client_email, string client_addr, string book_title,
            int quantity)
        {
            var stock = GetStock(book_title);
            if (stock < quantity)
            {
                CreateStoreOrder(client_name, client_email, client_addr, book_title, quantity + 10);
                return 2;
            }
            return ConfirmSell(client_name, book_title, quantity);
        }

        public void ReceiveOrder(string[] order)
        {
            bookTitle = order[0];
            qty = order[1];
            UpdateState(bookTitle, Convert.ToInt32(qty) - 10, DateTime.Today.AddDays(2).ToString("d"));

            NotifyOrder();
        }

        public void UpdateState(string title, int quantity, string date)
        {
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();

                var sqlcmd = "Update Orders set state= 'dispatch will occur at " + date + "' where quantity=" +
                             quantity + " and book_title = '" + title + "'";
                var cmd = new SqlCommand(sqlcmd, conn);
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

        public static void Notify()
        {
            subscribers.ForEach(delegate(IPrintReceipt callback)
            {
                if (((ICommunicationObject) callback).State == CommunicationState.Opened)
                    callback.PrintReceipt(cName, bTitle, q, p);
                else
                    subscribers.Remove(callback);
            });
        }

        public static void NotifyOrder()
        {
            subscribers.ForEach(delegate(IPrintReceipt callback)
            {
                if (((ICommunicationObject) callback).State == CommunicationState.Opened)
                    callback.UpdateOrder(bookTitle, qty);
                else
                    subscribers.Remove(callback);
            });
        }

        public void SendEmail(string client_email, string subject, string message)
        {
            var client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("tdinproj2@gmail.com", "tdinproj2");
            var mm = new MailMessage("tdinproj2@gmail.com", client_email, subject, message);
            mm.BodyEncoding = Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        public string GetPrice(string title, int quantity)
        {
            double singleprice;
            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                var sqlcmd = "Select price from Books where title = '" + title + "'";
                var cmd = new SqlCommand(sqlcmd, conn);
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
            return (singleprice * quantity).ToString();
        }
    }
}