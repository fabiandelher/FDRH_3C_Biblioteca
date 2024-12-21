using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using VO;

namespace FDRH_3C_Biblioteca.Gestion
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        private BLL_General bll = new BLL_General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLibrosDisponibles();
                string usuarioID = Request.QueryString["UsuarioID"];
                if (!string.IsNullOrEmpty(usuarioID))
                {
                    CargarUsuario(Convert.ToInt32(usuarioID));
                }
            }
        }

        private void CargarUsuario(int usuarioID)
        {
            try
            {
                DatosGeneralesVO datos = bll.ObtenerDatosPorID(usuarioID); // Este método debe ser implementado en la BLL.
                if (datos != null)
                {
                    hdnUsuarioID.Value = datos.UsuarioID.ToString();
                    txtNombreUsuario.Text = datos.UsuarioNombre;
                    txtUsuarioCorreo.Text = datos.UsuarioCorreo;
                    txtUsuarioTelefono.Text = datos.UsuarioTelefono;
                    ddlLibrosDisponibles.SelectedValue = datos.LibroID.ToString();
                    txtAutor.Text = datos.AutorNombre; 
                    txtCategoria.Text = datos.CategoriaNombre; 
                    rbPrestado.Checked = datos.PrestamoEstado == "Prestado";
                    rbDevuelto.Checked = datos.PrestamoEstado == "Devuelto";
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", $"Error al cargar usuario: {ex.Message}", "error");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLibrosDisponibles.SelectedValue == "0")
                {
                    MostrarAlerta("Advertencia", "Por favor, seleccione un libro válido.", "warning");
                    return;
                }
                // Obtener el libro seleccionado
                int libroID = Convert.ToInt32(ddlLibrosDisponibles.SelectedValue);
                var libroSeleccionado = bll.ObtenerLibroPorID(libroID); // Asegúrate de implementar esto en la BLL
                DatosGeneralesVO datos = new DatosGeneralesVO
                {
                    UsuarioID = string.IsNullOrEmpty(hdnUsuarioID.Value) ? 0 : Convert.ToInt32(hdnUsuarioID.Value),
                    UsuarioNombre = txtNombreUsuario.Text,
                    UsuarioCorreo = txtUsuarioCorreo.Text,
                    UsuarioTelefono = txtUsuarioTelefono.Text,
                    LibroID = Convert.ToInt32(ddlLibrosDisponibles.SelectedValue),
                    PrestamoEstado = rbPrestado.Checked ? "Prestado" : "Devuelto",
                    AutorNombre = libroSeleccionado.AutorNombre,
                    CategoriaNombre = libroSeleccionado.CategoriaNombre
                };
                if (datos.UsuarioID == 0)
                {
                    bll.GuardarDatosConsolidados(datos);
                    MostrarAlerta("Éxito", "Registro completado correctamente.", "success");
                    Response.Redirect("GestionDatos.aspx");
                }
                else
                {
                    bll.ActualizarDatos(datos); // Método para actualización
                    MostrarAlerta("Éxito", "Datos actualizados correctamente.", "success");
                    Response.Redirect("GestionDatos.aspx");
                }
                 
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", $"Error al guardar los datos: {ex.Message}", "error");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionDatos.aspx");
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // Tipo puede ser: success, error, warning, info
            string script = $"Swal.fire('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        private void CargarLibrosDisponibles()
        {
            try
            {
                var librosDisponibles = bll.ObtenerLibrosConDetalles();
                ddlLibrosDisponibles.DataSource = librosDisponibles;
                ddlLibrosDisponibles.DataTextField = "Titulo";
                ddlLibrosDisponibles.DataValueField = "LibroID";
                ddlLibrosDisponibles.DataBind();

                // Agregar opción predeterminada
                ddlLibrosDisponibles.Items.Insert(0, new ListItem("Seleccione un libro", "0"));

                // Guardar autor y categoría en ViewState para uso posterior
                ViewState["LibrosDetalles"] = librosDisponibles;
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", $"Error al cargar libros: {ex.Message}", "error");
            }
        }
        protected void ddlLibrosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int libroID = Convert.ToInt32(ddlLibrosDisponibles.SelectedValue);

                if (libroID > 0)
                {
                    var libro = bll.ObtenerLibroPorID(libroID); // Obtener el libro seleccionado
                    if (libro != null)
                    {
                        txtAutor.Text = libro.AutorNombre;
                        txtCategoria.Text = libro.CategoriaNombre;
                    }
                }
                else
                {
                    txtAutor.Text = string.Empty;
                    txtCategoria.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", $"Error al obtener libros: {ex.Message}", "error");
            }
        }



    }
}
