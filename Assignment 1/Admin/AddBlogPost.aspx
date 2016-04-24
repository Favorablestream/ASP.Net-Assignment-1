<%@ Page Title="Add Blog Post" Language="C#" MasterPageFile="MasterPageMenu.Master" AutoEventWireup="true" CodeBehind="AddBlogPost.aspx.cs" Inherits="Assignment_1.Admin.AddBlogPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
    <div style = "width: 600px; margin-left: auto; margin-right: auto;">

        <div>
            Post Title
            <br />
            <asp:TextBox ID="title" runat="server" Rows="1" Width="600px" ToolTip="Post Title">
            </asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate = "title" runat = "server" ErrorMessage = "This field is required">
            </asp:RequiredFieldValidator>
        </div>

        <br />

        <div>
            Post Description
            <br />
            <asp:TextBox ID="description" runat="server" Rows="1" Width="600px" ToolTip="Post Description">
            </asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate = "description" runat = "server" ErrorMessage = "This field is required">
            </asp:RequiredFieldValidator>
        </div>


        <br />

        <div>
            Post Text
            <br />
            <asp:TextBox ID="text" runat="server" Rows="20" Width="600px" TextMode="MultiLine" ToolTip="Post Text">
            </asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate = "text" runat = "server" ErrorMessage = "This field is required">
            </asp:RequiredFieldValidator>
        </div>

        <br />

        <div>
            <asp:CheckBox ID = "available" runat = "server" Checked = "true" ToolTip = "Make this post available for viewing?" Text = "Make Post Available?"/>
        </div>

        <br />

        <asp:Button ID="submit" Text="Add Post" CausesValidation="true" ToolTip="Add the new post" runat="server" OnClick = "submit_Click"/>

    </div>
    </div>
</asp:Content>
