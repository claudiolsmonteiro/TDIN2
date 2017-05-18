<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TDIN Book Store</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <label>Nome:<asp:TextBox ID="clNome" runat="server" /></label><br />
        <label>Morada:<asp:TextBox ID="clMorada" runat="server" /></label><br />
        <label>Email:<asp:TextBox ID="clEmail" runat="server" /></label><br />
        <label>Livro: <asp:TextBox ID="bTitulo" runat="server"/></label><br />
        <label>Quantidade:<asp:TextBox ID="bQuantidade" runat="server" /></label><br />
        <asp:Button ID="submit" runat="server" OnClick="submit_OnClick" Text="Encomendar" />
    </div>
    </form>

    <table>
        
    </table>
</body>
</html>
