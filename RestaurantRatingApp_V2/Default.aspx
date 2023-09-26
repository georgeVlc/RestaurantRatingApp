<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantRatingApp_V2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .bg-image {
            background-image: url('Images/DefaultPhoto.png');
            background-size: cover;
            background-position: center;
            min-height: 650px;
        }


        .transparent-card {
            background: rgba(255, 255, 255, 0.5); /* Adjust the last value (0.5) for transparency level */
            border: none; /* Remove border */
        }
    </style>


    <div class="container-fluid bg-image">
        <div class="row justify-content-center align-items-center" style="height: 700px; position: relative;">
            <div class="col-md-6">
                <div class="card transparent-card">
                    <div class="card-body">
                        <h5 class="card-title">Search</h5>
                        <div class="input-group">
                            <input type="search" class="form-control" id="searchInput" name="searchInput" placeholder="Type To Search" />
                            <div class="input-group-append">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary p-2" OnClick="SearchButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="card">
        <div class="card-body">

            <div class="row mt-4">
                <h2>Categories</h2>
                <div class="container">
                    <div class="shadow-lg p-3 mb-5 bg-body rounded">
                        <div class="row">
                            <asp:ListView ID="categoryList" runat="server"
                                GroupItemCount="5"
                                ItemType="System.String" SelectMethod="GetCousineTypesAsStrings">
                                <EmptyDataTemplate>
                                    <table>
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <GroupTemplate>
                                    <tr id="itemPlaceholderContainer" runat="server">
                                        <td id="itemPlaceholder" runat="server"></td>
                                    </tr>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div class="col align-content-center justify-content-center  text-center">
                                        <a href="SearchResults.aspx?category=<%#:Item%>">
                                            <h7><%#:Item%></h7>
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>

            <hr />
            <div class="row mt-4">
                <h2>Highest Rated</h2>
                <div class="d-flex justify-content-center">
                    <div class="row row-cols-4">
                        <asp:ListView ID="topRatedList" runat="server"
                            DataKeyNames="Name" GroupItemCount="4"
                            ItemType="RestaurantRatingApp_V2.Models.Restaurant" SelectMethod="GetTopRatedRestaurants">
                            <EmptyDataTemplate>
                                <table>
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <GroupTemplate>
                                <tr id="itemPlaceholderContainer" runat="server">
                                    <td id="itemPlaceholder" runat="server"></td>
                                </tr>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="col">
                                    <div class="card mb-2" style="min-width: 300px; min-height: 250px">
                                        <div class="row g-0">
                                            <div class="col-md-4">
                                                <img src="<%#:Item.ImgName%>" class="img-thumbnail" alt="..."/>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="card-body">
                                                    <a href="RestaurantPage.aspx?Name=<%#:Item.Name%>">
                                                        <h4 class="card-title"><%#:Item.Name%></h4>
                                                    </a>
                                                    <h7 class="card-subtitle mb-2 text-muted"><%#Item.Type%></h7>
                                                    <p class="card-text"><%#Item.Description%></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div class="row mt-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h4 class="card-title">Are you an Owner?</h4>
                        <p class="card-text">It doesn't matter whether you are just starting out your first bussiness or just trying to promote your existing one. You are at the right place either way. Register you restaurant with us for free and join our ever growing community of food enthusiasts from the link below.</p>
                        <asp:Button ID="registerbtn" runat="server" Text="Register" OnClick="register_click" class="btn btn-primary" />
                    </div>
                </div>
            </div>
            
            <div class="row mt-4">
                <h2>See More</h2>
             
                    <div class="card-body">

                <div class="container">
                    <div class="shadow-lg p-3 mb-5 bg-body rounded">
                        <div class="row">
                            <p>Nothing That Peaked Your Interest?</p>
                            <asp:Button runat="server" OnClick="SeeAll" Text="All Categories" class="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
           </div>


</asp:Content>
