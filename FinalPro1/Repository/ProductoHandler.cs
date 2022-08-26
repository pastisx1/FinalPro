using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using FinalPro1.Models;
using FinalPro1.Controllers.DTOS;


namespace FinalPro1.Repository
{
    public class ProductoHandler : DbHandler
    {
        //Mostrar Productos
        public static List<Producto> GetProductos()
        {

            List<Producto> resultado = new List<Producto>();

            using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))

            {
                using (SqlCommand sqlcommand = new SqlCommand())

                {

                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.Connection.Open();
                    sqlcommand.CommandText = "SELECT * FROM Producto";

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                    sqlDataAdapter.SelectCommand = sqlcommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlcommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(row["Id"]);
                        producto.Descripciones = row["Descripciones"].ToString();
                        producto.Costo = Convert.ToInt32(row["Costo"]);
                        producto.PrecioVenta = Convert.ToInt32(row["PrecioVenta"]);
                        producto.Stock = Convert.ToInt32(row["Stock"]);
                        producto.IdUsuario = Convert.ToInt32(row["IdUsuario"]);

                        resultado.Add(producto);



                    }



                }


            }

            return resultado;

        }

        //Modificar Producto
        public string ModificarProducto(Producto m_producto)
        {

            string Result = String.Empty;
            int _update = 0;


            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string QueryUpdate = "UPDATE Producto SET Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, Stock = @Stock, IdUsuario = @IdUsuario WHERE Id = @IdProducto";


                SqlParameter _IdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt) { Value = m_producto.Id };
                SqlParameter _Descripciones = new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = m_producto.Descripciones };
                SqlParameter _Costo = new SqlParameter("Costo", System.Data.SqlDbType.Decimal) { Value = m_producto.Costo };
                SqlParameter _PrecioVenta = new SqlParameter("PrecioVenta", System.Data.SqlDbType.Decimal) { Value = m_producto.PrecioVenta };
                SqlParameter _Stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = m_producto.Stock };
                SqlParameter _IdUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.BigInt) { Value = m_producto.IdUsuario };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(QueryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(_IdProducto);
                    sqlCommand.Parameters.Add(_Descripciones);
                    sqlCommand.Parameters.Add(_Costo);
                    sqlCommand.Parameters.Add(_PrecioVenta);
                    sqlCommand.Parameters.Add(_Stock);
                    sqlCommand.Parameters.Add(_IdUsuario);
                    _update = sqlCommand.ExecuteNonQuery();
                }
                if (_update == 1)
                {
                    Result = "Cambio el Id de Producto a: " + m_producto.Id;
                }
                else
                {
                    Result = "El Id de Producto no cambió sigue siendo : " + m_producto.Id;
                }
                sqlConnection.Close();
            }
            return Result;



        }

        //Eliminar Producto
        public string DeleteProducto(int IdProducto)
        {

            string Result = String.Empty;
            int supr = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter IdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt) { Value = IdProducto };


                string QueryDelpvendido = "DELETE FROM ProductoVendido WHERE IdProducto = @IdProducto";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(QueryDelpvendido, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdProducto);
                    supr = sqlCommand.ExecuteNonQuery();

                }
                sqlConnection.Close();

                string DelProducto = "DELETE FROM Producto WHERE Id = @IdProducto";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DelProducto, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdProducto);
                    supr = sqlCommand.ExecuteNonQuery();
                }
                if (supr == 1)
                {
                    Result = "El m_producto: '" + IdProducto + "' se borró.";
                }
                else
                {
                    Result = "El m_producto: '" + IdProducto + "' no fue modificado.";
                }
                sqlConnection.Close();

            }
        

            return Result;
        }


        //Crear Producto

        public string CrearProducto(PostProducto Producto)
        {
            string result = string.Empty;
            int pcreados = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string QCreap = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";

                //Parámetros
                SqlParameter _Descripciones = new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = Producto.Descripciones };
                SqlParameter _Costo = new SqlParameter("Costo", SqlDbType.Decimal) { Value = Producto.Costo };
                SqlParameter _PrecioVenta = new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = Producto.PrecioVenta };
                SqlParameter _Stock = new SqlParameter("Stock", SqlDbType.Int) { Value = Producto.Stock };
                SqlParameter _IdUsuario = new SqlParameter("IdUsuario", SqlDbType.Int) { Value = Producto.IdUsuario };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(QCreap, sqlConnection))
                {
                    sqlCommand.Parameters.Add(_Descripciones);
                    sqlCommand.Parameters.Add(_Costo);
                    sqlCommand.Parameters.Add(_PrecioVenta);
                    sqlCommand.Parameters.Add(_Stock);
                    sqlCommand.Parameters.Add(_IdUsuario);
                    pcreados = sqlCommand.ExecuteNonQuery();

                }
                if (pcreados == 1)
                {
                    result = "Producto: '" + Producto.Descripciones + "' creado con éxito.";
                }
                else
                {
                    result = "Producto: '" + Producto.Descripciones + "' esta vacío.";
                }
                sqlConnection.Close();



            }
            return result;
        }


        

    }

}
