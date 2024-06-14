using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021143.DomainModel;
namespace SV18T1021143.DataLayer
{
   public  interface ICategoryDAL
    {

        /// <summary>
        /// Tìm kiếm hiển thị danh sách mặt hàng dưới dạng là phân trang 
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(chuỗi rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        IList<Category> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một mặt hàng theo mã mặt hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
      //  IList<Category> List();
        /// <summary>
        /// Lấy thông tin của 1 loại hàng theo loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// Thêm 1 loại hàng mới hàm về trả về mã của loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// Cập nhật thông tin của một loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// Xóa một loại hàng dựa vào loại hàng.Lưu ý không thể xóa nếu loại hàng đã được sử dụng
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        bool Delete(int Category);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool InUsed(int employeeID);
    }
}
