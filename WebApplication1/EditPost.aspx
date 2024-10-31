<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPost.aspx.cs" Inherits="WebApplication1.EditPost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Edit Post</title>
<link href="Content/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/jquery-3.5.1.min.js"></script>
<script src="Scripts/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   
        <div class="container">
            <h2>Edit Post</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>

            <div class="form-group">
                <label for="Content">Post Content:</label>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ImageUpload">Post Image:</label>
                <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" />
                <asp:Image ID="CurrentImage" runat="server" CssClass="img-thumbnail" Width="200px" Visible="false" />
            </div>

            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save Changes" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" />
        </div>
    </form>
</body>
</html>


