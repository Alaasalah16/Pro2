<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="WebApplication1.UserDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
           <h1>Welcome, <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label></h1>

        <asp:Image ID="ProfilePicture" runat="server" Width="100px" Height="100px" />
        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>

        
        <h2>Upload Profile Picture</h2>
        <asp:FileUpload ID="ProfilePictureUpload" runat="server" />   
       <asp:Button ID="UploadProfilePictureButton" runat="server" Text="Upload" OnClick="UploadProfilePictureButton_Click" />
          <h2>Upload Profile </h2>
        <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btn btn-secondary" OnClick="btnEditProfile_Click" />

        <h2>Create New Post</h2>
        <asp:TextBox ID="NewPostContent" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
        <asp:FileUpload ID="NewPostImageUpload" runat="server" />
        <asp:Button ID="CreatePostButton" runat="server" Text="Create Post" OnClick="CreatePostButton_Click" />
<%--   <h2>Users Posts</h2>
        <asp:HyperLink ID="lnkViewUserPosts" runat="server" NavigateUrl="UserS Posts.aspx" CssClass="btn btn-primary">
    View Users' Posts
</asp:HyperLink>--%>

           <h2>Users Posts</h2>
        <asp:Button ID="btnViewUserPosts" runat="server" Text="View Users' Posts" CssClass="btn btn-primary" 
            OnClientClick="window.location.href='UserS Posts.aspx'; return false;" />

        <asp:GridView ID="PostsGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="PostsGridView_RowCommand"  DataKeyNames="PostId" >
    <Columns>
        <asp:BoundField DataField="Content" HeaderText="Content" />
       <asp:ButtonField ButtonType="Button" CommandName="EditPost" Text="Edit" />
        <asp:ButtonField ButtonType="Button" CommandName="DeletePost" Text="Delete" />
    </Columns>
</asp:GridView>

     
           <div style="text-align: right;">
       <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
   </div>
    </form>
</body>
</html>
