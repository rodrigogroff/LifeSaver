using Dapper;
using Master.Entity.Database;
using Npgsql;
using System;

namespace Master.Repository
{
    public interface IUserRegisterCodeRepo
    {
        public bool GetByFkUser(string conn, long fkUser, out UserRegisterCode tbl);
        public bool Update(string conn, UserRegisterCode mdl);
        public long Insert(string conn, UserRegisterCode mdl);
    }

    public class UserRegisterCodeRepo : IUserRegisterCodeRepo
    {
        public bool GetByFkUser(string conn, long fkUser, out UserRegisterCode user)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    user = connection.QueryFirstOrDefault<UserRegisterCode> 
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

        public void setUserParams(NpgsqlCommand cmd, UserRegisterCode mdl)
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

        public bool Update(string conn, UserRegisterCode mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"UserRegisterCode\" set " +
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

        public long Insert(string conn, UserRegisterCode mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"UserRegisterCode\" " +
                        "( \"fkUser\",\"stCode\",\"stMobile\",\"dtRequest\",\"dtExpires\" ) " +
                                                                   "VALUES (@fkUser,@stCode,@stMobile,@dtRequest,@dtExpires)" +
                                                                   ";select currval('public.\"UserRegisterCode_id_seq\"');", db))
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
