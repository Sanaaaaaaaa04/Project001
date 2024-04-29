<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="YourNamespace.profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Profile</title>
    <link href="styles.css" rel="stylesheet" />
    <style type="text/css">
        .btn
        {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>User Profile</h2>
            <div class="profile-img">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="imgProfile" runat="server" CssClass="profile-pic" Height = "100px" Width = "100px"/>
                <asp:FileUpload ID="fileUpload" runat="server" onchange="previewImage()" /></br>
            &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
            </div>
            <div>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:" Font-Bold="True" 
                    Font-Size="Medium"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" style="margin-left: 82px" 
                    Width="270px"></asp:TextBox></br>
                <br />
            </div>
            <div>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" Font-Bold="True" 
                    Font-Size="Medium"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" style="margin-left: 85px" 
                    Width="270px"></asp:TextBox></br>
                <br />
            </div>
            <div>
                <asp:Label ID="lblMobile" runat="server" Text="Mobile No:" Font-Bold="True" 
                    Font-Size="Medium"></asp:Label>
                <asp:TextBox ID="txtMobile" runat="server" style="margin-left: 89px" 
                    Width="270px"></asp:TextBox></br>
                <br />
            </div>
            <div>
                <asp:Label ID="lblEmail" runat="server" Text="Email:" Font-Bold="True" 
                    Font-Size="Medium"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" style="margin-left: 124px" 
                    Width="270px"></asp:TextBox></br>
                <br />
            </div>
            <div>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" BackColor="#66FF33" Font-Bold="True" 
                    Font-Size="Large" Text="Save" Width="83px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" 
                    CssClass="btn" BackColor="#3366CC" CausesValidation="False" Font-Bold="True" 
                    Font-Size="Large" Width="83px" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn" Style="display: none;" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
    </form>
    <script>
        function previewImage() {
            var preview = document.getElementById('imgProfile');
            var file = document.getElementById('<%= fileUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
</body>
</html>
