using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        var baseAddress = "http://localhost:8701/StoreRest/books";

        var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
        http.Accept = "application/json";
        http.Method = "GET";

        var response = http.GetResponse();
        var stream = response.GetResponseStream();
        var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();
        string text = content;

        JArray a = JArray.Parse(text);
        cell.ColSpan = 3;
        cell.InnerText = "Livros";
        row.Cells.Add(cell);
        BookContent.Rows.Add(row);

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.InnerText = "Titulo";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Preço";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Quantidade";
        row.Cells.Add(cell);

        BookContent.Rows.Add(row);

        foreach (JObject o in a.Children<JObject>())
        {
            foreach (JProperty p in o.Properties())
            {
                string name = p.Name;
                if(name.Equals("title"))
                    row = new HtmlTableRow();
                string value = (string)p.Value;
                cell = new HtmlTableCell();
                cell.InnerText = value;
                row.Cells.Add(cell);
                if (name.Equals("quantity"))
                    BookContent.Rows.Add(row);
            }
            BookContent.Rows.Add(row);
        }
    }

    protected void getorder_OnClick(object sender, EventArgs e)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        var baseAddress = "http://localhost:8701/StoreRest/orders/"+OrderName.Text;

        var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
        http.Accept = "application/json";
        http.Method = "GET";

        var response = http.GetResponse();
        var stream = response.GetResponseStream();
        var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();
        string text = content;
        JArray a = JArray.Parse(text);
        cell.ColSpan = 4;
        cell.InnerText = "Compras";
        row.Cells.Add(cell);
        OrderContent.Rows.Add(row);

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.InnerText = "GUID";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Titulo";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Quantidade";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Estado";
        row.Cells.Add(cell);

        OrderContent.Rows.Add(row);

        foreach (JObject o in a.Children<JObject>())
        {
            foreach (JProperty p in o.Properties())
            {
                string name = p.Name;
                if(!name.Equals("client_name")) { 
                    if (name.Equals("guid"))
                        row = new HtmlTableRow();
                    string value = (string)p.Value;
                    cell = new HtmlTableCell();
                    cell.InnerText = value;
                    row.Cells.Add(cell);
                    if (name.Equals("state"))
                        OrderContent.Rows.Add(row);
                }
            }
            OrderContent.Rows.Add(row);
        }
    }
    protected void submit_OnClick(object sender, EventArgs e)
    {

        string nome = clNome.Text, morada = clMorada.Text, email = clEmail.Text, livro = bTitulo.Text;
        int quantidade = System.Convert.ToInt32(bQuantidade.Text);
        var baseAddress = "http://localhost:8701/StoreRest/orders/new";

        var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
        http.Accept = "application/json";
        http.ContentType = "application/json";
        http.Method = "POST";


        string parsedContent = "{ \"Name\": \""+ nome+"\", \"Address\": \""+morada+"\", \"Email\": \""+email+"\", \"Book\": \""+livro+"\",\"Quantity\": \""+quantidade+"\" }";
        ASCIIEncoding encoding = new ASCIIEncoding();
        Byte[] bytes = encoding.GetBytes(parsedContent);

        Stream newStream = http.GetRequestStream();
        newStream.Write(bytes, 0, bytes.Length);
        newStream.Close();

        var response = http.GetResponse();

        var stream = response.GetResponseStream();
        var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();
        clNome.Text = "";
        clMorada.Text = "";
        clEmail.Text = "";
        bTitulo.Text = "";
        bQuantidade.Text = "";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Order Message", "alert('Ordem submetida com sucesso! Aguarde pelo Email.')", true);
    }
}