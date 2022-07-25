using Dapper;
using Master.Entity.Database;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository
{
    public interface IItemRepo
    {
        public bool GetById(string conn, long id, out Item tbl);
        public bool GetListByFkUserFkFolder(string conn, long fkUser, long fkFolder, out List<Item> lst);
        public bool Update(string conn, Item mdl);
        public long Insert(string conn, Item mdl);
    }

    public class ItemRepo : IItemRepo
    {
        public bool GetById(string conn, long id, out Item tbl)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    tbl = connection.QueryFirstOrDefault<Item>
                        ("SELECT * FROM \"Item\" where \"id\"=@id",
                        new { id });
                }

                return true;
            }
            catch
            {
                tbl = null;
                return false;
            }

            #endregion
        }


        public bool GetListByFkUserFkFolder(string conn, long fkUser, long fkFolder, out List<Item> lst)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    lst = connection.Query<Item>
                        ("SELECT * FROM \"Item\" where \"fkUser\"=@fkUser and \"fkFolder\"=@fkFolder order by \"stName\"", 
                        new { fkUser, fkFolder }).
                        ToList();
                }

                return true;
            }
            catch
            {
                lst = null;
                return false;
            }

            #endregion
        }

        public void setUserParams(NpgsqlCommand cmd, Item mdl)
        {
            #region - code - 

            cmd.Parameters.AddWithValue("id", ((object)mdl.id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkUser", ((object)mdl.fkUser) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkFolder", ((object)mdl.fkFolder) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuPeriod", ((object)mdl.nuPeriod) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuExpectedDay", ((object)mdl.nuExpectedDay) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("vlBaseCents", ((object)mdl.vlBaseCents) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bIncome", ((object)mdl.bIncome) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stName", ((object)mdl.stName) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtRegister", ((object)mdl.dtRegister) ?? DBNull.Value);

            #endregion
        }

        public bool Update(string conn, Item mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"Item\" set " +
                        "\"fkUser\"=@fkUser," +
                        "\"fkFolder\"=@fkFolder," +
                        "\"nuPeriod\"=@nuPeriod," +
                        "\"nuExpectedDay\"=@nuExpectedDay," +
                        "\"vlBaseCents\"=@vlBaseCents, " +
                        "\"bIncome\"=@bIncome, " +
                        "\"stName\"=@stName, " +
                        "\"dtRegister\"=@dtRegister " +
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

        public long Insert(string conn, Item mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"Item\" " +
                        "( \"fkUser\",\"fkFolder\",\"nuPeriod\",\"nuExpectedDay\",\"vlBaseCents\",\"bIncome\",\"stName\",\"dtRegister\" ) " +
                        "VALUES (@fkUser,@fkFolder,@nuPeriod,@nuExpectedDay,@vlBaseCents,@bIncome,@stName,@dtRegister)" +
                        ";select currval('public.\"Item_id_seq\"');", db))
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
