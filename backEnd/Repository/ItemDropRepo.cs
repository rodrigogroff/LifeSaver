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
        public bool GetByFkFolderFkItem(string conn, long fkFolder, long? fkItem, long? day, long? month, long? year, out List<ItemDrop> lst);
        public bool Update(string conn, ItemDrop mdl);
        public long Insert(string conn, ItemDrop mdl);
    }

    public class ItemDropRepo : IItemDropRepo
    {        
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
            cmd.Parameters.AddWithValue("bIncome", ((object)mdl.bIncome) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("dtRegister", ((object)mdl.dtRegister) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuInstallments", ((object)mdl.nuInstallments) ?? DBNull.Value);

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
                        "\"bIncome\"=@bIncome," +
                        "\"nuInstallments\"=@nuInstallments," +
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
                        "( \"fkItem\",\"fkUser\",\"fkFolder\",\"vlCents\",\"nuDay\",\"nuMonth\",\"nuYear\",\"bActive\",\"dtRegister\",\"nuInstallments\",\"bIncome\" ) " +
                        "VALUES (@fkItem,@fkUser,@fkFolder,@vlCents,@nuDay,@nuMonth,@nuYear,@bActive,@dtRegister,@nuInstallments,@bIncome)" +
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

        public bool GetByFkFolderFkItem ( string conn, 
                                            long fkFolder, 
                                            long? fkItem, 
                                            long? day, 
                                            long? month, 
                                            long? year, 
                                            out List<ItemDrop> lst )
        {
            #region - code - 

            try
            {
                using (var connection = new NpgsqlConnection(conn))
                {
                    connection.Open();
                                        
                    var str = "SELECT * FROM \"ItemDrop\" where \"fkFolder\"=@fkFolder " +
                                                        (fkItem != null ? " and \"fkItem\"=@fkItem" : "") +
                                                        (day != null ? " and \"nuDay\"=@day" : "") +
                                                        (month != null ? " and \"nuMonth\"=@month" : "") +
                                                        (year != null ? " and \"nuYear\"=@year" : "") +
                                                        " and \"bActive\"=@bActive order by \"dtRegister\"";

                    lst = connection.Query<ItemDrop>(str, 
                        new 
                        { 
                            fkFolder, 
                            fkItem,
                            day, 
                            month, 
                            year,
                            bActive = true,
                        }).
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
    }
}
