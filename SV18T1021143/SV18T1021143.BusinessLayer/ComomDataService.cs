using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SV18T1021143.BusinessLayer;
using SV18T1021143.DataLayer;
using SV18T1021143.DomainModel;
using SV18T1021143.DataLayer.SQLServer;

namespace SV18T1021143.BusinessLayer
{
   /// <summary>
   /// Các chức năng nghiệp vụ liên quan đến dữ liệu chung 
   /// nhà cung cấp khách hàng,nguời giao hàng, nhân viên, loại hàng)
   /// </summary>
    public static  class ComomDataService
    {
        private static readonly ICategoryDAL categoryDB;
        private static readonly ICustomerDAL customerDB;
        private static readonly ISupplierDAL supplierDB;
        private static readonly IShippersDAL shipperDB;
        private static readonly IEmployeeDAL employeeDB;
        private static readonly ICountryDAL countryDB;

        static ComomDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch(provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                    supplierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                    shipperDB = new DataLayer.SQLServer.ShippersDAL(connectionString);
                    employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                    break;
                default:
                    categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }
        //public static List<Category> ListOfCategories()
        //{
        //    return categoryDB.List().ToList();
        //}


        public static List<Category> ListOfCategory(int page,
                                                       int pageSize,
                                                       string searchValue,
                                                       out int rowCount)
        {
            if (page <= 0) page = 1;
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }



        /// <summary>
        /// Tìm kiếm và lấy danh sách 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomer(int page,
                                                            int pageSize,
                                                            string searchValue,
                                                            out int rowCount)
        {
            if (page <= 0) page = 1;
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }


        /// <summary>
        /// Boor sung 1 khách hàng mới 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data )
        {
            return customerDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data )
        {
            return customerDB.Update(data);
        }
        /// <summary>
        ///  Xóa 1 khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Kiểm tra 1 khách hàng hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }





        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }


        /// <summary>
        /// Boor sung 1 nhà cung cấp mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin  nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        ///  Xóa 1 khách nhà cung cấp dựa vào mã cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Kiểm tra 1 nhà cung cấp hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }


        public static Shippers GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }


        /// <summary>
        /// Bổ sung 1nhân viên giao hàng mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shippers data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin người giao hàng mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shippers data)
        {
            return shipperDB.Update(data);
        }
        /// <summary>
        ///  Xóa 1  nhân viên giao hàng dựa vào mã nhân viên giao hàng
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }
        /// <summary>
        /// Kiểm tra 1 nhân viên giao hàng hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }


        #region Các chức năng liên quan đến nhân viên
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
                page = 1;
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }
        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        /// <summary>
        /// Kiểm tra xem khách hàng đã có dữ liệu liên quan không
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        #endregion


        #region Các chức năng liên quan đến loại hàng
        /// <summary>
        /// Lấy danh sách loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
                page = 1;
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }
        /// <summary>
        /// Thêm mới loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        public static bool InUsedCategories(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>

        public static List<Supplier> ListOfSupplier(int page,
                                                        int pageSize,
                                                        string searchValue,
                                                        out int rowCount)
        {
            if (page <= 0) page = 1;
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Shippers> ListOfShipper(int page,
                                                      int pageSize,
                                                      string searchValue,
                                                      out int rowCount)
        {
            if (page <= 0) page = 1;
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Employee> ListOfEmployee(int page,
                                                       int pageSize,
                                                       string searchValue,
                                                       out int rowCount)
        {
            if (page <= 0) page = 1;
            rowCount = supplierDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }


    }
    
}
