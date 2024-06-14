using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021143.DomainModel;
namespace SV18T1021143.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu liên quan đến khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Tìm kiếm hiển thị danh sách khách hàng dưới dạng là phân trang 
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        IList<Customer> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm xem có bao nhiêu khách hàng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Customer Get(int customerID);
        /// <summary>
        /// Bổ sung 1 khách hàng Hàm trả về mã khách hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);
       /// <summary>
       /// Cập nhật một khách hàng
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// Xóa 1 khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int CustomerID);
        /// <summary>
        /// Kiểm tra xem 1 khách hàng có dữ liệu nào liên quan không
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool InUsed(int customerID);
    }


}
