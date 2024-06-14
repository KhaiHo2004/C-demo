using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021143.DomainModel;
using System.Data.SqlClient;
namespace SV18T1021143.DataLayer.SQLServer
{
    public class EmployeeDAL : BaseDAL, IEmployeeDAL
    {

        public EmployeeDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Employee data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees (LastName, FirstName, BirthDate, Photo, Notes, Email)
                                    VALUES(@lastName, @firstName, @birthDate, @photo, @notes, @email);
                                    SELECT SCOPE_IDENTITY();";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@lastName", data.LastName);
                cmd.Parameters.AddWithValue("@firstName", data.FirstName);
                cmd.Parameters.AddWithValue("@photo", data.Photo);
                cmd.Parameters.AddWithValue("@notes", data.Notes);
                cmd.Parameters.AddWithValue("@birthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@email", data.Email);

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
                                from Employees
                                        where (@searchValue = N'')
                                            or(
                                                    (LastName like @searchValue)
                                                    or
                                                    (FirstName like @searchValue)
                                            
                                               
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

        public bool Delete(int employeeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Employees Where EmployeeID = @employeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            };
            return result;
        }

        public Employee Get(int employeeID)
        {
           Employee data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @employeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                   /*     BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),*/
                        BirthDate = Convert.ToString(dbReader["BirthDate"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Notes = Convert.ToString(dbReader["Notes"])
                    };
                }
                cn.Close();
            };
            return data;

        }

        public bool InUsed(int employeeID)
        {
            {
                bool result = false;
                using (SqlConnection cn = OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select case when exists(select *from Orders where EmployeeID = @employeeID) then 1 else 0 end";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@employeeID", employeeID);

                    result = Convert.ToBoolean(cmd.ExecuteScalar());
                    cn.Close();
                };
                return result;
            }
        }

        public IList<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";


            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                            from
                                (
                                    select *,
                                            row_number() over(order by FirstName) as RowNumber
                                    from Employees
                                    where (@searchValue = N'')
                                        or(
                                                (LastName like @searchValue)
                                                or
                                                (FirstName like @searchValue)
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
                    data.Add(new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToString(dbReader["BirthDate"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Email = Convert.ToString(dbReader["Email"]),
                    }); ;
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public bool Update(Employee data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName, 
                                    FirstName = @firstName, 
                                    Photo = @photo, 
                                    Notes = @notes, 
                                    BirthDate = @birthDate, 
                                    Email = @email
                                    WHERE EmployeeID = @employeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@employeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@lastName", data.LastName);
                cmd.Parameters.AddWithValue("@firstName", data.FirstName);
                cmd.Parameters.AddWithValue("@photo", data.Photo);
                cmd.Parameters.AddWithValue("@notes", data.Notes);
                cmd.Parameters.AddWithValue("@birthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@email", data.Email);


                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();


            }
            return result;
        }
    }
}
   



