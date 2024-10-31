<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
  <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
<asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
<asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    </form>
</body>
</html>
