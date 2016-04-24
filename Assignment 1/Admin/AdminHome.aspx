<%@ Page Title="Admin Home" Language="C#" MasterPageFile="~/Admin/MasterPageMenu.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="Assignment_1.Admin.AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
    <div style = "width: 600px; margin-left: auto; margin-right: auto;">

            <div>
                <asp:Label ID = "userLabel" runat = "server">
                </asp:Label>
            </div>

                <br /> <br />

            <div>
                <asp:GridView ID = "posts" runat = "server" AutoGenerateColumns = "false" EmptyDataText = "No Posts to Display" ToolTip = "All Posts" OnRowEditing = "posts_RowEditing" OnRowDeleting = "posts_RowDeleting" Width = "600px">
                    <Columns>
                        <asp:HyperLinkField DataTextField = "title" HeaderText = "Post Title" DataNavigateUrlFields = "id" DataNavigateUrlFormatString = "/Site/FullBlogPost.aspx?postID={0}"/>
                        <asp:BoundField HeaderText = "Description" DataField = "description"/>
                        <asp:BoundField HeaderText = "Date/Time Posted" DataField = "posted"/>
                        <asp:TemplateField HeaderText="Is Post Available" SortExpression="is_available">
                            <ItemTemplate><%# (Convert.ToBoolean (Eval ("is_available"))) ? "Yes" : "No" %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton = "true" ShowDeleteButton = "true" />
                        <asp:BoundField DataField = "id" ItemStyle-CssClass = "InvisibleColumn"/>
                    </Columns>
                </asp:GridView>
            </div>

            <div>
                <asp:Label ID = "confirmLabel" runat = "server" Text = "Are you sure?" Visible = "false">
                </asp:Label> <br />
                <asp:Button ID = "deleteButton" Text = "Delete Post" OnClick = "deleteButton_Click" runat = "server" Visible = "false"/> <br />
                <asp:Button ID = "cancelButton" Text = "Cancel" OnClick = "cancelButton_Click" runat = "server" Visible = "false"/>
            </div>

       </div>
       </div>

</asp:Content>
