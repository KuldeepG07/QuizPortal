<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepwd.aspx.cs" Inherits="QuizPortal.changepwd" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .container {
            margin-top: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Change Password</h2>

            <div class="form-group">
                <label for="txtOldPassword">Old Password:</label>
                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter old password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword" ErrorMessage="Old password is required" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label for="txtNewPassword">New Password:</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter new password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New password is required" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label for="txtConfirmPassword">Confirm New Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm new password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm password is required" CssClass="text-danger" Display="Dynamic" />
                <asp:CompareValidator ID="cvPasswordMatch" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match" CssClass="text-danger" Display="Dynamic" />
            </div>

            <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

            <div class="mt-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" PostBackUrl="~/profile.aspx" />
            </div>
        </div>
    </form>
</body>
</html>

