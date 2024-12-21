<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="FDRH_3C_Biblioteca.Gestion.RegistroUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>
            <asp:Label ID="lblTitulo" runat="server" Text="Registro de Usuario"></asp:Label>
        </h2>

        <asp:Panel ID="PanelRegistro" runat="server" CssClass="p-4 border">
            <asp:HiddenField ID="hdnUsuarioID" runat="server" />

            <div class="mb-3">
                <label for="txtNombreUsuario" class="form-label">Nombre Usuario</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtUsuarioCorreo" class="form-label">Correo Electrónico</label>
                <asp:TextBox ID="txtUsuarioCorreo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtUsuarioTelefono" class="form-label">Teléfono</label>
                <asp:TextBox ID="txtUsuarioTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="ddlLibrosDisponibles" class="form-label">Seleccionar Libro</label>
                <asp:DropDownList ID="ddlLibrosDisponibles" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlLibrosDisponibles_SelectedIndexChanged">
                    <asp:ListItem Text="Seleccione un libro" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="txtAutor" class="form-label">Autor del Libro</label>
                <asp:TextBox ID="txtAutor" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtCategoria" class="form-label">Categoría del Libro</label>
                <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-4">
                <label class="form-label">Estado del Préstamo</label>
                <div class="form-check">
                    <asp:RadioButton ID="rbPrestado" runat="server" GroupName="EstadoPrestamo" CssClass="form-check-input" />
                    <label class="form-check-label" for="rbPrestado">Prestado</label>
                </div>
                <div class="form-check">
                    <asp:RadioButton ID="rbDevuelto" runat="server" GroupName="EstadoPrestamo" CssClass="form-check-input" />
                    <label class="form-check-label" for="rbDevuelto">Devuelto</label>
                </div>
            </div>

            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success me-2" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnCancelar_Click" />
        </asp:Panel>
    </div>
</asp:Content>
