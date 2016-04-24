<%@ Page Title="Bad Words" Language="C#" MasterPageFile="MasterPageMenu.Master" AutoEventWireup="true" CodeBehind="BadWords.aspx.cs" Inherits="Assignment_1.Admin.BadWords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
        <div style = "width: 600px; margin-left: auto; margin-right: auto;">

            <br /> <br />

            <div>
                Bad Words <br />
                <asp:GridView ID = "badwords" runat = "server" AllowPaging = "true" PageSize = "20" AutoGenerateColumns = "false" EmptyDataText = "No Bad Words to Display" ToolTip = "Bad Words =(" Width = "600px" OnRowDeleting = "badwords_RowDeleting">
                    <Columns>
                        <asp:BoundField HeaderText = "Bad Word" DataField = "word"/>
                        <asp:BoundField HeaderText = "Date/Time Added" DataField = "added"/>
                        <asp:CommandField ShowDeleteButton = "true" />
                        <asp:BoundField DataField = "id" HeaderText = "" ItemStyle-CssClass = "InvisibleColumn"/>
                    </Columns>
                </asp:GridView>
            </div>

            <br /> <br />

            <div>
                <asp:TextBox ID = "writeBadWord" runat = "server">
                </asp:TextBox>
            </div>

            <asp:RegularExpressionValidator runat="server" ControlToValidate="writeBadWord" ErrorMessage="Enter one word, no spaces allowed" ValidationExpression="[^\s]+" />
            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="writeBadWord" ErrorMessage="This field is required" />

            <br />

            <asp:Button ID = "submitBadWord" Text = "Add Bad Word" OnClick  = "submitBadWord_Click" runat = "server" CausesValidation = "true"/>
       </div>
    </div>

</asp:Content>
