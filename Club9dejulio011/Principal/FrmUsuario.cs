using System;
using System.Drawing;
using System.Windows.Forms;
using Entidades.Usuarios;
using LogicaNegocio.Usuarios;
using Presentacion.Utilidades;

namespace Presentacion.Principal
{
    public partial class FrmUsuario : Form
    {
        private ClsUsuario ObjUsuario = null;
        private readonly ClsUsuarioLn ObjUsuarioLn = new ClsUsuarioLn();
        private ClsUtilidades ObjUtilidades = new ClsUtilidades();

        public FrmUsuario()
        {
            InitializeComponent();
            CargarListaUsuarios();
        }

        private void ValidarCampos()
        {
            ObjUtilidades = new ClsUtilidades()
            {
                LstTxtBox = new System.Collections.Generic.List<TextBox>()

            };
            ObjUtilidades.LstTxtBox.Add(txtNombre);
            ObjUtilidades.LstTxtBox.Add(txtPassword);
            ObjUtilidades.LstTxtBox.Add(txtIdRol);

            ObjUtilidades.ValidarTexbox(ObjUtilidades.LstTxtBox);
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtIdRol.Text = string.Empty;
            LblUsuario.Text = string.Empty;
        }
        private void CargarListaUsuarios()
        {
            ObjUsuario = new ClsUsuario();
            ObjUsuarioLn.Index(ref ObjUsuario);
            if(ObjUsuario.MensajeError == null)
            {
                dgvUsuarios.DataSource = ObjUsuario.DtResultados;
                ObjUtilidades.FormatoDataGridView(ref dgvUsuarios);
                dgvUsuarios.Columns[0].DisplayIndex =dgvUsuarios.ColumnCount -1;
            }
            else
            {
                MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, System.EventArgs e)
        {
            ValidarCampos();

            if(ObjUtilidades.MensajeError == null)
            {
                ObjUsuario = new ClsUsuario()
                {
                    Nombre = txtNombre.Text,
                    Password = txtPassword.Text,
                    UsuarioId = Convert.ToInt32(txtIdRol.Text)

                };
                ObjUsuarioLn.Create(ref ObjUsuario);

                if (ObjUsuario.MensajeError == null)
                {
                    MessageBox.Show("El ID:" + ObjUsuario.ValoScalar + ", fue agregado correctamente");
                    CargarListaUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(ObjUtilidades.MensajeError.ToString(), "Mensaje Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            DialogResult respuesta = MessageBox.Show("¿Esta seguro que desea actualizar el registro" + LblUsuario.Text + "?", "Mensaje del sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (respuesta == DialogResult.OK)
            {
                ObjUsuario = new ClsUsuario()
                {
                    IdUsuario = Convert.ToInt32(LblUsuario.Text),
                    Nombre = txtNombre.Text,
                    Password = txtPassword.Text,
                    UsuarioId = Convert.ToInt32(txtIdRol.Text)

                };

                ObjUsuarioLn.Update(ref ObjUsuario);

                if (ObjUsuario.MensajeError == null)
                {
                    MessageBox.Show("El Id: " + ObjUsuario.ValoScalar + ", fue actualizado correctamente");
                    CargarListaUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(ObjUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(dgvUsuarios.Columns[e.ColumnIndex].Name == "Editar")
                {
                    ObjUsuario = new ClsUsuario()
                    {
                        IdUsuario = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["ID_Usuario"].Value.ToString())

                    };

                    LblUsuario.Text = ObjUsuario.IdUsuario.ToString();

                    ObjUsuarioLn.Read(ref ObjUsuario);
                    txtNombre.Text = ObjUsuario.Nombre;
                    txtPassword.Text = ObjUsuario.Password;
                    txtIdRol.Text = Convert.ToString(ObjUsuario.UsuarioId);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            DialogResult respuesta = MessageBox.Show("¿Esta seguro que desea eliminar el registro"+LblUsuario.Text+"?","Mensaje del sistema",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            
            if(respuesta == DialogResult.OK)
            {
                ObjUsuario = new ClsUsuario()
                {
                    IdUsuario = Convert.ToInt32(LblUsuario.Text)

                };

                ObjUsuarioLn.Delete(ref ObjUsuario);

                CargarListaUsuarios();
                LimpiarCampos();
            }
            
            
            
            
            
           
        }
    }
}
