using Dapper;
using Master.Entity.Database;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository
{
    public interface IItemDropRepo
    {
        public bool GetListByFkUserFkFolder(string conn, long fkUser, long fkFolder, out List<ItemDrop> lst);
        public bool Update(string conn, ItemDrop mdl);
        public long Insert(string conn, ItemDrop mdl);
    }

    public class ItemDropRepo : IItemDropRepo
    {
        public bool GetListByFkUserFkFolder(string conn, long fkUser, long fkFolder, out List<ItemDrop> lst)
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();

                    lst = connection.Query<ItemDrop>
                        ("SELECT * FROM \"ItemDrop\" where \"fkUser\"=@fkUser and \"fkFolder\"=@fkFolder", 
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

        public void setUserParams(NpgsqlCommand cmd, ItemDrop mdl)
        {
            #region - code - 

            cmd.Parameters.AddWithValue("id", ((object)mdl.id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkItem", ((object)mdl.fkItem) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkUser", ((object)mdl.fkUser) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkFolder", ((object)mdl.fkFolder) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("vlCents", ((object)mdl.vlCents) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuDay", ((object)mdl.nuDay) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuMonth", ((object)mdl.nuMonth) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuYear", ((object)mdl.nuYear) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bActive", ((object)mdl.bActive) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtRegister", ((object)mdl.dtRegister) ?? DBNull.Value);

            #endregion
        }

        public bool Update(string conn, ItemDrop mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"ItemDrop\" set " +
                        "\"fkItem\"=@fkItem," +
                        "\"fkUser\"=@fkUser," +
                        "\"fkFolder\"=@fkFolder," +
                        "\"vlCents\"=@vlCents," +
                        "\"nuDay\"=@nuDay," +
                        "\"nuMonth\"=@nuMonth," +
                        "\"nuYear\"=@nuYear," +
                        "\"bActive\"=@bActive," +
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

        public long Insert(string conn, ItemDrop mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"ItemDrop\" " +
                        "( \"fkItem\",\"fkUser\",\"fkFolder\",\"vlCents\",\"nuDay\",\"nuMonth\",\"nuYear\",\"bActive\",\"dtRegister\" ) " +
                        "VALUES (@fkItem,@fkUser,@fkFolder,@vlCents,@nuDay,@nuMonth,@nuYear,@bActive,@dtRegister)" +
                        ";select currval('public.\"ItemDrop_id_seq\"');", db))
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
