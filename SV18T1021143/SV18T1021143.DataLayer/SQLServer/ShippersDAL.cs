using SV18T1021143.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SV18T1021143.DataLayer.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class ShippersDAL : BaseDAL, IShippersDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShippersDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public int Add(Shippers data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Shippers(ShipperName, Phone)
                                    VALUES(@shipperName, @phone);
                                    SELECT SCOPE_IDENTITY();";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@phone", data.Phone);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();


            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*)
                                from Shippers
                                        where (@searchValue = N'')
                                            or(
                                                    (ShipperName like @searchValue)
                                                  
                                               
                                    )";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                ;
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public bool Delete(int shipperID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Shippers Where ShipperID = @shipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            };
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public Shippers Get(int shipperID)
        {
            Shippers data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Shippers WHERE ShipperID = @shipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@shipperID", shipperID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Shippers()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"]),

                    };
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public bool InUsed(int shipperID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select case when exists(select *from Orders where ShipperID = @shipperID) then 1 else 0 end";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());
                cn.Close();
            };
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Shippers> List(int page, int pageSize, String searchValue)
        {
            List<Shippers> data = new List<Shippers>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";


            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                            from
                                (
                                    select *,
                                            row_number() over(order by ShipperName) as RowNumber
                                    from Shippers
                                    where (@searchValue = N'')
                                        or(
                                                (ShipperName like @searchValue)
                                             
                                            )
                                ) as t
                            where t.RowNumber between(@page -1) *@pageSize + 1 and @page *@pageSize
                            order by t.RowNumber; ";
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Shippers()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"]),
                    }); ;
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Shippers data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Shippers
                                    SET ShipperName = @shipperName, 
                                    Phone = @phone
                                    WHERE ShipperID = @shipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@phone", data.Phone);
                cmd.Parameters.AddWithValue("@shipperID", data.ShipperID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();


            }
            return result;
        }
    }
   
}
