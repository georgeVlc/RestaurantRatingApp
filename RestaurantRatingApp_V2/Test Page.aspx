<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test Page.aspx.cs" Inherits="RestaurantRatingApp_V2.Test_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



     <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="UsernameTextBox" CssClass="col-md-2 control-label">Username</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="UsernameTextBox" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UsernameTextBox"
                                CssClass="text-danger" ErrorMessage="The Username field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="PasswordTextBox" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                    </div>







</asp:Content>
