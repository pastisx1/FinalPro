using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using FinalPro1.Models;



namespace FinalPro1.Repository
{
    public class VentaHandler : DbHandler
    {
        List<Venta> ListVentas = new List<Venta>();
        DataTable ttable = new DataTable();

        public string Value { get; private set; }

        //da total de las ventas
        public static List<Venta> FullVentas(int IDUsuario)
        {

            List<Venta> ListVentas = new List<Venta>();
            DataTable ttable = new DataTable();


            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select p.IdUsuario, pv.IdProducto, p.Descripciones, pv.Stock, p.PrecioVenta " +
                    "from ProductoVendido pv join Producto p on pv.IdProducto = p.Id " +
                    "where p.IdUsuario = @IDUsuario", sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@IDUsuario", IDUsuario);
                    SqlDataAdapter SqlAdapter = new SqlDataAdapter();
                    SqlAdapter.SelectCommand = sqlCommand;
                    SqlAdapter.Fill(ttable);

                    foreach (DataRow line in ttable.Rows)
                    {
                        Venta _venta = new Venta();
                        _venta.IdUsuario = Convert.ToInt32(line["IdUsuario"]);
                        _venta.IdProducto = Convert.ToInt32(line["IdProducto"]);
                        _venta.Descripciones = line["Descripciones"].ToString();
                        _venta.Stock = Convert.ToInt32(line["Stock"]);
                        _venta.PrecioVenta = Convert.ToInt32(line["PrecioVenta"]);
                        _venta.Venta_v = _venta.Stock * _venta.PrecioVenta;
                        ListVentas.Add(_venta);
                    }
                    sqlConnection.Close();
                }
            }
            return ListVentas;
        }

        //borrar ventas
            public static string DeleteVenta(int IdVenta)
        {
            string result = String.Empty;
            int BorraVenta;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string QueryDeleteVenta = "DELETE FROM Venta WHERE Id = @IDVenta";
                SqlParameter param_IDVenta = new SqlParameter("IDVenta", System.Data.SqlDbType.BigInt) { Value = IdVenta };
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(QueryDeleteVenta, sqlConnection))
                {
                    sqlCommand.Parameters.Add(param_IDVenta);
                    BorraVenta = sqlCommand.ExecuteNonQuery();
                }
                if (BorraVenta == 1)
                {
                    result = "El Id de venta: '" + IdVenta + "' fue eliminado.";
                }
                else
                {
                    result = "El Id de venta: '" + IdVenta + "' quedo sin modificar.";
                }
                sqlConnection.Close();
            }
            return result;

        }


        //crear ventas
        public string CrearVentas(int IdProducto, int IdUsuario)
        {
            string result = String.Empty;
            int CreaV = 0;
            
            DataTable tIdVenta = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))

            {
                string QAVenta = "INSERT INTO Venta ( Comentarios ) VALUES ( @Comentarios )";

                SqlParameter _Comentarios = new SqlParameter("Comentarios", SqlDbType.VarChar);
                Value = "La Venta se cargo - Fecha: " + DateTime.Now + " - Producto: " + IdProducto + " - Vendedor: " + IdUsuario;

                sqlConnection.Open();
            
            
               using (SqlCommand sqlCommand = new SqlCommand(QAVenta, sqlConnection))
               {
                  sqlCommand.Parameters.Add(_Comentarios);
                  CreaV = sqlCommand.ExecuteNonQuery();
               }
                    if (CreaV == 1)
                    {
                        result = "Exitoso";
                    }
                    else
                    {
                        result = "Venta No Creada - Error al Crear";
                    }
              
                sqlConnection.Close();
            }
            
          
            return result;

                
        }


    }

      
}
