using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using VO;

namespace FDRH_3C_Biblioteca.Gestion
{
    public partial class GestionDatos : System.Web.UI.Page
    {
        private BLL_General bll;

        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new BLL_General();

            if (!IsPostBack)

            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
                CargarDatos();
            }
        }

        /// <summary>
        /// Método para cargar los datos en el GridView.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                // Obtiene los datos consolidados desde la BLL
                List<DatosGeneralesVO> datos = bll.ObtenerDatosConsolidados();

                // Asignar los datos al GridView
                GridViewDatos.DataSource = datos;
                GridViewDatos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar los datos: {ex.Message}", "error");
            }
        }
        protected void btnAbrirRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroUsuario.aspx");
        }

        /// <summary>
        /// Botón para guardar o actualizar registros.
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo objeto de datos
                DatosGeneralesVO datos = new DatosGeneralesVO
                {
                    UsuarioNombre = txtNombreUsuario.Text,
                    LibroTitulo = txtTituloLibro.Text
                };

                // Validar los datos antes de procesarlos
                bll.ValidarDatosGenerales(datos);

                if (string.IsNullOrEmpty(hdnRegistroID.Value))
                {
                    // Insertar registro
                    bll.InsertarDatos(datos);
                    MostrarMensaje("Registro insertado correctamente.", "success");
                }
                else
                {
                    // Actualizar registro
                    datos.UsuarioID = Convert.ToInt32(hdnRegistroID.Value);
                    bll.ActualizarDatos(datos);
                    MostrarMensaje("Registro actualizado correctamente.", "success");
                }

                // Recargar los datos en el GridView
                CargarDatos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar los datos: {ex.Message}", "error");
            }
        }

        private void SweetAlertmg(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            PanelCRUD.Visible = false;
        }
        /// <summary>
        /// Evento del GridView para manejar acciones de edición y eliminación.
        /// </summary>
        protected void GridViewDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int usuarioID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"RegistroUsuario.aspx?UsuarioID={usuarioID}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    int usuarioID = Convert.ToInt32(e.CommandArgument);
                    bll.EliminarDatos(usuarioID);
                    MostrarMensaje("Registro eliminado correctamente.", "success");
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar el registro: {ex.Message}", "error");
                }
            }
        }


        /// <summary>
        /// Método para mostrar mensajes al usuario.
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar</param>
        private void MostrarMensaje(string mensaje, string tipo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert",
        $"Swal.fire({{title: '{mensaje}', icon: '{tipo}', confirmButtonText: 'Aceptar'}});", true);
        }
        

        /// <summary>
        /// Método para limpiar el formulario.
        /// </summary>
        private void LimpiarFormulario()
        {
            hdnRegistroID.Value = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtTituloLibro.Text = string.Empty;
        }
    }
}