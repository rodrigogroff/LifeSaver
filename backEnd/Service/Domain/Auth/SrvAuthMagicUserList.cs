using Dapper;
using Master.Entity.Database;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthMagicUserList: SrvBaseService
    {
        public bool UserList(string conn, out List<User> lst)
        {
            using (var connection = new NpgsqlConnection(conn))
            {
                connection.Open();
                lst = connection.Query<User>("SELECT * FROM \"User\"").ToList();
            }

            return true;
        }
    }
}
