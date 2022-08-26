using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using FinalPro1.Models;

namespace FinalPro1.Repository
{
    public class UsuarioHandler : DbHandler

    {
        //Conseguir Usuarios
        public static List<Usuario> GetUsuarios(String NombreUsuario)

        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(NombreUsuario))
            { 
            using (SqlCommand sqlCommand= new SqlCommand("SELECT * FROM Usuario", sqlConnection))
                { 

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        //se fija si hay filas
                        if (dataReader.HasRows)

                        {
                            while (dataReader.Read())
                            { 
                            
                            Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                                
                                resultados.Add(usuario);


                            }
                        
                                                
                        }                                         
                                        
                    
                    }
                    
                    sqlConnection.Close();
                }
            
            }
        
        return resultados;
        
        
        }

        //Borrar Usuario
        public bool DeleteUsuario(int Id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Usuario WHERE Id = @Id";

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.DbType.Int32);
                sqlParameter.Value = Id;

                sqlConnection.Open();


                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if(numberOfRows > 0)
                    {
                        resultado = true;

                    }
                }


                    sqlConnection.Close();

            }
            return resultado;
        }

        //Modificar Usuarios
        public string ModificarUsuario(Usuario usuario)
        {
            string _posicion = String.Empty;
            int _update = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                string queryupdate = "UPDATE Usuario SET Nombre= @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario, Contraseña = @Contraseña, Mail = @Mail WHERE Id = @IdUsuario";

                SqlParameter _UsuarioId = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int) { Value = usuario.Id };
                SqlParameter _Nombre = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter _Apellido = new SqlParameter("Apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter _NombreUsuario = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter _Contraseña = new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter _Mail = new SqlParameter("Mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryupdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(_UsuarioId);
                    sqlCommand.Parameters.Add(_Nombre);
                    sqlCommand.Parameters.Add(_Apellido);
                    sqlCommand.Parameters.Add(_NombreUsuario);
                    sqlCommand.Parameters.Add(_Contraseña);
                    sqlCommand.Parameters.Add(_Mail);
                    _update = sqlCommand.ExecuteNonQuery();
                }
                if (_update == 1)
                {
                    _posicion = "Se actualizó el Id del usuario a : " + usuario.Id;
                }
                else
                {
                    _posicion = "Ups! Apareció un conflicto y no pudo cambiar el Id de usuario a : " + usuario.Id;
                }
                sqlConnection.Close();



            }
            return _posicion;


        }


        //Crear Usuarios

        public string CrearUsuario(Usuario usuario)
        {

            string _posicion = String.Empty;
            int _creados = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string QueryUpdate = "INSERT INTO Usuario ( Nombre, Apellido, NombreUsuario, Contraseña, Mail ) VALUES " +
                    "( @Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail )";

                //Parámetros
                SqlParameter _Nombre = new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter _Apellido = new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter _NombreUsuario = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter _Contraseña = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter _Mail = new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(QueryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(_Nombre);
                    sqlCommand.Parameters.Add(_Apellido);
                    sqlCommand.Parameters.Add(_NombreUsuario);
                    sqlCommand.Parameters.Add(_Contraseña);
                    sqlCommand.Parameters.Add(_Mail);
                    _creados = sqlCommand.ExecuteNonQuery();
                }
                if (_creados == 1)
                {
                    _posicion = "El usuario: '" + usuario.NombreUsuario + "' creado con exito.";
                }
                else
                {
                    _posicion = "Error - El usuario '" + usuario.NombreUsuario + "' no se creo.";
                }
                sqlConnection.Close();
            }

            return _posicion;
        }

        //Validar Usuarios

        public static Usuario ValidarUsuario(string user, string psw)
        {
            
            Usuario _usuario = new Usuario();
            DataTable ttable = new DataTable();

            
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @user and Contraseña = @psw", sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@user", user);
                    sqlCommand.Parameters.AddWithValue("@psw", psw);
                    SqlDataAdapter SqlAdapter = new SqlDataAdapter();
                    SqlAdapter.SelectCommand = sqlCommand;
                    SqlAdapter.Fill(ttable);

                    if (ttable.Rows.Count > 0)
                    {
                        foreach (DataRow line in ttable.Rows)
                        {
                            _usuario.Id = Convert.ToInt32(line["Id"]);
                            _usuario.Nombre = line["Nombre"].ToString();
                            _usuario.Apellido = line["Apellido"].ToString();
                            _usuario.Mail = line["Mail"].ToString();
                            _usuario.NombreUsuario = user;
                            _usuario.Contraseña = psw;

                            
                            break;
                        }
                    }
                    else
                    {
                       
                        _usuario.Id = 0;
                    }
                    sqlConnection.Close();
                }
            }
            return _usuario;
        }


    }
}
