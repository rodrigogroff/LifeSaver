using Dapper;
using Npgsql;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthMagicCleanDB : SrvBaseService
    {
        public bool Clean(string conn)
        {
            using (var db = new NpgsqlConnection(conn))
            {
                db.Open();

                db.Query("delete from \"Item\"");
                db.Query("delete from \"ItemDrop\"");
                db.Query("delete from \"ItemDropRegistry\"");
                db.Query("delete from \"ItemFolder\"");
                db.Query("delete from \"User\"");
                db.Query("delete from \"UserPassRenewal\"");
                db.Query("delete from \"UserRegisterCode\"");
            }

            return true;
        }
    }
}
