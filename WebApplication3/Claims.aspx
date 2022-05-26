<%@ Page Async="true" Title="Claims" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Claims.aspx.cs" Inherits="WebApplication3.Claims" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h3>Main Claims:</h3>
    <% if (Request.IsAuthenticated) { %>
<table class="table table-striped table-bordered table-hover">
    <tr><td>Name</td><td><%= oidcName %></td></tr>
    <tr><td>Username</td><td><%= oidcUserName %></td></tr>
    <tr><td>Subject</td><td><%= oidcSubject %></td></tr>
    <tr><td>TenantId</td><td><%= oidcTenantId %></td></tr>
</table>
<br />
<h3>User Additional Claims (Information Client):</h3>
<table class="table table-striped table-bordered table-hover table-condensed">
   <% foreach (var claim in userInfoProfile)
    { %>
        <tr><td> <%= claim.Type%></td><td> <%= claim.Value%></td></tr>
     <%} %>
</table>
<br />
<h3>All security Claims:</h3>
<table class="table table-striped table-bordered table-hover table-condensed">
    <% foreach (var claim in System.Security.Claims.ClaimsPrincipal.Current.Claims)
    { %>
        <tr><td><%= claim.Type %></td><td><%= claim.Value %></td></tr>
    <% } %>
</table>
    <% } %>
</asp:Content>
