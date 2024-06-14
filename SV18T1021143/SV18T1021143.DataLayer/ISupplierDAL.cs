using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021143.DomainModel;

namespace SV18T1021143.DataLayer

{
    /// <summary>
    /// Định nghĩa các phép xử lí liên quan đến nhà cung cấp
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Tìm kiếm hiển thị danh sách nhà cung cấp dưới dạng là phân trang 
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        IList<Supplier> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm xem có bao nhiêu nhà cung cấp thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một nhà cung cấp theo mã nhà cung cấp
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Bổ sung 1 nhà cung cấp Hàm trả về mã nhà cung cấp được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);
        /// <summary>
        /// Cập nhật một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// Xóa 1 nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int supplierID);
        /// <summary>
        /// Kiểm tra xem 1 nhà cung cấp có dữ liệu nào liên quan không
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool InUsed(int supplierID);


    }
}
