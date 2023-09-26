<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="RestaurantRatingApp_V2.SearchResults" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4 ">
                    <div class="card mt-4">
                        <div class="card-body p-3">
                            <asp:UpdatePanel
                                ID="UpdatePanel1"
                                runat="server"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h4 class="card-title">Cuisine Filters</h4>
                                      <span>Pick you Favourite</span>
                                    <asp:RadioButtonList ID="rbfilterlist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged">
                                        <asp:ListItem Text="Greek" Value="GREEK" />
                                        <asp:ListItem Text="Asian" Value="ASIAN" />
                                        <asp:ListItem Text="Contemporary" Value="CONTEMPORARY" />
                                        <asp:ListItem Text="Italian" Value="ITALIAN" />
                                        <asp:ListItem Text="Mexican" Value="MEXICAN" />
                                    </asp:RadioButtonList>

                                    <asp:Button ID="filterButton2" runat="server" class="btn btn-primary" Text="Remove Filters" OnClick="removefilterButtonClick" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>

                <div class="col-md-8">
                    <asp:UpdatePanel
                        ID="restaurantUpdatePanel"
                        runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:ListView ID="ListView1" runat="server"
                                DataKeyNames="Name" GroupItemCount="1"
                                ItemType="RestaurantRatingApp_V2.Models.Restaurant" SelectMethod="GetRestaurants">
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
                                    <div class="card m-4" style="max-width: 800px;">
                                        <div class="row g-0">
                                            <div class="col-md-4">
                                                <img src="<%#:Item.ImgName %>" class="img-fluid rounded-start" alt="...">
                                            </div>
                                            <div class="col-md-8">
                                                <div class="card-body p-1">
                                                    <a href="RestaurantPage.aspx?Name=<%#:Item.Name%>">
                                                        <h4 class="card-title"><%#:Item.Name%></h4>
                                                    </a>
                                                    <h8 class="card-subtitle mb-2 text-muted"><%#Item.Type%></h8>
                                                    <p class="card-text"><%#Item.Description%></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbfilterlist" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                 
                   
                </div>
            </div>
        </div>
    </div>














    <!--
     <div class="card mb-2">
                                <div class="card-body p-1">
                                   
                                    <a href="RestaurantPage.aspx?Name=:Item.Name%>">
                                         <h4 class="card-title">:Item.Name></h4>
                                    </a>
                                    <h7 class="card-subtitle mb-2 text-muted"><#Item.Type</h7>
                                    <p class="card-text"><#Item.Description</p>
                                </div>
                            </div>
    !-->



</asp:Content>
