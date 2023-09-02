<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantRatingApp_V2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <div class="container-fluid">
            <img src="Images/49679194.png" class="img-fluid mx-auto d-block" alt="..." />
            <div class="search-wrapper">
                <div class="form-floating mb-3">
                    <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
                    <label for="floatingInput">Email address</label>
                </div>
            </div>
        </div>
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>



    <h2>Highest Rated</h2>
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
                    <div class="card mb-2" style="min-width: 300px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img src="/Catalog/Images/<%#:Item.ImgName%>" class="img-fluid rounded-start" alt="...">
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


    <div class="container">
        <h2>Categories</h2>
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
                    <div class="col align-content-center justify-content-center">
                        <a href="SearchResults.aspx?category=<%#:Item%>">
                            <h7><%#:Item%></h7> 
                        </a>
                     </div>
                </ItemTemplate>
            </asp:ListView>


        </div>
    </div>


    

</asp:Content>
