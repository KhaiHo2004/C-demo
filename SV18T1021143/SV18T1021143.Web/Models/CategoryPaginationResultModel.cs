using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021143.DomainModel;
namespace SV18T1021143.Web.Models
{
    public class CategoryPaginationResultModel: PaginationResultModel
    {
        public List<Category> Data { get; set; }
    }
}