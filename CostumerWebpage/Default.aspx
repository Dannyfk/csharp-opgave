<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CostumerWebpage.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                 Welcome to Global trust<br />
        <br />
        Please login to browse and order trips around the world<br />
        if you are not a user yet then register first<br />
        <br />
        Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tBoxEmail" runat="server" Width="286px"></asp:TextBox>
        <br />
        Password:&nbsp;
        <asp:TextBox ID="tBoxPassword" runat="server" Width="286px"></asp:TextBox>
        <br />
                 <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" />
    
    </div>
    </form>
</body>
</html>
