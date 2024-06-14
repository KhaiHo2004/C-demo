using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021143.DataLayer.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseDAL
    {
        /// <summary>
        /// Chuỗi thamn số kết nối CSDL SQL Server
        /// </summary>
        protected string _connectionString;
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="connectionString"></param>
        public BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = _connectionString;
            cn.Open();
            return cn;
        }
    }
}