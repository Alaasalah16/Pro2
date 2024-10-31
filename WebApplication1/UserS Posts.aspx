<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserS_Posts.aspx.cs" Inherits="WebApplication1.UserS_Posts" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Users' Posts</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Users' Posts</h2>
            <asp:GridView ID="AllPostsGridView" runat="server" AutoGenerateColumns="false" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="User" />
                    <asp:BoundField DataField="Content" HeaderText="Content" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="PostImage" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Width="100px" Visible='<%# !string.IsNullOrEmpty(Eval("ImagePath").ToString()) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
