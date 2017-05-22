using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;

namespace Store
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StoreWebService" in both code and config file together.

    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class StoreWebService : IStoreWebService
    {
        public static string connString = ConfigurationManager.ConnectionStrings["StoreDB"].ToString();
        public StoreService sv;

        public Books GetBooks()
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

        public string AddOrder( /*OnlineOrder onlineorder*/
            string Name, string Address, string Email, string Book, int Quantity)
        {
            sv = new StoreService();
            var stock = sv.GetStock(Book);
            if (stock < Quantity)
            {
                sv.CreateStoreOrder(Name, Email, Address, Book, Quantity + 10);

                return "Order placed";
            }
            sv.ConfirmOnlineSell(Name, Book, Quantity);
            sv.StoreOnlineOrder(Name, Email, Address, Book, Quantity);
            return "Order placed instore, shipping tomorrow";
        }

        public Orders GetOrder(string name)
        {
            var conn = new SqlConnection(connString);
            var retList = new Orders();
            try
            {
                conn.Open();
                var sqlcmd = "Select guid, client_name, book_title, quantity, state from Orders where client_name ='" +
                             name + "'";
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
    }
}