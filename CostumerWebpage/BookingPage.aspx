<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookingPage.aspx.cs" Inherits="CostumerWebpage.BookingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Confirm booking" />
    
    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Return" Width="120px" />
    
    </div>
    </form>
</body>
</html>
