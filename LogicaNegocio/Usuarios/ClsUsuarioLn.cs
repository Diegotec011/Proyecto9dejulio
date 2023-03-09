using System;
using System.Data;
using AccesoDatos.Database;
using Entidades.Usuarios;

namespace LogicaNegocio.Usuarios
{
    public class ClsUsuarioLn
    {

        #region Variables privadas

         private ClsDataBase ObjDataBase = null;

        #endregion

        #region Metodo Logear

        public void Login(ref ClsUsuario ObjUsuario)
        {

            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "USUARIOS",
                NombreSP = "[SP_Logueo]",
                Scalar = false,

            };


            ObjDataBase.DtParametros.Rows.Add(@"@Nombre", "17", ObjUsuario.Nombre);
            ObjDataBase.DtParametros.Rows.Add(@"@Password", "17", ObjUsuario.Password);
            

            Ejecutar(ref ObjUsuario);
        }

        public DataTable LogicaNegocio(ClsUsuario objeuser)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Metodo index

        public void Index(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "USUARIOS",
                NombreSP = "[SP_Usuarios_Index]",
                Scalar = false

            };
            Ejecutar(ref ObjUsuario);
        }

        #endregion


        #region CRUD Usuarios

        public void Create(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "USUARIOS",
                NombreSP = "[SP_Usuarios_Create]",
                Scalar = true

            };
           
           
            ObjDataBase.DtParametros.Rows.Add(@"@Nombre", "17", ObjUsuario.Nombre);
            ObjDataBase.DtParametros.Rows.Add(@"@Password", "17", ObjUsuario.Password);
            ObjDataBase.DtParametros.Rows.Add(@"@Usuario_IDRol", "4", ObjUsuario.UsuarioId);

            Ejecutar(ref ObjUsuario);
        }

        public void Read(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "USUARIOS",
                NombreSP = "[SP_Usuarios_Read]",
                Scalar = false

            };
            ObjDataBase.DtParametros.Rows.Add(@"@Id_Usuario", "4", ObjUsuario.IdUsuario);
            Ejecutar(ref ObjUsuario);
        }

        public void Update(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "USUARIOS",
                NombreSP = "[SP_Usuarios_Update]",
                Scalar = true

            };
            ObjDataBase.DtParametros.Rows.Add(@"@Id_Usuario", "4", ObjUsuario.IdUsuario);
            ObjDataBase.DtParametros.Rows.Add(@"@Nombre", "17", ObjUsuario.Nombre);
            ObjDataBase.DtParametros.Rows.Add(@"@Password", "17", ObjUsuario.Password);
            ObjDataBase.DtParametros.Rows.Add(@"@Usuario_IDRol", "4", ObjUsuario.UsuarioId);
            Ejecutar(ref ObjUsuario);
        }

        public void Delete(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase = new ClsDataBase()
            {
                NombreTabla = "USUARIOS",
                NombreSP = "[SP_Usuarios_Delete]",
                Scalar = true

            };
            ObjDataBase.DtParametros.Rows.Add(@"@Id_Usuario", "4", ObjUsuario.IdUsuario);
            Ejecutar(ref ObjUsuario);
        }

        #endregion

        #region Metodos privados

        private void Ejecutar(ref ClsUsuario ObjUsuario)
        {
            ObjDataBase.CRUD(ref ObjDataBase);

            if(ObjDataBase.MensajeErrorDB == null)
            {
                if (ObjDataBase.Scalar)
                {
                    ObjUsuario.ValoScalar = ObjDataBase.ValorScalar;
                }
                else
                {
                    ObjUsuario.DtResultados = ObjDataBase.DsResultados.Tables[0];
                    if (ObjUsuario.DtResultados.Rows.Count == 1)
                    {
                        foreach (DataRow item in ObjUsuario.DtResultados.Rows)
                        {
                            ObjUsuario.IdUsuario = Convert.ToInt32(item["ID_Usuario"].ToString());
                            ObjUsuario.Nombre = (item["Nombre"].ToString());
                            ObjUsuario.Password = (item["Password"].ToString());
                            ObjUsuario.UsuarioId = Convert.ToInt32(item["Usuario_IDRol"].ToString());
                        }
                    }
                }
            }
            else 
            {
                ObjUsuario.MensajeError = ObjDataBase.MensajeErrorDB;
            }
        }

        #endregion
    }
}
