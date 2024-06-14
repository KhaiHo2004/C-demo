using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021143.DomainModel;
namespace SV18T1021143.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm và lấy dứ liệu của người giao hàng dưới dạng phân trang
    /// </summary>
    public class ShipperPaginationResultModel : PaginationResultModel
    {
        public List<Shippers> Data { get; set; }

      
    }
}