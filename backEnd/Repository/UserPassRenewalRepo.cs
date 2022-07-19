using Dapper;
using Master.Entity.Database;
using Npgsql;
using System;

namespace Master.Repository
{
    public interface IUserPassRenewalRepo
    {
        public bool GetByFkUser(string conn, long fkUser, out UserPassRenewal tbl);
        public bool Update(string conn, UserPassRenewal mdl);
        public long Insert(string conn, UserPassRenewal mdl);
    }

    public class UserPassRenewalRepo : IUserPassRenewalRepo
    {
        public bool GetByFkUser(string conn, long fkUser, out UserPassRenewal user)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    user = connection.QueryFirstOrDefault<UserPassRenewal> 
                        ("SELECT * FROM \"UserRegisterCode\" where \"fkUser\"=@fkUser and \"stCode\" <> '' order by \"dtRequest\" desc", new { fkUser });
                }

                return true;
            }
            catch
            {
                user = null;
                return false;
            }

            #endregion
        }

        public void setUserParams(NpgsqlCommand cmd, UserPassRenewal mdl)
        {
            #region - code - 

            cmd.Parameters.AddWithValue("id", ((object)mdl.id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkUser", ((object)mdl.fkUser) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stCode", ((object)mdl.stCode) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stMobile", ((object)mdl.stMobile) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtRequest", ((object)mdl.dtRequest) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtExpires", ((object)mdl.dtExpires) ?? DBNull.Value);

            #endregion
        }

        public bool Update(string conn, UserPassRenewal mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"UserPassRenewal\" set " +
                        "\"fkUser\"=@fkUser," +
                        "\"stCode\"=@stCode," +
                        "\"stMobile\"=@stMobile," +
                        "\"dtRequest\"=@dtRequest," +
                        "\"dtExpires\"=@dtExpires " +
                        "where id=@id", db))
                    {
                        setUserParams(cmd, mdl);
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }

            #endregion
        }

        public long Insert(string conn, UserPassRenewal mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"UserPassRenewal\" " +
                        "( \"fkUser\",\"stCode\",\"stMobile\",\"dtRequest\",\"dtExpires\" ) " +
                                                                   "VALUES (@fkUser,@stCode,@stMobile,@dtRequest,@dtExpires)" +
                                                                   ";select currval('public.\"UserPassRenewal_id_seq\"');", db))
                    {
                        setUserParams(cmd, mdl);
                        return (long)cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                return 0;
            }

            #endregion
        }
    }
}
