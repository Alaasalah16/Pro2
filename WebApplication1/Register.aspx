<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
</head>
<body>
    <form id="form1" runat="server">
 <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
<asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
<asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
<asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    </form>
</body>
</html>
