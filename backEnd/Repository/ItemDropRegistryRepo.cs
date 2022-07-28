using Master.Entity.Database;
using Npgsql;
using System;

namespace Master.Repository
{    
    public interface IItemDropRegistryRepo
    {
        public bool Update(string conn, ItemDropRegistry mdl);
        public long Insert(string conn, ItemDropRegistry mdl);
    }

    public class ItemDropRegistryRepo : IItemDropRegistryRepo
    {
        public void setUserParams(NpgsqlCommand cmd, ItemDropRegistry mdl)
        {
            #region - code - 

            cmd.Parameters.AddWithValue("id", ((object)mdl.id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkItem", ((object)mdl.fkItem) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkUser", ((object)mdl.fkUser) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkFolder", ((object)mdl.fkFolder) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fkItemDrop", ((object)mdl.fkItemDrop) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuMonth", ((object)mdl.nuMonth) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuYear", ((object)mdl.nuYear) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("nuPayment", ((object)mdl.nuPayment) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("vlCents", ((object)mdl.vlCents) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("bIncome", ((object)mdl.bIncome) ?? DBNull.Value);

            #endregion
        }

        public bool Update(string conn, ItemDropRegistry mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("update \"ItemDropRegistry\" set " +
                        "\"fkItem\"=@fkItem," +
                        "\"fkUser\"=@fkUser," +
                        "\"fkFolder\"=@fkFolder," +
                        "\"fkItemDrop\"=@fkItemDrop," +
                        "\"nuMonth\"=@nuMonth," +
                        "\"nuYear\"=@nuYear," +
                        "\"nuPayment\"=@nuPayment," +
                        "\"bIncome\"=@bIncome," +
                        "\"vlCents\"=@vlCents " +
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

        public long Insert(string conn, ItemDropRegistry mdl)
        {
            #region - code - 

            try
            {
                using (var db = new NpgsqlConnection(conn))
                {
                    db.Open();

                    using (var cmd = new NpgsqlCommand("INSERT INTO \"ItemDropRegistry\" " +
                        "( \"fkItem\",\"fkUser\",\"fkFolder\",\"fkItemDrop\",\"nuMonth\",\"nuYear\",\"nuPayment\",\"vlCents\",\"bIncome\" ) " +
                        "VALUES (@fkItem,@fkUser,@fkFolder,@fkItemDrop,@nuMonth,@nuYear,@nuPayment,@vlCents,@bIncome)" +
                        ";select currval('public.\"ItemDropRegistry_id_seq\"');", db))
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
