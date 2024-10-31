<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="WebApplication1.AdminDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Admin Dashboard</h2>

            <h3>Manage Users</h3>
            <asp:Button ID="btnDownloadLog" runat="server" Text="Download Log" OnClick="btnDownloadLog_Click" CssClass="btn btn-primary" />

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

            <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="UsersGridView_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="User ID" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnPromoteToAdmin" runat="server" CommandName="PromoteToAdmin" CommandArgument='<%# Eval("Id") %>' Text="Promote to Admin" CssClass="btn btn-warning" />
                            <asp:Button ID="btnDeactivate" runat="server" CommandName="DeactivateUser" CommandArgument='<%# Eval("Id") %>' Text="Deactivate" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="DeleteUser" CommandArgument='<%# Eval("Id") %>' Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <h3>Manage Posts</h3>
            <asp:GridView ID="PostsGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="PostsGridView_RowCommand">
                <Columns>
                    <asp:BoundField DataField="PostId" HeaderText="Post ID" />
                    <asp:BoundField DataField="Content" HeaderText="Content" />
                    <asp:BoundField DataField="UserId" HeaderText="User ID" />
                    <asp:BoundField DataField="Approved" HeaderText="Approved" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" CommandName="ApprovePost" CommandArgument='<%# Eval("PostId") %>' Text="Approve" />
                            <asp:Button ID="btnDeletePost" runat="server" CommandName="DeletePost" CommandArgument='<%# Eval("PostId") %>' Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
