using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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