using Dapper;
using Master.Entity.Database;
using Npgsql;
using System;

namespace Master.Repository
{
    public interface IUserRepo
    {
        public bool GetUserById(string conn, long id, out User user);
        public bool GetUserByEmail(string conn, string email, out User user);
        public bool GetUserByMobile(string conn, string mobile, out User user);
        public bool Update(string conn, User mdl);
        public long Insert(string conn, User mdl);
    }

    public class UserRepo : IUserRepo
    {
        public bool GetUserByEmail(string conn, string email, out User user)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    user = db.QueryFirstOrDefault<User> 
                        ("SELECT * FROM \"User\" where \"stEmail\"=@email", new { email = email.ToLower() });
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

        public bool GetUserByMobile(string conn, string mobile, out User user)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    user = db.QueryFirstOrDefault<User> 
                        ("SELECT * FROM \"User\" where \"stMobile\"=@mobile", new { mobile });
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

        public bool GetUserById(string conn, long id, out User user)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    user = db.QueryFirstOrDefault<User> 
                        ("SELECT * FROM \"User\" where \"id\"=@id", new { id });
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

        public void setUserParams(NpgsqlCommand cmd, User mdl)
        {
            #region - code - 

            cmd.Parameters.AddWithValue("id", ((object)mdl.id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stEmail", ((object)mdl.stEmail) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stMobile", ((object)mdl.stMobile) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stPassword", ((object)mdl.stPassword) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stName", ((object)mdl.stName) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bActive", ((object)mdl.bActive) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtJoin", ((object)mdl.dtJoin) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtLastLogin", ((object)mdl.dtLastLogin) ?? DBNull.Value);

            #endregion
        }

        public bool Update(string conn, User mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"User\" set " +
                        "\"stEmail\"=@stEmail," +
                        "\"stMobile\"=@stMobile," +
                        "\"stPassword\"=@stPassword," +
                        "\"stName\"=@stName," +
                        "\"bActive\"=@bActive," +
                        "\"dtJoin\"=@dtJoin," +
                        "\"dtLastLogin\"=@dtLastLogin " +
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

        public long Insert(string conn, User mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"User\" ( \"stEmail\",\"stMobile\",\"stPassword\",\"stName\",\"bActive\",\"dtJoin\",\"dtLastLogin\" ) " +
                                                                   "VALUES (@stEmail,@stMobile,@stPassword,@stName,@bActive,@dtJoin,@dtLastLogin)" +
                                                                   ";select currval('public.\"User_id_seq\"');", db))
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
