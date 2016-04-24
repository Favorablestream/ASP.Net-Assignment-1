<%@ Page Title="Full Blog Post" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="FullBlogPost.aspx.cs" Inherits="Assignment_1.FullBlogPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
        <div style = "width: 600px; margin-left: auto; margin-right: auto;">

            <div>
                <asp:Label ID = "error" Visible = "false" runat = "server">
                </asp:Label>
            </div>

            <br />

            <div>
                Post Title <br />
                <asp:TextBox ID = "title" runat = "server" Rows = "1" Width = "600px" ReadOnly = "true" ToolTip = "Post Title">
                </asp:TextBox> 
            </div>

            <br />

            <div>
                Post Description <br />
                <asp:TextBox ID = "description" runat = "server" Rows = "1" Width = "600px" ReadOnly = "true" ToolTip = "Post Description">
                </asp:TextBox> 
            </div>

            <br />

            <div>
                Post Author <br />
                <asp:TextBox ID = "author" runat = "server" Rows = "1" Width = "600px" ReadOnly = "true" ToolTip = "Post Author">
                </asp:TextBox> 
            </div>

            <br />

            <div>
                Post Text <br />
                <asp:TextBox ID = "text" runat = "server" Rows = "20" Width = "600px" TextMode = "MultiLine" ReadOnly = "true" ToolTip = "Post Text">
                </asp:TextBox> 
            </div>

            <br />

            <div>
                Date/Time Posted <br />
                <asp:TextBox ID = "posted" runat = "server" Rows = "1" Width = "600px" ReadOnly = "true" ToolTip = "Date/Time Posted">
                </asp:TextBox> 
            </div>

            <br />

            <div>
                <asp:GridView ID = "comments" runat = "server" AutoGenerateColumns = "false" EmptyDataText = "No Comments to Display" Width = "600px" ToolTip = "Comments" OnRowDeleting = "comments_RowDeleting">
                    <Columns>
                        <asp:BoundField HeaderText = "Comment" DataField = "text"/>
                        <asp:BoundField HeaderText = "Posted" DataField = "posted"/>
                        <asp:TemplateField HeaderText = "Author">
                            <ItemTemplate>
                                <%# Eval ("firstname") + " " + Eval ("lastname")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="true" Visible = "false"/>
                        <asp:BoundField HeaderText = "" DataField = "id" ItemStyle-CssClass = "InvisibleColumn"/>
                    </Columns>
                </asp:GridView>
            </div>

            <br />

            <div>
                Add a comment <br />
                <asp:TextBox ID = "writeComment" runat = "server" Width = "600px" TextMode = "MultiLine" Rows = "5" ToolTip = "Write a Comment">
                </asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate = "writeComment" ErrorMessage = "You must write a comment first" runat = "server">
                </asp:RequiredFieldValidator>
                <br />
                <asp:Button ID = "submitComment" runat = "server" OnClick = "submitComment_Click" Text = "Submit Comment" CausesValidation = "true" ToolTip = "Submit Comment"/>
            </div>

       </div>
    </div>

</asp:Content>
