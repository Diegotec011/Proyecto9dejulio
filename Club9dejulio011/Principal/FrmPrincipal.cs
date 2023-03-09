using System;
using System.Data;
using System.Windows.Forms;
using Presentacion.Principal;


namespace Presentacion
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }



        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //Presidencia
            if (FrmLogin.area == "2")
            {

                
                    btn1Socios.Enabled = true;
                    btn2PagoCuotas.Enabled = true;
                    btn3AlqDeCanchas.Enabled = true;
                    btn4Stock.Enabled = true;
                    btn5Agenda.Enabled = true;
                    btn6Personal.Enabled = true;
                    btn7Usuarios.Enabled = true;

                    txtArea.Text = "Presidencia";
                
            }
            //Comision Directiva
            else if(FrmLogin.area == "1002")
            {
                btn1Socios.Enabled = false;
                btn2PagoCuotas.Enabled = false;
                btn3AlqDeCanchas.Enabled = false;
                btn4Stock.Enabled = true;
                btn5Agenda.Enabled = false;
                btn6Personal.Enabled = true;
                btn7Usuarios.Enabled = false;

                txtArea.Text = "Comision Directiva";
            }
            //Administracion
            else if (FrmLogin.area == "5")
            {
                btn1Socios.Enabled = true;
                btn2PagoCuotas.Enabled = false;
                btn3AlqDeCanchas.Enabled = false;
                btn4Stock.Enabled = false;
                btn5Agenda.Enabled = true;
                btn6Personal.Enabled = false;
                btn7Usuarios.Enabled = false;

                txtArea.Text = "Administracion";
            }
            //Tesoreria
            else if (FrmLogin.area == "6")
            {
                btn1Socios.Enabled = true;
                btn2PagoCuotas.Enabled = true;
                btn3AlqDeCanchas.Enabled = false;
                btn4Stock.Enabled = false;
                btn5Agenda.Enabled = false;
                btn6Personal.Enabled = false;
                btn7Usuarios.Enabled = false;

                txtArea.Text = "Tesoreria";
            }

            txtUsuario.Text = FrmLogin.nombre_usu;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
    }
}