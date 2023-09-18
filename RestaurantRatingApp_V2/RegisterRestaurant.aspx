<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterRestaurant.aspx.cs" Inherits="RestaurantRatingApp_V2.RegisterRestaurant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="Card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-8">
                    <div class="form-horizontal">
                        <h2>Restaurant Registration Form</h2>
                        <p>To be able to register your restaurant with us you must be logged in from an owner account</p>
                       <hr />
                        <div class="form-group">
                            <asp:Label ID="lblRestaurantName" runat="server" Text="Restaurant Name:"></asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtRestaurantName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRestaurantName" runat="server"
                                    ControlToValidate="txtRestaurantName"
                                    InitialValue=""
                                    ErrorMessage="Restaurant Name is required."
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    CssClass="validation-error">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblCategory" runat="server" Text="Category:"></asp:Label>
                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlCategory" runat="server">
                                    <asp:ListItem Text="Greek" Value="Greek" />
                                    <asp:ListItem Text="Asian" Value="Asian" />
                                    <asp:ListItem Text="Contemporary" Value="Contemporary" />
                                    <asp:ListItem Text="Italian" Value="Italian" />
                                    <asp:ListItem Text="Mexican" Value="Mexican" />

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCategory" runat="server"
                                    ControlToValidate="ddlCategory"
                                    InitialValue=""
                                    ErrorMessage="Category is required."
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    CssClass="validation-error">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescription" runat="server"
                                    ControlToValidate="txtDescription"
                                    InitialValue=""
                                    ErrorMessage="Description is required."
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    CssClass="validation-error">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
