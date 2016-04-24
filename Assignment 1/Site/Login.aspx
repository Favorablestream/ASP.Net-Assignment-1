<%@ Page Title="Login" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment_1.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
        <div style = "width: 300px; margin-left: auto; margin-right: auto;">

            <div>
                <asp:Label id = "error" runat = "server" Visible = "false">
                </asp:Label>
            </div>

            <br />

            Would you like to <a href = "/Site/Register.aspx">register</a> first?

            <br /> <br />

            <div>
                <asp:Login ID = "login" runat = "server" TitleText = "Login" UserNameLabelText = "Username: " PasswordLabelText = "Password: " LoginButtonText = "Login" FailureText = "Login Unsuccessful" DestinationPageUrl = "Default.aspx" DisplayRememberMe = "false" OnAuthenticate = "login_Authenticate" ToolTip = "Enter your username and password" >
                </asp:Login>
            </div>
       </div>
    </div>

</asp:Content>
