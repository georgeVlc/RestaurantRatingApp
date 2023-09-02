<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterRestaurant.aspx.cs" Inherits="RestaurantRatingApp_V2.RegisterRestaurant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-body p-4">
            <h3 class="card-title">Restaurant Registration Form</h3>
            <p>Please Fill All Fields Correctly</p>
            
            <div class="row">
                <asp:Label ID="lblRestaurantName" runat="server" Text="Restaurant Name:"></asp:Label>
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
            <div class="row">
            <asp:Label ID="lblCategory" runat="server" Text="Category:"></asp:Label>
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
            <div class="row">
            <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
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
            <div class="row">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </div>
      </div>
</asp:Content>
