using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using FinalPro1.Models;



namespace FinalPro1.Repository
{
    public class ProductoVendidoHandler : DbHandler
    {
        public static List<ProductoVendido> MostrarProductosVendidos(int IdUsuario)
        {
          
            DataTable ttable = new DataTable();

            List<ProductoVendido> Listproductos = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT p.IdUsuario, pv.IdProducto, p.Descripciones, pv.Stock, p.PrecioVenta " +
                    "FROM ProductoVendido pv join Producto p on pv.IdProducto = p.Id " +
                    "WHERE p.IdUsuario = @IdUsuario", sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    SqlDataAdapter SqlAdapter = new SqlDataAdapter();
                    SqlAdapter.SelectCommand = sqlCommand;
                    SqlAdapter.Fill(ttable);

                    foreach (DataRow line in ttable.Rows)
                    {
                        ProductoVendido _producto = new ProductoVendido();
                        _producto.IdUsuario = Convert.ToInt32(line["IdUsuario"]);
                        _producto.IdProducto = Convert.ToInt32(line["IdProducto"]);
                        _producto.Descripciones = line["Descripciones"].ToString();
                        _producto.Stock = Convert.ToInt32(line["Stock"]);
                        _producto.PrecioVenta = Convert.ToDouble(line["PrecioVenta"]);
                        Listproductos.Add(_producto);
                    }
                    sqlConnection.Close();
                }
            }
            return Listproductos;
        }


    }
}
