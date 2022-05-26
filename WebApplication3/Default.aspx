<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication3.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <div ID="LoginTipoDocumentoDiv" class="col-md-12" runat="server">
            <br />
            <asp:DropDownList ID="TipoDocumento" runat="server" class="form-control">
                <asp:ListItem Value="EM">Correo electrónico</asp:ListItem>
                <asp:ListItem Value="CC">Cédula de ciudadanía</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div ID="LoginDocumentoDiv" class="col-md-12" runat="server">
            <br />
            <asp:TextBox ID="Documento" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div ID="LoginDiv" class="col-md-12" runat="server">
            <br />
            <asp:Button ID="LoginButton" runat="server" Text="Iniciar Sesión" OnClick="login_Click" class = "btn btn-primary" />
            <asp:Button ID="LoginRegister" runat="server" Text="Registrar" OnClick="register_Click" class = "btn btn-primary" />
        </div>
        
        <div ID="LoginIn" class="col-md-12" runat="server">
            <br />
            <asp:Label runat="server" ID="textUser"></asp:Label>
        </div>

        <div ID="ClaimsDiv" class="col-md-12" runat="server">
            <br />
            <asp:Button ID="ClaimsButton" runat="server" Text="See Your Claims" OnClick="claims_Click"  class = "btn btn-primary" />
        </div>

        <div ID="PersonalizarDiv" class="col-md-12" runat="server">
            <br />
            <asp:Button ID="PersonalizarButton" runat="server" Text="Personalizar" OnClick="personalizar_Click"  class = "btn btn-primary" />
        </div>

        <div ID="LoginDivLogout" class="col-md-12" runat="server">
            <br />
            <asp:Button ID="LogoutButton" runat="server" Text="Sign out" OnClick="logout_Click" class = "btn btn-danger" />
        </div>
        
    </div>
</asp:Content>
