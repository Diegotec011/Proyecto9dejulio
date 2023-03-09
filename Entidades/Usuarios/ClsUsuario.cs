using System.Data;

namespace Entidades.Usuarios
{
    public class ClsUsuario
    {
        #region Atributos privados

        private int _idUsuario;
        private string _nombre, _password;
        private int _usuarioId;

        private string _mensajeError, _valoScalar;
        private DataTable _dtResultados;
        
        #endregion

        #region Atributos publicos
        
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Password { get => _password; set => _password = value; }
        public int UsuarioId { get => _usuarioId; set => _usuarioId = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValoScalar { get => _valoScalar; set => _valoScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }

        #endregion


    }
}
