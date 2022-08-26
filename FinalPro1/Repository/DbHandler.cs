using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FinalPro1.Repository
{
    public abstract class DbHandler
    {
        public const string ConnectionString = "Server=DESKTOP-OU63S65;Initial Catalog=SistemaGestion;Trusted_Conection=true";
    }
}

