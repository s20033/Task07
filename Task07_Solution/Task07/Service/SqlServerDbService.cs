using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Task07.Service
{
    public class SqlServerDbService: IDbService
    {
        public bool CheckIndex(string index)
        {


            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s20033;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = @"select s.IndexNumber, s.FirstName, s.LastName from Student s;";
                con.Open();
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    return false;
                }
                return true;


            }
        }
    }
}
