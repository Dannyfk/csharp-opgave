<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="CostumerWebpage.Page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Select a destination for your travel<br />
        <asp:DropDownList ID="destinationList" runat="server" OnSelectedIndexChanged="destinationList_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <br />
        <asp:ListBox ID="vacationHouseList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="vacationHouseList_SelectedIndexChanged" Visible="False" Width="363px"></asp:ListBox>
        <br />
        Max persons: <asp:Label ID="lblPersons" runat="server" Visible="False"></asp:Label>
        <br />
        Distance to closest shopping:
        <asp:Label ID="lblShopping" runat="server" Visible="False"></asp:Label>
        <br />
        Distance to closest beach:
        <asp:Label ID="lblBeach" runat="server" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblInfo" runat="server" Text="Please select the week you want to book" Visible="False"></asp:Label>
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <br />
        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
        <br />
        <br />
        <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
        <br />
    
    </div>
    </form>
</body>
</html>
