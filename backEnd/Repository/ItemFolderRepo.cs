using Dapper;
using Master.Entity.Database;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository
{
    public interface IItemFolderRepo
    {
        public bool GetById(string conn, long fkFolder, out ItemFolder folder);
        public bool GetListByFkUser(string conn, long fkUser, out List<ItemFolder> lst);
        public bool Update(string conn, ItemFolder mdl);
        public long Insert(string conn, ItemFolder mdl);
    }

    public class ItemFolderRepo : IItemFolderRepo
    {
        public bool GetById(string conn, long fkFolder, out ItemFolder tbl)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    tbl = connection.QueryFirstOrDefault<ItemFolder>
                        ("SELECT * FROM \"ItemFolder\" where \"id\"=@fkFolder",
                        new { fkFolder });
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

        public bool GetListByFkUser(string conn, long fkUser, out List<ItemFolder> lst)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    lst = connection.Query<ItemFolder>
                        ("SELECT * FROM \"ItemFolder\" where \"fkUser\"=@fkUser order by \"stName\"", 
                        new { fkUser }).
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

        public void setUserParams(NpgsqlCommand cmd, ItemFolder mdl)
        {
            #region - code - 

            cmd.Parameters.AddWithValue("id", ((object)mdl.id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bIncome", ((object)mdl.bIncome) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtRegister", ((object)mdl.dtRegister) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkFolder", ((object)mdl.fkFolder) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkUser", ((object)mdl.fkUser) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stName", ((object)mdl.stName) ?? DBNull.Value);
            
            #endregion
        }

        public bool Update(string conn, ItemFolder mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"ItemFolder\" set " +
                        "\"fkUser\"=@fkUser," +
                        "\"fkFolder\"=@fkFolder," +
                        "\"dtRegister\"=@dtRegister," +
                        "\"bIncome\"=@bIncome," +
                        "\"stName\"=@stName " +
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

        public long Insert(string conn, ItemFolder mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"ItemFolder\" " +
                        "( \"fkFolder\",\"fkUser\",\"dtRegister\",\"bIncome\",\"stName\" ) " +
                        "VALUES (@fkUser,@fkUser,@dtRegister,@bIncome,@stName)" +
                        ";select currval('public.\"ItemFolder_id_seq\"');", db))
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
