using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021143.Web.Models
{
    /// <summary>
    /// Lớp cơ sở cho các Model chứa dữ liệu dưới dạng phân trang 
    /// </summary>
    public abstract class PaginationResultModel
    {/// <summary>
    /// Trang hiện tại
    /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// Giá trị tì kiếm
        /// </summary>
        /// 
        public int PageSize { get; set; }
        /// <summary>
        /// Số dòng dữ liệu truy vấns được
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// Số trang
        /// </summary>
        public int PageCount {
            get
            {
                int p = RowCount/PageSize;
                if(RowCount % PageSize > 0)
                    p += 1;
                    return p;
            }
        }
      
    }
}