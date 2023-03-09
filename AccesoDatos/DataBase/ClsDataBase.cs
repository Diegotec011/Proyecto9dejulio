using System;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Database
{
    public class ClsDataBase
    {

        #region Variables privadas
        
        private SqlConnection _objsqlConnection;
        private SqlDataAdapter _objsqlDataAdapter;
        private SqlCommand _objsqlCommand;
        private DataSet _dsResultados;
        private DataTable _dtParametros;
        private string _nombreTabla, _nombreSP, _mensajeErrorDB, _valorScalar, _nombreDB;
        private bool _scalar;
        
        #endregion


        #region Variables publicas

        public SqlConnection ObjsqlConnection { get => _objsqlConnection; set => _objsqlConnection = value; }
        public SqlDataAdapter ObjsqlDataAdapter { get => _objsqlDataAdapter; set => _objsqlDataAdapter = value; }
        public SqlCommand ObjsqlCommand { get => _objsqlCommand; set => _objsqlCommand = value; }
        public DataSet DsResultados { get => _dsResultados; set => _dsResultados = value; }
        public DataTable DtParametros { get => _dtParametros; set => _dtParametros = value; }
        public string NombreTabla { get => _nombreTabla; set => _nombreTabla = value; }
        public string NombreSP { get => _nombreSP; set => _nombreSP = value; }
        public string MensajeErrorDB { get => _mensajeErrorDB; set => _mensajeErrorDB = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public string NombreDB { get => _nombreDB; set => _nombreDB = value; }
        public bool Scalar { get => _scalar; set => _scalar = value; }

        #endregion

        #region Constructores

        public ClsDataBase()
        {
            DtParametros = new DataTable("SpParametros");
            DtParametros.Columns.Add("Nombre");
            DtParametros.Columns.Add("TipoDato");
            DtParametros.Columns.Add("Valor");

            NombreDB = "Club9dejulio";
        }

        #endregion

        #region Metodos privados

        private void CrearConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            switch(ObjDataBase.NombreDB )
            {
                case "Club9dejulio":
                    
                    ObjDataBase.ObjsqlConnection = new SqlConnection(Properties.Settings.Default.cadenaConeccionBase9dejulio);
                    
                break;

                   default:
                
                break;
                    
            }
        }
        private void ValidarConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            if(ObjDataBase.ObjsqlConnection.State == ConnectionState.Closed)
            {
                ObjDataBase.ObjsqlConnection.Open();
            }
            else
            {
                ObjDataBase.ObjsqlConnection.Close();
                ObjDataBase.ObjsqlConnection.Dispose();
            }
        }
        private void AgregarParametros(ref ClsDataBase ObjDataBase)
        {
            if(ObjDataBase.DtParametros != null)
            {
                SqlDbType TipoDatoSQL = new SqlDbType();

                foreach ( DataRow item in ObjDataBase.DtParametros.Rows)
                {
                    switch(item[1])
                    {
                        case "1":
                            TipoDatoSQL = SqlDbType.Bit;
                        break;

                        case "2":
                            TipoDatoSQL = SqlDbType.TinyInt;
                            break;
                        case "3":
                            TipoDatoSQL = SqlDbType.SmallInt;
                            break;
                        case "4":
                            TipoDatoSQL = SqlDbType.Int;
                            break;
                        case "5":
                            TipoDatoSQL = SqlDbType.BigInt;
                            break;
                        case "6":
                            TipoDatoSQL = SqlDbType.Decimal;
                            break;
                        case "7":
                            TipoDatoSQL = SqlDbType.SmallMoney;
                            break;
                        case "8":
                            TipoDatoSQL = SqlDbType.Money;
                            break;
                        case "9":
                            TipoDatoSQL = SqlDbType.Float;
                            break;
                        case "10":
                            TipoDatoSQL = SqlDbType.Real;
                            break;
                        case "11":
                            TipoDatoSQL = SqlDbType.Date;
                            break;
                        case "12":
                            TipoDatoSQL = SqlDbType.Time;
                            break;
                        case "13":
                            TipoDatoSQL = SqlDbType.SmallDateTime;
                            break;
                        case "14":
                            TipoDatoSQL = SqlDbType.Char;
                            break;
                        case "15":
                            TipoDatoSQL = SqlDbType.NChar;
                            break;
                        case "16":
                            TipoDatoSQL = SqlDbType.VarChar;
                            break;
                        case "17":
                            TipoDatoSQL = SqlDbType.NVarChar;
                            break;
                        
                        default:

                            break;

                    }

                    if (ObjDataBase.Scalar)
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            ObjDataBase.ObjsqlCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = DBNull.Value;
                        }
                        else
                        {
                            ObjDataBase.ObjsqlCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = item[2].ToString();
                        }
                    }
                    else
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            ObjDataBase.ObjsqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = DBNull.Value;
                        }
                        else
                        {
                            ObjDataBase.ObjsqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = item[2].ToString();
                        }
                    }
                }
            }
        }
        private void PrepararConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            CrearConexionBaseDatos(ref ObjDataBase);
            ValidarConexionBaseDatos(ref ObjDataBase);

        }

        private void EjecutarDataAdapter(ref ClsDataBase ObjDataBase)
        {
            try
            {
                PrepararConexionBaseDatos(ref ObjDataBase);
                ObjDataBase.ObjsqlDataAdapter = new SqlDataAdapter(ObjDataBase.NombreSP, ObjDataBase.ObjsqlConnection);
                ObjDataBase.ObjsqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AgregarParametros(ref ObjDataBase);
                ObjDataBase.DsResultados = new DataSet();
                ObjDataBase.ObjsqlDataAdapter.Fill(ObjDataBase.DsResultados,ObjDataBase.NombreTabla);
                
            }
            catch (Exception ex)
            {

                ObjDataBase.MensajeErrorDB = ex.Message.ToString();
            }
            finally
            {
                if(ObjDataBase.ObjsqlConnection.State == ConnectionState.Open)
                {
                    ValidarConexionBaseDatos(ref ObjDataBase);
                }
            }
        }
        private void EjecutarCommand(ref ClsDataBase ObjDataBase)
        {
            try
            {
                PrepararConexionBaseDatos(ref ObjDataBase);
                ObjDataBase.ObjsqlCommand = new SqlCommand(ObjDataBase.NombreSP, ObjDataBase.ObjsqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                AgregarParametros(ref ObjDataBase);

                if (ObjDataBase.Scalar)
                {
                    ObjDataBase.ValorScalar = ObjDataBase.ObjsqlCommand.ExecuteScalar().ToString().Trim();

                }
                else 
                {
                    ObjDataBase.ObjsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ObjDataBase.MensajeErrorDB = ex.Message.ToString();
                
            }
            finally
            {
                if (ObjDataBase.ObjsqlConnection.State == ConnectionState.Open)
                {
                    ValidarConexionBaseDatos(ref ObjDataBase);
                }
            }
        }
        #endregion


        #region Metodos publicos

        public void CRUD(ref ClsDataBase ObjDataBase)
        {
            if (ObjDataBase.Scalar)
            {
                EjecutarCommand(ref ObjDataBase);
            }
            else
            {
                EjecutarDataAdapter(ref ObjDataBase);
            }
        }

        #endregion







    }
}
