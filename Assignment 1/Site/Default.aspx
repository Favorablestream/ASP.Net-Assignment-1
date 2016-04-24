<%@ Page Title="Home" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment_1.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
        <div style = "width: 600px; margin-left: auto; margin-right: auto;">

            <asp:Label ID = "userLabel" runat = "server">
            </asp:Label>

            <br /> <br />

            <asp:GridView ID = "top5Posts" runat = "server" AllowPaging = "true" PageSize = "5" AutoGenerateColumns = "false" EmptyDataText = "No Posts to Display" ToolTip = "Top 5 Most Recent Posts" OnPageIndexChanging = "top5Posts_PageIndexChanging" Width = "600px">
                <Columns>
                    <asp:HyperLinkField DataTextField = "title" HeaderText = "Post Title" DataNavigateUrlFields = "id" DataNavigateUrlFormatString = "FullBlogPost.aspx?postID={0}"/>
                    <asp:BoundField HeaderText = "Description" DataField = "description"/>
                    <asp:BoundField HeaderText = "Date/Time Posted" DataField = "posted"/>
                </Columns>
            </asp:GridView>

            <br /> <br />

            <div>
                <asp:Button ID = "logout" runat = "server" Text = "Logout" Visible = "false" OnClick =" logout_Click" />
            </div>
       </div>
    </div>

</asp:Content>
