<%@ Page Title="Edit User" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Assignment_1.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style = "text-align: center;">
        <div style = "width: 600px; margin-left: auto; margin-right: auto;">

            <table>
                <tr>
                    <td>
                        <div>
                            First Name: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "firstName" runat = "server" ToolTip = "Enter Your First Name">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "firstName" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Last Name: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "lastname" runat = "server" ToolTip = "Enter Your Last Name">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "lastname" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Username: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "username" runat = "server" ToolTip = "Enter your Username">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "username" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Email: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "email" runat = "server" ToolTip = "Enter your Email" TextMode = "Email">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "email" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate = "email" ControlToCompare = "emailConfirm" runat = "server" ErrorMessage = "These Fields must Match">
                        </asp:CompareValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Confirm Email: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "emailConfirm" runat = "server" ToolTip = "Confirm Your Email" TextMode = "Email">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "emailConfirm" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Password: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "password" runat = "server" ToolTip = "Enter a Password" TextMode = "Password">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "password" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate = "password" ControlToCompare = "passwordConfirm" runat = "server" ErrorMessage = "These Fields must Match">
                        </asp:CompareValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Confirm Password: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "passwordConfirm" runat = "server" ToolTip = "Confirm Your Password" TextMode = "Password">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "passwordConfirm" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>


                <tr>
                    <td>
                        <div>
                            Phone Number: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "phone" runat = "server" ToolTip = "Enter Your Phone Number" TextMode = "Phone">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>

                        <asp:RequiredFieldValidator ControlToValidate = "phone" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Date of Birth:
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Calendar ID = "dob" runat = "server" ToolTip = "Select Your Date of Birth" SelectedDate = "1995-12-29" VisibleDate = "1995-12-29">
                            </asp:Calendar>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            Country: 
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID = "country" runat = "server" ToolTip = "Enter Your Country">
                            </asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate = "country" runat = "server" ErrorMessage = "This Field is Required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:CheckBox ID = "admin" runat = "server" Text = "Would you like to be an admin?" ToolTip = "With Great Power..." Checked = "false" />
                        </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <div>
                            <asp:Button ID = "submit" runat = "server" text = "Submit" CausesValidation = "true" OnClick = "submit_Click" />
                        </div>
                    </td>
                </tr>
            </table>

       </div>
    </div>

</asp:Content>
