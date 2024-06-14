using SV18T1021143.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021143.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm và lấy dứ liệu của khách hàng dưới dạng phân trang
    /// </summary>
    public class SupplierPaginationResultModel:PaginationResultModel
    {
        public List<Supplier> Data { get; set; }
    }
}