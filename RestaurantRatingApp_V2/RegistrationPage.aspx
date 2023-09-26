<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="RestaurantRatingApp_V2.RegistrationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-body">
            <h2>Sign Up</h2>
            <div class="row">
                <div class="col-md-8">
                    <div class="form-horizontal">
                        <h4>Create a new account and join our community today</h4>
                        <hr />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>

                        <asp:ValidationSummary runat="server" CssClass="text-danger" />

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
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox"
                                    CssClass="text-danger" ErrorMessage="The passwomrd field is required." />
                            </div>
                        </div>
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label" >Confirm password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" class="btn btn-primary" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
