using System;
using System.Data;
using System.Windows.Forms;
using Entidades.Usuarios;
using LogicaNegocio.Usuarios;

namespace Presentacion.Principal
{
    public partial class FrmLogin : Form
    {
        ClsUsuario objeuser = new ClsUsuario();
        
        ClsUsuarioLn objnuser =new ClsUsuarioLn();
        
        FrmPrincipal frm1 = new FrmPrincipal();

        public static string nombre_usu;
        public static string area;
            
        void P_logueo()
        {
            DataTable dt = new DataTable();
            objeuser.Nombre = txtUsu.Text;
            objeuser.Password = txtPass.Text;



            dt = objnuser.LogicaNegocio(objeuser);
            
            
                

            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("Bienvenido" + dt.Rows[0][1].ToString(),"Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nombre_usu = dt.Rows[0][1].ToString();
                area = dt.Rows[0][0].ToString();

                frm1.ShowDialog();

                FrmLogin login = new FrmLogin();
                login.ShowDialog();

                if (login.DialogResult == DialogResult.OK)
                    Application.Run(new FrmPrincipal());

                txtUsu.Clear();
                txtPass.Clear();

            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
             
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            P_logueo();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
