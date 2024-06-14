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
    public class CategoryDAL : BaseDAL, ICategoryDAL
    {

        public CategoryDAL(string connectionString) : base(connectionString)
        {

        }

        /// <summary>

        /// </summary>
        //private string connectionString;
        //public CategoryDAL(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
        public int Add(Category data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Categories(CategoryName, Description)
                                    VALUES(@categoryName, @description);
                                    SELECT SCOPE_IDENTITY();";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@description", data.Description);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();


            }
            return result;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*)
                                from Categories
                                        where (@searchValue = N'')
                                            or(
                                                    (CategoryName like @searchValue)

                                    )";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Categories Where CategoryID = @categoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            };
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            Category data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Categories WHERE CategoryID = @categoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),

                    };
                }
            }

            return data;
        }

        public bool InUsed(int categoryID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select case when exists(select *from Products where CategoryID = @CategoryID) then 1 else 0 end";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());
                cn.Close();
            };
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public IList<Category> List()
        //{
        //    //List<Category> data = new List<Category>();
        //    //using (SqlConnection connection = new SqlConnection())
        //    //{
        //    //    connection.ConnectionString = connectionString;
        //    //    connection.Open();
        //    //    SqlCommand cmd = new SqlCommand();
        //    //    cmd.CommandText = "SELECT * FROM Categories";
        //    //    cmd.CommandType = CommandType.Text;
        //    //    cmd.Connection = connection;
        //    //    SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    //    while(dbReader.Read())
        //    //    {
        //    //        data.Add(new Category() {
        //    //              CatagoryID = Convert.ToInt32(dbReader["CategoryID"]),
        //    //              CatagoryName = Convert.ToString(dbReader["CategoryName"]),
        //    //              Description = Convert.ToString(dbReader["Description"])
        //    //        });;
        //    //    }

        //    //    connection.Close();
        //    //}
        //    //    return data;
        //}



        public IList<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";


            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                            from
                                (
                                    select *,
                                            row_number() over(order by CategoryName) as RowNumber
                                    from Categories
                                    where (@searchValue = N'')
                                        or(
                                                (CategoryName like @searchValue)
                                               
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
                    data.Add(new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),
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
        public bool Update(Category data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Categories
                                    SET CategoryName = @categoryName, 
                                    Description = @description
                                    WHERE CategoryID = @categoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@categoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@description", data.Description);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();


            }
            return result;
        }
    
    }
}
