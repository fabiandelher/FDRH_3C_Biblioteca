<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionDatos.aspx.cs" Inherits="FDRH_3C_Biblioteca.Gestion.GestionDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Gestión de Datos Consolidados</h2>

        <asp:Button ID="btnAbrirRegistrar" runat="server" CssClass="btn btn-primary mb-3" Text="Registrar Nuevo" OnClick="btnAbrirRegistrar_Click" />

        <asp:GridView ID="GridViewDatos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewDatos_RowCommand">
            <Columns>

                <asp:BoundField DataField="UsuarioID" HeaderText="ID Usuario" />
                <asp:BoundField DataField="UsuarioNombre" HeaderText="Nombre Usuario" />
                <asp:BoundField DataField="LibroTitulo" HeaderText="Título del Libro" />
                <asp:BoundField DataField="AutorNombre" HeaderText="Autor" />
                <asp:BoundField DataField="CategoriaNombre" HeaderText="Categoría" />
                <asp:BoundField DataField="FechaPrestamo" HeaderText="Fecha Préstamo" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="PrestamoEstado" HeaderText="Estado del Préstamo" />


                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>

                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("UsuarioID") %>' CssClass="btn btn-primary btn-sm">
            <i class="bi bi-pencil"></i> Editar
                        </asp:LinkButton>


                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("UsuarioID") %>' CssClass="btn btn-danger btn-sm">
            <i class="bi bi-trash"></i> Eliminar
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="PanelCRUD" runat="server" CssClass="p-4 border" Visible="False">
            <h5>Formulario de Datos</h5>
            <asp:HiddenField ID="hdnRegistroID" runat="server" />

            <div class="mb-3">
                <label for="txtNombreUsuario" class="form-label">Nombre Usuario</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtTituloLibro" class="form-label">Título del Libro</label>
                <asp:TextBox ID="txtTituloLibro" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success me-2" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnCancelar_Click" />
        </asp:Panel>
    </div>
</asp:Content>
