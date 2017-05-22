<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TDIN Book Store</title>
</head>
<body>
<br/>
<br/>

<h1>TDIN Book Store</h1>
<br/>
<br/>
<table id="BookContent" border="1" runat="server"></table>
<br/>
<form id="form1" runat="server">
    <div>
        <label>Nome:<asp:TextBox ID="clNome" runat="server"/></label><br/>
        <label>Morada:<asp:TextBox ID="clMorada" runat="server"/></label><br/>
        <label>Email:<asp:TextBox ID="clEmail" runat="server"/></label><br/>
        <label>Livro: <asp:TextBox ID="bTitulo" runat="server"/></label><br/>
        <label>Quantidade:<asp:TextBox ID="bQuantidade" runat="server"/></label><br/>
        <asp:Button ID="submit" runat="server" OnClick="submit_OnClick" Text="Encomendar"/>
        <br/>
        <br/>
        <br/>
        <br/>
        <label>Nome:<asp:TextBox ID="OrderName" runat="server"/></label>
        <asp:Button ID="getorder" runat="server" OnClick="getorder_OnClick" Text="Consultar"/>
    </div>
</form>
<table id="OrderContent" border="1" runat="server"></table>
</body>
</html>