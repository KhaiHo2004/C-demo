using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021143.DomainModel;
namespace SV18T1021143.DataLayer.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShippersDAL
    {
        /// <summary>
        /// Tìm kiếm hiển thị danh sách khách hàng dưới dạng là phân trang 
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        IList<Shippers> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm xem có bao nhiêu tên người giao hàng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của người giao hàng theo mã người giao hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        Shippers Get(int shipperID);
        /// <summary>
        /// Bổ sung 1 khách hàng Hàm trả về mã khách hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shippers data);
        /// <summary>
        /// Cập nhật mã người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shippers data);
        /// <summary>
        /// Xóa 1 người giao hàng dựa vào mã người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int shipperID);
        /// <summary>
        /// Kiểm tra xem 1 khách hàng có dữ liệu nào liên quan không
        /// </summary>
        /// <param name="shipperID"></param>    
        /// <returns></returns>
        bool InUsed(int shipperID);
    }
}
