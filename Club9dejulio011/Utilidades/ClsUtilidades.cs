using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Utilidades
{
    public class ClsUtilidades
    {
        private string _mensajeError;
        private List<TextBox> _lstTxtBox;

        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public List<TextBox> LstTxtBox { get => _lstTxtBox; set => _lstTxtBox = value; }



        public void FormatoDataGridView(ref DataGridView Dgv)
        {
            DataGridViewCellStyle estilo = Dgv.ColumnHeadersDefaultCellStyle;
            estilo.Alignment = DataGridViewContentAlignment.MiddleCenter;
            estilo.Font = new Font(Dgv.Font, FontStyle.Bold);
            Dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            Dgv.AllowUserToAddRows = false;
            Dgv.AllowUserToDeleteRows = false;
            Dgv.ReadOnly = true;
        }

        public void ValidarTexbox(List<TextBox> LstTxtBox)
        {
            MensajeError = null;

            foreach(TextBox txt in LstTxtBox)
            {
                if (txt.Text.Equals(string.Empty))
                {
                    MensajeError = MensajeError + "\n" + txt.Name.Remove(0,3) + "No puede estar en blanco";
                }
            }
        }
    }
}
